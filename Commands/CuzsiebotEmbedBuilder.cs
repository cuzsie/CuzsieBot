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
    class CuzsiebotEmbedBuilder
    {
        public EmbedBuilder buildEmbed(string Title = "Title", string[] fields = null, string imageUrl = "https://cdn.discordapp.com/attachments/912843824819232829/927994985566064710/unknown.png", string thumbnailUrl = "https://cdn.discordapp.com/attachments/912843824819232829/927994985566064710/unknown.png", string url = "https://google.com")
        {
            EmbedBuilder builder = new EmbedBuilder();

            builder.WithTitle(Title);
            foreach (string Item in fields)
            {
                builder.AddField(Item, "");
            }
            
            builder.WithThumbnailUrl(thumbnailUrl);
            builder.ImageUrl = imageUrl;
            builder.Url = url;


            return builder;
        }
    }
}
