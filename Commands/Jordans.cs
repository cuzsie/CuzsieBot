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
	public class Jordans : Command
	{
		public string[] jStatus = { "Yo Jordans... Fake as fuck!", "Yo Jordans... Real!" };

		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			Random rng = new Random();

			await userMessage.Channel.SendMessageAsync(jStatus[rng.Next(0, jStatus.Length)]);
			return Task.CompletedTask;
		}
	}
}
