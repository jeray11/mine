namespace mine.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_tables_GenericAttribute : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GenericAttribute",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        EntityId = c.Int(nullable: false),
                        KeyGroup = c.String(nullable: false, maxLength: 400, storeType: "nvarchar"),
                        Key = c.String(nullable: false, maxLength: 400, storeType: "nvarchar"),
                        Value = c.String(nullable: false, unicode: false),
                        StoreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.GenericAttribute");
        }
    }
}
