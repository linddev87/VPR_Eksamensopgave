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
            try
            {
                string input = GetUserInput().ToLower();
                string[] inputArr = input.Split(" ");

                IActionRequest action = ActionRequestFactory.GetActionRequest(inputArr);

                string result = action.Run();

                Message(result);

                Init();
            } 
            catch(Exception e)
            {
                Message("Something went wrong: " + e.Message);
            }
        }

        internal static void Message(string message)
        {
            Console.WriteLine(message);
        }

        public static string GetUserInput()
        {
            return Console.ReadLine();
        }
    }
}