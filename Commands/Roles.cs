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
	public class Roles : Command
	{
		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			SocketGuildChannel chnl = userMessage.Channel as SocketGuildChannel;
			SocketGuild Guild = chnl.Guild;
			EmbedBuilder builder = new EmbedBuilder();
			string roles = "Roles: \n";

			builder.WithTitle(Guild.Name + " Roles");

			foreach(SocketRole role in Guild.Roles)
            {
				roles += "**" + role.Name + "**\n";
            }

			builder.Description = roles;
			builder.WithThumbnailUrl(Guild.IconUrl);

			await userMessage.Channel.SendMessageAsync("", false, builder.Build());

			return Task.CompletedTask;
		}
	}
}
