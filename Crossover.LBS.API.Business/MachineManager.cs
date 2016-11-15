using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crossover.LBS.API.Business.Domain;
using Crossover.LBS.API.Contracts.DTO;
using Crossover.LBS.API.Contracts.Interfaces;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Z.EntityFramework.Plus;

namespace Crossover.LBS.API.Business
{
    public class MachineManager : IMachineManager
    {
        private readonly LbsContext _lbsContext;

        public MachineManager(LbsContext lbsContext)
        {
            _lbsContext = lbsContext;
        }

        public async Task<MachineDto> Add(MachineDto machineDto)
        {
            var machineEntity = Mapper.Map<MachineDto, Machine>(machineDto);
            _lbsContext.Machines.Add(machineEntity);
            await _lbsContext.SaveChangesAsync();

            return Mapper.Map<Machine, MachineDto>(machineEntity);
        }

        public async Task<List<MachineBackupDto>> GetMachinesAndBackups()
        {
            var machines = await _lbsContext.Machines.Include(x => x.BackupConfigs).ThenInclude(c => c.BackupLogs).ToListAsync();

            var machineBackups = new List<MachineBackupDto>();
            foreach (var machine in machines)
            {
                foreach (var backupConfig in machine.BackupConfigs)
                {
                    if (backupConfig.BackupLogs.Count > 0)
                        backupConfig.LastRun = backupConfig.BackupLogs.Select(x => x.LogDate).Max();
                }


                machineBackups.Add(new MachineBackupDto
                {
                    Id = machine.Id,
                    IPAddress = machine.IPAddress,
                    IsActive = machine.IsActive,
                    BackupConfigs = Mapper.Map<IEnumerable<MachineBackup>, List<BackupDto>>(machine.BackupConfigs),
                });
            }

            return machineBackups;


        }

        public async Task EnableDisable(int machineId, bool isActive)
        {
            var machineToUpdate = new Machine { Id = machineId };
            _lbsContext.Machines.Attach(machineToUpdate);
            machineToUpdate.IsActive = isActive;

            await _lbsContext.SaveChangesAsync();
        }



        public async Task Delete(int machineId)
        {
            await
                _lbsContext.BackupLogs.Where(
                    x =>
                        _lbsContext.MachineBackupConfigurations.Where(m => m.MachineId == machineId)
                            .Select(b => b.Id)
                            .Contains(x.MachineBackupId)).DeleteAsync();
            await _lbsContext.MachineBackupConfigurations.Where(x => x.MachineId == machineId).DeleteAsync();
            await _lbsContext.Machines.Where(x => x.Id == machineId).DeleteAsync();
            
        }


        

        public async Task<BackupDto> SaveBackupConfig(BackupDto backupDto)
        {


            if (backupDto.Id > 0)
            {
                var machineBackupToUpdate = new MachineBackup { Id = backupDto.Id };
                _lbsContext.MachineBackupConfigurations.Attach(machineBackupToUpdate);
                Mapper.Map(backupDto, machineBackupToUpdate);

                await _lbsContext.SaveChangesAsync();
                return backupDto;
            }
            else
            {
                var machineBackupEntity = Mapper.Map<BackupDto, MachineBackup>(backupDto);
                _lbsContext.MachineBackupConfigurations.Add(machineBackupEntity);
                await _lbsContext.SaveChangesAsync();

                return Mapper.Map<MachineBackup, BackupDto>(machineBackupEntity);
            }
        }

        public async Task<BackupDto> GetBackupConfig(int machineBackupId)
        {
            return
                Mapper.Map<MachineBackup, BackupDto>(
                    await _lbsContext.MachineBackupConfigurations.SingleAsync(x => x.Id == machineBackupId));
        }

        public async Task DeleteBackupConfig(int machineBackupId)
        {
            await _lbsContext.MachineBackupConfigurations.Where(m => m.Id == machineBackupId).DeleteAsync();
         
        }


        public async Task<List<BackupDto>> GetBackupConfigs(string ipAddress)
        {
            var machine = await _lbsContext.Machines.SingleOrDefaultAsync(m => m.IPAddress == ipAddress && m.IsActive);

            if (machine != null)
            {
                return Mapper.Map<List<MachineBackup>, List<BackupDto>>(await _lbsContext.MachineBackupConfigurations.Where(x => machine.Id == x.MachineId).ToListAsync());
            }

            throw new ArgumentException($"cannot find {ipAddress}");
        }

    }
}
