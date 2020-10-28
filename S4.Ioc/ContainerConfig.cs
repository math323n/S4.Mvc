using Autofac;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using S4.DataAccess;
using S4.DataAccess.Base;
using S4.Entities.Models;
using S4.Entities.Models.Context;
using S4.Ioc.Containers;

namespace S4.Ioc
{
    public static class ContainerConfig
    {
        public static void Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ProductRepository>().As<IProductRepository>();
            builder.RegisterType<RepositoryBase<Category>>().As<IRepositoryBase<Category>>();
            builder.RegisterType<NorthwindContext>().As<DbContext>();
            builder.RegisterType<UserStore>();
            builder.RegisterType<SupplierRepository>().As<ISupplierRepository>();

            IContainer container = builder.Build();
            AutofacContainer.Initialize(container);
        }
    }
}
