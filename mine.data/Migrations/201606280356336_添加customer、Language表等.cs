namespace mine.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class 添加customerLanguage表等 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Customer",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CustomerGuid = c.Guid(nullable: false),
                        Username = c.String(maxLength: 1000, storeType: "nvarchar"),
                        Email = c.String(maxLength: 1000, storeType: "nvarchar"),
                        Password = c.String(unicode: false),
                        PasswordFormatId = c.Int(nullable: false),
                        PasswordSalt = c.String(unicode: false),
                        AdminComment = c.String(unicode: false),
                        IsTaxExempt = c.Boolean(nullable: false),
                        AffiliateId = c.Int(nullable: false),
                        VendorId = c.Int(nullable: false),
                        HasShoppingCartItems = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                        IsSystemAccount = c.Boolean(nullable: false),
                        SystemName = c.String(maxLength: 400, storeType: "nvarchar"),
                        LastIpAddress = c.String(unicode: false),
                        CreatedOnUtc = c.DateTime(nullable: false, precision: 0),
                        LastLoginDateUtc = c.DateTime(precision: 0),
                        LastActivityDateUtc = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Language",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        LanguageCulture = c.String(nullable: false, maxLength: 20, storeType: "nvarchar"),
                        UniqueSeoCode = c.String(maxLength: 2, storeType: "nvarchar"),
                        FlagImageFileName = c.String(maxLength: 50, storeType: "nvarchar"),
                        Rtl = c.Boolean(nullable: false),
                        LimitedToStores = c.Boolean(nullable: false),
                        DefaultCurrencyId = c.Int(nullable: false),
                        Published = c.Boolean(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.LocaleStringResource",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LanguageId = c.Int(nullable: false),
                        ResourceName = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        ResourceValue = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.LanguageId);
            
            CreateTable(
                "dbo.Log",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LogLevelId = c.Int(nullable: false),
                        ShortMessage = c.String(nullable: false, unicode: false),
                        FullMessage = c.String(unicode: false),
                        IpAddress = c.String(maxLength: 200, storeType: "nvarchar"),
                        CustomerId = c.Int(),
                        PageUrl = c.String(unicode: false),
                        ReferrerUrl = c.String(unicode: false),
                        CreatedOnUtc = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .Index(t => t.CustomerId);
            
            CreateTable(
                "dbo.StoreMapping",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityId = c.Int(nullable: false),
                        EntityName = c.String(nullable: false, maxLength: 400, storeType: "nvarchar"),
                        StoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Store", t => t.StoreId, cascadeDelete: true)
                .Index(t => t.StoreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.StoreMapping", "StoreId", "dbo.Store");
            DropForeignKey("dbo.Log", "CustomerId", "dbo.Customer");
            DropForeignKey("dbo.LocaleStringResource", "LanguageId", "dbo.Language");
            DropIndex("dbo.StoreMapping", new[] { "StoreId" });
            DropIndex("dbo.Log", new[] { "CustomerId" });
            DropIndex("dbo.LocaleStringResource", new[] { "LanguageId" });
            DropTable("dbo.StoreMapping");
            DropTable("dbo.Log");
            DropTable("dbo.LocaleStringResource");
            DropTable("dbo.Language");
            DropTable("dbo.Customer");
        }
    }
}
