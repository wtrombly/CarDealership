using CarInventory.Core.Interfaces;
using CarInventory.Core.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarInventory.Services
{
    public class EstimateAutoLoans : IEstimateAutoLoans
    {

        private readonly IValidateCandidates _validateCandidatesService;
        private readonly IValidateCreditScore _validateCreditScore;

        //inject dependency into the service using a constructor
        public EstimateAutoLoans(IValidateCreditScore _validateCreditScore, IValidateCandidates validateCandidatesService)
        {
            this._validateCreditScore = _validateCreditScore;
            this._validateCandidatesService = validateCandidatesService;
        }

        public LoanTermModel GetLoanTerms(
            string candidateName,
            int creditScore,
            int downPayment,
            int term,
            double totalLoanAmount)
        {
            
            var isCreditScoreValid = _validateCreditScore.IsValidCreditScore(creditScore);

            if (!isCreditScoreValid)
            {
                return new LoanTermModel
                {
                    QualifiesForLoan = false
                };
            }

            var isCandidateNameValid = _validateCandidatesService.IsValidCandidate(candidateName);

            if (!isCandidateNameValid)
            {
                return new LoanTermModel
                {
                    QualifiesForLoan = false
                };
            }

            var rate = GetAPR(creditScore);
            
            var monthlyPayment = GetMonthlyPayment(rate, term, totalLoanAmount - downPayment);

            return new LoanTermModel
            {
                QualifiesForLoan = true,
                CreditScore = creditScore,
                NumberOfMonths = term,
                InterestRate = rate,
                MonthlyPayment = monthlyPayment,
                TotalOwedOverTime = GetTotalAmountOwed(monthlyPayment, term)
            };
        }
        // TODO: remove hardcoded threshholds, set them as variables that the user can edit later in a data table/data base.
        public double GetAPR(int creditScore)
        {
            if (creditScore < 650)
            {
                return 10;
            } else if (creditScore < 700)
            {
                return 5;
            }
            else
            {
                return 1;
            }
        }
        // TODO: Please figure out this Math
        public double GetMonthlyPayment(double rate, int term,double principal)
        {
            return Math.Round(principal * Math.Pow((1 + (rate / term)), (term / 12)) / term, 3);
        }

        public double GetTotalAmountOwed(double monthlyPayment, int term)
        {
            return monthlyPayment * term;   
        }
    }
}
