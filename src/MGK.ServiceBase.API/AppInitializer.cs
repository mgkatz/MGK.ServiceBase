using MGK.ServiceBase.Configuration.Constants;
using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace MGK.ServiceBase
{
	/// <summary>
	/// This static class if for the exclusive use of the Program.cs because at that point the services are not injected yet.
	/// </summary>
	public static class AppInitializer
	{
		/// <summary>
		/// Setups and gets the builder of the application configuration.
		/// </summary>
		private static readonly Func<IConfigurationBuilder, IConfigurationBuilder> _configBuilder = (x)
			=> x.SetBasePath(Directory.GetCurrentDirectory())
			.AddJsonFile(FileConfigBase, optional: false, reloadOnChange: true)
			.AddJsonFile(FileConfig, optional: true, reloadOnChange: true)
			.AddEnvironmentVariables();

		/// <summary>
		/// Gets the application configuration file path.
		/// </summary>
		private static string FileConfig
			=> $"Configuration/appsettings{(EnvName == null ? string.Empty : $".{EnvName}")}.json";

		/// <summary>
		/// Gets the application configuration file path.
		/// </summary>
		private static string FileConfigBase => "Configuration/appsettings.json";

		/// <summary>
		/// Gets the environment's name where the application should take the configuration.
		/// </summary>
		public static string EnvName => Environment.GetEnvironmentVariable(ApplicationConstants.EnvironmentName);

		/// <summary>
		/// Gets the application configuration information.
		/// </summary>
		/// <returns>The application configuration.</returns>
		public static IConfiguration GetConfiguration()
			=> _configBuilder(new ConfigurationBuilder()).Build();

		/// <summary>
		/// Sets the configuration builder.
		/// </summary>
		/// <param name="configurationBuilder">The configuration builder to set.</param>
		public static void SetConfigurationBuilder(IConfigurationBuilder configurationBuilder)
		{
			_configBuilder(configurationBuilder);
		}
	}
}
