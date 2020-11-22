using App.ActionRequests;
using App.Controllers;
using App.Interfaces;

namespace App.Factories
{
    /// <summary>
    /// The ActionRequestFactory returns an IActionRequest based on the users input. See the relevant ActionRequests for further info.
    /// </summary>
    public static class ActionRequestFactory
    {
        public static IActionRequest GetActionRequest(string[] inputArr)
        {
            string[] parameters = GetParams(inputArr);

            switch (inputArr[0])
            {
                case "help":
                    return new HelpRequest(parameters);
                case "assetlookup":
                    return new AssetLookupRequest(parameters);
                case "assetreport":
                    return new AssetReportRequest(parameters);
                case "shortlist":
                    return new AssetShortlistRequest(parameters);
                case "showshortlist":
                    return new ShowShortlistRequest();
                case "updatedatabase":
                    return new UpdateDatabaseRequest();
                default:
                    return new InvalidRequest();
            }
        }

        /// <summary>
        /// Separates the users command from the arguments and returns only the arguments
        /// </summary>
        /// <param name="inputArr">The input array collected in the UserInterface.</param>
        /// <returns>string array</returns>
        public static string[] GetParams(string[] inputArr)
        {
            string[] parameters = new string[inputArr.Length - 1];
            for (int i = 0; i < inputArr.Length - 1; i++)
            {
                parameters[i] = inputArr[i + 1];
            }

            return parameters;
        }
    }
}
