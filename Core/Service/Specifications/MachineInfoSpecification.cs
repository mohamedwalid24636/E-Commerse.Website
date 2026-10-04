using DomanLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Specifications
{
    public class MachineInfoSpecification : BaseSpecification<MachineReport,int>
    {
        public MachineInfoSpecification(int id) : base(m => m.Id == id)
        {
            AddInclude(m => m.Softwares);
            AddInclude(m => m.Errors);
        }
        public MachineInfoSpecification() : base(null)
        {
            AddInclude(m => m.Softwares);
            AddInclude(m => m.Errors);
        }
    }
}
