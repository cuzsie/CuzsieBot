using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using MarkovSharp;
using MarkovSharp.TokenisationStrategies;

// A command to generate a new term for "Among Us"
namespace CuzsieBot
{
	public class AmogUs : Command
	{
		// Replacements for "Among"
		public string[] among =
		{
			"Amid",
			"Amidst",
			"Encompassed By",
			"With",
			"Surrounded By",
			"Among"
		};

		// Replacements for "Us"
		public string[] us =
		{
			"Them",
			"You",
			"Thy",
			"The Group"
		};


		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			Random rng = new Random();

			// Send a message with a random "Among" and a random "Us"
			await userMessage.Channel.SendMessageAsync(among[rng.Next(0, among.Length)] + " " + us[rng.Next(0, us.Length)]);

			return Task.CompletedTask;
		}
	}
}
