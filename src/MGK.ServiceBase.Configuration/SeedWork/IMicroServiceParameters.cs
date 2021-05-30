using System;

namespace MGK.ServiceBase.Configuration.SeedWork
{
	public interface IMicroServiceParameters
	{
		Type ApiStartup { get; }
		string ClientAlias { get; }
		string ContextPath { get; }
		string SwaggerDocumentName { get; }
	}
}
