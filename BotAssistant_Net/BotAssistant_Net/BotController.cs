using Discord.Commands;
using Discord.WebSocket;
using Discord;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BotAssistant_Net
{
    internal class BotController
    {
        public const string PREFIX_COMMAND = "";

        static void Main( string[] args ) => new BotController().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient m_Client;
        private CommandService m_Commands;
        private IServiceProvider m_Services;

        private async Task RunBotAsync()
        {
            m_Client = new DiscordSocketClient();
            m_Commands = new CommandService();

            m_Services = new ServiceCollection()
                .AddSingleton( m_Client )
                .AddSingleton( m_Commands )
                .BuildServiceProvider();

            string token = "NzIxMDMxNTMyNjI5NzIxMTMw.GPv3p6.xilVv9CvwIFBQcsviEnAG2hMkKVAl1_ol00bQs";

            m_Client.Log += _client_Log;

            await RegisterCommandsAsync();

            await m_Client.LoginAsync( TokenType.Bot, token );

            await m_Client.StartAsync();

            await Task.Delay( -1 );

        }

        private Task _client_Log( LogMessage arg )
        {
            Console.WriteLine( arg );
            return Task.CompletedTask;
        }

        private async Task RegisterCommandsAsync()
        {
            m_Client.MessageReceived += HandleCommandAsync;
            await m_Commands.AddModulesAsync( Assembly.GetEntryAssembly(), m_Services );
        }

        private async Task HandleCommandAsync( SocketMessage arg )
        {
            var message = arg as SocketUserMessage;
            var context = new SocketCommandContext( m_Client, message );
            if( message.Author.IsBot ) return;

            int argPos = 0;
            if( message.HasStringPrefix( PREFIX_COMMAND, ref argPos ) )
            {
                var result = await m_Commands.ExecuteAsync( context, argPos, m_Services );
                if( !result.IsSuccess )
                {
                    Console.WriteLine( result.ErrorReason );
                }

                if( result.Error.Equals( CommandError.UnmetPrecondition ) )
                {
                    await message.Channel.SendMessageAsync( result.ErrorReason );
                }
            }
        }
    }
}