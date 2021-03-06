﻿using System.Data.Entity;
using BLL.Interface.Services;
using BLL.Interfacies.Services;
using BLL.Services;
using DAL.Concrete;
using DAL.Interface.Repository;
using Ninject;
using Ninject.Web.Common;
using ORM;

namespace DependencyResolver
{
    public static class ResolverConfig
    {
        public static void ConfigurateResolverWeb(this IKernel kernel)
        {
            Configure(kernel, true);
        }

        public static void ConfigurateResolverConsole(this IKernel kernel)
        {
            Configure(kernel, false);
        }

        private static void Configure(IKernel kernel, bool isWeb)
        {
            if (isWeb)
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InRequestScope();
                kernel.Bind<DbContext>().To<EntityModel>().InRequestScope();
            }
            else
            {
                kernel.Bind<IUnitOfWork>().To<UnitOfWork>().InSingletonScope();
                kernel.Bind<DbContext>().To<EntityModel>().InSingletonScope();
            }

            kernel.Bind<IUserRepository>().To<UserRepository>();
            kernel.Bind<IRoleRepository>().To<RoleRepository>();
            kernel.Bind<IProfileRepository>().To<ProfileRepository>();
            kernel.Bind<IAreaRepository>().To<AreaRepository>();
            kernel.Bind<IUserService>().To<UserService>();
            kernel.Bind<IRoleService>().To<RoleService>();
            kernel.Bind<IProfileService>().To<ProfileService>();
            kernel.Bind<IEmailService>().To<EmailService>();
        }
    }
}