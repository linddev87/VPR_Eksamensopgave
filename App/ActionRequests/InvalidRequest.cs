using App.Interfaces;
using System;

namespace App.Controllers
{
    /// <summary>
    /// Used whenever the user inputs something that we don't have a better option for.
    /// </summary>
    public class InvalidRequest : IActionRequest
    {
        private string[] Params { get; set; }
        public InvalidRequest()
        {

        }

        public string Run()
        {
            return GetResultString();
        }

        private string GetResultString()
        {
            return "Your request was invalid. Please type 'help' for a list of options";
        }
    }
}