using Discord;
using Discord.Commands;

namespace TutorialBot.Modules
{
    public class CommandsEn : ModuleBase<SocketCommandContext>
    {
        [Command( "What time is it" )]
        public async Task TimeNow()
        {
            DateTime localDate = DateTime.Now;

            await Context.Channel.SendMessageAsync( "Time: " + localDate + " :flag_pl: " );
        }

        [Command( "ping" )]
        public async Task Ping()
        {
            await ReplyAsync( "pong" );
        }
    }
}
