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
	public class Help: Command
	{
		public string helpCommands =
		"\n**Commands**\n" +
		"\n!cuzsiequote - Generate a Cuzsie Quote using an AI Algorithim. Use !cuzsiequote smart or !cuzsiequote dumb to make it produce different outcomes with risk of copying cuzsie quotes directly." +
		"\n!generateinsult - Generate an insult to screw someone over." + 
		"\n!coulsoncharacter - Generate your own coulson character using an AI Algorithim.";

		public string helpModeration =
		"\n**Commands**\n" +
		"\n!ban - (DISABLED)" +
		"\n!kick - (DISABLED)";


		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			await userMessage.Channel.SendMessageAsync("```\nCommands\n!cuzsiequote - Generate a Cuzsie Quote using an AI Algorithim. Use !cuzsiequote smart or !cuzsiequote dumb to make it produce different outcomes with risk of copying cuzsie quotes directly.\n!generateinsult - Generate an insult to screw someone over.\n!coulsoncharacter - Generate your own coulson character using an AI Algorithim.```");
			return Task.CompletedTask;
		}
	}
}
