using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DomanLayer.Models
{
    public class MachineReport : BaseEntity<int>
    {

        public string MachineId { get; set; } = string.Empty;
        public string MachineName { get; set; } = string.Empty;

        public DateTime CreatedAt { get; set; }

        public List<Software> Softwares { get; set; } = new();
        public List<Error> Errors { get; set; } = new();
    }
}

