using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using MarkovSharp;
using MarkovSharp.TokenisationStrategies;

namespace CuzsieBot
{
	public class AmogUs : Command
	{
		public string[] among =
		{
			"Amid",
			"Amidst",
			"Encompassed By",
			"With",
			"Surrounded By",
			"Among"
		};

		public string[] us =
		{
			"Them",
			"You",
			"Thy",
			"The Group"
		};

		public string helpModeration = "";

		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			Random rng = new Random();

			await userMessage.Channel.SendMessageAsync(among[rng.Next(0, among.Length)] + " " + us[rng.Next(0, us.Length)]);
			return Task.CompletedTask;
		}
	}
}
