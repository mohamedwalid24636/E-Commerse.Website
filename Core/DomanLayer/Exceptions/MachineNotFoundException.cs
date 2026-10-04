using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomanLayer.Exceptions
{
    public sealed class MachineNotFoundException(int Id) : NotFoundException($"MachineInfo With Id = {Id} Is Not Found")
    {
    }
}
