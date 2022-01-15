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
					if (character.ToString() == String.Empty || character.ToString() == "*")
                    {
						converted += " ";
                    }
					else
                    {
						converted += $":regional_indicator_{character}:"; 
                    }
					
                }

			}

			await userMessage.Channel.SendMessageAsync(converted);
			return Task.CompletedTask;
		}
	}
}
