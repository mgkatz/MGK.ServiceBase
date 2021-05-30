using MGK.Extensions;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using System;
using System.Linq;

namespace MGK.ServiceBase.DAL.Infrastructure.Scaffolding
{
	public class CustomModelGenerator : CSharpModelGenerator
	{
		private readonly string[] _exclusions;
		private readonly string[] _inclusions;

		public CustomModelGenerator(
			ModelCodeGeneratorDependencies dependencies,
			ICSharpDbContextGenerator cSharpDbContextGenerator,
			ICSharpEntityTypeGenerator cSharpEntityTypeGenerator)
			: this(dependencies, cSharpDbContextGenerator, cSharpEntityTypeGenerator, Array.Empty<string>(), Array.Empty<string>())
		{
		}

		public CustomModelGenerator(
			ModelCodeGeneratorDependencies dependencies,
			ICSharpDbContextGenerator cSharpDbContextGenerator,
			ICSharpEntityTypeGenerator cSharpEntityTypeGenerator,
			string[] inclusions,
			string[] exclusions)
			: base(dependencies, cSharpDbContextGenerator, cSharpEntityTypeGenerator)
		{
			_inclusions = inclusions;
			_exclusions = exclusions;
		}

		public override ScaffoldedModel GenerateModel(IModel model, ModelCodeGenerationOptions options)
		{
			while (model.GetEntityTypes().Any(et => et.Name.In(_exclusions, ignoreCase: true))
				&& !model.GetEntityTypes().Any(et => et.Name.In(_inclusions, ignoreCase: true)))
			{
				var entityType = model.GetEntityTypes()
					.First(et => et.Name.In(_exclusions, ignoreCase: true))
					as EntityType;
				model.AsModel().RemoveEntityType(entityType);
			}

			return base.GenerateModel(model, options);
		}
	}
}
