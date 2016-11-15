using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crossover.LBS.API.Contracts.DTO;
using RestSharp;

namespace Crossover.LBS.Client.API
{
    public static class LBSLogApi
    {
        const string BaseUrl = "http://localhost:1673/api/Log";

        public static void Log(int backupConfigId, string message)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest(Method.POST) {RequestFormat = DataFormat.Json};
            var log = new BackupLogDto
            {
                MachineBackupId = backupConfigId,
                Message = message,
                LogDate = DateTime.Now
            };

            request.AddBody(log);
            client.Execute(request);
        }
    }
}
