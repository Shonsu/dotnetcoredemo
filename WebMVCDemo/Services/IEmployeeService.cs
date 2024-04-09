using System;
using Microsoft.EntityFrameworkCore;
using WebMVCDemo.Controllers;
using WebMVCDemo.Models;
using ZstdSharp;

namespace WebMVCDemo.Services
{
    public interface IEmployeeService
    {
        List<Employee> GetEmployees();
        Employee GetEmployee(int id);
        Employee GetEmployee(string name);
        void AddEmployee(Employee employee);
        void UpdateEmployee(EmployeeDto employee);
        public Employee SetSupervisor(int employeeId, int suerpvisorId);


    }

    public class EmployeeService : IEmployeeService
    {
        private readonly AppDbContext _db;

        public EmployeeService(AppDbContext appDbContext)
        {
            _db = appDbContext;
        }

        public void AddEmployee(Employee employee)
        {
            _db.Employees.Add(employee);
            _db.SaveChanges();
        }

        public Employee GetEmployee(int id)
        {
            return _db.Employees.Where(e => e.Id == id).Include(e => e.Supervisor).SingleOrDefault();
            //return _db.Employees.Find(id);
        }

        public Employee GetEmployee(string name)
        {
            return _db.Employees.Where(e => e.FirstName.Equals(name, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();

        }

        public List<Employee> GetEmployees()
        {
            return _db.Employees.ToList();
        }

        public void UpdateEmployee(EmployeeDto employee)
        {
            //Employee employee1 = _db.Employees.Where(e=>e.Id==employee.Id).Include(e=>e.Supervisor).Single();
            Employee employee1 = _db.Employees.Find(employee.Id);
            _db.Entry(employee1).CurrentValues.SetValues(employee);
            _db.SaveChanges();
        }
        public Employee SetSupervisor(int employeeId, int supervisorId)
        {
            Employee employee = this.GetEmployee(employeeId);
            Employee supervisor = this.GetEmployee(supervisorId);

            if (employee == null || supervisor ==null)
            {
                throw new Exception();
            }
            //employee.SupervisorId = supervisorId;
            employee.Supervisor = supervisor;
            _db.SaveChanges();
            return employee;
        }
    }
    public class MockEmployeeService : IEmployeeService
    {
        private List<Employee> employees;

        public MockEmployeeService()
        {
            employees = new List<Employee>{
                new Employee (1,  "Adam", "Kowal",new DateTime(2011, 05, 10), "IT", null ),
                new Employee (2, "Paweł", "Lis",new DateTime(2012, 05, 10),"IT", 1 ),
                new Employee (3, "Damian", "Dzik",new DateTime(2012, 05, 10),"IT", 2 ),
                new Employee (4, "Henryk", "Liść",new DateTime(2014, 05, 10),"IT", 3 ),
                new Employee (5, "Marek", "Sosna",new DateTime(2011, 05, 10),"IT", 4)
            };
            employees[0].Supervisor = employees[2];
            employees[1].Supervisor = employees[0];
            employees[2].Supervisor = employees[1];
            employees[3].Supervisor = employees[2];
            employees[4].Supervisor = employees[3];

        }

        public void AddEmployee(Employee employee)
        {
            employee.Id = employees.Count;
            employees.Add(employee);
        }

        public Employee GetEmployee(int id)
        {
            return employees[id].Supervisor;
        }

        public Employee GetEmployee(string name)
        {
            throw new NotImplementedException();
        }

        public List<Employee> GetEmployees()
        {
            return employees;
        }

        public Employee SetSupervisor(int employeeId, int suerpvisorId)
        {
            throw new NotImplementedException();
        }

        public void UpdateEmployee(EmployeeDto employee)
        {
            throw new NotImplementedException();
        }

    }
}

