using Helpers;
using Implementations;
using Interfaces;
using IocProject;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests
{
    [TestFixture]
    public class ContainerTester
    {
        [Test]
        [ExpectedException("System.Exception")]
        public void TestResolveNotRegistered()
        {
            Container container = new Container();
            
            IRepository repositoryA = container.Resolve<IRepository>();
        }

        [Test]
        public void TestRegister()
        {
            Container container = new Container();
            container.Register<IRepository, RepositoryA>();
            IRepository repositoryA = container.Resolve<IRepository>();
            Assert.IsNotNull(repositoryA);
        }

        [Test]
        public void TestCreateSinglton()
        {
            Container container = new Container();
            container.Register<IRepository, RepositoryA>(LifeCycle.Singleton);
            IRepository repositoryA = container.Resolve<IRepository>();

            Assert.IsNotNull(repositoryA);
            IRepository repositoryB = container.Resolve<IRepository>();

            Assert.AreSame(repositoryA, repositoryB);
        }

        [Test]
        public void TestCreateTransient()
        {
            Container container = new Container();
            container.Register<IRepository, RepositoryA>();
            IRepository repositoryA = container.Resolve<IRepository>();

            Assert.IsNotNull(repositoryA);
            IRepository repositoryB = container.Resolve<IRepository>();

            Assert.AreNotSame(repositoryA, repositoryB);
        }


        [Test]
        [ExpectedException("System.Exception")]
        public void TestCreatWithConstructorInjectionNotAllRegistered()
        {
            Container container = new Container();
            container.Register<IRepository, RepositoryA>();
            container.Register<ICustomController, CustomController>();
            ICustomController controller = container.Resolve<ICustomController>();
            Assert.IsNotNull(controller);
        }
        [Test]
        public void TestCreatWithConstructorInjectionAllRegistered()
        {
            Container container = new Container();
            container.Register<IRepository, RepositoryA>();
            container.Register<IEmailService, EmailService>();
            container.Register<ICustomController, CustomController>();
            ICustomController controller = container.Resolve<ICustomController>();
            Assert.IsNotNull(controller);
        }

    }
}
