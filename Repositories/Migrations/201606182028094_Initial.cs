namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbCarriers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        NickName = c.String(nullable: false, maxLength: 100),
                        Status = c.Boolean(nullable: false),
                        Deleted = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.tbRates",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdCarrier = c.Int(nullable: false),
                        price = c.Decimal(nullable: false, precision: 18, scale: 2),
                        IdUser = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.tbCarrier", t => t.IdCarrier, cascadeDelete: true)
                .ForeignKey("dbo.tbUser", t => t.IdUser, cascadeDelete: true)
                .Index(t => t.IdCarrier)
                .Index(t => t.IdUser);
            
            CreateTable(
                "dbo.tbUser",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        User = c.String(nullable: false, maxLength: 50),
                        Password = c.Binary(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.tbRates", "IdUser", "dbo.tbUser");
            DropForeignKey("dbo.tbRates", "IdCarrier", "dbo.tbCarriers");
            DropIndex("dbo.tbRates", new[] { "IdUser" });
            DropIndex("dbo.tbRates", new[] { "IdCarrier" });
            DropTable("dbo.tbUsers");
            DropTable("dbo.tbRates");
            DropTable("dbo.tbCarriers");
        }
    }
}
