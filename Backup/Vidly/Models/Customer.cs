using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Vidly.Models
{
    public class Customer
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Customer's name")] // customize validation error message
        [StringLength(255)]
        [Display(Name ="First Name")]
        public string Name { get; set; }

        [Display(Name ="Last Name")]
        public string LastName { get; set; }  
        
        [Display(Name ="Day of Birth")]
        [Min18YearsIfAMember] //customize our own validation
        public DateTime? BirthDate { get; set; }

        public bool IsSuscribedToNewsLetter { get; set; }

        public MembershipType MembershipType { get; set; }

        [Display(Name = "Membership Type")]
        public byte MembershipTypeId { get; set; }
    }
}