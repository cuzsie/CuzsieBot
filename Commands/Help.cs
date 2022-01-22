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
		public string helpCommands = "";
		public string helpModeration = "";

		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			EmbedBuilder builder = new EmbedBuilder();

			foreach (KeyValuePair<string, Command> command in Program.Commands) { helpCommands += ("\n!" + command.Key); }
			foreach (KeyValuePair<string, Command> command in Program.ModerationCommands) { helpModeration += ("\n" + command.Key);}

			builder.WithTitle("**Commands**");
			builder.Description = helpCommands;

			await userMessage.Channel.SendMessageAsync("", false, builder.Build());
			return Task.CompletedTask;
		}
	}
}
