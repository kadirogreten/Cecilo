namespace Cecilo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class lang : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Belgelerimizs", "Lang", c => c.Int(nullable: false,defaultValue:0));
            AddColumn("dbo.Etikets", "Lang", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("dbo.Uruns", "Lang", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("dbo.Kategoris", "Lang", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("dbo.Markalars", "Lang", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("dbo.Renks", "Lang", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("dbo.HakkimizdaMenus", "Lang", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("dbo.Iletisims", "Lang", c => c.Int(nullable: false, defaultValue: 0));
            AddColumn("dbo.Sliders", "Lang", c => c.Int(nullable: false, defaultValue: 0));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Sliders", "Lang");
            DropColumn("dbo.Iletisims", "Lang");
            DropColumn("dbo.HakkimizdaMenus", "Lang");
            DropColumn("dbo.Renks", "Lang");
            DropColumn("dbo.Markalars", "Lang");
            DropColumn("dbo.Kategoris", "Lang");
            DropColumn("dbo.Uruns", "Lang");
            DropColumn("dbo.Etikets", "Lang");
            DropColumn("dbo.Belgelerimizs", "Lang");
        }
    }
}
