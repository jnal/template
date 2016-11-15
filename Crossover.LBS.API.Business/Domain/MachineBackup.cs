using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Crossover.LBS.API.Business.Domain
{
    [Table("machine_backup")]
    public class MachineBackup
    {
        [Key]
        public int Id { get; set; }
        [Column("machine_id")]
        public int MachineId { get; set; }
        [Column("source_path")]
        public string SourcePath { get; set; }
        [Column("source_domain")]
        public string SourceDomain { get; set; }
        [Column("source_username")]
        public string SourceUsername { get; set; }
        [Column("source_password")]
        public string SourcePassword { get; set; }

        [Column("destination_path")]
        public string DestinationPath { get; set; }
        [Column("destination_domain")]
        public string DestinationDomain { get; set; }
        [Column("destination_username")]
        public string DestinationUsername { get; set; }
        [Column("destination_password")]
        public string DestinationPassword { get; set; }
        public DateTime Schedule { get; set; }

        [Column("last_run")]
        public DateTime? LastRun { get; set; }

        public ICollection<BackupLog> BackupLogs { get; set; }

    }
}
