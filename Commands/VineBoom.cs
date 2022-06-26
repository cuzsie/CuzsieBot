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
        private Random random = new Random();

        private string GetImageURL()
        {
            return "http://generated.inspirobot.me/" + random.Next(1, 87).ToString("D3") + "/aXm" + random.Next(1000, 9001) + "xjU.jpg";
        }

        private ulong nextFileUuid = 0;

        private async Task<string> DownloadNewImage()
        {
            string fileName = nextFileUuid + ".jpg";
            nextFileUuid += 1;

            using (WebClient webClient = new WebClient())
            {
                await webClient.DownloadFileTaskAsync(new Uri(GetImageURL()), fileName);
            }

            return fileName;
        }

        public override async Task<Task> Run(List<Parameter> Params, SocketUserMessage userMessage)
        {
            string path = System.IO.Directory.GetCurrentDirectory() + "/assets/vineboom.mp3";
            Console.WriteLine(path);
            // await userMessage.Channel.SendFileAsync(path);

            await userMessage.Channel.SendMessageAsync("Penis");
            //System.IO.File.Delete(path);

            return Task.CompletedTask;
        }
    }
}
