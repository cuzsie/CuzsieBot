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
	public class EmailAddress : Command
	{

		public static string[] EmailAdd = { "iscool", "2008", "gamez", "isawesomecringe", "plays", "bruh", "120", "420", "69", "yt" };

		public static string[] Websites = { "gmail.com", "yahoo.com", "icloud.com", "scratch.mit.edu", "discord.app"};

		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{

			Random rng = new Random();

			SocketUser user = userMessage.Author;

			if (Params.Count != 0)
			{
				if (Params[0].Type == ParamaterType.User)
				{
					if (Params[0].User != null)
					{
						user = Params[0].User;
					}
				}
			}

			string username = user.Username;

			username.Replace(' ', '\0');



			await userMessage.Channel.SendMessageAsync
				(
					user.Username + ", your generated email is...\n**" + 
					username +
					EmailAdd[rng.Next(0,EmailAdd.Length - 1)] + 
					"@" + 
					Websites[rng.Next(0, Websites.Length - 1)] + 
					"**\nTry !password to generate a password!"
				);
			
			return Task.CompletedTask;
		}
	}
}
