using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomanLayer.Models
{
    public class Error
    {
        public int Id { get; set; }
        public ErrorType Type { get; set; }
        public string Source { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime Time { get; set; }

        public int MachineReportId { get; set; }
    }
}
