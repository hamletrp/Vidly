using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vidly.Models;

namespace Vidly_Tests
{
    [TestClass]
    public class Min18YearsIfAMember_Test
    {
        [TestMethod]
        public void IsValidIf18Minimun()
        {

            //Arrange
            Min18YearsIfAMember IsMember = new Min18YearsIfAMember();

            //Act
            var result = IsMember.Valid(18, null);

            //Assertion
            Assert.AreEqual(ValidationResult.Success, result);

        }

        [TestMethod]
        public void IsInValidIfLesserThan18()
        {
            //Arrange
            Min18YearsIfAMember IsMember = new Min18YearsIfAMember();

            //Act
            var result = IsMember.Valid(17, null);

            //Assert
            Assert.AreEqual("Customer should be at least 18 years old.", result.ErrorMessage);

            // Hamlet: Adding change to test autobuild trigger 2
        }
    }
}
