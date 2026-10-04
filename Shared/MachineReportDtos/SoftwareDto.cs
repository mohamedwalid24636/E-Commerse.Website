using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.MachineReportDtos
{
    public class SoftwareDto
    {
        public string Name { get; set; } = string.Empty;
        public string? Version { get; set; }
        public string Status { get; set; } = string.Empty; // "Added" أو "Removed"
    }
}
