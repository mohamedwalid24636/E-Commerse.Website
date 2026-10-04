using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.MachineReportDtos
{
    public class MachineReportDto
    {

        public string MachineId { get; set; } = string.Empty;
        public string MachineName { get; set; } = string.Empty;


        public List<SoftwareDto> Softwares { get; set; } = new();
        public List<ErrorDto> Errors { get; set; } = new();
    }
}
