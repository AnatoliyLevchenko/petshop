using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Ninject;
using Ninject.Modules;
using PetShop.Domain.Interfaces;
using PetShop.Domain.Repositories;

namespace PetShop.BLL.Infrastructure
{
    /// <summary>
    /// Injects dependency in this project.
    /// </summary>
    public class ServiceModule : NinjectModule
    {
        private readonly string connectionString;
        public ServiceModule(string connection)
        {
            connectionString = connection;
        }
        public override void Load()
        {
            Bind<IUnitOfWork>().To<EfUnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}
