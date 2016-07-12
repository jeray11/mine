namespace mine.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_table_forumspost : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Forums_Post",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TopicId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        Text = c.String(nullable: false, unicode: false),
                        IPAddress = c.String(nullable: false, maxLength: 100, storeType: "nvarchar"),
                        CreatedOnUtc = c.DateTime(nullable: false, precision: 0),
                        UpdatedOnUtc = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId, cascadeDelete: true)
                .ForeignKey("dbo.ForumTopic", t => t.TopicId, cascadeDelete: true)
                .Index(t => t.TopicId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Forums_Post", "TopicId", "dbo.ForumTopic");
            DropForeignKey("dbo.Forums_Post", "CustomerId", "dbo.Customer");
            DropIndex("dbo.Forums_Post", new[] { "CustomerId" });
            DropIndex("dbo.Forums_Post", new[] { "TopicId" });
            DropTable("dbo.Forums_Post");
        }
    }
}
