using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.Drawing;
using System.Threading;
using CuzsieBot.Internal;

namespace CuzsieBot
{
	static class SystemMessages
    {
		public static string launcherCredits = "Launcher by cuzsie#3829";
		public static string launcherCompany = "Bot by";
		public static string statusEnding = " | $prefix$help for help.";	
		public static string tokenError = "Your bot's token could not be found. Try entering your bots token in a file named 'token.txt' in the config folder.";
		public static string ranCommand = "Command Run";
		public static string[] initMessages = { "Loading bot client...", "Adding commands...", "Finishing setup...", "Logging in..." };
    }

	public class Program
	{
		public static Program Instance;
		public BotMetadata meta;
		public static DiscordSocketClient _client;
		public static NotifyIcon notifyIcon = new NotifyIcon();
		public static bool Visible = true;

        public static Dictionary<string, Command> Commands = new Dictionary<string, Command>();
		public static Dictionary<string, Command> ModerationCommands = new Dictionary<string, Command>();
		public static Dictionary<string, Command> FunCommands = new Dictionary<string, Command>();
	
		public string botPrefix;
		public string status;
		public ActivityType statusType;

		public Program() => Console.WriteLine("Program class initialized");

		public static Task Main(string[] args)
		{
			Program.Instance = new Program();
			Instance.MainAsync();
			Instance.NotifyThread();

			return Task.CompletedTask;
		}

		public async Task MainAsync()
		{
			Console.ReadLine();

			if (File.Exists(System.IO.Directory.GetCurrentDirectory() + "/bot.json"))
            {
				string token;

				meta = Internal.BotMeta.ParseFromJSON(System.IO.Directory.GetCurrentDirectory() + "/bot.json");
				token = meta.token;
				botPrefix = meta.prefix;
				status = meta.status;
				statusType = meta.statusType;

				Console.WriteLine($"---{meta.name}---");
				Console.WriteLine($"---{SystemMessages.launcherCredits}---");

				BotLog(SystemMessages.initMessages[0]);
				_client = new DiscordSocketClient();
				_client.Log += Log;
				_client.MessageReceived += MessageSent;

				BotLog(SystemMessages.initMessages[1]);
				await AddCommands();

				BotLog(SystemMessages.initMessages[2]);
				await _client.SetGameAsync(status + SystemMessages.tokenError.Replace("$prefix$", botPrefix), null, statusType);

				BotLog(SystemMessages.initMessages[3]);
				await _client.LoginAsync(TokenType.Bot, token);
				await _client.StartAsync();
				await Task.Delay(-1);

				Thread t = new Thread(() => LaunchBalloonNotification(notifyIcon, Application.ProductName, "Bot loaded. Double click the system tray icon to hide the client window."));
				t.Start();
            }
			else
            {
				BotMetadata metadata = new BotMetadata();

				Console.WriteLine($"Seems like your missing a bot file... lets create it!");
				Console.WriteLine($"Please enter your bot's name below.");
				Console.Write("");

				while(Console.ReadKey(true).Key != ConsoleKey.Enter){}

				string input = Console.ReadLine();
				Console.WriteLine($"Your bots name has been set as '{input}'.");

				metadata.name = input;

				Console.WriteLine($"Please enter your bot's token below.");
				Console.WriteLine($"If your having trouble finding this, visit the Discord Developer Portal, and create a bot.");
				Console.WriteLine($"https://discord.com/developers/applications");
				Console.Write("");

				while (Console.ReadKey(true).Key != ConsoleKey.Enter){}

				string token = Console.ReadLine();
				Console.WriteLine($"Your bots token has been set as '{token}'.");

				metadata.token = token;

				Console.WriteLine($"Please enter your bot's prefix below.");
				Console.WriteLine($"This prefix should be used to initiate commands with your bot. EG: !help");
				Console.Write("");

				while (Console.ReadKey(true).Key != ConsoleKey.Enter){}

				string prefix = Console.ReadLine();
				Console.WriteLine($"Your bots prefix has been set as '{prefix}'.");

				metadata.prefix = prefix;

				Console.WriteLine($"Please enter your bot's status below.");
				Console.WriteLine($"The status will show on the bots profile when the bot is online.");
				Console.Write("");

				while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }

				string status = Console.ReadLine();
				Console.WriteLine($"Your bots prefix has been set as '{status}'.");

				metadata.status = status;

				Console.WriteLine($"Were almost done! Finally, how should your bots status show.");
				Console.WriteLine($"The status will show as either Playing status, Streaming status, Watching status, or Listening to status");
				Console.WriteLine($"P - Playing");
				Console.WriteLine($"S - Streaming");
				Console.WriteLine($"W - Watching");
				Console.WriteLine($"L - Listening");
				Console.WriteLine($"C - Custom Status (No type)");
				Console.Write("");

				ActivityType type = ActivityType.Playing;
				bool enteredRight = false;

				while (!enteredRight) 
				{ 
					switch (Console.ReadKey(true).Key)
                    {
						case ConsoleKey.P:
							type = ActivityType.Playing;
							enteredRight = true;
							break;
						case ConsoleKey.S:
							type = ActivityType.Streaming;
							enteredRight = true;
							break;
						case ConsoleKey.W:
							type = ActivityType.Watching;
							enteredRight = true;
							break;
						case ConsoleKey.L:
							type = ActivityType.Listening;
							enteredRight = true;
							break;
					}
				}

				metadata.statusType = type;

				Console.WriteLine("Activity type set.");

				Console.WriteLine("Finishing setup...");

				BotMeta.ParseToJSON(metadata);

				Console.WriteLine("Setup process finished!");

				Environment.Exit(0);
			}
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
				}
				if (Commands.TryGetValue(command[0], out Command cmd))
				{
					Console.WriteLine($"{SystemMessages.ranCommand}: {cmd}");
					return cmd.Run(Params, userMessage);
				}

