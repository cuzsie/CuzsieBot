using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using MarkovSharp;
using MarkovSharp.TokenisationStrategies;
using Discord.Webhook;

namespace CuzsieBot
{
	public class SayAs : Command
	{
		public string helpCommands = "";
		public string helpModeration = "";

		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			IWebhook dumbWebhook;

			DiscordWebhookClient victim = new DiscordWebhookClient(dumbWebhook);

			await victim.SendMessageAsync("Me when the amongus! :trolled:");
			return Task.CompletedTask;
		}
	}
}
