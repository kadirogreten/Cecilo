namespace Cecilo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class urundetay : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Uruns", "Detay", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Uruns", "Detay");
        }
    }
}
