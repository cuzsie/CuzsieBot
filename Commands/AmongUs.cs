using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using MarkovSharp;
using MarkovSharp.TokenisationStrategies;
//using Discord.Addons.Interactive;

namespace CuzsieBot
{
	public class AmongUs : Command
	{
		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			bool hasPlayerCnt = false;

			await userMessage.Channel.SendMessageAsync("https://cdn.discordapp.com/attachments/929555151142912090/929602480558452776/DCAmongUs.png");
			await userMessage.Channel.SendMessageAsync("Welcome to Among Us: Discord hosted by CuzsieBot!\nTo get started, please @ the players that will be playing! (EG: @CuzsieBot#9009)");

			while (hasPlayerCnt == false)
            {
				//var response = await NextMessageAsync();
			}

			AGame _game = new AGame();
			_game.players = 3;
			_game.host = userMessage.Author;

			return Task.CompletedTask;
		}

		public class AGame
        {
			public int players;
			public SocketUser host;
        }
	}
}
