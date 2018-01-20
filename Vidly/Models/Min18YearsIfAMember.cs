using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Vidly.DTOs;
using Vidly.Models;

namespace Vidly.Models
{
    public class Min18YearsIfAMember : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //var customer = (Customer)validationContext.ObjectInstance; // we need to cast the customer object

            /*
            if (customer.MembershipTypeId == MembershipType.Unknown ||
                customer.MembershipTypeId == MembershipType.PayAsYouGo)
                return ValidationResult.Success;

            if (customer.BirthDate == null)
                return new ValidationResult("BirthDate is Required.");

            var age = DateTime.Today.Year - customer.BirthDate.Value.Year;
            */

            return (int.Parse(value.ToString()) >= 18) 
                ? ValidationResult.Success 
                : new ValidationResult("Customer should be at least 18 years old."); 
        }

        public ValidationResult Valid(object value, ValidationContext validationContext)
        {
            return IsValid(value, validationContext);
        }
    }
}