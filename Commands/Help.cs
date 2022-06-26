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
		public string helpFun = "";
		public string[] validPrefixes = {"commands", "moderation", "fun"};

		// Messages
		public string invalidPrefixMessage = "Please provide a valid prefix (eg: -help moderation, -help commands)";

		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			// Reset values
			helpCommands = "";
			helpModeration = "";
			helpFun = "";

			// Get all commands and add them to the helpCommands and helpModeration string
			foreach (KeyValuePair<string, Command> command in Program.Commands) 
				helpCommands += ($"\n{Program.Instance.botPrefix}" + command.Key); 
			
			foreach (KeyValuePair<string, Command> command in Program.ModerationCommands) 
				helpModeration += ($"\n{Program.Instance.botPrefix}" + command.Key);

			foreach (KeyValuePair<string, Command> command in Program.FunCommands)
				helpFun += ($"\n{Program.Instance.botPrefix}" + command.Key);

			foreach (string item in validPrefixes)
			{
				EmbedBuilder builder = new EmbedBuilder();

				builder.WithTitle($"**{item}**");

				string targetDesc = "";

				switch (Params[0].String.ToLower())
				{
					case "commands":
						targetDesc = helpCommands;
						break;
					case "moderation":
						targetDesc = helpModeration;
						break;
					case "fun":
						targetDesc = helpFun;
						break;
					default:
						targetDesc = "Description1 is invalid or null.";
						break;
				}

				builder.Description = targetDesc;
				await userMessage.Channel.SendMessageAsync(" ", false, builder.Build());
			}

			return Task.CompletedTask;
		}
	}
}
