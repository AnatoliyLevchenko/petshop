using Ninject.Modules;
using PetShop.BLL.Interfaces;
using PetShop.BLL.Services;

namespace PetShop.WebUI.Infrastructure
{
    /// <summary>
    /// Injects dependency in this project.
    /// </summary>
    public class OrderModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IOrderService>().To<OrderService>();
            Bind<IUserService>().To<UserService>();
        }
    }
}