namespace Cecilo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lang1 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Iletisims", "Lang");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Iletisims", "Lang", c => c.Int(nullable: false));
        }
    }
}
