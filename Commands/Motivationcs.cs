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
	public class Motivation : Command
	{
        private Random random = new Random();

        private async Task<IUserMessage> EditOrSendNewMessage(EmbedBuilder embedToSend, IUserMessage messageToEdit)
        {
            IMessage result = await messageToEdit.Channel.GetMessageAsync(messageToEdit.Id);

            if (result == null)
                return await messageToEdit.Channel.SendMessageAsync(embed: embedToSend.Build());

            else
            {
                await messageToEdit.ModifyAsync(delegate (MessageProperties properties)
                {
                    properties.Embed = embedToSend.Build();
                });

                return messageToEdit;
            }
        }

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
            Discord.Rest.RestUserMessage msg = await userMessage.Channel.SendMessageAsync("Generating your quote, this may take a second.");

            SocketGuildChannel channel = (SocketGuildChannel)userMessage.Channel;

            string path = await DownloadNewImage();
            await userMessage.Channel.SendFileAsync(path);
            System.IO.File.Delete(path);

            await userMessage.Channel.DeleteMessageAsync(msg);

            return Task.CompletedTask;
		}
	}
}
