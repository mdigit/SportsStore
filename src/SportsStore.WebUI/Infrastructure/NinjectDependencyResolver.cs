using Ninject;
using SportsStore.Domain.Abstract;
using SportsStore.Domain.Concrete;
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using SportsStore.Domain.Entities;
using System.Configuration;

namespace SportsStore.WebUI.Infrastructure
{
    public class NinjectDependencyResolver : IDependencyResolver
    {
        private IKernel Kernel { get; set; }

        public NinjectDependencyResolver(IKernel kernel)
        {
            Kernel = kernel;
            AddBindings();
        }

        public object GetService(Type serviceType)
        {
            return Kernel.TryGet(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return Kernel.GetAll(serviceType);
        }

        private void AddBindings()
        {
            Kernel.Bind<IProductRepository>().To<EFProductRepository>();

            var emailSettings = new EmailSettings
            {
                WriteAsFile = bool.Parse(ConfigurationManager.AppSettings["Email.WriteAsFile"] ?? "false")
            };
            Kernel.Bind<IOrderProcessor>().To<EmailOrderProcessor>();
        }
    }
}