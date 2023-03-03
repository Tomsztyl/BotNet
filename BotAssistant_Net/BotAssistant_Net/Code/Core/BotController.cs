using Discord.Commands;
using Discord.WebSocket;
using Discord;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using Newtonsoft.Json;
using BotAssistant_Net.Code.Core.MySQL;

namespace BotAssistant_Net.Code.Core
{
    internal class BotController
    {
        private const string NAME_FILE_DATA = "BotPropertiesData.json";
        public static BotProperties BotPropertiesData { get; private set; } = new BotProperties();

        static void Main( string[] args ) => new BotController().InitializeBot().GetAwaiter().GetResult();

        private DiscordSocketClient m_Client = null;
        private CommandService m_Commands = null;
        private IServiceProvider m_Services = null;

        //MySQL
        private DataManager m_DataManager = new DataManager();

        private async Task InitializeBot()
        {
            bool tryLoad = TryLoadFileData();
            if( !tryLoad )
            {
                return;
            }
            m_DataManager.Initialize();
            await RunBot();
        }

        private async Task RunBot()
        {
            m_Client = new DiscordSocketClient();
            m_Commands = new CommandService();

            m_Services = new ServiceCollection()
                .AddSingleton( m_Client )
                .AddSingleton( m_Commands )
                .BuildServiceProvider();


            m_Client.Log += ClientLog;

            await RegisterCommandsAsync();
            await m_Client.LoginAsync( TokenType.Bot, BotPropertiesData.TokenBot );
            await m_Client.StartAsync();
            await Task.Delay( -1 );
        }

        private bool TryLoadFileData()
        {
            string path = Directory.GetCurrentDirectory();
            string formatPath = string.Format( "{0}/{1}", path, NAME_FILE_DATA );
            if( !File.Exists( formatPath ) )
            {
                FileStream fileCreated = File.Create( @formatPath );
                fileCreated.Close();
                string json = JsonConvert.SerializeObject( BotPropertiesData, Formatting.Indented );
                File.WriteAllText( @formatPath, json );
                Debuger.PrintLog( "File not exist!", ETypeLog.Warning );
                string log = string.Format( "Complete the file [{0}] that was created in the same path and run again!", NAME_FILE_DATA );
                string pathCreated = string.Format( "[{0}] PATH: {1}", NAME_FILE_DATA , @formatPath );
                Debuger.PrintLog( log, ETypeLog.Warning );
                Debuger.PrintLog( pathCreated, ETypeLog.Warning );
                Debuger.PrintLog( "Shut down app", ETypeLog.Warning );
                return false;
            }
            else
            {
                string loadText = File.ReadAllText( @formatPath );
                BotPropertiesData = JsonConvert.DeserializeObject<BotProperties>( loadText );
                string pathCreated = string.Format( "[{0}] PATH: {1}", NAME_FILE_DATA, @formatPath );
                Debuger.PrintLog( pathCreated, ETypeLog.Succes );
                Debuger.PrintLog( "File loaded!", ETypeLog.Succes );
                return true;
            }
        }

        private Task ClientLog( LogMessage arg )
        {
            Debuger.PrintLog( arg.Message );
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

            m_DataManager.SetTeacherUser( string.Format("@{0}#{1}", arg.Author.Username, arg.Author.Discriminator) );
            int argPos = 0;
            if( message.HasStringPrefix( BotPropertiesData.PrefixBot, ref argPos ) )
            {
                var result = await m_Commands.ExecuteAsync( context, argPos, m_Services );
                if( !result.IsSuccess )
                {
                    Debuger.PrintLog( result.ErrorReason );
                }

                if( result.Error.Equals( CommandError.UnmetPrecondition ) )
                {
                    await message.Channel.SendMessageAsync( result.ErrorReason );
                }
            }
            else
            {
                await HandleAIBrain( arg );
            }
        }

        private async Task HandleAIBrain( SocketMessage arg )
        {
           
        }

    }

}