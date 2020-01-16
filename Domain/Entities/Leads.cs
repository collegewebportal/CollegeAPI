using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.Entities
{
    public class Leads
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [Required]
        [DataMember(Name = "Name")]
        public string FirstName { get; set; }

        [Required]
        [DataMember(Name = "phone")]
        public long Phone { get; set; }

        [Required]
        [DataMember(Name = "email")]
        public string Email { get; set; }

        [Required]
        [DataMember(Name = "address")]
        public string Address { get; set; }

        [Required]
        [DataMember(Name = "isCompleted")]
        public string IsCompleted { get; set; }

        [Required]
        [DataMember(Name = "comments")]
        public string Comments { get; set; }
    }
}
