using System;
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
            while (true)
            {
                count++; //iterate by one
                client.SetPresence(new RichPresence()
                {
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