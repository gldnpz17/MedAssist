namespace DataAccessV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Accounts",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Username = c.String(nullable: false, maxLength: 64),
                        Password = c.String(nullable: false, maxLength: 64),
                        IsHospital = c.Boolean(nullable: false),
                        IsPharmacy = c.Boolean(nullable: false),
                        MedWalletBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AmbulanceRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Sender = c.Guid(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        IsDone = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppointmentRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.Guid(nullable: false),
                        DoctorId = c.Int(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        IsDone = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Doctors",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Specialization = c.String(nullable: false),
                        Status = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MedicationRequests",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.Guid(nullable: false),
                        MedicationId = c.Int(nullable: false),
                        Amount = c.Int(nullable: false),
                        Latitude = c.Double(nullable: false),
                        Longitude = c.Double(nullable: false),
                        IsDone = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Medications",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 64),
                        Description = c.String(nullable: false, maxLength: 128),
                        RawImage = c.Binary(nullable: false),
                        Price = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MedWalletTransactions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        AccountId = c.Guid(nullable: false),
                        ChangeInBalance = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Description = c.String(nullable: false, maxLength: 512),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.MedWalletVouchers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Code = c.Guid(nullable: false),
                        Value = c.Decimal(nullable: false, precision: 18, scale: 2),
                        Claimed = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.MedWalletVouchers");
            DropTable("dbo.MedWalletTransactions");
            DropTable("dbo.Medications");
            DropTable("dbo.MedicationRequests");
            DropTable("dbo.Doctors");
            DropTable("dbo.AppointmentRequests");
            DropTable("dbo.AmbulanceRequests");
            DropTable("dbo.Accounts");
        }
    }
}
