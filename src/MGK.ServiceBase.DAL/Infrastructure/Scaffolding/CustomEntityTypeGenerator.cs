using MGK.Extensions.Constants;
using MGK.ServiceBase.DAL.Infrastructure.DataUnits;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using System;

namespace MGK.ServiceBase.DAL.Infrastructure.Scaffolding
{
	public class CustomEntityTypeGenerator : CSharpEntityTypeGenerator
	{
		public CustomEntityTypeGenerator(IAnnotationCodeGenerator annotationCodeGenerator, ICSharpHelper cSharpHelper)
			: base(annotationCodeGenerator, cSharpHelper)
		{
		}

		public override string WriteCode(IEntityType entityType, string @namespace, bool useDataAnnotations)
		{
			var oldString = $"public partial class {entityType.Name}";
			var newString = $"{oldString} : {typeof(DataUnit).Name}";
			var baseCode = base
				.WriteCode(entityType, @namespace, useDataAnnotations)
				.Replace(oldString, newString, StringComparison.InvariantCultureIgnoreCase);

			return $"using {typeof(DataUnit).Namespace};{StringConstants.CRLF}{baseCode}";
		}
	}
}
