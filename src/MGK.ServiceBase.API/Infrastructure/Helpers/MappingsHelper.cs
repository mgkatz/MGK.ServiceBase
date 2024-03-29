﻿using AutoMapper;
using System.Reflection;

namespace MGK.ServiceBase.API.Infrastructure.Helpers;

public static class MappingsHelper
{
	public static Assembly GetMappingsAssembly<T>() where T : Profile
		=> typeof(T).Assembly;
}
