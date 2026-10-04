using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomanLayer.Models
{
    public class Software
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Version { get; set; }

        public int MachineReportId { get; set; }
    }
}
