namespace vismaUzd.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateEmp : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FirstName = c.String(),
                        LastName = c.String(),
                        BirthDate = c.DateTime(nullable: false),
                        EmploymentDate = c.DateTime(nullable: false),
                        BossId = c.Guid(nullable: false),
                        HomeAddress = c.String(),
                        CurrentSalary = c.Double(nullable: false),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Employees");
        }
    }
}
