namespace Cecilo.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ilk : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Belgelerimizs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BelgeAdi = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Etikets",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EtiketAdi = c.String(),
                        Sira = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Uruns",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UrunAdi = c.String(),
                        AltBaslik = c.String(),
                        Aciklama = c.String(),
                        Fiyat = c.Decimal(precision: 18, scale: 2),
                        KategoriId = c.Int(nullable: false),
                        IsNew = c.Boolean(nullable: false),
                        IsHome = c.Boolean(nullable: false),
                        IsPopular = c.Boolean(nullable: false),
                        MarkalarId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kategoris", t => t.KategoriId, cascadeDelete: true)
                .ForeignKey("dbo.Markalars", t => t.MarkalarId, cascadeDelete: true)
                .Index(t => t.KategoriId)
                .Index(t => t.MarkalarId);
            
            CreateTable(
                "dbo.Kategoris",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UstKategoriId = c.Int(),
                        KategoriAdi = c.String(),
                        Sira = c.Short(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Kategoris", t => t.UstKategoriId)
                .Index(t => t.UstKategoriId);
            
            CreateTable(
                "dbo.Markalars",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MarkaAdi = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Renks",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RenkKodu = c.String(),
                        RenkAdi = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Resims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Sira = c.Short(nullable: false),
                        IsSelected = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        UrunId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Uruns", t => t.UrunId, cascadeDelete: true)
                .Index(t => t.UrunId);
            
            CreateTable(
                "dbo.HakkimizdaMenus",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MenuAdi = c.String(),
                        MenuBaslik = c.String(),
                        Aciklama = c.String(),
                        Detail = c.String(),
                        Image = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.Sliders",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Baslik = c.String(),
                        Aciklama = c.String(),
                        Url = c.String(),
                        Sira = c.Int(),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.UrunEtikets",
                c => new
                    {
                        Urun_Id = c.Int(nullable: false),
                        Etiket_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Urun_Id, t.Etiket_Id })
                .ForeignKey("dbo.Uruns", t => t.Urun_Id, cascadeDelete: true)
                .ForeignKey("dbo.Etikets", t => t.Etiket_Id, cascadeDelete: true)
                .Index(t => t.Urun_Id)
                .Index(t => t.Etiket_Id);
            
            CreateTable(
                "dbo.RenkUruns",
                c => new
                    {
                        Renk_Id = c.Int(nullable: false),
                        Urun_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Renk_Id, t.Urun_Id })
                .ForeignKey("dbo.Renks", t => t.Renk_Id, cascadeDelete: true)
                .ForeignKey("dbo.Uruns", t => t.Urun_Id, cascadeDelete: true)
                .Index(t => t.Renk_Id)
                .Index(t => t.Urun_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Resims", "UrunId", "dbo.Uruns");
            DropForeignKey("dbo.RenkUruns", "Urun_Id", "dbo.Uruns");
            DropForeignKey("dbo.RenkUruns", "Renk_Id", "dbo.Renks");
            DropForeignKey("dbo.Uruns", "MarkalarId", "dbo.Markalars");
            DropForeignKey("dbo.Uruns", "KategoriId", "dbo.Kategoris");
            DropForeignKey("dbo.Kategoris", "UstKategoriId", "dbo.Kategoris");
            DropForeignKey("dbo.UrunEtikets", "Etiket_Id", "dbo.Etikets");
            DropForeignKey("dbo.UrunEtikets", "Urun_Id", "dbo.Uruns");
            DropIndex("dbo.RenkUruns", new[] { "Urun_Id" });
            DropIndex("dbo.RenkUruns", new[] { "Renk_Id" });
            DropIndex("dbo.UrunEtikets", new[] { "Etiket_Id" });
            DropIndex("dbo.UrunEtikets", new[] { "Urun_Id" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.Resims", new[] { "UrunId" });
            DropIndex("dbo.Kategoris", new[] { "UstKategoriId" });
            DropIndex("dbo.Uruns", new[] { "MarkalarId" });
            DropIndex("dbo.Uruns", new[] { "KategoriId" });
            DropTable("dbo.RenkUruns");
            DropTable("dbo.UrunEtikets");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Sliders");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.HakkimizdaMenus");
            DropTable("dbo.Resims");
            DropTable("dbo.Renks");
            DropTable("dbo.Markalars");
            DropTable("dbo.Kategoris");
            DropTable("dbo.Uruns");
            DropTable("dbo.Etikets");
            DropTable("dbo.Belgelerimizs");
        }
    }
}
