namespace Cecilo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class title : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Kategoris", "CategoryTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Kategoris", "CategoryTitle");
        }
    }
}
