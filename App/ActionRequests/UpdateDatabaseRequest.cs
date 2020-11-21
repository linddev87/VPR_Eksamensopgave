using App.Interfaces;
using App.Model;
using App.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.ActionRequests
{
    class UpdateDatabaseRequest : IActionRequest
    {
        public UpdateDatabaseRequest()
        {

        }
        public string Run()
        {
            return UpdateDatabaseForShortlist();
        }

        private string UpdateDatabaseForShortlist()
        {
            try
            {
                DbMaintenanceService.Run();

                return "The database update was initiated.";
            }
            catch(Exception e)
            {
                return $"Something went wrong with your request: {e.Message}";
            }

            
        }
    }
}
