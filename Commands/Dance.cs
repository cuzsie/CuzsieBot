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
	public class Dance : Command
	{

		public string[] gifs = 
		{ 
			"https://tenor.com/view/dance-kid-gif-17962746", 
			"https://tenor.com/view/yay-yes-yeahhh-cute-girl-happy-dance-gif-14559695",
			"https://tenor.com/view/woo-dance-dancing-celebrate-gif-18613546",
			"https://tenor.com/view/victory-dance-weekend-vibe-gif-4519322",
			"https://tenor.com/view/bts-bangtan-boys-bangtan-sonyeondan-jungkook-jeon-jungkook-gif-17359075",
			"https://tenor.com/view/snoop-dogg-dancing-fun-funny-dance-dance-moves-gif-15455986"
		};

		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			Random rng = new Random();




			await userMessage.Channel.SendMessageAsync(gifs[rng.Next(0, gifs.Length)]);
			return Task.CompletedTask;
		}
	}
}
