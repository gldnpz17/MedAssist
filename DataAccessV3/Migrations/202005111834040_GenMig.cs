namespace DataAccessV3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class GenMig : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.Accounts");
            AlterColumn("dbo.Accounts", "Id", c => c.Guid(nullable: false, identity: true, defaultValueSql: "newid()"));
            AddPrimaryKey("dbo.Accounts", "Id");
        }

        public override void Down()
        {
            DropPrimaryKey("dbo.Accounts");
            AlterColumn("dbo.Accounts", "Id", c => c.Guid(nullable: false, identity: true));
            AddPrimaryKey("dbo.Accounts", "Id");
        }
    }
}
