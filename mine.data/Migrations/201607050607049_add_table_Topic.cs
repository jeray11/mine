namespace mine.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_table_Topic : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Topic",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        SystemName = c.String(unicode: false),
                        IncludeInSitemap = c.Boolean(nullable: false),
                        IncludeInTopMenu = c.Boolean(nullable: false),
                        IncludeInFooterColumn1 = c.Boolean(nullable: false),
                        IncludeInFooterColumn2 = c.Boolean(nullable: false),
                        IncludeInFooterColumn3 = c.Boolean(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                        AccessibleWhenStoreClosed = c.Boolean(nullable: false),
                        IsPasswordProtected = c.Boolean(nullable: false),
                        Password = c.String(unicode: false),
                        Title = c.String(unicode: false),
                        Body = c.String(unicode: false),
                        TopicTemplateId = c.Int(nullable: false),
                        MetaKeywords = c.String(unicode: false),
                        MetaDescription = c.String(unicode: false),
                        MetaTitle = c.String(unicode: false),
                        LimitedToStores = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Topic");
        }
    }
}
