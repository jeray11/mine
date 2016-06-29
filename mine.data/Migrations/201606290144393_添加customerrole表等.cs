namespace mine.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class 添加customerrole表等 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CustomerRole",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        FreeShipping = c.Boolean(nullable: false),
                        TaxExempt = c.Boolean(nullable: false),
                        Active = c.Boolean(nullable: false),
                        IsSystemRole = c.Boolean(nullable: false),
                        SystemName = c.String(maxLength: 255, storeType: "nvarchar"),
                        PurchasedWithProductId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PermissionRecord",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, unicode: false),
                        SystemName = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                        Category = c.String(nullable: false, maxLength: 255, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PermissionRecord_Role_Mapping",
                c => new
                    {
                        PermissionRecord_Id = c.Int(nullable: false),
                        CustomerRole_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.PermissionRecord_Id, t.CustomerRole_Id })
                .ForeignKey("dbo.PermissionRecord", t => t.PermissionRecord_Id, cascadeDelete: true)
                .ForeignKey("dbo.CustomerRole", t => t.CustomerRole_Id, cascadeDelete: true)
                .Index(t => t.PermissionRecord_Id)
                .Index(t => t.CustomerRole_Id);
            
            CreateTable(
                "dbo.Customer_CustomerRole_Mapping",
                c => new
                    {
                        Customer_Id = c.Int(nullable: false),
                        CustomerRole_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Customer_Id, t.CustomerRole_Id })
                .ForeignKey("dbo.Customer", t => t.Customer_Id, cascadeDelete: true)
                .ForeignKey("dbo.CustomerRole", t => t.CustomerRole_Id, cascadeDelete: true)
                .Index(t => t.Customer_Id)
                .Index(t => t.CustomerRole_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Customer_CustomerRole_Mapping", "CustomerRole_Id", "dbo.CustomerRole");
            DropForeignKey("dbo.Customer_CustomerRole_Mapping", "Customer_Id", "dbo.Customer");
            DropForeignKey("dbo.PermissionRecord_Role_Mapping", "CustomerRole_Id", "dbo.CustomerRole");
            DropForeignKey("dbo.PermissionRecord_Role_Mapping", "PermissionRecord_Id", "dbo.PermissionRecord");
            DropIndex("dbo.Customer_CustomerRole_Mapping", new[] { "CustomerRole_Id" });
            DropIndex("dbo.Customer_CustomerRole_Mapping", new[] { "Customer_Id" });
            DropIndex("dbo.PermissionRecord_Role_Mapping", new[] { "CustomerRole_Id" });
            DropIndex("dbo.PermissionRecord_Role_Mapping", new[] { "PermissionRecord_Id" });
            DropTable("dbo.Customer_CustomerRole_Mapping");
            DropTable("dbo.PermissionRecord_Role_Mapping");
            DropTable("dbo.PermissionRecord");
            DropTable("dbo.CustomerRole");
        }
    }
}
