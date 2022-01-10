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

			foreach(SocketUser socketUser in userMessage.MentionedUsers)
            {


				if (sussyAmongUs > 0)
				{
					Console.WriteLine("DUDE ITS LOWER TF YOU TRYNA DO >:(");
				}
                else
				{
					ushort scale = 256;

					EmbedBuilder builder = new EmbedBuilder();

					builder.WithTitle(socketUser.Username + "'s avatar.");
					builder.ImageUrl = socketUser.GetAvatarUrl(ImageFormat.Auto, scale);

					Console.WriteLine("Ok we boutta send this guys");
					await userMessage.Channel.SendMessageAsync("", false, builder.Build());
				}

				Console.WriteLine("UserCount at " + sussyAmongUs);
				sussyAmongUs++;
				Console.WriteLine("It is now at " + sussyAmongUs);
			}

			return Task.CompletedTask;
		}
	}
}
