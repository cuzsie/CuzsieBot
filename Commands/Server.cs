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

			builder.Description = 
				$"Server Name: **{Guild.Name}**\n" +
				$"Description: **{Guild.Description}**\n" +
				$"Owner: **{Guild.Owner}**\n" +
				$"Members: **{Guild.MemberCount}**\n" +
				$"Created: **{Guild.CreatedAt}**\n" +
				
				$"Emotes: **{Guild.Emotes.Count}** (!emotes)  " +
				$"Roles: **{Guild.Roles.Count}** (!roles)  " +
				$"Channels: **{Guild.Channels.Count}** (!channels)  ";

			Console.WriteLine("Fields Made");

			builder.WithThumbnailUrl(Guild.IconUrl);

			Console.WriteLine("!server has been finished");

			await userMessage.Channel.SendMessageAsync("" , false , builder.Build());
			
			return Task.CompletedTask;
		}
	}
}
