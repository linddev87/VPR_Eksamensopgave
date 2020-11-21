using App.Interfaces;
using System;

namespace App.Controllers
{
    public class InvalidRequest : IActionRequest
    {
        public string[] Params { get; private set; }
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