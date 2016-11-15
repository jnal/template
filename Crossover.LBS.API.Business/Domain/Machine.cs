using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Crossover.LBS.API.Business.Domain
{
    [Table("machine")]
    public class Machine
    {
        [Key]
        public int Id { get; set; }
        [Column("ip_address")]
        public string IPAddress { get; set; }

        public bool IsActive { get; set; }
        public ICollection<MachineBackup> BackupConfigs { get; set; }
    }
}
