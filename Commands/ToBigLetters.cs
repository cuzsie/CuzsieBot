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
	public class ToBigLetters : Command
	{
		public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
		{
			string converted = "";

			if (Params.Count != 0)
            {
				string input = "";

				foreach (Parameter Param in Params)
                {
					input += $" {Param.String}";
                }
				
				foreach(char character in input)
                {
					converted += coolChecks(character);	
                }

			}

			await userMessage.Channel.SendMessageAsync(converted);

			return Task.CompletedTask;
		}

		string coolChecks(char character)
        {
			string converted = "";

			character.ToString().Replace("`", "");

			if (character.ToString() == String.Empty || character.ToString() == " ") { converted += " "; }
			else if (character.ToString() == "!") { converted += ":exclamation:"; }
			else if (character.ToString() == "?") { converted += ":question:"; }
			else if (character.ToString() == "#") { converted += ":hash:"; }
			else if (character.ToString() == "0" || character.ToString() == "1" || character.ToString() == "2" || character.ToString() == "3" || character.ToString() == "4" || character.ToString() == "5" || character.ToString() == "6" || character.ToString() == "7" || character.ToString() == "8" || character.ToString() == "9") { converted += $":{numberToStringVal(character.ToString())}:"; }
			else if (character.ToString() == "*") { converted += ":asterisk:"; }
			else if (character.ToString() == ">" || character.ToString() == "→") { converted += ":arrow_backward:"; }
			else if (character.ToString() == "<" || character.ToString() == "←") { converted += ":arrow_forward:"; }
			else if (character.ToString() == ".") { converted += ":record_button:"; }
			else if (character.ToString() == "\"") { converted += ":pause_button:"; }
			else if (character.ToString() == "-") { converted += ":no_entry:"; }
			else if (character.ToString() == "|") { converted += ":regional_indicator_i:"; }
			else if (character.ToString() == "(" || character.ToString() == "[" || character.ToString() == "{") { converted += ":arrow_right_hook:"; }
			else if (character.ToString() == ")" || character.ToString() == "]" || character.ToString() == "}") { converted += ":leftwards_arrow_with_hook:"; }
			else if (character.ToString() == "+") { converted += ":heavy_plus_sign:"; }
			else if (character.ToString() == "÷") { converted += ":heavy_division_sign:"; }
			else if (character.ToString() == "^" || character.ToString() == "↑") { converted += ":arrow_up_small:"; }
			else if (character.ToString() == "$") { converted += ":heavy_dollar_sign:"; }
			else if (character.ToString() == "\\") { converted += ":bone:"; }
			else if (character.ToString() == "/") { converted += ":link:"; }
			else if (character.ToString() == "♡") { converted += ":hearts:"; }
			else if (character.ToString() == "∞") { converted += ":infinity:"; }
			else if (character.ToString() == "²") { converted += ":two:"; }
			else if (character.ToString() == "▓") { converted += ":chains:"; }
			else if (character.ToString() == "↓" || character.ToString() == "▼") { converted += ":arrow_down_small:"; }
			else if (character.ToString() == "@" || character.ToString() == "▼") { converted += ":arrow_right_hook::regional_indicator_a:::leftwards_arrow_with_hook:"; }
			else if (character.ToString() == ":" || character.ToString() == ";") { converted += ":eight:"; }
			else { converted += $":regional_indicator_{character.ToString().ToLower()}:"; }

			return converted;
		}	

		string numberToStringVal(string input)
        {
			string silly = "null";
			
			switch(input)
            {
				case "0":
					silly = "zero";
					break;
				case "1":
					silly = "one";
					break;
				case "2":
					silly = "two";
					break;
				case "3":
					silly = "three";
					break;
				case "4":
					silly = "four";
					break;
				case "5":
					silly = "five";
					break;
				case "6":
					silly = "six";
					break;
				case "7":
					silly = "seven";
					break;
				case "8":
					silly = "eight";
					break;
				case "9":
					silly = "nine";
					break;
				default:
					silly = "null";
					break;
			}

			return silly;
		}
	}
}
