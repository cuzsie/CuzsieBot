using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace CuzsieBot
{
	public class Help: Command
	{
		public string helpCommands = "";
		public string helpModeration = "";

		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			EmbedBuilder builder = new EmbedBuilder();
			EmbedBuilder buildermod = new EmbedBuilder();

			// Reset values
			helpCommands = "";
			helpModeration = "";

			// Get all commands and add them to the helpCommands and helpModeration string
			foreach (KeyValuePair<string, Command> command in Program.Commands) 
				helpCommands += ("\n-" + command.Key); 
			
			foreach (KeyValuePair<string, Command> command in Program.ModerationCommands) 
				helpModeration += ("\n-" + command.Key);
			

			// Create a new embed with helpCommands and helpModeration
			builder.WithTitle("**Commands**");
			builder.Description = helpCommands;

			buildermod.WithTitle("**Moderation**");
			buildermod.Description = helpModeration;

			// Send the final built message(s)
			await userMessage.Channel.SendMessageAsync("", false, builder.Build());

			await userMessage.Channel.SendMessageAsync("", false, buildermod.Build());
			return Task.CompletedTask;
		}
	}
}
