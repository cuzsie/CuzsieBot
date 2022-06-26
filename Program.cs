using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace CuzsieBot
{
	public class Program
	{
		public static Program Instance;
		public static Task Main(string[] args) => new Program().MainAsync();
		public static DiscordSocketClient _client;
		[DllImport("kernel32.dll")] static extern IntPtr GetConsoleWindow();
		[DllImport("user32.dll")] static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
		const int SW_HIDE = 0;
		const int SW_SHOW = 5;


		///////////////
		public string botPrefix = "-"; // The prefix of the bot
        public static Dictionary<string, Command> Commands = new Dictionary<string, Command>(); // Commands
		public static Dictionary<string, Command> ModerationCommands = new Dictionary<string, Command>(); // Moderation Commands
		public static Dictionary<string, Command> FunCommands = new Dictionary<string, Command>(); // Fun Commands

		public Program()
		{
			Program.Instance = this;
		}

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

				if (ModerationCommands.TryGetValue(command[0], out Command cmdMod))
				{
					return cmdMod.Run(Params, userMessage);
				}

				if (FunCommands.TryGetValue(command[0], out Command cmdFun))
				{
					return cmdFun.Run(Params, userMessage);
				}
			}
			return Task.CompletedTask;
		}

		public async Task MainAsync()
		{
			Console.ReadLine();
			Console.WriteLine("---CuzsieBot Launcher Version 1---");
			Console.WriteLine("---by Cuzsie#3829---");

			// Creates a notification icon on the tray
			NotifyIcon tray = new NotifyIcon();
			tray.Icon = System.Drawing.Icon.ExtractAssociatedIcon(System.IO.Directory.GetCurrentDirectory() + "\\CuzsieBot.exe");
			tray.Visible = true;
			tray.Click += OnTrayClick;

			Console.WriteLine("CuzsieBot: Loading bot client...");
			_client = new DiscordSocketClient();
			_client.Log += Log;
			_client.MessageReceived += MessageSent;

			if (!File.Exists(Path.Combine(Environment.CurrentDirectory, "Config", "token.txt")))
			{
				Console.WriteLine("Error: token.txt was not found. Try adding a token.txt file in your bot's path.");
				return;
			}

			var token = File.ReadAllText(Path.Combine(Environment.CurrentDirectory,"Config","token.txt"));

			Console.WriteLine("CuzsieBot: Adding commands...");
			await AddCommands();

			Console.WriteLine("CuzsieBot: Loading Status Message...");
			await _client.SetGameAsync("-help for help | https://discord.gg/s5hdfdqBp2", null, ActivityType.Playing);

			Console.WriteLine("CuzsieBot: Logging In...");
			await _client.LoginAsync(TokenType.Bot, token);
			await _client.StartAsync();

			await Task.Delay(-1);

			Console.WriteLine("CuzsieBot: Setup Success! Loading Discord service...");
		}

		public void OnTrayClick(object sender, EventArgs e)
        {
			var handle = GetConsoleWindow();
			ShowWindow(handle, SW_SHOW);
		}

		public async Task AddCommands()
		{
			Command[] Mark = {new CuzsieQuote(), new Motivation()};

			foreach (Command command in Mark)
				await command.Init();

			
			Commands.Add("help", new Help());

			// Commands
			Commands.Add("avatar", new Avatar());
			Commands.Add("server", new Server());
			Commands.Add("roles", new Roles());
			Commands.Add("emotes", new Emotes());

			// Moderation
			ModerationCommands.Add("ban", new Ban());
			ModerationCommands.Add("kick", new Kick());

			// Fun
			FunCommands.Add("cuzsiequote", Mark[0]);
			FunCommands.Add("inspirobot", Mark[1]);
			FunCommands.Add("coulsoncharacter", new CoulsonCharacter());
			FunCommands.Add("8ball", new EightBall());
			FunCommands.Add("hug", new Hug());
			FunCommands.Add("vineboom", new VineBoom());
			FunCommands.Add("bigify", new ToBigLetters());
		}
	}
}
