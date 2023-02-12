using Discord.Commands;
using Discord.WebSocket;
using Discord;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using System.Xml.Linq;
using Newtonsoft.Json;

namespace BotAssistant_Net
{
    internal class BotController
    {
        private const string NAME_FILE_DATA = "BotPropertiesData.json";
        private BotProperties m_BotPropertiesData = new BotProperties();

        static void Main( string[] args ) => new BotController().RunBotAsync().GetAwaiter().GetResult();

        private DiscordSocketClient m_Client = null;
        private CommandService m_Commands = null;
        private IServiceProvider m_Services = null;

        private async Task RunBotAsync()
        {
            bool tryLoad = TryLoadFileData();
            if( !tryLoad )
            {
                return;
            }

            m_Client = new DiscordSocketClient();
            m_Commands = new CommandService();

            m_Services = new ServiceCollection()
                .AddSingleton( m_Client )
                .AddSingleton( m_Commands )
                .BuildServiceProvider();


            m_Client.Log += _client_Log;

            await RegisterCommandsAsync();
            await m_Client.LoginAsync( TokenType.Bot, m_BotPropertiesData.TokenBot );
            await m_Client.StartAsync();
            await Task.Delay( -1 );

        }

        private bool TryLoadFileData()
        {
            string path = Directory.GetCurrentDirectory();
            string formatPath = string.Format( "{0}\\{1}", path, NAME_FILE_DATA );
            if( !File.Exists( formatPath ) )
            {
                FileStream fileCreated = File.Create( @formatPath );
                fileCreated.Close();
                string json = JsonConvert.SerializeObject( m_BotPropertiesData, Formatting.Indented );
                File.WriteAllText( @formatPath, json );
                PrintLog( "File not exist!" );
                string log = string.Format( "Complete the file [{0}] that was created in the same path and run again!", NAME_FILE_DATA );
                PrintLog( log );
                PrintLog( "Shut down app" );
                return false;
            }
            else
            {
                string loadText = File.ReadAllText( @formatPath );
                m_BotPropertiesData = JsonConvert.DeserializeObject<BotProperties>( loadText );
                PrintLog( "File loaded!" );
                return true;
            }
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
            if( message.HasStringPrefix( m_BotPropertiesData.TokenBot, ref argPos ) )
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

        #region DEBUG
        private const string LOG_PREFIX = "BOT[LOG] ~ ";

        public void PrintLog( string logText )
        {
            string text = string.Format( "{0}{1}", LOG_PREFIX, logText );
            Console.WriteLine( text );
        }

        #endregion
    }

    internal class BotProperties
    {
        public string TokenBot { get; set; } = "gfdg54t546gfgfdgdfghthg45+65dwejelk4";
        public MysqlProperties MysqlProperties { get; set; } = new MysqlProperties();
    }

    internal class MysqlProperties
    {
        public string Server { get; set; } = "127.0.0.1;";
        public string Database { get; set; } = "testdb;";
        public string Uid { get; set; } = "root;";
        public string Password { get; set; } = ";";
    }
}