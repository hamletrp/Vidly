using System;
using System.ComponentModel.DataAnnotations;

namespace Vidly.DTOs
{
    public class CustomerDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter Customer's name")] // customize validation error message
        [StringLength(255)]
        public string Name { get; set; }

        public string LastName { get; set; }

       // [Min18YearsIfAMember] //customize our own validation
        public DateTime? BirthDate { get; set; }

        public bool IsSuscribedToNewsLetter { get; set; }
        
        public byte MembershipTypeId { get; set; }

        public MembershipTypeDto MembershipType { get; set; }
    }
}