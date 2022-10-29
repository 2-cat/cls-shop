namespace webshop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addressbaseentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Addresses", "DateCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Addresses", "DateModified", c => c.DateTime());
            AddColumn("dbo.Addresses", "IsDeleted", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Addresses", "IsDeleted");
            DropColumn("dbo.Addresses", "DateModified");
            DropColumn("dbo.Addresses", "DateCreated");
        }
    }
}
