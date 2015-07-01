using Helpers;
using Implementations;
using Interfaces;
using IocProject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCApp1.Controllers
{
    public class IOCControllerFactory: DefaultControllerFactory
    {
        protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
        {
            IocProject.Container iocContainer = new Container();
            iocContainer.Register<IEmailService, Implementations.EmailService>(LifeCycle.Singleton);
            iocContainer.Register<IRepository, RepositoryA>(LifeCycle.Singleton);
            iocContainer.Register<BaseController, BaseController>(LifeCycle.Singleton);

            IController controller = (IController)iocContainer.Resolve<BaseController>();
            if (controller != null)
            {
                return controller;
            }
            else
            {
                return base.GetControllerInstance(requestContext,controllerType);
            }
        }
    }
}