using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Crossover.LBS.API.Business.Domain
{
    [Table("machine_backup_log")]
    public class BackupLog
    {
        [Key]
        public int Id { get; set; }

        public string Message { get; set; }
        public DateTime LogDate { get; set; }
        [Column("machine_backup_id")]
        public int MachineBackupId { get; set; }
    }
}
