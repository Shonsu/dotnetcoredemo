namespace WebMVCDemo.Controllers
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateHired { get; set; }
        public string? Department { get; set; }
        public int? SupervisorId { get; set; }

        public EmployeeDto(int id, string? firstName, string? lastName, DateTime dateHired, string? department, int? supervisorId)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            DateHired = dateHired;
            Department = department;
            SupervisorId = supervisorId;
        }

        public EmployeeDto()
        {
        }

        public override string ToString()
        {
            return $@"{FirstName}
                {LastName}
                {DateHired}
                {Department}";
        }

    }
}