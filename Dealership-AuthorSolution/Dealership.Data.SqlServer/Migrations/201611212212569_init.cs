namespace Dealership.Data.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.SqlServerCars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Seats = c.Int(nullable: false),
                        Type = c.Int(nullable: false),
                        Wheels = c.Int(nullable: false),
                        Make = c.String(),
                        Model = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SqlServerUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SqlServerUsers", t => t.SqlServerUser_Id)
                .Index(t => t.SqlServerUser_Id);
            
            CreateTable(
                "dbo.SqlServerComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Content = c.String(),
                        Author = c.String(),
                        SqlServerCar_Id = c.Int(),
                        SqlServerMotorcycle_Id = c.Int(),
                        SqlServerTruck_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SqlServerCars", t => t.SqlServerCar_Id)
                .ForeignKey("dbo.SqlServerMotorcycles", t => t.SqlServerMotorcycle_Id)
                .ForeignKey("dbo.SqlServerTrucks", t => t.SqlServerTruck_Id)
                .Index(t => t.SqlServerCar_Id)
                .Index(t => t.SqlServerMotorcycle_Id)
                .Index(t => t.SqlServerTruck_Id);
            
            CreateTable(
                "dbo.SqlServerMotorcycles",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Wheels = c.Int(nullable: false),
                        Make = c.String(),
                        Model = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SqlServerUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SqlServerUsers", t => t.SqlServerUser_Id)
                .Index(t => t.SqlServerUser_Id);
            
            CreateTable(
                "dbo.SqlServerTrucks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Type = c.Int(nullable: false),
                        Wheels = c.Int(nullable: false),
                        Make = c.String(),
                        Model = c.String(),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        SqlServerUser_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.SqlServerUsers", t => t.SqlServerUser_Id)
                .Index(t => t.SqlServerUser_Id);
            
            CreateTable(
                "dbo.SqlServerUsers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Username = c.String(),
                        FirstName = c.String(),
                        LastName = c.String(),
                        Password = c.String(),
                        Role = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.SqlServerTrucks", "SqlServerUser_Id", "dbo.SqlServerUsers");
            DropForeignKey("dbo.SqlServerMotorcycles", "SqlServerUser_Id", "dbo.SqlServerUsers");
            DropForeignKey("dbo.SqlServerCars", "SqlServerUser_Id", "dbo.SqlServerUsers");
            DropForeignKey("dbo.SqlServerComments", "SqlServerTruck_Id", "dbo.SqlServerTrucks");
            DropForeignKey("dbo.SqlServerComments", "SqlServerMotorcycle_Id", "dbo.SqlServerMotorcycles");
            DropForeignKey("dbo.SqlServerComments", "SqlServerCar_Id", "dbo.SqlServerCars");
            DropIndex("dbo.SqlServerTrucks", new[] { "SqlServerUser_Id" });
            DropIndex("dbo.SqlServerMotorcycles", new[] { "SqlServerUser_Id" });
            DropIndex("dbo.SqlServerComments", new[] { "SqlServerTruck_Id" });
            DropIndex("dbo.SqlServerComments", new[] { "SqlServerMotorcycle_Id" });
            DropIndex("dbo.SqlServerComments", new[] { "SqlServerCar_Id" });
            DropIndex("dbo.SqlServerCars", new[] { "SqlServerUser_Id" });
            DropTable("dbo.SqlServerUsers");
            DropTable("dbo.SqlServerTrucks");
            DropTable("dbo.SqlServerMotorcycles");
            DropTable("dbo.SqlServerComments");
            DropTable("dbo.SqlServerCars");
        }
    }
}
