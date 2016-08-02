namespace mine.data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_table_currency : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Currency",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        CurrencyCode = c.String(nullable: false, maxLength: 50, storeType: "nvarchar"),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
                        DisplayLocale = c.String(maxLength: 50, storeType: "nvarchar"),
                        CustomFormatting = c.String(maxLength: 50, storeType: "nvarchar"),
                        LimitedToStores = c.Boolean(nullable: false),
                        Published = c.Boolean(nullable: false),
                        DisplayOrder = c.Int(nullable: false),
                        CreatedOnUtc = c.DateTime(nullable: false, precision: 0),
                        UpdatedOnUtc = c.DateTime(nullable: false, precision: 0),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Currency");
        }
    }
}
