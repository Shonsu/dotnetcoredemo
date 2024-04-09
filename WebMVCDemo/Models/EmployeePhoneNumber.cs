using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace WebMVCDemo.Models
{
    public class EmployeePhoneNumber
    {
        public int EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public string PhoneNumber { get; set; }
    }
}