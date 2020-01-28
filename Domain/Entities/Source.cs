using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.Entities
{
    public class Source
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [Required]
        [DataMember(Name = "Name")]
        public string Name { get; set; }

        [Required]
        [DataMember(Name = "phone")]
        public long Phone { get; set; }

        [Required]
        [DataMember(Name = "email")]
        public string Email { get; set; }

        [Required]
        [DataMember(Name = "address")]
        public string Address { get; set; }

    }
}
