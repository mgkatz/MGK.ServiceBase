using MGK.Acceptance;
using MGK.Extensions;
using MGK.Extensions.Constants;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using System;
using System.Text;
using System.Diagnostics.CodeAnalysis;

namespace MGK.ServiceBase.DAL.Infrastructure.Scaffolding
{
	public class CustomDbContextGenerator : CSharpDbContextGenerator
	{
		private readonly StringComparison _stringComparison = StringComparison.InvariantCultureIgnoreCase;

		private struct CodeEpics
		{
			public const string Backup = "backup";
			public const string CloseBracket = "}";
			public const string Context = "Context";
			public const string DbSet = "public virtual DbSet";
			public const string ModelBuilderEntity = "modelBuilder.Entity";
			public const string Namespace = "namespace";
			public const string OnConfiguring = "void OnConfiguring";
			public const string OnModelCreating = "void OnModelCreating";
			public const string OnModelCreatingPartial = "OnModelCreatingPartial";
			public const string OpenBracket = "{";
		};

		public CustomDbContextGenerator(
			[NotNull] IProviderConfigurationCodeGenerator providerConfigurationCodeGenerator,
			[NotNull] IAnnotationCodeGenerator annotationCodeGenerator,
			[NotNull] ICSharpHelper cSharpHelper)
			: base(providerConfigurationCodeGenerator, annotationCodeGenerator, cSharpHelper)
		{
		}

		public override string WriteCode(
			IModel model,
			string contextName,
			string connectionString,
			string contextNamespace,
			string modelNamespace,
			bool useDataAnnotations,
			bool suppressConnectionStringWarning,
			bool suppressOnConfiguring)
		{
			Ensure.Parameter.IsNotNullNorEmptyNorWhiteSpace(
				contextName,
				nameof(contextName),
				DALResources.MessagesResources.ErrorContextName);

			var database = contextName.Replace(CodeEpics.Context, string.Empty, _stringComparison);

			var baseCode = base.WriteCode(
				model,
				contextName,
				connectionString,
				contextNamespace,
				modelNamespace,
				useDataAnnotations,
				suppressConnectionStringWarning,
				suppressOnConfiguring);
			var sb = new StringBuilder();

			AddUsings(sb, modelNamespace);
			AddMainCode(sb, baseCode);

			return sb.ToString();
		}

		private void AddMainCode(StringBuilder sb, string baseCode)
		{
			var codeLines = baseCode.Substring(baseCode.IndexOf(CodeEpics.Namespace, _stringComparison));
			var codeLinesList = codeLines
				.Substring(0, codeLines.LastIndexOf(CodeEpics.CloseBracket, _stringComparison))
				.Split(StringConstants.CRLF);
			var isOnConfiguringMethodAdded = false;
			var addRemainingLines = false;
			var isBuildingEntityBackup = false;

			foreach (var line in codeLinesList)
			{
				// Write namespace line, base constructors and DbSet lines until the OnConfiguring method.
				if (!isOnConfiguringMethodAdded
					&& !line.Contains(CodeEpics.OnConfiguring, _stringComparison))
				{
					// Evaluates if the line to be written is a DbSet and contains the word "backup".
					var isDbSetBackup = line.Contains(CodeEpics.DbSet, _stringComparison)
						&& line.Contains(CodeEpics.Backup, _stringComparison);

					// The line is written except when is a DbSet that contains the word "backup".
					if (!isDbSetBackup)
					{
						sb.AppendLine(line);
					}

					continue;
				}

				// Write the OnConfiguring method if it wasn's written yet.
				if (!isOnConfiguringMethodAdded)
				{
					isOnConfiguringMethodAdded = true;
				}

				// Evaluate when starts the OnModelCreating method to avoid the base code of the OnConfiguring method.
				if (line.Contains(CodeEpics.OnModelCreating, _stringComparison)
					&& !line.Contains(CodeEpics.OnModelCreatingPartial, _stringComparison))
				{
					sb.AppendLine(line);
					addRemainingLines = true;
					continue;
				}

				// When adding remaining lines but it is the model definition for backup entities then stop adding
				// the lines and set a signal to evaluate when ends the definition of the backup entity
				if (addRemainingLines
					&& line.Contains(CodeEpics.ModelBuilderEntity, _stringComparison)
					&& line.Contains(CodeEpics.Backup, _stringComparison))
				{
					isBuildingEntityBackup = true;
					addRemainingLines = false;
					continue;
				}

				// When the addition of lines is stopped because of an existing backup entity but starts the
				// addition of a new model definition of a non backup entity then enable again the adding of
				// remaining lines
				if (isBuildingEntityBackup
					&& line.Contains(CodeEpics.ModelBuilderEntity, _stringComparison)
					&& !line.Contains(CodeEpics.Backup, _stringComparison))
				{
					isBuildingEntityBackup = false;
					addRemainingLines = true;
				}

				// Write the rest of the lines.
				if (addRemainingLines
					&& !isBuildingEntityBackup
					&& !line.Contains(CodeEpics.OnModelCreatingPartial, _stringComparison))
				{
					sb.AppendLine(line);
				}
			}

			sb.AppendLine(CodeEpics.CloseBracket);
		}

		private void AddUsings(StringBuilder sb, string modelNamespace)
		{
			sb.AppendLine(DALResources.StructureResources.ContextUsings
				.Replace("{{modelNamespace}}", modelNamespace)
				.AddCRLF());
		}
	}
}
