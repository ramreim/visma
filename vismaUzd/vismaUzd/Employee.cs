using System;
using System.ComponentModel.DataAnnotations;

namespace vismaUzd
{
    public class Employee
    {
        [Required(AllowEmptyStrings = false)]
        //[MaxLength(80)]

        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime EmploymentDate { get; set; }

        public Guid BossId { get; set; }

        public string HomeAddress { get; set; }

        public double CurrentSalary { get; set; }

        public Role Role { get; set; }
    }
}