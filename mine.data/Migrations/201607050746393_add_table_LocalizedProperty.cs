namespace mine.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_table_LocalizedProperty : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.LocalizedProperty",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityId = c.Int(nullable: false),
                        LanguageId = c.Int(nullable: false),
                        LocaleKeyGroup = c.String(nullable: false, maxLength: 400, storeType: "nvarchar"),
                        LocaleKey = c.String(nullable: false, maxLength: 400, storeType: "nvarchar"),
                        LocaleValue = c.String(nullable: false, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Language", t => t.LanguageId, cascadeDelete: true)
                .Index(t => t.LanguageId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.LocalizedProperty", "LanguageId", "dbo.Language");
            DropIndex("dbo.LocalizedProperty", new[] { "LanguageId" });
            DropTable("dbo.LocalizedProperty");
        }
    }
}
