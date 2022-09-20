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
	public class Avatar : Command
	{
		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			List<SocketUser> user = null;

			int sussyAmongUs = 0;

			foreach (SocketUser socketUser in userMessage.MentionedUsers)
			{
				if (sussyAmongUs > 0) { }
				else
				{
					ushort scale = 256;
					string hqTag = "";

					if (Params[1].String == "hq")
                    {
						scale = 1024;
						hqTag = "(HQ)";
                    }
						

					EmbedBuilder builder = new EmbedBuilder();

					builder.WithTitle(socketUser.Username + $"'s avatar. {hqTag}");
					builder.ImageUrl = socketUser.GetAvatarUrl(ImageFormat.Auto, scale);

					await userMessage.Channel.SendMessageAsync("", false, builder.Build());
				}

				sussyAmongUs++;
			}
			

			return Task.CompletedTask;
		}
	}
}
