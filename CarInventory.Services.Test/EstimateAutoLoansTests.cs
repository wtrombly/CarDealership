using CarInventory.Core.Interfaces;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;

namespace CarInventory.Services.Test
{
    [TestClass]
    public class EstimateAutoLoansTests
    {
        EstimateAutoLoans testSubject;

        Mock<IValidateCandidates> validateCandidatesMock;
        Mock<IValidateCreditScore> validateCreditScoreMock;

        [TestInitialize]
        public void Setup()
        {
            validateCandidatesMock = new Mock<IValidateCandidates>();  
            validateCreditScoreMock = new Mock<IValidateCreditScore>();
            testSubject = new EstimateAutoLoans(validateCreditScoreMock.Object, validateCandidatesMock.Object);
            
        }
        // candidate's name (validate)
        // credit score  (Validate)
        // down payment  (validate, used in calculation)
        // length of term (validate, used in calculation)
        // total loan amount (used in calculation, and validate)
        [TestMethod]
        public void GetLoanTerms_CandidateIsValid_ShouldQualifyForLoan()
        {
            validateCandidatesMock.Setup(x => x.IsValidCandidate(It.IsAny<string>())).Returns(true);
            validateCreditScoreMock.Setup(x => x.IsValidCreditScore(It.IsAny<int>())).Returns(true);

            var result = testSubject.GetLoanTerms("Stuart Millner", 760, 5000, 48, 50000);
            Assert.IsTrue(result.QualifiesForLoan);   
        }

        [TestMethod]
        public void GetLoanTerms_ShouldReturnLoanTerms()
        {
            validateCandidatesMock.Setup(x => x.IsValidCandidate(It.IsAny<string>())).Returns(true);
            validateCreditScoreMock.Setup(x => x.IsValidCreditScore(It.IsAny<int>())).Returns(true);

            var terms = testSubject.GetLoanTerms("Stu Millner", 650, 5000, 72, 40000);

            Assert.IsTrue(terms.QualifiesForLoan);
            Assert.AreEqual(5, terms.InterestRate);
            Assert.AreEqual(727.252, terms.MonthlyPayment);
            Assert.AreEqual(72, terms.NumberOfMonths);
            Assert.AreEqual(52362.144, terms.TotalOwedOverTime);
            Assert.AreEqual(650,terms.CreditScore);
        }

        [TestMethod]
        public void GetLoanTerms_CandidateNotValid_ShouldQualifyForLoan()
        {
            validateCandidatesMock.Setup(x => x.IsValidCandidate(It.IsAny<string>())).Returns(false);
            validateCreditScoreMock.Setup(x => x.IsValidCreditScore(It.IsAny<int>())).Returns(true);

            var result = testSubject.GetLoanTerms("Stuart Millner", 760, 5000, 48, 50000);
            Assert.IsFalse(result.QualifiesForLoan);
        }

        [TestMethod]
        public void GetLoanTerms_CreditScoreIsValid_ShouldQualifyForLoan()
        {
            validateCandidatesMock.Setup(x => x.IsValidCandidate(It.IsAny<string>())).Returns(true);
            validateCreditScoreMock.Setup(x => x.IsValidCreditScore(It.IsAny<int>())).Returns(true);

            var result = testSubject.GetLoanTerms("Stuart Millner", 760, 5000, 48, 50000);
            Assert.IsTrue(result.QualifiesForLoan);
        }

        [TestMethod]
        public void GetLoanTerms_CreditScoreIsInValid_ShouldNotQualifyForLoan()
        {
            validateCandidatesMock.Setup(x => x.IsValidCandidate(It.IsAny<string>())).Returns(false);
            validateCreditScoreMock.Setup(x => x.IsValidCreditScore(It.IsAny<int>())).Returns(false);

            var result = testSubject.GetLoanTerms("Stuart Millner", 760, 5000, 48, 50000);
            Assert.IsFalse(result.QualifiesForLoan);
        }


        //APR Test Section
        [TestMethod]
        public void GetAPR_CreditScoreLessThan650_ShouldReturn10()
        {
            var result = testSubject.GetAPR(649);

            Assert.AreEqual(10, result);
        }

        [TestMethod]
        public void GetAPR_CreditScoreLessThan700_ShouldReturn5()
        {
            var result = testSubject.GetAPR(699);

            Assert.AreEqual(5, result);
        }

        [TestMethod]
        public void GetAPR_CreditScoreGreaterThanOrEqualTo700_ShouldReturn5()
        {
            var result = testSubject.GetAPR(700);

            Assert.AreEqual(1, result);
        }


        //Monthly Payment Test Section
        [TestMethod]
        public void GetMonthlyPayment_ShouldReturnMonthlyPayment()
        {
            var result = testSubject.GetMonthlyPayment(.05, 72, 40000);

            Assert.AreEqual(557.874, result);
        }

        [TestMethod]
        public void GetTotalAmountOwed_ShouldReturnTotalAmountOwed()
        {
            var result = testSubject.GetTotalAmountOwed(749.50, 72);

            Assert.AreEqual(53964, result);
        }

        

        


    }
}
