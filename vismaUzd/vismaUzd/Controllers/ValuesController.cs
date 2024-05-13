using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Web.Http;

namespace vismaUzd.Controllers
{
    public class BlogDbContext : DbContext
    {
        public DbSet<Employee> Employees { get; set; }
    }

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

    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
