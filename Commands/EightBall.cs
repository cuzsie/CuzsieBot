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
	public class EightBall : Command
	{
		public static string[] Options = { "It is certain", "Yes.", "My sources say no.", "No.", "Concentrate and ask again", "Reply hazy, try again", "Without a doubt", "Most likely", "Ask again later", "Cannot predict now", "Nope.", "My sources say no.", "probably not." };

		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			Random rng = new Random();

			await userMessage.Channel.SendMessageAsync(Options[rng.Next(0, Options.Length - 1)]);

			return Task.CompletedTask;
		}
	}
}
