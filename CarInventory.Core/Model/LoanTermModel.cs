
namespace CarInventory.Core.Model
{
    public class LoanTermModel
    {
        public bool QualifiesForLoan { get; set; }
        public int NumberOfMonths { get; set; }
        public int CreditScore { get; set; }
        public double InterestRate { get; set; }
        public double TotalOwedOverTime { get; set; }
        public double MonthlyPayment { get; set; }  

    }
}
