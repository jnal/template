using Microsoft.EntityFrameworkCore;

namespace Crossover.LBS.API.Business.Domain
{
    public class LbsContext : DbContext
    {
        public LbsContext(DbContextOptions<LbsContext> options)
        : base(options)
        { }

        public DbSet<Credential> Credentials { get; set; }
        public DbSet<Machine> Machines { get; set; }
        public DbSet<MachineBackup> MachineBackupConfigurations { get; set; }
        public DbSet<BackupLog> BackupLogs { get; set; }


    }
}
