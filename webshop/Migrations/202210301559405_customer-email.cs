namespace webshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class customeremail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Customers", "EmailAddress", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Customers", "EmailAddress");
        }
    }
}
