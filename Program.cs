using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CuzsieBot
{
	public class Program
	{
		public static Program Instance;

		public string botPrefix = "!";

		public Program()
		{
			Program.Instance = this;
		}

		public async Task<Task> SendMessageWithFilters(ISocketMessageChannel channel, string str)
		{
			if (str.Contains("@everyone") || str.Contains("@here"))
			{
				return channel.SendMessageAsync("Message contained a ping.");
			}
			return channel.SendMessageAsync(str);
		}

		public static Task Main(string[] args) => new Program().MainAsync();
		public static Dictionary<string, Command> Commands = new Dictionary<string, Command>();
		public static DiscordSocketClient _client;

		private Task Log(LogMessage msg)
		{
			Console.WriteLine(msg.ToString());
			return Task.CompletedTask;
		}

		private async Task<Task> MessageSent(SocketMessage message)
		{
			if (!(message is SocketUserMessage userMessage)) return Task.CompletedTask;
			if (userMessage.Source != MessageSource.User) return Task.CompletedTask;
		

			if (userMessage.Content.StartsWith(botPrefix))
			{
				string[] command = userMessage.Content.Substring(1).Split(" ");

				List<Parameter> Params = new List<Parameter>();

				for (int i = 1; i < command.Length; i++)
				{
					if (int.TryParse(command[i], out int integer))
						Params.Add(new Parameter(integer));

					else if (float.TryParse(command[i], out float floating_point))
						Params.Add(new Parameter(floating_point));

					else if (command[i].StartsWith("<@!"))//<@!921989230983536700>
					{
						if (ulong.TryParse(command[i].Substring(3, command[i].Length - 4), out ulong id))
						{
							SocketUser user = _client.GetUser(id);
							Params.Add(new Parameter(user));
						}
						
						else
							Params.Add(new Parameter());
					}
					
					else
						Params.Add(new Parameter(command[i]));
						Console.WriteLine("Command Executed: " + userMessage.Content);
				}
				if (Commands.TryGetValue(command[0],out Command cmd))
				{
					return cmd.Run(Params,userMessage);
				}
			}
			return Task.CompletedTask;
		}

		public async Task MainAsync()
		{
			Console.WriteLine("---CuzsieBot Launcher Version 1---");
			Console.WriteLine("---by cuzsie#3829---");

			_client = new DiscordSocketClient();
			_client.Log += Log;
			_client.MessageReceived += MessageSent;

			if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "Config", "token.txt")))
			{
				Console.WriteLine("token.txt not found");
				return;
			}

			var token = File.ReadAllText(Path.Combine(Environment.CurrentDirectory,"Config","token.txt"));

			await AddCommands();
			await _client.SetGameAsync("!help for help | https://discord.gg/s5hdfdqBp2", null, ActivityType.Playing);
			await _client.LoginAsync(TokenType.Bot, token);
			await _client.StartAsync();

			await Task.Delay(-1);
		}

		public async Task AddCommands()
		{
			Command[] Mark = {new CuzsieQuote(), new Insult(), new Motivation(), new FnfSongGenerator() };

			foreach (Command command in Mark)
				await command.Init();

			
			Commands.Add("help", new Help());

			// Commands
			Commands.Add("cuzsiequote", Mark[0]);
			Commands.Add("generateinsult", Mark[1]);
			Commands.Add("inspirobot", Mark[2]);
			Commands.Add("fnfsong", Mark[3]);
			Commands.Add("coulsoncharacter", new CoulsonCharacter());
			Commands.Add("8ball", new EightBall());
			Commands.Add("igotyoip", new Ip());
			Commands.Add("jordans", new Jordans());
			Commands.Add("dance", new Dance());
			Commands.Add("avatar", new Avatar());
			Commands.Add("hug", new Hug());
			Commands.Add("slur", new GenerateSlur());
			Commands.Add("vineboom", new VineBoom());
			Commands.Add("server", new Server());
			Commands.Add("roles", new Roles());
			Commands.Add("emotes", new Emotes());
			Commands.Add("amongus", new AmongUs());
			Commands.Add("bigify", new ToBigLetters());

			// Moderation
			Commands.Add("ban", new Ban());
			Commands.Add("kick", new Kick());
		}
	}
}
