using App.Interfaces;
using App.Services;
using System;

namespace App.ActionRequests
{
    /// <summary>
    /// Call the DbMaintenanceService methods required to update the database with the current shortlist
    /// </summary>
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
            catch (Exception e)
            {
                return $"Something went wrong with your request: {e.Message}";
            }
        }
    }
}
