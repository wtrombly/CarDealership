using CarInventory.Core;
using CarInventory.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CarInventory.Web 
{
    public class CarInventoryService : IGetInventory
    {
        private readonly ICarInventoryRepository _repository;
        // we need to provide to the constructor below an interface (ICarInventoryRepository) so that we can swap it out later for another data source object.
        // we pass the interface repository to the CarinventoryService so that we can inject this interface, which allows us to mock test this later for unit testing.
        public CarInventoryService(ICarInventoryRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<CarInventoryModel> GetAvailableInventory()
        {
            var inventory = _repository.GetInventory();

            return inventory.Where(x => x.AmountInventory > 0)
                .Select(item => new CarInventoryModel
                {
                    AmountInventory = item.AmountInventory,
                    Make = item.Make,
                    Model = item.Model,
                    MSRP = item.MSRP, 
                    YearModel = item.YearModel,
                });
        }
    }
}