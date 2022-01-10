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
	public class Ban : Command
	{
		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			SocketGuildChannel chnl = userMessage.Channel as SocketGuildChannel;
			SocketGuild Guild = chnl.Guild;
			SocketUser user = null;

			bool passedChecks = false;
			string type = "Type";

			if (!userMessage.Author.IsBot)
            {
				if (Params[0].Type == ParamaterType.User && Params[0].User != null)
				{
					user = Params[0].User;
					passedChecks = true;
					type = "User";
				}
				else if (Params.Count! < 0 && Params[0].Type == ParamaterType.String)
				{
					// user = Params[0].String;
					passedChecks = true;
					type = "String";
				}
			}		

			if (passedChecks)
			{
				SocketGuildUser fartUser = userMessage.Author as SocketGuildUser;

				if (CanBan(fartUser))
				{
					string reason = "";
					string username = user.Username;

					await Guild.AddBanAsync(user, 0, reason);
					await userMessage.Channel.SendMessageAsync("Banned user '" + username + "'");
					return Task.CompletedTask;
				}
				else
                {
					await userMessage.Channel.SendMessageAsync("You dont have the required permissions to ban that user!");
					return Task.CompletedTask;
				}
			}

			else
				return Task.CompletedTask;
		}


		public bool CanBan(SocketUser user)
		{
			SocketGuildUser uFix = user as SocketGuildUser;
			return uFix.GuildPermissions.BanMembers;
		}
	}
	
	
	public class Kick : Command
	{
		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			try
            {
				SocketGuildChannel chnl = userMessage.Channel as SocketGuildChannel;
				SocketGuild Guild = chnl.Guild;
				SocketUser user;
				bool passedChecks = false;

				if (Params[0].Type == ParamaterType.User && Params[0].User != null)
				{
					user = Params[0].User;
					passedChecks = true;
				}
				else
				{
					user = null;
				}

				SocketGuildUser author = userMessage.Author as SocketGuildUser;
				SocketGuildUser toKick = user as SocketGuildUser;

				if (!author.GuildPermissions.KickMembers && passedChecks || !author.GuildPermissions.Administrator && passedChecks)
				{
					await userMessage.Channel.SendMessageAsync("You dont have the required permissions to ban that user!");
					return Task.CompletedTask;
				}
				else if (author.GuildPermissions.KickMembers && passedChecks || author.GuildPermissions.Administrator && passedChecks)
				{
					await toKick.KickAsync();
					await userMessage.Channel.SendMessageAsync($":wave: Kicked {user.Username} from the server.");
					return Task.CompletedTask;
				}
				else
				{
					await userMessage.Channel.SendMessageAsync("Something went wrong while trying to preform this command. Try again later.\nIf the problem persists, contact cuzsie#3829 for further help.");
					return Task.CompletedTask;
				}
			}
			catch
            {
				await userMessage.Channel.SendMessageAsync("Something went wrong while trying to preform this command. Try again later.\nIf the problem persists, contact cuzsie#3829 for further help.");
				return Task.CompletedTask;
			}
		}
	}
}
