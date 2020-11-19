using App.Interfaces;

namespace App.Controllers
{
    internal class AssetReportRequest : IActionRequest
    {
        public string Action { get; private set; }
        public string[] Params { get; private set; }
        
        public AssetReportRequest()
        {

        }

        public AssetReportRequest(string[] parameters)
        {
            Params = parameters;
        }

        public string Run()
        {
            return "";
        }
    }
}