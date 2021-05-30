using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.Scaffolding;
using Microsoft.EntityFrameworkCore.Scaffolding.Internal;
using Microsoft.Extensions.DependencyInjection;

namespace MGK.ServiceBase.DAL.Infrastructure.Scaffolding
{
	public class CustomDesignTimeServices : IDesignTimeServices
	{
		public virtual void ConfigureDesignTimeServices(IServiceCollection serviceCollection)
		{
			serviceCollection.AddSingleton<ICSharpEntityTypeGenerator, CustomEntityTypeGenerator>();
			serviceCollection.AddSingleton<ICSharpDbContextGenerator, CustomDbContextGenerator>();
			serviceCollection.AddSingleton<IModelCodeGenerator, CustomModelGenerator>();
		}
	}
}
