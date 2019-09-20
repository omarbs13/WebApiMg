using System;
using System.Collections.Generic;
namespace MdbApi.Domain.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }

        public string Password { get; set; }

        public string RoleName { get; set; }

        public Person Person { get; set; }
    }

    public class Student
    {
        public string NoControl { get; set; }
        public List<Tutoress> Tutoress { get; set; }

    }

    public class Tutoress : Person
    {
        public string RelationShip { get; set; }
    }

    public class Address
    {
        public string Street { get; set; }
        public string ApartmentNumber { get; set; }
        public string Colony { get; set; }
        public string State { get; set; }
        public string Municipality { get; set; }
        public int ZipCode { get; set; }
        public int PhoneNumber { get; set; }
        public string Email { get; set; }
    }

    public class Person : BaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string MaidenName { get; set; }
        public string Curp { get; set; }
        public DateTime DateBirth { get; set; }
        public string Sex { get; set; }
        public string Nacionality { get; set; }
        public string Photo { get; set; }
        public Address Address { get; set; }
        public bool Status { get; set; }
        public DateTime CreateDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public Student Student { get; set; }
    }
}