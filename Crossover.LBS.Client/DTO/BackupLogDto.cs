using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crossover.LBS.API.Contracts.DTO
{
    public class BackupLogDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public DateTime LogDate { get; set; }
        public int MachineBackupId { get; set; }
    }
}
