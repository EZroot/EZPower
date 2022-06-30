using System;

namespace EZPower
{
    public static class Debug
    {
        public static bool ShowCurrentTime = true;
        public static bool ShowLogs = false;

        public static void Log(object msg)
        {
            if (!ShowLogs)
            {
                return;
            }
            PreMessage();
            Console.WriteLine(msg);
        }

        public static void Log(object msg, bool singleLine = true)
        {
            if (!ShowLogs)
            {
                return;
            }
            PreMessage();
            if (singleLine)
            {
                Console.WriteLine(msg);
            }
            else
            {
                Console.Write(msg);
            }
        }

        public static void Warn(object msg)
        {
            if (!ShowLogs)
            {
                return;
            }
            PreMessage();
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Error(object msg)
        {
            if (!ShowLogs)
            {
                return;
            }
            PreMessage();
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Log(object msg, ConsoleColor color)
        {
            if (!ShowLogs)
            {
                return;
            }
            PreMessage();
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Log(object msg, ConsoleColor color, bool singleLine = true)
        {
            if (!ShowLogs)
            {
                return;
            }
            PreMessage();
            Console.ForegroundColor = color;
            if(singleLine)
            {
                Console.WriteLine(msg);
            }
            else
            {
                Console.Write(msg);
            }
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Print(object value, Vector2 position)
        {
            if (!ShowLogs)
            {
                return;
            }
            Console.SetCursorPosition(position.x, position.y);
            Console.Write(value);
        }

        static void PreMessage()
        {
            if (ShowCurrentTime)
            {
                Console.Write("[" + DateTime.Now + "] ");
            }
        }
    }
}
