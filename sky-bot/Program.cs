using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace sky_bot {

    class Program {

        protected const string AppName = "SkyBot";

        // TODO: Move to config file
        protected const string AppToken = "MjAzOTQzMzE0NTUzMDQ0OTky.CmwsDg.WgcyRwajzMcSWcVs9x5Cmp8DKVk";

        protected static DiscordClient client;

        static void Main(string[] args) {

            client = new DiscordClient(x => {
                x.AppName = AppName;
            });

            client.UsingCommands(x => {
                x.PrefixChar = '.';
            });

            client.GetService<CommandService>().CreateCommand("game")
                .Description("Sets the bot's currently playing game")
                .Parameter("game", ParameterType.Unparsed)
                .Do(e => {
                    client.SetGame(e.GetArg("game"));
                });

            client.ExecuteAndWait(async () => {
                while(true) {
                    try {
                        await client.Connect(AppToken);
                        client.SetGame("cuddling simkitty");
                        break;
                    }
                    catch(Exception exception) {
                        await Task.Delay(client.Config.FailedReconnectDelay);
                    }
                }
            });

        }

    }

}