				if (ModerationCommands.TryGetValue(command[0], out Command cmdMod))
				{
					Console.WriteLine($"{SystemMessages.ranCommand}: {cmdMod}");
					return cmdMod.Run(Params, userMessage);
				}

				if (FunCommands.TryGetValue(command[0], out Command cmdFun))
				{
					Console.WriteLine($"{SystemMessages.ranCommand}: {cmdFun}");
					return cmdFun.Run(Params, userMessage);
				}
			}
			return Task.CompletedTask;
		}

		public void BotLog(string message)
        {
			Console.WriteLine(Application.ProductName + $": {message}");
        }

		public async Task AddCommands()
		{
			Command[] Mark = {new CuzsieQuote(), new Motivation()};

			foreach (Command command in Mark)
				await command.Init();

			
			Commands.Add("help", new Help());

			Commands.Add("avatar", new Avatar());
			Commands.Add("server", new Server());
			Commands.Add("roles", new Roles());
			Commands.Add("emotes", new Emotes());

			ModerationCommands.Add("ban", new Ban());
			ModerationCommands.Add("kick", new Kick());

			FunCommands.Add("cuzsiequote", Mark[0]);
			FunCommands.Add("inspirobot", Mark[1]);
			FunCommands.Add("coulsoncharacter", new CoulsonCharacter());
			FunCommands.Add("8ball", new EightBall());
			FunCommands.Add("hug", new Hug());
			FunCommands.Add("vineboom", new VineBoom());
			FunCommands.Add("bigify", new ToBigLetters());
		}

		public static bool IsBotMessage(SocketMessage message)
        {
			bool returnval = true;

			if (!(message is SocketUserMessage userMessage)) returnval = false;
			if (message.Source != MessageSource.User) returnval = false;

			return returnval;
		}

		void NotifyThread()
		{
			notifyIcon = new NotifyIcon();
			notifyIcon.DoubleClick += (s, e) =>
			{
				Visible = !Visible;
				SetConsoleWindowVisibility(Visible);

				if (!Visible)
				{
					try
					{
						Thread t = new Thread(() => LaunchBalloonNotification(notifyIcon, Application.ProductName, "Bot minimized to system tray."));
						t.Start();
					}
					catch
					{
						Console.WriteLine("Something went wrong while minimizing the console application.");
					}
				}
			};

			notifyIcon.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
			notifyIcon.Visible = true;
			notifyIcon.Text = Application.ProductName;

			var contextMenu = new ContextMenuStrip();
			contextMenu.Items.Add("Exit", null, (s, e) => { Application.Exit(); });
			notifyIcon.ContextMenuStrip = contextMenu;

			Application.Run();
		}

		private static void LaunchBalloonNotification(NotifyIcon ico, String title, String msg)
		{
			ico.ShowBalloonTip(
				10000,
				title,
				msg,
				ToolTipIcon.Info
			);
		}

		[DllImport("user32.dll")]
		public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
		[DllImport("user32.dll")]
		static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);
		public static void SetConsoleWindowVisibility(bool visible)
		{
			IntPtr hWnd = FindWindow(null, Console.Title);
			if (hWnd != IntPtr.Zero)
			{
				if (visible) ShowWindow(hWnd, 1); //1 = SW_SHOWNORMAL           
				else ShowWindow(hWnd, 0); //0 = SW_HIDE               
			}
		}
	}
}
