using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Domain.Entities
{
   public class User
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "phone")]
        public long Phone { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }
    }
}
