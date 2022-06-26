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
	public class Emotes : Command
	{
		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			SocketGuildChannel chnl = userMessage.Channel as SocketGuildChannel;
			SocketGuild Guild = chnl.Guild;

			EmbedBuilder builder = new EmbedBuilder();

			builder.WithTitle(Guild.Name + " Emotes");

			string roles = "Emotes: \n";

			foreach (GuildEmote emote in Guild.Emotes)
			{
				roles += "<:" + emote.Name + ":" + emote.Id + ">";
			}

			if (roles.Length > 50)
			{
				builder.Description = "There are too many emotes to display.";
			}
			else
            {
				builder.Description = roles;
            }
			
			builder.WithThumbnailUrl(Guild.IconUrl);

			await userMessage.Channel.SendMessageAsync("", false, builder.Build());

			return Task.CompletedTask;
		}
	}
}
