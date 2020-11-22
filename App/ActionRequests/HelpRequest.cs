using App.Interfaces;

namespace App.ActionRequests
{
    /// <summary>
    /// Implementation of IActionRequest to handle 'help' commands.
    /// Very generic at present - can be expanded to handle more specific help requests.
    /// </summary>
    public class HelpRequest : IActionRequest
    {
        private string[] Params { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Help information for the given 'help' command.</returns>
        private string GetHelpString()
        {
            // The idea is to add more specific help items at a later point if the user has supplied arguments to the help command. 
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