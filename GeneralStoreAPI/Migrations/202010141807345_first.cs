namespace GeneralStoreAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class first : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "FirstName", c => c.String(nullable: false));
            DropColumn("dbo.Customers", "FirsttName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Customers", "FirsttName", c => c.String(nullable: false));
            DropColumn("dbo.Customers", "FirstName");
        }
    }
}
