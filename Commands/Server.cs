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
			string adminSingle = "";

			foreach (SocketGuildUser user in Guild.Users)
            {
				if (user.GuildPermissions.Administrator)
                {
					string easterEgg = " (hey, its me!)";

					adminSingle += "\n" + user.Username;

					if (user.Id == 922665299701006356)
						adminSingle += easterEgg;
                }
            }

			EmbedBuilder builder = new EmbedBuilder();

			builder.WithTitle(Guild.Name);

			builder.Description = 
				$"**Server Name:** {Guild.Name}\n" +
				$"**Description:** {Guild.Description}\n" +
				$"**Owner:** {Guild.Owner}\n" +
				$"**Members:** {Guild.MemberCount}\n" +
				$"**Created:** {Guild.CreatedAt}\n" +
				"-------------------------\n" +
				$"**Emotes:** {Guild.Emotes.Count} (!emotes)\n" +
				$"**Roles:** {Guild.Roles.Count} (!roles)\n" +
				$"**Channels:** {Guild.Channels.Count} (!channels)\n" +
				"-------------------------\n" +
				$"**Server Administrators:** {adminSingle}";

			builder.WithThumbnailUrl(Guild.IconUrl);

			await userMessage.Channel.SendMessageAsync("" , false , builder.Build());
			
			return Task.CompletedTask;
		}
	}
}
