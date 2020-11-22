using App.Controllers;
using App.Factories;
using App.Interfaces;
using System;

namespace App.UI
{
    /// <summary>
    /// The static UserInterface is the presentationlayer of the application.
    /// To access this layer from any class, use UserInterface.Message()
    /// </summary>
    public static class UserInterface
    {
        public static void Init()
        {
            try
            {
                string input = GetUserInput().ToLower();
                string[] inputArr = input.Split(" ");

                //See the ActionRequestFactory, IActionRequest interface and the relevant ActionRequests for further info.
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

        /// <summary>
        /// Global access to the presentation layer
        /// </summary>
        /// <param name="message"></param>
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