using MGK.Acceptance;
using MGK.Extensions;
using MGK.ServiceBase.Services.Infrastructure.Exceptions;
using MGK.ServiceBase.Services.SeedWork;
using System;

namespace MGK.ServiceBase.Services
{
	public abstract class ServiceProvider<TKey, TService> : IServiceProvider<TKey, TService>
		where TKey : notnull
		where TService : IService
	{
		protected ServiceProvider(Func<TKey, TService> services)
		{
			Services = services;
		}

		protected Func<TKey, TService> Services { get; }

		public virtual TOutputService Get<TOutputService>(TKey key)
			where TOutputService : class, IService
		{
			var service = Services(key) as TOutputService;

			if (service == null)
			{
				Raise.Error.Generic<ServiceRegistrationException>(
					ServicesResources.MessagesResources.ErrorRegisteringTitle,
					ServicesResources.MessagesResources.ErrorRegisteringMessage.Format(key.ToString()));
			}

			return service;
		}
	}
}
