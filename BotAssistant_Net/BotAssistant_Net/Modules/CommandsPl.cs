using Discord.Commands;
using Discord;

namespace TutorialBot.Modules
{
    public class CommandsPl : ModuleBase<SocketCommandContext>
    {

        [Command( "Dodaj" )]
        public async Task Addition( float numberFirst, float numberSecond )
        {
            float result = numberFirst + numberSecond;
            string resultString = result.ToString();

            await Context.Channel.SendMessageAsync( "Wynik Wynosi: " + resultString );
            // await ReplyAsync("Pong");
        }
        [Command( "Odejmij" )]
        public async Task Subtraction( float numberFirst, float numberSecond )
        {
            float result = numberFirst - numberSecond;
            string resultString = result.ToString();

            await Context.Channel.SendMessageAsync( "Wynik Wynosi: " + resultString );
            // await ReplyAsync("Pong");
        }
        [Command( "Pomnóż" )]
        public async Task Multiplication( float numberFirst, float numberSecond )
        {
            float result = numberFirst * numberSecond;
            string resultString = result.ToString();

            await Context.Channel.SendMessageAsync( "Wynik Wynosi: " + resultString );
            // await ReplyAsync("Pong");
        }
        [Command( "Podziel" )]
        public async Task Division( float numberFirst, float numberSecond )
        {
            float result = numberFirst / numberSecond;
            string resultString = result.ToString();

            await Context.Channel.SendMessageAsync( "Wynik Wynosi: " + resultString );
            // await ReplyAsync("Pong");
        }
        [Command( "Wypisz" )]
        public async Task Printing( float numberFirst, float numberSecond )
        {
            // float result = numberFirst / numberSecond;
            await Context.Channel.SendMessageAsync( "Wypisuje liczby " );

            for( ; numberFirst <= numberSecond; numberFirst++ )
            {
                string resultString = numberFirst.ToString();
                await Context.Channel.SendMessageAsync( "  " + resultString );
            }

            // await ReplyAsync("Pong");
        }
        [Command( "ID Użytkownika" )]
        public async Task IDUser( IGuildUser user )
        {
            await Context.Channel.SendMessageAsync( "ID Użytkownika to: " + user.ToString() );
        }
        [Command( "Pomoc" )]
        public async Task Help()
        {
            await Context.Channel.SendMessageAsync( "Dostepne Komędy To: " );
            await Context.Channel.SendMessageAsync( "Dodaj <liczba1> <liczba2>" );
            await Context.Channel.SendMessageAsync( "Odejmij <liczba1> <liczba2>" );
            await Context.Channel.SendMessageAsync( "Pomnóż <liczba1> <liczba2>" );
            await Context.Channel.SendMessageAsync( "Podziel <liczba1> <liczba2>" );
            await Context.Channel.SendMessageAsync( "ID Użytkownika <@UserName>" );
        }
        [Command( "Czy kocha mnie" )]
        public async Task Love( string user )
        {
            Random rnd = new Random();
            int random = rnd.Next( 3, 5 );

            if( random % 2 == 0 )
            {
                await Context.Channel.SendMessageAsync( user + " kocha Cię :heart:" );
            }
            else
            {
                await Context.Channel.SendMessageAsync( user + " nie kocha Cię :broken_heart:" );
            }

        }
        [Command( "Jaką księżniczką Disneya jest" )]
        public async Task PrincesDisneya( IGuildUser user )
        {
            Random rnd = new Random();
            int random = rnd.Next( 0, 6 );


            /*0-MULAN
              1-ROSZPUNKA
              2-MERIDA
              3-Elsa
              4-Anna
              5-Moana*/
            if( random == 0 )
            {
                await Context.Channel.SendMessageAsync( user + " jest Mulan" );
                await Context.Channel.SendMessageAsync( "https://tenor.com/view/mulan-ouat-gif-11243119" );
            }
            else if( random == 1 )
            {
                await Context.Channel.SendMessageAsync( user + " jest Roszpunką" );
                await Context.Channel.SendMessageAsync( "https://tenor.com/view/jplove-rapunzel-disney-gif-5962112" );
            }
            else if( random == 2 )
            {
                await Context.Channel.SendMessageAsync( user + " jest Meridą" );
                await Context.Channel.SendMessageAsync( "https://tenor.com/view/merida-brave-loool-kiss-pout-gif-12604284" );
            }
            else if( random == 3 )
            {
                await Context.Channel.SendMessageAsync( user + " jest Elsą" );
                await Context.Channel.SendMessageAsync( "https://tenor.com/view/elsa-frozen-frozen2-sass-gif-15763725" );
            }
            else if( random == 4 )
            {
                await Context.Channel.SendMessageAsync( user + " jest Anną" );
                await Context.Channel.SendMessageAsync( "https://tenor.com/view/anna-frozen-giddy-excited-so-gif-12410907" );
            }
            else if( random == 5 )
            {
                await Context.Channel.SendMessageAsync( user + " jest Moaną" );
                await Context.Channel.SendMessageAsync( "https://tenor.com/view/moana-nope-hmm-uninterested-um-gif-8071029" );
            }
        }
        [Command( "Jaki jest czas" )]
        public async Task TimeNow()
        {
            DateTime localDate = DateTime.Now;

            await Context.Channel.SendMessageAsync( "Godzina: " + localDate + " :flag_pl: " );
        }
    }
}