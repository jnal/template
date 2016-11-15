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
    public class MachineController : Controller
    {
        private readonly IMachineManager _machineManager;
        public MachineController(IMachineManager machineManager)
        {
            _machineManager = machineManager;
        }



        [HttpPost]
        public async Task<IActionResult> Create([FromBody]MachineDto machine)
        {
            var createdMachine = await _machineManager.Add(machine);
            return CreatedAtRoute(new { createdMachine.Id }, createdMachine);
        }

        [HttpGet]
        public async Task<IActionResult> GetMachinesAndBackups()
        {
            return Ok(await _machineManager.GetMachinesAndBackups());
        }


        [HttpPut("enabledisable/{id}/{isactive}")]
        public async Task<IActionResult> EnableDisable(int id, bool isActive)
        {
            await _machineManager.EnableDisable(id, isActive);
            return new NoContentResult();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _machineManager.Delete(id);
            return new NoContentResult();

        }

        [HttpGet("backup/ip/{ipaddress}")]
        public async Task<IActionResult> GetBackups(string ipaddress)
        {
            return Ok(await _machineManager.GetBackupConfigs(ipaddress));

        }


        [HttpGet("backup/{machineBackupId}")]
        public async Task<IActionResult> GetBackup(int machineBackupId)
        {
            return Ok(await _machineManager.GetBackupConfig(machineBackupId));
           
        }


        [HttpDelete("backup/{machineBackupId}")]
        public async Task<IActionResult> DeleteBackup(int machineBackupId)
        {
            await _machineManager.DeleteBackupConfig(machineBackupId);
            return new NoContentResult();

        }

        [HttpPut("backup")]
        public async Task<IActionResult> UpdateMachineBackup([FromBody] BackupDto backup)
        {
            await _machineManager.SaveBackupConfig(backup);
            return new NoContentResult();
        }

        [HttpPost("backup")]
        public async Task<IActionResult> CreateMachineBackup([FromBody]BackupDto backup)
        {
            var backupConfig = await _machineManager.SaveBackupConfig(backup);
            return CreatedAtRoute(new { backupConfig.Id }, backupConfig);
        }

    }
}
