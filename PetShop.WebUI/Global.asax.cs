using System.Web.Mvc;
using System.Web.Routing;
using AutoMapper;
using Ninject;
using Ninject.Modules;
using PetShop.BLL.DTO;
using PetShop.BLL.Infrastructure;
using PetShop.WebUI.Infrastructure;
using PetShop.WebUI.Models;
using NinjectDependencyResolver = Ninject.Web.Mvc.NinjectDependencyResolver;
using PetShop.Domain.Entities;

namespace PetShop.WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<ProductViewModel, ProductDTO>();
                cfg.CreateMap<ProductDTO, ProductViewModel>();
                cfg.CreateMap<OrderDTO, OrderViewModel>();
                cfg.CreateMap<OrderViewModel, OrderDTO>();
                cfg.CreateMap<Product, ProductDTO>();
                cfg.CreateMap<ProductDTO, Product>();
                cfg.CreateMap<OrderDTO, Order>();
                cfg.CreateMap<Order, OrderDTO>();
                cfg.CreateMap<ProductViewModel, Product>();
                cfg.CreateMap<ProductViewModel, ProductDTO>();
            });
            NinjectModule orderModule = new OrderModule();
            NinjectModule serviceModule = new ServiceModule("DefaultConnection");
            var kernel = new StandardKernel(orderModule,serviceModule);
            DependencyResolver.SetResolver(new NinjectDependencyResolver(kernel));
        }
    }
}
