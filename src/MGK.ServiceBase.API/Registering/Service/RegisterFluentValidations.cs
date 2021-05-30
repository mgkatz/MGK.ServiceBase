using FluentValidation;
using MediatR;
using MGK.ServiceBase.Infrastructure;
using MGK.ServiceBase.Services.SeedWork;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace MGK.ServiceBase.Registering.Service
{
	/// <summary>
	/// Register services related to fluent validation.
	/// </summary>
	public class RegisterFluentValidations : IServiceRegistration
	{
		/// <summary>
		/// Configures services related to fluent validation through the IServiceCollection.
		/// </summary>
		/// <param name="services">The collection of services.</param>
		/// <param name="configuration">The configuration information.</param>
		public virtual void RegisterServices(IServiceCollection services, IConfiguration configuration)
		{
			// Add all validations in the assembly
			services.AddValidatorsFromAssembly(Assembly.GetCallingAssembly());
			// Configure request validations
			services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));
		}
	}
}
