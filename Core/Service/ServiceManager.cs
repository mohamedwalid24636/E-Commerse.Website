using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using DomanLayer.Contracts;
using DomanLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using ServiceAbstraction;

namespace Service
{
    public class ServiceManager(IUnitOfWork _UnitOfWork, IMapper _mapper, IBasketRepository _basketRepository, UserManager<ApplicationUser> _userManager, IConfiguration _configuration) /*: IServiceManager*/
    {

        public readonly Lazy<IOrderService> _LazyOrderService = new Lazy<IOrderService>(() => new OrderService(_mapper, _basketRepository, _UnitOfWork , _configuration));   // انا مبعتش ال Object انا بعتت طريقة انشاءه *** lamda Expression

        private readonly Lazy<IProductService> _LazyProductService = new Lazy<IProductService>(() => new ProductService(_UnitOfWork, _mapper));

        private readonly Lazy<IBasketService> _LazyBasketService = new Lazy<IBasketService>(() => new BasketService(_basketRepository, _mapper));

        private readonly Lazy<IAuthenticationService> _LazyAuthenticationService = new Lazy<IAuthenticationService>(() => new AuthenticationService(_userManager, _configuration, _mapper));

        // Need Review Again

        public IBasketService BasketService => _LazyBasketService.Value;
        public IProductService ProductService => _LazyProductService.Value;
        public IAuthenticationService AuthenticationService => _LazyAuthenticationService.Value;
        public IOrderService OrderService => _LazyOrderService.Value;

    }

}


//Here We Impelemented Service Manager By Lazy Loading 