using System;

namespace MGK.ServiceBase.SeedWork
{
	public interface IServiceParameters
	{
		Type ApiStartup { get; }
		string ClientAlias { get; }
		string ContextPath { get; }
		string SwaggerDocumentName { get; }
	}
}
