namespace mine.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_table_store : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Store",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 400, storeType: "nvarchar"),
                        Url = c.String(nullable: false, maxLength: 400, storeType: "nvarchar"),
                        SslEnabled = c.Boolean(nullable: false),
                        SecureUrl = c.String(maxLength: 400, storeType: "nvarchar"),
                        DisplayOrder = c.Int(nullable: false),
                        CompanyName = c.String(maxLength: 1000, storeType: "nvarchar"),
                        CompanyAddress = c.String(maxLength: 1000, storeType: "nvarchar"),
                        CompanyPhoneNumber = c.String(maxLength: 1000, storeType: "nvarchar"),
                        CompanyVat = c.String(maxLength: 1000, storeType: "nvarchar"),
                        Hosts = c.String(maxLength: 1000, storeType: "nvarchar"),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Store");
        }
    }
}
