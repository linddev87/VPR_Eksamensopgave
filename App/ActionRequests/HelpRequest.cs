using App.Interfaces;
using System;

namespace App.ActionRequests
{
    internal class HelpRequest : IActionRequest
    {
        public string[] Params { get; private set; }

        public HelpRequest()
        {

        }

        public HelpRequest(string[] parameters)
        {
            Params = parameters;
        }

        public string Run()
        {
            return GetHelpString();
        }

        private string GetHelpString()
        {
            if(Params.Length > 1)
            {
                return $"We couldn't find a specific help item for '{Params[1]}'. Type 'help' for a list of options.";
            }
            else
            {
                return $"Available commands:\n   help\n   assetlookup {{symbol}}\n   assetreport {{symbol}}\n   assetreport shortlist\n   shortlist add {{symbol}}\n   shortlist remove {{symbol}}\n   showshortlist\n   updatedatabase";
            }
        }
    }
}