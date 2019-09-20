using System;
using MdbApi.Domain.Entities;

namespace MdbApi.Application.Models
{
    public class PersonModel
    {
        public string Id { get; set; }
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

    public class PersonModelAdd
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