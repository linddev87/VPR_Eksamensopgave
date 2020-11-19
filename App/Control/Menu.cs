using System;

namespace App.Control
{
    public static class Menu
    {
        public static void Init()
        {
            var action = GetUserInput().ToLower();

            ProcessAction(action);
        }

        private static void ProcessAction(string action)
        {
            switch (action)
            {
                case "help"
            }
        }

        public static string GetUserInput()
        {
            Console.WriteLine("What would you like to do? (Type 'help' for options.");
            return Console.ReadLine();
        }
    }
}