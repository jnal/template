using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crossover.LBS.API.Contracts.DTO;

namespace Crossover.LBS.API.Contracts.Interfaces
{
    public interface IMachineManager
    {
        Task<MachineDto> Add(MachineDto machineDto);
        Task<List<MachineBackupDto>> GetMachinesAndBackups();
        Task EnableDisable(int machineId, bool isActive);
        Task Delete(int machineId);

        Task<BackupDto> SaveBackupConfig(BackupDto backupDto);
        Task<BackupDto> GetBackupConfig(int machineBackupId);
        Task<List<BackupDto>> GetBackupConfigs(string ipAddress);
        Task DeleteBackupConfig(int machineBackupId);
    }
}
