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
	public class GenerateSlur : Command
	{

		public static string[] Starters = { "n", "f", "c", "a", "b"};

		public static string[] Middles = { "igg", "agg", "un", "gur", "uck"};

		public static string[] Ends = { "er", "ot", "ot", "d", "k"};

		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{

			Random rng = new Random();

			string slur = Starters[rng.Next(0, Starters.Length - 1)] + Middles[rng.Next(0, Middles.Length - 1)] + Ends[rng.Next(0, Ends.Length - 1)];

			if (slur == "nigger" || slur == "faggot")
				slur = "[REDACTED]";

			await userMessage.Channel.SendMessageAsync(slur);

			return Task.CompletedTask;
		}
	}
}