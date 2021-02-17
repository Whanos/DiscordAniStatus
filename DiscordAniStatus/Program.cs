using System;
using System.Collections.Generic;
using System.Threading;
using DiscordRPC;
using DiscordRPC.Logging;

namespace DiscordAniStatus
{
    internal static class Program
    {
        private static DiscordRpcClient client;
        public static void Main(string[] args)
        {
            client = new DiscordRpcClient("ID HERE");
            
            client.Logger = new ConsoleLogger() { Level = LogLevel.Warning};

            client.OnReady += (sender, e) =>
            {
                Console.WriteLine($"Received ready from user {e.User.Username}");
            };

            client.OnPresenceUpdate += (sender, e) =>
            {
                Console.WriteLine($"Received presence update! {e.Presence}");
            };

            client.Initialize(); //start
            Animated();
        }

        private static void Animated()
        {
            uint count = 0;
            //buttons example
            //you can have a max of two buttons
            List<Button> buttons = new List<Button>()
            {
                new Button()
                {
                    Label = "Label on the button",
                    Url = "Url the button will take you too"
                },
                new Button()
                {
                Label = "Label on the button2",
                Url = "Url the button will take you too2"
                }
            };
            while (true)
            {
                count++; 
                client.SetPresence(new RichPresence()
                {
                    //uncomment to enable buttons
                    //Buttons = buttons.ToArray(),
                    Details = "details here",
                    State = $"status here",
                    Assets = new Assets()
                    {
                        LargeImageKey = "large image key 1",
                        LargeImageText = "large image text 1"
                    }
                });
                Thread.Sleep(1000); //you MUST wait at least a second or it will not work
                client.SetPresence(new RichPresence()
                {
                    //uncomment to enable buttons
                    //Buttons = buttons.ToArray()
                    Details = "details here",
                    State = $"state here",
                    Assets = new Assets()
                    {
                        LargeImageKey = "next frame key",
                        LargeImageText = "next frame text"
                    }
                });
                //you can repeat this as many times as you like
                Thread.Sleep(1000);
            }
        }
    }
}