﻿namespace BotAssistant_Net.Code.Core
{
    public enum ETypeLog
    {
        Log,
        Succes,
        Warning,
        Error,
    }

    public static class Debuger
    {
        private const string LOG_PREFIX = "BOT[LOG]~ ";

        public static void PrintLog( string logText, ETypeLog eTypeLog = ETypeLog.Log )
        {
            SetColorConsole( eTypeLog );
            string text = string.Format( "{0}{1}", LOG_PREFIX, logText );
            Console.WriteLine( text );
            Console.ResetColor();
        }

        private static void SetColorConsole( ETypeLog eTypeLog )
        {
            switch( eTypeLog )
            {
                case ETypeLog.Log:
                    Console.ResetColor();
                    break;
                case ETypeLog.Warning:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    break;
                case ETypeLog.Error:
                    Console.ForegroundColor = ConsoleColor.Red;
                    break;
                case ETypeLog.Succes:
                    Console.ForegroundColor = ConsoleColor.Green;
                    break;
            }
        }
    }
}