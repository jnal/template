using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crossover.LBS.API.Contracts.DTO
{
    public class BackupDto
    {
        public int Id { get; set; }
        public int MachineId { get; set; }
        public string SourcePath { get; set; }
        public string SourceDomain { get; set; }
        public string SourceUsername { get; set; }
        public string SourcePassword { get; set; }
        public string DestinationPath { get; set; }
        public string DestinationDomain { get; set; }
        public string DestinationUsername { get; set; }
        public string DestinationPassword { get; set; }
        public DateTime Schedule { get; set; }
        public DateTime? LastRun { get; set; }
    }
}
