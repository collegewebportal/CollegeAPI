﻿using System.Runtime.Serialization;

namespace Domain
{

    public class Student
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "firstName")]
        public string FirstName { get; set; }

        [DataMember(Name = "lastName")]
        public string LastName { get; set; }

        [DataMember(Name = "phone")]
        public long Phone { get; set; }

        [DataMember(Name = "gender")]
        public string Gender { get; set; }

        [DataMember(Name = "email")]
        public string Email { get; set; }

        [DataMember(Name = "dob")]
        public string DateOfBirth { get; set; }

        [DataMember(Name = "address")]
        public Address AddressInfo { get; set; }

        [DataMember(Name = "qualification")]
        public Qualification AcademicDetails { get; set; }

    }

    public class Qualification
    {

        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "academic")]
        public string Academic { get; set; }

        [DataMember(Name = "year")]
        public string Year { get; set; }

        [DataMember(Name = "school")]
        public string School { get; set; }

        [DataMember(Name = "marksObtained")]
        public string MarksObtained { get; set; }
    }

    public class Address
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }

        [DataMember(Name = "addressline1")]
        public string Addressline1 { get; set; }

        [DataMember(Name = "addressline1")]
        public string Addressline2 { get; set; }

        [DataMember(Name = "town")]
        public string Town { get; set; }

        [DataMember(Name = "state")]
        public string State { get; set; }

        [DataMember(Name = "pincode")]
        public string PinCode { get; set; }
    }
}


