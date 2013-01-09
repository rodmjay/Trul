using System;
using AutoMapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trul.Application.DTO;
using Trul.Domain.Entities;
using Trul.Infrastructure.Crosscutting.Adapter;
using Trul.Infrastructure.Crosscutting.NetFramework.Adapter;

namespace Trul.Infrastructure.Crosscutting.NetFrm.Test
{
    [TestClass]
    public class AutomapperTypeAdapterTests
    {
        public AutomapperTypeAdapterTests() {
            TypeAdapterFactory.SetCurrent(new AutomapperTypeAdapterFactory());
        }


        [TestMethod]
        public void MapperTest() {
            
            ITypeAdapter adap = TypeAdapterFactory.CreateAdapter();

            var map = Mapper.CreateMap<Menu, MenuDTO>();
            
            var menu = new Menu { ID = 1, ApplicationCode = "code", LinkName = "link" };
            var menuDTO = adap.Adapt<Menu, MenuDTO>(menu);
            menu.Equals(menuDTO);

            Assert.IsTrue(menu.ID == menuDTO.ID);
        }
    }
}
