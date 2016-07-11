namespace mine.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_table_forumtopic : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ForumTopic",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ForumId = c.Int(nullable: false),
                        CustomerId = c.Int(nullable: false),
                        TopicTypeId = c.Int(nullable: false),
                        Subject = c.String(nullable: false, maxLength: 450, storeType: "nvarchar"),
                        NumPosts = c.Int(nullable: false),
                        Views = c.Int(nullable: false),
                        LastPostId = c.Int(nullable: false),
                        LastPostCustomerId = c.Int(nullable: false),
                        LastPostTime = c.DateTime(precision: 0),
                        CreatedOnUtc = c.DateTime(nullable: false, precision: 0),
                        UpdatedOnUtc = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Customer", t => t.CustomerId)
                .ForeignKey("dbo.Forums_Forum", t => t.ForumId, cascadeDelete: true)
                .Index(t => t.ForumId)
                .Index(t => t.CustomerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ForumTopic", "ForumId", "dbo.Forums_Forum");
            DropForeignKey("dbo.ForumTopic", "CustomerId", "dbo.Customer");
            DropIndex("dbo.ForumTopic", new[] { "CustomerId" });
            DropIndex("dbo.ForumTopic", new[] { "ForumId" });
            DropTable("dbo.ForumTopic");
        }
    }
}
