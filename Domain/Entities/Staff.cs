using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.Entities
{
    public class Staff
    {
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

        public string Password { get; set; }

        
        private DateTime _joiningDate;

        public DateTime JoiningDate
        {
            get { return _joiningDate = DateTime.Now; }
            set { _joiningDate = value; }
        }


    }
}
