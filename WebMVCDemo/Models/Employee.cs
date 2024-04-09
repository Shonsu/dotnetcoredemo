
using System.ComponentModel.DataAnnotations;

namespace WebMVCDemo.Models
{
	public class Employee
	{
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateHired { get; set; }
        public string? Department { get; set; }
        // [Required]
        public string? Hash { get; set; }
        // [Required]
        public bool? isAdmin { get; set; }
        public int? SupervisorId { get; set; }
        public virtual Employee? Supervisor { get; set; }
        public virtual List<EmployeePhoneNumber> PhoneNumber { get; set; }
        public int AddressId { get; set; }
        public virtual Address Address { get; set; }

        public Employee()
        {
        }

        public Employee(int id, string? firstName, string? lastName, DateTime dateHired, string? department, int? supervisorId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateHired = dateHired;
            Department = department;
            SupervisorId = supervisorId;
        }

        public Employee(string? firstName, string? lastName, DateTime dateHired, string? department)
        {
            FirstName = firstName;
            LastName = lastName;
            DateHired = dateHired;
            Department = department;
        }

        public Employee(int id, string? firstName, string? lastName, DateTime dateHired, string? department)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateHired = dateHired;
            Department = department;
        }

        public override string ToString()
        {
            return $"[{Id}, {FirstName} {LastName}, {DateHired:d}]";
        }
    }
}

