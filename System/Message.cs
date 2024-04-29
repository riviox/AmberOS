using System;

namespace amberos.System
{
    public static class Message
    {
        public static void Error(string err)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"[Error] ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(err);
            Console.ForegroundColor = ConsoleColor.White;
        }
        
        public static void Info(string info)
        {
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"[Info] ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(info);
            Console.ForegroundColor = ConsoleColor.White;
        }
        public static void Warning(string err)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"[Warning] ");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(err);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static string CenterText(string text)
		{
			int consoleWidth = 90;
			int padding = (consoleWidth - text.Length) / 2;
			string centeredText = text.PadLeft(padding + text.Length).PadRight(consoleWidth);
			return centeredText;
		}
    }
}