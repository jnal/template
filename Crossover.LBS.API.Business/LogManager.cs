using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Crossover.LBS.API.Business.Domain;
using Crossover.LBS.API.Contracts.DTO;
using Crossover.LBS.API.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Crossover.LBS.API.Business
{
    public class LogManager : ILogManager
    {
        private readonly LbsContext _lbsContext;

        public LogManager(LbsContext lbsContext)
        {
            _lbsContext = lbsContext;
        }

        public async Task<List<BackupLogDto>> GetBackupLogs(int machineBackupId)
        {
            var logs = await _lbsContext.BackupLogs.Where(x => x.MachineBackupId == machineBackupId).ToListAsync();
            return Mapper.Map<IEnumerable<BackupLog>, List<BackupLogDto>>(logs);

        }


        public async Task<BackupLogDto> Log(BackupLogDto backupLogDto)
        {
            var logEntity = Mapper.Map<BackupLogDto, BackupLog>(backupLogDto);
            _lbsContext.BackupLogs.Add(logEntity);

            await _lbsContext.SaveChangesAsync();

            return Mapper.Map<BackupLog, BackupLogDto>(logEntity);
        }
    }
}
