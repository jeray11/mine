namespace mine.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_table_PrivateMessage : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Forums_PrivateMessage",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        StoreId = c.Int(nullable: false),
                        FromCustomerId = c.Int(nullable: false),
                        ToCustomerId = c.Int(nullable: false),
                        Subject = c.String(nullable: false, maxLength: 450, storeType: "nvarchar"),
                        Text = c.String(nullable: false, unicode: false),
                        IsRead = c.Boolean(nullable: false),
                        IsDeletedByAuthor = c.Boolean(nullable: false),
                        IsDeletedByRecipient = c.Boolean(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.FromCustomerId)
                .ForeignKey("dbo.Customer", t => t.ToCustomerId)
                .Index(t => t.FromCustomerId)
                .Index(t => t.ToCustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Forums_PrivateMessage", "ToCustomerId", "dbo.Customer");
            DropForeignKey("dbo.Forums_PrivateMessage", "FromCustomerId", "dbo.Customer");
            DropIndex("dbo.Forums_PrivateMessage", new[] { "ToCustomerId" });
            DropIndex("dbo.Forums_PrivateMessage", new[] { "FromCustomerId" });
            DropTable("dbo.Forums_PrivateMessage");
        }
    }
}
