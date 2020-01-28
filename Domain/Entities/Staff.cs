using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.Entities
{
    public class Staff
    {
        [Key]
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "Name")]
        public string FirstName { get; set; }

        [DataMember(Name = "phone")]
        public long Phone { get; set; }

        
        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        
        private DateTime _joiningDate;

        public DateTime JoiningDate
        {
            get { return _joiningDate = DateTime.Now; }
            set { _joiningDate = value; }
        }


    }
}
