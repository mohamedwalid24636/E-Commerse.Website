using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Shared.DataTransferObjects.BaketModulesDtos;

namespace ServiceAbstraction
{
    public interface IPaymentService
    {
        public Task<BasketDto> CreateOrUpdatePaymentInentAsync(string BasketId);
    }
}
