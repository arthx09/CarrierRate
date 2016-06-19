namespace Repositories.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.tbCarrier",
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
                "dbo.tbRate",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdCarrier = c.Int(nullable: false),
                        Rate = c.Decimal(nullable: false, precision: 18, scale: 2),
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
            DropForeignKey("dbo.tbRate", "IdUser", "dbo.tbUser");
            DropForeignKey("dbo.tbRate", "IdCarrier", "dbo.tbCarrier");
            DropIndex("dbo.tbRate", new[] { "IdUser" });
            DropIndex("dbo.tbRate", new[] { "IdCarrier" });
            DropTable("dbo.tbUser");
            DropTable("dbo.tbRate");
            DropTable("dbo.tbCarrier");
        }
    }
}
