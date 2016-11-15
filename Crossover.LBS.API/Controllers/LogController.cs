using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crossover.LBS.API.Contracts.DTO;
using Crossover.LBS.API.Contracts.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Crossover.LBS.API.Controllers
{
    [Route("api/[controller]")]
    public class LogController : Controller
    {
        private readonly ILogManager _logManager;
        public LogController(ILogManager logManager)
        {
            _logManager = logManager;
        }

        [HttpGet("machinebackup/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return Ok(await _logManager.GetBackupLogs(id));
        }


        [HttpPost]
        public async Task<IActionResult> Create([FromBody]BackupLogDto backupLog)
        {
            var createdLog = await _logManager.Log(backupLog);
            return CreatedAtRoute(new { createdLog.Id }, createdLog);
        }
    }
}
