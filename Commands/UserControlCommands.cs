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
			ISocketMessageChannel channel = userMessage.Channel;
			SocketGuildChannel secondChannel = (SocketGuildChannel)channel;
			SocketGuild guild = secondChannel.Guild;
			IGuildUser user = guild.GetUser(userMessage.MentionedUsers.ToArray()[0].Id);
			
			if (CanBan(userMessage.Author, userMessage))
			{
				string reason = userMessage.Content.Replace($"{Program.Instance.botPrefix}ban ", "");
				string username = user.Username;

				await UserExtensions.BanAsync(user, 0, reason);

				EmbedBuilder builder = new EmbedBuilder();

				builder.WithTitle("Banned User");
				builder.Description += "**The ban hammer has spoken!**\n";
				builder.Description += username + " has been banned.\n\n";
				builder.Description += "Reason: " + reason + "\n\n";
				builder.ImageUrl = user.GetAvatarUrl();

				await userMessage.Channel.SendMessageAsync("", false, builder.Build());
			}
			else
            {
				await userMessage.Channel.SendMessageAsync("Cannot ban that user! You dont have the required permissions!");
			}
			
			return Task.CompletedTask;
		}

		public bool CanBan(SocketUser user, SocketUserMessage userMessage)
		{
			ISocketMessageChannel channel = userMessage.Channel;
			var secondChannel = channel as SocketGuildChannel;
			SocketGuild guild = secondChannel.Guild;

			return guild.GetUser(user.Id).GuildPermissions.BanMembers;
		}
	}

	public class Kick : Command
	{
		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			ISocketMessageChannel channel = userMessage.Channel;
			SocketGuildChannel secondChannel = (SocketGuildChannel)channel;
			SocketGuild guild = secondChannel.Guild;
			IGuildUser user = guild.GetUser(userMessage.MentionedUsers.ToArray()[0].Id);

			if (CanKick(userMessage.Author, userMessage))
			{
				string reason = userMessage.Content.Replace($"{Program.Instance.botPrefix}ban ", "");
				string username = user.Username;

				await user.KickAsync(reason);

				EmbedBuilder builder = new EmbedBuilder();

				builder.WithTitle("Kicked User");
				builder.Description += "**A user has been kicked!**\n";
				builder.Description += username + " has been kicked from the server.\n\n";
				builder.Description += "Reason: " + reason + "\n\n";
				builder.ImageUrl = user.GetAvatarUrl();

				await userMessage.Channel.SendMessageAsync("", false, builder.Build());
			}
			else
			{
				await userMessage.Channel.SendMessageAsync("Cannot ban that user! You dont have the required permissions!");
			}

			return Task.CompletedTask;
		}

		public bool CanKick(SocketUser user, SocketUserMessage userMessage)
		{
			ISocketMessageChannel channel = userMessage.Channel;
			var secondChannel = channel as SocketGuildChannel;
			SocketGuild guild = secondChannel.Guild;

			return guild.GetUser(user.Id).GuildPermissions.KickMembers;
		}
	}
}
