using System;
using vismaUzd.Controllers;

namespace vismaUzd
{
    public class EmployeeValidation
    {
        public void Validation(Employee e, Employee employeeBoss)
        {
            if (employeeBoss == null)
                throw new Exception($"Boss id \"{e.BossId}\" don't exist in DB");

            var today = DateTime.Today;

            var age = today.Year - e.BirthDate.Year;

            if (e.BirthDate.Date > today.AddYears(-age)) age--;

            if (age < 18)
                throw new Exception("Age can't be < 18");

            if (age > 70)
                throw new Exception("Age can't be > 70");

            if (e.FirstName == e.LastName)
                throw new Exception("Firstname and Lastname can't be equals");

            if (e.FirstName.Length > 50)

                throw new Exception("FirstName length can't be > 50");

            if (e.LastName.Length > 50)
                throw new Exception("LastName length can't be > 50");

            if (e.CurrentSalary < 0)
                throw new Exception("CurrentSalary can't be < 0");

            if (e.EmploymentDate > DateTime.Now.Date)
                throw new Exception("EmploymentDate can't be future");

            if (e.EmploymentDate < new DateTime(2000, 1, 1).Date)
                throw new Exception($"EmploymentDate can't earlier than {new DateTime(2000, 1, 1).Date}");
        }
    }
}