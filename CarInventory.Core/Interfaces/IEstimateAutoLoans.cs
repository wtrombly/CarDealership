using CarInventory.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarInventory.Core.Interfaces
{
    public interface IEstimateAutoLoans
    {
        public LoanTermModel GetLoanTerms(
            string candidateName,
            int creditScore,
            int downPayment,
            int term,
            double totalLoanAmount);

        double GetAPR(int creditScore);

        double GetMonthlyPayment(double interestRate, int term, double totalLoanAmount);
        
    }
}
