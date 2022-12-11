using System;
using System.Collections.Generic;
using System.Text;

namespace CarInventory.Core
{
    public class CarInventoryDto
    {
        public int YearModel { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
        public int AmountInventory { get; set; }
        public decimal MSRP { get; set; }
        public float EngineSize { get; set; }   


    }
}
