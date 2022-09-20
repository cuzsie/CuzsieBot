using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.IO;
using Discord;
using Discord.WebSocket;

namespace CuzsieBot.Internal
{
    public class BotMetadata
    {
        public string name { get; set; }
        public string author { get; set; }
        public string prefix { get; set; }
        public string status { get; set; }
        public string token { get; set; }
        public ActivityType statusType { get; set; }
    }

    public class BotMeta
    { 
        // Big chance this wont ever be used in the bot client
        public static void ParseToJSON(BotMetadata meta)
        {
            string json = JsonSerializer.Serialize(meta);
            string path = System.IO.Directory.GetCurrentDirectory() + "/bot.json";

            File.WriteAllText(path, json); 
        }

        public static BotMetadata ParseFromJSON(string path)
        {
            BotMetadata meta = JsonSerializer.Deserialize<BotMetadata>(File.ReadAllText(path));
            return meta;
        }
    }
}