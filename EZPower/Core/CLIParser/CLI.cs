using System;
using System.Collections.Generic;
using System.Text;

namespace EZPower
{
    //replace the shit debug stuff
    public static class CLI
    {
        public static string DefaultPrefix = ">";
        public static void Clear()
        {
            Console.Clear();
        }

        public static string ReadLine(string prefix)
        {
            Print(prefix, ConsoleColor.White, true);
            return Console.ReadLine();
        }

        public static string ReadLine()
        {
            Print(DefaultPrefix, ConsoleColor.White, false);
            return Console.ReadLine();
        }

        public static void Print(object msg)
        {
            Console.WriteLine(msg);
        }

        public static void Print(object msg, bool singleLine = true)
        {
            if (singleLine)
            {
                Console.WriteLine(msg);
            }
            else
            {
                Console.Write(msg);
            }
        }

        public static void Print(object msg, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(msg);
            Console.ForegroundColor = ConsoleColor.White;
        }

        public static void Print(object msg, ConsoleColor color, bool singleLine = true)
        {
            Console.ForegroundColor = color;
            if (singleLine)
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
            Console.SetCursorPosition(position.x, position.y);
            Console.Write(value);
        }

    }
}
