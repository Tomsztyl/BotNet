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
        }

        [Command( "Odejmij" )]
        public async Task Subtraction( float numberFirst, float numberSecond )
        {
            float result = numberFirst - numberSecond;
            string resultString = result.ToString();

            await Context.Channel.SendMessageAsync( "Wynik Wynosi: " + resultString );
        }

        [Command( "Pomnóż" )]
        public async Task Multiplication( float numberFirst, float numberSecond )
        {
            float result = numberFirst * numberSecond;
            string resultString = result.ToString();

            await Context.Channel.SendMessageAsync( "Wynik Wynosi: " + resultString );
        }

        [Command( "Podziel" )]
        public async Task Division( float numberFirst, float numberSecond )
        {
            float result = numberFirst / numberSecond;
            string resultString = result.ToString();

            await Context.Channel.SendMessageAsync( "Wynik Wynosi: " + resultString );
        }

        [Command( "Wypisz" )]
        public async Task Printing( float numberFirst, float numberSecond )
        {
            await Context.Channel.SendMessageAsync( "Wypisuje liczby " );

            for( ; numberFirst <= numberSecond; numberFirst++ )
            {
                string resultString = numberFirst.ToString();
                await Context.Channel.SendMessageAsync( "  " + resultString );
            }
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

        [Command( "Jaki jest czas" )]
        public async Task TimeNow()
        {
            DateTime localDate = DateTime.Now;

            await Context.Channel.SendMessageAsync( "Godzina: " + localDate + " :flag_pl: " );
        }


    }
}