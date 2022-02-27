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
	// A command to ban people from a guild (work in progress)
	public class Ban : Command
	{
		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			// Get all the varriables the api needs to ban the user
			SocketGuildChannel chnl = userMessage.Channel as SocketGuildChannel;
			SocketGuild Guild = chnl.Guild;
			SocketUser user = null;

			bool passedChecks = false; // If the checks making sure a user was mentioned was passed

			if (!userMessage.Author.IsBot)
            {
				// Check to see if a user was mentioned
				if (Params[0].Type == ParamaterType.User && Params[0].User != null)
				{
					user = Params[0].User;
					passedChecks = true;
				}
			}		


			if (passedChecks)
			{
				SocketGuildUser fartUser = userMessage.Author as SocketGuildUser;

				// Check if the user can be banned, if so, ban the user
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

		// Check to see if a user can be banned (aka: has the correct perms)
		public bool CanBan(SocketUser user)
		{
			SocketGuildUser uFix = user as SocketGuildUser;
			return uFix.GuildPermissions.BanMembers;
		}
	}

	// A command to kick people from a guild (work in progress)
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
