using System;
using System.Collections.Generic;
using System.Text;
using Discord;
using Discord.WebSocket;
using System.IO;
using System.Threading.Tasks;
using System.Linq;

namespace CuzsieBot
{
    class Hug : Command
    {
		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			SocketUser user = null;

			if (Params.Count != 0)
			{
				if (Params[0].Type == ParamaterType.User)
				{
					if (Params[0].User != null)
						user = Params[0].User;
				}
			}

			if (Params[0].User == null)
			{
				await userMessage.Channel.SendMessageAsync("Specify a user.");
				return Task.CompletedTask;
			}
			else if (Params[0].User != null)
			{
				string username = user.Username;

				await userMessage.Channel.SendMessageAsync(userMessage.Author.Username + " hugged " + username + "! :hugging:");
				return Task.CompletedTask;
			}

			else
				return Task.CompletedTask;
		}
	}
}
