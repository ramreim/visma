using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Globalization;
using System.Linq;
using System.Web.Http;

namespace vismaUzd.Controllers
{
    public class BlogDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
    }

    public struct CountAndAverageSalary
    {
        public int CountByRole { get; set; }
        public double AverageSalary { get; set; }

        public CountAndAverageSalary(int count, double averageSalary)
        {
            CountByRole = count;

            AverageSalary = averageSalary;
        }
    }



    public class EmployeeController : ApiController
    {
        //https://localhost:44392/api/Employee/values
        // GET api/values
        [Route("api/Employee/Values")]
        [HttpGet]
        public IEnumerable<Employee> Get()
        {
            BlogDbContext con = new BlogDbContext();

            var employees = con.Employees;

            return employees;
        }

        //https://localhost:44392/api/Employee/values/5ea5de1d-33a2-4dda-9989-dd8040f9f0d1
        // GET api/values/5
        [Route("api/Employee/Values/{userid:guid}")]
        [HttpGet]
        public List<Employee> Get(Guid userid)
        {
            BlogDbContext con = new BlogDbContext();

            var employees = con.Employees.Where(x => x.Id == userid).ToList();

            try
            {
                if (employees.Count == 0)
                    throw new Exception("Employee by Id not found");
            }

            catch (Exception ex)
            {
                LoggingException.Log(ex.Message);
            }

            return employees;
        }

        //https://localhost:44392/api/Employee/Boss/2280385d-2fb4-4e62-828c-08c2f3ce865f
        // GET api/values/5
        [Route("api/Employee/Boss/{userid:guid}")]
        [HttpGet]
        public List<Employee> GetByBossId(Guid userid)
        {
            BlogDbContext con = new BlogDbContext();

            var employees = con.Employees.Where(x => x.BossId == userid).ToList();

            try
            {
                if (employees.Count == 0)
                    throw new Exception("Employee by Boss Id not found");
            }

            catch (Exception ex)
            {
                LoggingException.Log(ex.Message);
            }

            return employees;
        }

        // POST api/values
        public void Post([FromBody] Employee e)
        {
            BlogDbContext con = new BlogDbContext();

            var boss = con.Employees.Where(x => x.Id == e.BossId).FirstOrDefault();

            e.Id = Guid.NewGuid();

            var validation = new EmployeeValidation();

            try
            {
                validation.Validation(e, boss);

                con.Employees.Add(e);

                con.SaveChanges();
            }
            catch (Exception ex)
            {
                LoggingException.Log(ex.Message);
            }
        }

        // PUT api/values/5
        //[Route("api/People/Values/{userid:int}")]
        [Route("api/Employee/Values")]
        [HttpPut]
        public void Put([FromBody] Employee e)
        {
            BlogDbContext con = new BlogDbContext();

            con.Employees.AddOrUpdate(e);

            con.SaveChanges();
        }

        //https://localhost:44392/api/People/Values/UpdateSalary?id=2280385d-2fb4-4e62-828c-08c2f3ce865f&salary=150.35
        // PUT api/values/5
        //[Route("api/People/Values/{userid:int}")]
        [Route("api/Employee/UpdateSalary")]
        [HttpPut]
        public void UpdateJustEmployeeSalary(Guid id, string salary)
        {
            try
            {
                BlogDbContext con = new BlogDbContext();

                var e = con.Employees.Where(x => x.Id == id).FirstOrDefault();

                e.CurrentSalary = Convert.ToDouble(salary);

                con.Employees.AddOrUpdate(e);

                con.SaveChanges();
            }
            catch (Exception ex)
            {
                LoggingException.Log(ex.Message);
            }
        }

        //https://localhost:44392/api/Employee/Values/ByNameStartDateEndDate?name=Ramunas&startDate=2020-10-05&endDate=2025-10-20
        [Route("api/Employee/ByNameStartDateEndDate")]
        [HttpGet]
        public List<Employee> GetByNameStartDateEndDate(string name, string startDate, string endDate)
        {
            var employees = new List<Employee>();

            var con = new BlogDbContext();

            try
            {
                var start = DateTime.Parse(startDate, new CultureInfo("lt-LT"));

                var end = DateTime.Parse(endDate, new CultureInfo("lt-LT"));

                employees = con.Employees.Where(x => x.FirstName == name && x.EmploymentDate > start && x.EmploymentDate < end).ToList();

                if (employees.Count == 0)
                    throw new Exception("Employee not found");
            }

            catch (Exception ex)
            {
                LoggingException.Log(ex.Message);
            }

            return employees;
        }

        //Getting employee count and average salary for particular Role
        //https://localhost:44392/api/Employee/Values/GettingEmployeeCountAndAverageSalaryForParticularRole?roleStr=Accountant
        //https://localhost:44392/api/Employee/Values/GettingEmployeeCountAndAverageSalaryForParticularRole?roleStr=Developer
        [Route("api/Values/GettingEmployeeCountAndAverageSalaryForParticularRole")]
        [HttpGet]
        public CountAndAverageSalary GettingEmployeeCountAndAverageSalaryForParticularRole(string roleStr)
        {
            var employees = new List<Employee>();

            var averageSalary = 0.0;

            var con = new BlogDbContext();

            try
            {
                var role = (Role)Enum.Parse(typeof(Role), roleStr);

                employees = con.Employees.Where(x => x.Role == role).ToList();

                if (employees.Count == 0)
                    throw new Exception($"Employee '{roleStr}' not found");

                averageSalary = employees.Select(x => x.CurrentSalary).Average();
            }
            catch (Exception ex)
            {
                LoggingException.Log(ex.Message);
            }

            return new CountAndAverageSalary(employees.Count, averageSalary);
        }

        // DELETE api/values/5
        [Route("api/Employee/Values/{userid:guid}")]
        [HttpDelete]
        public void Delete(Guid userid)
        {
            BlogDbContext con = new BlogDbContext();

            var del = con.Employees.Where(x => x.Id == userid).FirstOrDefault();

            try
            {
                if (del == null)
                    throw new Exception("Employee by Id not found");

                con.Employees.Remove(del);

                con.SaveChanges();
            }

            catch (Exception ex)
            {
                LoggingException.Log(ex.Message);
            }
        }
    }
}
