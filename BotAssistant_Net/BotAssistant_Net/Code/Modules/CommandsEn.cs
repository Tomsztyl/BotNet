using BotAssistant_Net.Code.Core;
using Discord;
using Discord.Commands;
using Discord.WebSocket;

namespace TutorialBot.Modules
{
    public class CommandsEn : ModuleBase<SocketCommandContext>
    {

        [Command( "What time is it" )]
        public async Task TimeNow()
        {
            DateTime localDate = DateTime.Now;


            await Context.Channel.SendMessageAsync();

            await Context.Channel.SendMessageAsync( "Time: " + localDate + " :flag_pl: " );
        }

        [Command( "ping" )]
        public async Task Ping()
        {
            await ReplyAsync( "pong" );
        }

        [Command( "Info" )]
        public async Task SendRichEmbedAsync()
        {
            Color colorRainbow = BotExtensions.GetOrderColorRainbow( Context.Client );

            var embed = new EmbedBuilder
            {
                // Embed property can be set within object initializer
                Title = "Hello world!",
                Description = "I am a description set by initializer."
            };
            // Or with methods
            embed.AddField( "Field title",
                "Field value. I also support [hyperlink markdown](https://example.com)!" )
                .WithAuthor( Context.Client.CurrentUser )
                .WithFooter( footer => footer.Text = "I am a footer." )
                .WithColor( colorRainbow )
                .WithTitle( "I overwrote \"Hello world!\"" )
                .WithDescription( "I am a description." )
                .WithUrl( "https://example.com" )
                .WithCurrentTimestamp();
            embed.WithImageUrl( Context.Client.CurrentUser.GetAvatarUrl() );

            //Your embed needs to be built before it is able to be sent
            await ReplyAsync( embed: embed.Build() );
        }
    }
}
