﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
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

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
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
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}