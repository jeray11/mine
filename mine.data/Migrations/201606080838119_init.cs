namespace mine.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Forums_Group",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        DisplayOrder = c.Int(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false, precision: 0),
                        UpdatedOnUtc = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Forums_Forum",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ForumGroupId = c.Int(nullable: false),
                        Name = c.String(nullable: false, maxLength: 200, storeType: "nvarchar"),
                        Description = c.String(unicode: false),
                        NumTopics = c.Int(nullable: false),
                        NumPosts = c.Int(nullable: false),
                        LastTopicId = c.Int(nullable: false),
                        LastPostId = c.Int(nullable: false),
                        LastPostCustomerId = c.Int(nullable: false),
                        LastPostTime = c.DateTime(precision: 0),
                        DisplayOrder = c.Int(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false, precision: 0),
                        UpdatedOnUtc = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Forums_Group", t => t.ForumGroupId, cascadeDelete: true)
                .Index(t => t.ForumGroupId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Forums_Forum", "ForumGroupId", "dbo.Forums_Group");
            DropIndex("dbo.Forums_Forum", new[] { "ForumGroupId" });
            DropTable("dbo.Forums_Forum");
            DropTable("dbo.Forums_Group");
        }
    }
}
