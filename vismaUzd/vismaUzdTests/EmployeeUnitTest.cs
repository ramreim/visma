using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using vismaUzd;
using vismaUzd.Controllers;

namespace vismaUzdTests
{
    [TestClass]
    public class EmployeeUnitTest
    {
        [TestMethod]
        public void EmployeeObjectProperties()
        {
            var e = new Employee();

            e.FirstName = "Ramunas";

            e.LastName = "Reimontas";

            e.Role = Role.Developer;

            Assert.AreEqual(e.FirstName, "Ramunas");
            Assert.AreEqual(e.LastName, "Reimontas");
            Assert.AreEqual(e.Role, Role.Developer);
        }

        [TestMethod]
        public void BossIdExistBeforeInsertingEmployeerToDb()
        {
            var valid = new EmployeeValidation();

            var e = new Employee();

            e.BossId = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709");

            var ex = Assert.ThrowsException<Exception>(() => valid.Validation(e, null));

            Assert.AreEqual(ex.Message, "Boss id \"9d2b0228-4d0d-4c23-8b49-01a698857709\" don't exist in DB");
        }

        [TestMethod]
        public void FirstNameAndLastNameCannotBeEquals()
        {
            var valid = new EmployeeValidation();

            var e = new Employee();

            e.FirstName = "Ramunas";

            e.LastName = "Ramunas";

            e.BirthDate = new DateTime(1980, 1, 1).Date;

            e.EmploymentDate = new DateTime(2001, 1, 1).Date;

            e.BossId = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709");

            var ex = Assert.ThrowsException<Exception>(() => valid.Validation(e, e));

            Assert.AreEqual(ex.Message, "Firstname and Lastname can't be equals");
        }

        [TestMethod]
        public void SalaryLessZero()
        {
            var valid = new EmployeeValidation();

            var e = new Employee();

            e.FirstName = "Ramunas";

            e.LastName = "Reimontas";

            e.BirthDate = new DateTime(1980, 1, 1).Date;

            e.EmploymentDate = new DateTime(2001, 1, 1).Date;

            e.BossId = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709");

            e.CurrentSalary = -0.1;

            var ex = Assert.ThrowsException<Exception>(() => valid.Validation(e, e));

            Assert.AreEqual(ex.Message, "CurrentSalary can't be < 0");
        }

        [TestMethod]
        public void AgeCantBeGreather70()
        {
            var valid = new EmployeeValidation();

            var e = new Employee();

            e.FirstName = "Ramunas";

            e.LastName = "Reimontas";

            e.BirthDate = new DateTime(1901, 1, 1).Date;

            e.EmploymentDate = new DateTime(2001, 1, 1).Date;

            e.BossId = new Guid("9D2B0228-4D0D-4C23-8B49-01A698857709");

            var ex = Assert.ThrowsException<Exception>(() => valid.Validation(e, e));

            Assert.AreEqual(ex.Message, "Age can't be > 70");
        }
    }
}
