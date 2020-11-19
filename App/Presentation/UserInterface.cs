using App.Controllers;
using App.Factories;
using App.Interfaces;
using System;

namespace App.UI
{
    public static class UserInterface
    {
        public static void Init()
        {
            string input = GetUserInput().ToLower();
            string[] inputArr = input.Split(" ");

            IActionRequest action = ActionRequestFactory.GetActionRequest(inputArr);

            string result = action.Run();

            Console.WriteLine(result);
            Console.WriteLine();

            Init();
        }

        public static string GetUserInput()
        {
            Console.WriteLine("What would you like to do? (Type 'help' for options)");
            return Console.ReadLine();
        }
    }
}