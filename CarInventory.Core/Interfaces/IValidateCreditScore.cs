using System;
using System.Collections.Generic;
using System.Text;

namespace CarInventory.Core.Interfaces
{
    public interface IValidateCreditScore
    {
        bool IsValidCreditScore(int creditScore);
    }
}
