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
	public class Server : Command
	{
		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			SocketGuildChannel chnl = userMessage.Channel as SocketGuildChannel;
			SocketGuild Guild = chnl.Guild;

			EmbedBuilder builder = new EmbedBuilder();

			builder.WithTitle(Guild.Name);

			builder.Description = $"Server Name: {Guild.Name}\nDescription: {Guild.Description}\nOwner: {Guild.Owner}\nCreated: {Guild.CreatedAt}";

			Console.WriteLine("Fields Made");

			builder.WithThumbnailUrl(Guild.IconUrl);

			Console.WriteLine("!server has been finished");

			await userMessage.Channel.SendMessageAsync("" , false , builder.Build());
			return Task.CompletedTask;
		}
	}
}
