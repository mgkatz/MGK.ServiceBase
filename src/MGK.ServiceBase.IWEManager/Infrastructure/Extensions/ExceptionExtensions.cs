﻿using MGK.ServiceBase.IWEManager.Infrastructure.Exceptions;
using System;
using System.Text;

namespace MGK.ServiceBase.IWEManager.Infrastructure.Extensions
{
	/// <summary>
	/// Extensions for the exceptions in the application.
	/// </summary>
	public static class ExceptionExtensions
	{
		/// <summary>
		/// Gets all the messages in an exception including the inner exceptions.
		/// </summary>
		/// <param name="source">The exception.</param>
		/// <returns>All the messages concatenated.</returns>
		public static string GetExceptionMesssages(this Exception source)
		{
			var sb = new StringBuilder();
			sb.AppendLine(source.Message);

			if (source is BaseException baseException)
			{
				sb.AppendLine(baseException.Details);
			}

			if (source.InnerException != null)
				sb.AppendLine(source.InnerException.GetExceptionMesssages());

			return sb.ToString();
		}
	}
}
