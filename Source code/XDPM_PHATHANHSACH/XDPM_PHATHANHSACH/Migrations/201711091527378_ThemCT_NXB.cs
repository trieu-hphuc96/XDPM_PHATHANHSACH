namespace XDPM_PHATHANHSACH.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThemCT_NXB : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CT_NXB",
                c => new
                    {
                        MANXB = c.Int(nullable: false),
                        MASACH = c.Int(nullable: false),
                        GIA = c.Int(),
                        SLLAY = c.Int(),
                        SLDABAN = c.Int(),
                    })
                .PrimaryKey(t => new { t.MANXB, t.MASACH })
                .ForeignKey("dbo.SACH", t => t.MASACH, cascadeDelete: true)
                .ForeignKey("dbo.NXB", t => t.MANXB, cascadeDelete: true)
                .Index(t => t.MANXB)
                .Index(t => t.MASACH);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.CT_NXB", "MANXB", "dbo.NXB");
            DropForeignKey("dbo.CT_NXB", "MASACH", "dbo.SACH");
            DropIndex("dbo.CT_NXB", new[] { "MASACH" });
            DropIndex("dbo.CT_NXB", new[] { "MANXB" });
            DropTable("dbo.CT_NXB");
        }
    }
}
