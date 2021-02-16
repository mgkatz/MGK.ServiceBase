using MGK.Acceptance;
using MGK.ServiceBase.Infrastructure.Extensions;
using MGK.ServiceBase.SeedWork;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using MGK.ServiceBase.Constants;

namespace MGK.ServiceBase
{
	/// <summary>
	/// Implements a way to have at hand information of a multi-tenant environment
	/// </summary>
	public sealed class MultiTenantSetup : IMultiTenantSetup
	{
		private readonly IHttpContextAccessor _httpContextAccessor;
		private readonly IConfiguration _configuration;

		/// <summary>
		/// Creates an instance of the information of the multi-tenant environments.
		/// </summary>
		/// <param name="httpContextAccessor">The http context accessor.</param>
		/// <param name="configuration">The application configuration.</param>
		public MultiTenantSetup(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
		{
			Ensure.Parameter.IsNotNull(configuration, nameof(configuration));

			_httpContextAccessor = httpContextAccessor;
			_configuration = configuration;
			CurrentClientAlias = GetClientAlias();
		}

		/// <summary>
		/// Gets the client alias that is currently configured in the request.
		/// </summary>
		public string CurrentClientAlias { get; }

		/// <summary>
		/// Gets the information of the databases configured by alias.
		/// </summary>
		public IDictionary<string, IDbServerSetup> DatabasesByAliases
			=> _configuration.GetDatabaseSetupList();

		/// <summary>
		/// Extract the client alias from the request.
		/// </summary>
		/// <returns>The client alias in the request.</returns>
		private string GetClientAlias()
		{
			if (!_configuration.GetValue<bool>(AppConfigurationKeys.UseMultiTenant))
				return string.Empty;

			Ensure.Value.IsNotNull(_httpContextAccessor, "HttpContextAccessor");
			Ensure.Value.IsNotNull(_httpContextAccessor.HttpContext, BaseResources.MessagesResources.ErrorRequestContextNull);

			string clientAlias = _httpContextAccessor.GetCurrentAlias();
			Ensure.Value.IsNotNullNorEmptyNorWhiteSpace(clientAlias, BaseResources.MessagesResources.ErrorAliasNotExist);

			return clientAlias;
		}
	}
}
