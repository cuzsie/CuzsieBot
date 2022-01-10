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
	public class Insult : MarkovCommand
	{
		public override async Task<Task> Init()
		{
			FileToLook = "insults.txt";

			return base.Init();
		}

		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			// await userMessage.Channel.SendMessageAsync("testing reasons move past me :(");
			return base.Run(Params, userMessage);
		}
	}
}
