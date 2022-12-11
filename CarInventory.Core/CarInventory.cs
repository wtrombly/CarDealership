using System;
using System.Collections.Generic;
using System.Text;

namespace CarInventory.Core
{
    public class CarInventoryModel
    {
        public  int YearModel { get; set; }
        public string Make { get; set; }   
        public string Model { get; set; }  
        public int AmountInventory { get; set; }   
        public decimal MSRP { get; set; }
        
    }
}
