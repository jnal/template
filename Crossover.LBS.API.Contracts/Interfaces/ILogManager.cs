using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crossover.LBS.API.Contracts.DTO;

namespace Crossover.LBS.API.Contracts.Interfaces
{
    public interface ILogManager
    {
        Task<List<BackupLogDto>> GetBackupLogs(int machineBackupId);
        Task<BackupLogDto> Log(BackupLogDto backupLogDto);
    }
}
