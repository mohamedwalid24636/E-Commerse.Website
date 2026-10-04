using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomanLayer.Exceptions
{
    public abstract class NotFoundException(string Message) : Exception(Message)
    {

    }
}


// I Need Service layer  تشوف ال Exception عشان كده حطيتها هنا 
// كان ممكن احط ال Exceptions In Service Layer علي طول 