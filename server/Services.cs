﻿using Microsoft.AspNetCore.SignalR;
using RMUD3.Server.SignalR;

namespace RMUD3.Server
{
	public class Services
	{

		private static Services? instance;

		private readonly WebApplication? app;

		private Services(WebApplication? app)
		{
			this.app = app;
		}

		public static void Create(WebApplication? app)
		{
			instance ??= new Services(app);
		}

		private static T? GetService<T>() where T : notnull
		{
			if (instance is null)
				throw new InvalidOperationException("Services not created");

			if (instance.app is null)
				return default;

			return instance.app.Services.GetRequiredService<T>();
		}

		public static IHubContext<MiddlewareHub, IMiddlewareHubClient>? HubContext
		{
			get => GetService<IHubContext<MiddlewareHub, IMiddlewareHubClient>>();
		}

		public static IEnvService? Env
		{
			get => GetService<IEnvService>();
		}

		public static IDbService? Db
		{
			get => GetService<IDbService>();
		}

	}
}
