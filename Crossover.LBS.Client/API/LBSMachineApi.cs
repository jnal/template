using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Crossover.LBS.API.Contracts.DTO;
using Crossover.LBS.Client.DTO;
using RestSharp;

namespace Crossover.LBS.Client
{
    public static class LBSMachineApi
    {
        const string BaseUrl = "http://localhost:1673/api/Machine";



        public static List<BackupDto> GetBackupSchedule(string ipAddress)
        {
            var client = new RestClient(BaseUrl);
            var request = new RestRequest("backup/ip/{ipAddress}");
            request.AddUrlSegment("ipAddress", ipAddress);
            return client.Execute<List<BackupDto>>(request).Data;
        }


      
    }

   
}
