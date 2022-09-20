using Discord;
using Discord.WebSocket;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Linq;
using MarkovSharp;
using MarkovSharp.TokenisationStrategies;
using System.Net;

namespace CuzsieBot
{
    public class VineBoom : Command
    {
        public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "/assets/vineboom.mp3";

            await userMessage.Channel.SendFileAsync(path);

            return Task.CompletedTask;
        }
    }
}
