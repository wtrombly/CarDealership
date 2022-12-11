using CarInventory.Core;
using CarInventory.Core.Interfaces;
using CarInventory.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarInventory.Services.Test
{
    [TestClass]
    public  class CarInventoryServiceTests
    {

        // Any business logic you have needs to have unit test, so 98 to 100 percent of business logic should have unit tests.

        private CarInventoryService Service;
        private Mock<ICarInventoryRepository> mockRepository;
        [TestInitialize]
        public void Setup()
        {
            mockRepository = new Mock<ICarInventoryRepository>();

            Service = new CarInventoryService(mockRepository.Object);
        }

        [TestMethod]
        public void GetInventory_ItemHasZeroInventory_ShouldReturnEmpty()
        {
            mockRepository.Setup(x => x.GetInventory())
                .Returns(new List<CarInventoryDto> {
                    new CarInventoryDto
                    {
                        AmountInventory = 0,
                        EngineSize = 5.7f,
                        Make = "Ford",
                        Model = "Fiesta",
                        MSRP = 50000m,
                        YearModel = 2022
                    }
                });

            var items = Service.GetAvailableInventory();

            Assert.AreEqual(0, items.Count());
        }

        [TestMethod]
        public void GetInventory_ItemHasMoreThanZeroInventory_ShouldReturnItems()
        {
            mockRepository.Setup(x => x.GetInventory())
                .Returns(new List<CarInventoryDto> {
                    new CarInventoryDto
                    {
                        AmountInventory = 1,
                        EngineSize = 5.7f,
                        Make = "Ford",
                        Model = "Fiesta",
                        MSRP = 50000m,
                        YearModel = 2022
                    }
                });

            var items = Service.GetAvailableInventory();

            Assert.AreEqual(1, items.Count());
        }

        /*[TestMethod]
        public void GetInventory_ItemHasMultipleInventory_ShouldReturnItems()
        {
            mockRepository.Setup(x => x.GetInventory())
                .Returns(new List<CarInventoryDto> {
                    new CarInventoryDto
                    {
                        AmountInventory = 1,
                        EngineSize = 5.7f,
                        Make = "Ford",
                        Model = "Fiesta",
                        MSRP = 50000m,
                        YearModel = 2022
                    },
                    new CarInventoryDto
                    {
                        AmountInventory = 2,
                        EngineSize = 5.7f,
                        Make = "Ford",
                        Model = "Mustang",
                        MSRP = 50000m,
                        YearModel = 2022
                    },
                    new CarInventoryDto
                    {
                        AmountInventory = 7,
                        EngineSize = 5.7f,
                        Make = "Ford",
                        Model = "F150",
                        MSRP = 50000m,
                        YearModel = 2022
                    },
                });

            var items = Service.GetAvailableInventory();

            Assert.AreEqual(3, items.Count());
        }*/
    }
}
