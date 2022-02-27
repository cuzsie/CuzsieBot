using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CuzsieBot
{
	public class CoulsonCharacter : Command
	{
		// Letters / phrases that can be put together to make a name and/or an object
		public static char[] c_Character1 = {'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'R', 'S', 'T', 'Z' };
		public static char[] c_Character2 = { 'O', 'A', 'E'};
		public static char[] Constants = { 'B', 'C', 'D', 'F', 'G', 'H', 'J', 'K', 'L', 'M', 'N', 'P', 'Q', 'R', 'S', 'T', 'V', 'W', 'X', 'Y', 'Z' };
		public static char[] Vowels = { 'A', 'E', 'I', 'O', 'U' };
		public static string[] Colors = { "Red", "Blue", "Green", "Yellow", "Black", "White", "Orange", "Purple", "Gray", "Rainbow", "Tan", "Dark Green" };
		public static string[] Objects = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Config", "toys.txt"));
		public static string[] Adjectives = File.ReadAllLines(Path.Combine(Environment.CurrentDirectory, "Config", "english-adjectives.txt"));
		public static string[] CatchType = {"Woof Woof", "Its a me, a NAME", "Its FOOD time!"};
		public static string[] Foods = { "Pizza", "Peaches", "Ice Cream", "Dog Food", "Blueberry's", "" };

		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			// If there are no paramiters (AKA: Running !coulsoncharacter with no extras added)
			if (Params.Count == 0)
			{
				Random rng = new Random();

				// The name of the character
				string Name =
					c_Character1[rng.Next(0, c_Character1.Length - 1)].ToString() +
					c_Character2[rng.Next(0, c_Character2.Length - 1)].ToString() +
					"ULS" +
					c_Character2[rng.Next(0, c_Character2.Length - 1)].ToString() +
					c_Character1[rng.Next(0, c_Character1.Length - 1)].ToString();

				string Height = rng.Next(3, 7) + "," + rng.Next(0, 9); // The height of your character
				string Weight = rng.Next(30, 200) + " Pounds"; // The weight of your character
				string FurColor = Colors[rng.Next(0, Colors.Length)]; // The fur color of your character
				string ShirtColor = Colors[rng.Next(0, Colors.Length)]; // The shirt color of your character
				string PantsColor = Colors[rng.Next(0, Colors.Length)]; // The pants color of your character
				string fToy = Objects[rng.Next(0, Objects.Length)]; // Your characters favorite toy
				string Catch = Constants[rng.Next(0, Constants.Length - 1)].ToString() + "oo" + Constants[rng.Next(0, Constants.Length - 1)].ToString().ToLower(); // Your characters catchphrase
				string PolishedCatch = Catch + " " + Catch; // The polished version of the catchphrase used in the command.

				await ReturnCharacter(userMessage, Name, Height, Weight, FurColor, ShirtColor, PantsColor, fToy, PolishedCatch);
			}

			// Load an existing character
			else if (Params[0].String == "loadprofile")
			{
				string profile = Params[1].String;

				switch(profile.ToLower())
                {
					case "coulson":
						string Name = "COULSON";
						string Height = "5,1";
						string Weight = "140 Pounds"; // he can be fucking fat
						string FurColor = "Tan";
						string ShirtColor = "Green";
						string PantsColor = "Dark Green";
						string fToy = "Plush Racoon";
						string Catch = "Woof Woof!";

						await ReturnCharacter(userMessage, Name, Height, Weight, FurColor, ShirtColor, PantsColor, fToy, Catch);
						break;
					default:
						await userMessage.Channel.SendMessageAsync("The character profile '" + Params[1].String + "' does not exist or has not been added to the registry yet.");
						break;
				}
			}

			// Simplified version of the default !coulsoncharacter
			else if (Params[0].String == "simple")
			{
				Random rng = new Random();

				string message = "";

				string Name =
					c_Character1[rng.Next(0, c_Character1.Length - 1)].ToString() +
					c_Character2[rng.Next(0, c_Character2.Length - 1)].ToString() +
					"ULS" +
					c_Character2[rng.Next(0, c_Character2.Length - 1)].ToString() +
					c_Character1[rng.Next(0, c_Character1.Length - 1)].ToString();

				string FurColor = Colors[rng.Next(0, Colors.Length)];
				string ShirtColor = Colors[rng.Next(0, Colors.Length)];
				string PantsColor = Colors[rng.Next(0, Colors.Length)];
				string Catch = Constants[rng.Next(0, Constants.Length - 1)].ToString() + "oo" + Constants[rng.Next(0, Constants.Length - 1)].ToString().ToLower();

				// Create the message
				ReturnSimplifiedCharacter(userMessage, Name, FurColor, ShirtColor, PantsColor, Catch);
			}
			else
            {
				await userMessage.Channel.SendMessageAsync("The provided prefix ' " + Params[0].String + " ' was not found.Please provide a valid prefix.");
			}

			return Task.CompletedTask;
		}

		// Send a character message with all of the variables.
		public async Task<Task> ReturnCharacter(SocketUserMessage userMessage, string Name, string Height, string Weight, string FurColor, string ShirtColor, string PantsColor, string fToy, string Catch)
        {
			string message = "";

			message += "**" + Name + "**";
			
			message += "\n\n\n**__General Information__**";
			message += "\n\nHeight: " + Height;
			message += "\n\nWeight: " + Weight;
			message += "\n\nFur Color: " + FurColor;
			message += "\n\nShirt Color: " + ShirtColor;
			message += "\n\nPants Color: " + PantsColor;


			message += "\n\n\n**__Personality__**";
			message += "\n\nFavorite Toy: " + fToy;
			message += "\n\nCatchphrase : " + Catch;

			await userMessage.Channel.SendMessageAsync(message);

			return Task.CompletedTask;
		}

		public async Task<Task> ReturnSimplifiedCharacter(SocketUserMessage userMessage, string Name, string FurColor, string ShirtColor, string PantsColor, string Catch)
		{
			string message = "";

			message += "**" + Name + "**";
			message += "\n**__General Information__**";
			message += "\nFur Color: " + FurColor;
			message += "\nShirt Color: " + ShirtColor;
			message += "\nPants Color: " + PantsColor;
			message += "\n**__Personality__**";
			message += "\nCatchphrase : " + Catch + " " + Catch;

			await userMessage.Channel.SendMessageAsync(message);

			return Task.CompletedTask;
		}
	}
}
