using App.ActionRequests;
using App.Controllers;
using App.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace App.Factories
{
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
                default:
                    return new InvalidRequest();
            }
        }
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
