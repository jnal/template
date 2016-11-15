using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crossover.LBS.API.Contracts.DTO
{
    public class MachineDto
    {
        public int Id { get; set; }
        public string IPAddress { get; set; }
        public bool IsActive { get; set; }
    }
}
