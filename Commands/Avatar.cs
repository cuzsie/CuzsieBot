using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using MarkovSharp;
using MarkovSharp.TokenisationStrategies;

// A command to get a specified user's avatar
namespace CuzsieBot
{
	public class Avatar : Command
	{
		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			List<SocketUser> user = null;

			// How many users were pinged in the message.
			// This is to prevent multiple users avatars getting grabbed and the bot getting overloaded.
			int sussyAmongUs = 0;

			foreach(SocketUser socketUser in userMessage.MentionedUsers)
            {
				// Make sure that it only gets 1 user.
				if (sussyAmongUs > 0)
					Console.WriteLine("An error occured: Users mentioned cannot be more that 1");
                else
				{
					// The scale of the avatar (make this bigger if you want the avatar to appear more high-quality {must be a multiple of 128, eg: 128, 256, etc})
					ushort scale = 256;

					EmbedBuilder builder = new EmbedBuilder();

					// Create an embed with the users avatar
					builder.WithTitle(socketUser.Username + "'s avatar.");
					builder.ImageUrl = socketUser.GetAvatarUrl(ImageFormat.Auto, scale);

					await userMessage.Channel.SendMessageAsync("", false, builder.Build());
				}

				// Add to the usercount.
				sussyAmongUs++;
			}

			return Task.CompletedTask;
		}
	}
}
