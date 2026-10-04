using DomanLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomanLayer.Contracts
{
    public interface IMachineReportRepository
    {
        Task AddAsync(MachineReport report);
    }
}
