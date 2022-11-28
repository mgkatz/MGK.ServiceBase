using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace MGK.ServiceBase.DAL.Infrastructure.Scaffolding;

public class CustomDesignTimeServices : IDesignTimeServices
{
	public virtual void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
	{
		// TODO: the following lines have been commented until some breaking changes in EF Core 7 are reviewed.

		//serviceCollection.AddSingleton<ICSharpEntityTypeGenerator, CustomEntityTypeGenerator>();
		//serviceCollection.AddSingleton<ICSharpDbContextGenerator, CustomDbContextGenerator>();
		//serviceCollection.AddSingleton<IModelCodeGenerator, CustomModelGenerator>();
	}
}
