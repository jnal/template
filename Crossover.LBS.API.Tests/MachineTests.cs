using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Crossover.LBS.API.Business;
using Crossover.LBS.API.Business.Domain;
using Crossover.LBS.API.Contracts.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AutoMapper;
using Crossover.LBS.API.Business.MapperProfiles;

namespace Crossover.LBS.API.Tests
{
    [TestClass]
    public class MachineTests
    {
        private IMachineManager _sut;

        [TestInitialize]
        public void Setup()
        {
            Mapper.Initialize(cfg => {
                cfg.AddProfile<LbsProfile>();
            });
        }

        private static DbContextOptions<LbsContext> CreateNewContextOptions()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkInMemoryDatabase()
                .BuildServiceProvider();

            var builder = new DbContextOptionsBuilder<LbsContext>();
            builder.UseInMemoryDatabase()
                   .UseInternalServiceProvider(serviceProvider);

            return builder.Options;
        }

        [TestMethod]
        public void Should_Throw_ArgumentException_When_IPAddress_Is_Not_Found()
        {
            var options = CreateNewContextOptions();
            using (var context = new LbsContext(options))
            {
                context.Machines.Add(new Machine {IPAddress = "11.11.11.11"});
                context.Machines.Add(new Machine { IPAddress = "22.22.22.22" });


                context.SaveChanges();
            }


            _sut = new MachineManager(new LbsContext(options));

            ThrowsAsync<ArgumentException>(() => _sut.GetBackupConfigs("123.123.123.123"));

            
        }

        [TestMethod]
        public void Should_Not_Throw_ArgumentException_When_IPAddress_Is_Found()
        {
            var options = CreateNewContextOptions();
            using (var context = new LbsContext(options))
            {
                context.Machines.Add(new Machine { IPAddress = "11.11.11.11" });
                context.Machines.Add(new Machine { IPAddress = "22.22.22.22" });

                context.SaveChanges();
            }


            _sut = new MachineManager(new LbsContext(options));

             Assert.IsTrue(_sut.GetBackupConfigs("11.11.11.11").Id > 0);


        }


        [TestMethod]
        public void Should_Not_Get_Inactive()
        {
            var options = CreateNewContextOptions();
            using (var context = new LbsContext(options))
            {
                context.Machines.Add(new Machine { IPAddress = "11.11.11.11" , IsActive = false });
   
                context.SaveChanges();
            }


            _sut = new MachineManager(new LbsContext(options));

            ThrowsAsync<ArgumentException>(() => _sut.GetBackupConfigs("11.11.11.11"));


        }

        //copied from https://msdn.microsoft.com/en-us/magazine/dn818493.aspx
        private static async Task ThrowsAsync<TException>(Func<Task> action, bool allowDerivedTypes = true)
        {
            try
            {
                await action();
                Assert.Fail("Delegate did not throw expected exception " + typeof(TException).Name + ".");
            }
            catch (Exception ex)
            {
                if (allowDerivedTypes && !(ex is TException))
                    Assert.Fail("Delegate threw exception of type " + ex.GetType().Name + ", but " + typeof(TException).Name + " or a derived type was expected.");
                if (!allowDerivedTypes && ex.GetType() != typeof(TException))
                    Assert.Fail("Delegate threw exception of type " + ex.GetType().Name + ", but " + typeof(TException).Name + " was expected.");
            }
        }
    }
}
