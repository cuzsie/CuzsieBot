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
	public class Ip : Command
	{
		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{

			Random rng = new Random();

			await userMessage.Channel.SendMessageAsync
			(
				rng.Next(0, 256) + 
				"." +
				rng.Next(0, 256) + 
				"." +
				rng.Next(0, 256) +
				"." +
				rng.Next(0, 256)
			);
			
			return Task.CompletedTask;
		}
	}
}
