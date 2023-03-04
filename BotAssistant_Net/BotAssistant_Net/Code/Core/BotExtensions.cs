using Discord;
using Discord.WebSocket;

namespace BotAssistant_Net.Code.Core
{
    public static class BotExtensions
    {
        private static Dictionary<DiscordSocketClient, Color> m_LastUsedColor = new Dictionary<DiscordSocketClient, Color>();

        public static Color GetOrderColorRainbow( DiscordSocketClient client )
        {
            List<Color> rainbowColors = new List<Color>()
            {
                Color.Red,
                Color.Orange,
                Color.Gold,
                Color.Green,
                Color.Blue,
                Color.Purple,
                Color.Magenta,
            };

            Color currentColor = new Color();

            if( m_LastUsedColor.TryGetValue( client, out Color value ) )
            {
                int index = rainbowColors.IndexOf( value );
                if( index >= rainbowColors.Count - 1 )
                {
                    currentColor = rainbowColors.First();
                }
                else
                {
                    currentColor = rainbowColors[index + 1];
                }
                m_LastUsedColor[client] = currentColor;
            }
            else
            {
                currentColor = rainbowColors.First();
                m_LastUsedColor.Add( client, currentColor );
            }
            return currentColor;
        }
    }
}
