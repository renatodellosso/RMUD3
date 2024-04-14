﻿using Tapper;

namespace RMUD3.Server.ServerComponents
{

	[TranspilationSource]
	public enum MainPageClientAction
	{
		MainPage
	}

	public class MainPageServerComponent : ServerComponent
	{
		public MainPageServerComponent() : base("MainPage")
		{
			AddActionHandler(MainPageClientAction.MainPage, (string args) =>
			{
				Console.WriteLine("MainPage action received! Args: " + args);
			});
		}
	}
}