namespace XDPM_PHATHANHSACH.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DoiNULL_NXB_DL : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PHIEUTHU", "MADL", "dbo.DAILY");
            DropForeignKey("dbo.PHIEUXUAT", "MADL", "dbo.DAILY");
            DropForeignKey("dbo.PHIEUCHI", "MANXB", "dbo.NXB");
            DropForeignKey("dbo.PHIEUNHAP", "MANXB", "dbo.NXB");
            DropIndex("dbo.PHIEUTHU", new[] { "MADL" });
            DropIndex("dbo.PHIEUCHI", new[] { "MANXB" });
            DropIndex("dbo.PHIEUNHAP", new[] { "MANXB" });
            DropIndex("dbo.PHIEUXUAT", new[] { "MADL" });
            AlterColumn("dbo.PHIEUTHU", "MADL", c => c.Int(nullable: false));
            AlterColumn("dbo.PHIEUCHI", "MANXB", c => c.Int(nullable: false));
            AlterColumn("dbo.PHIEUNHAP", "MANXB", c => c.Int(nullable: false));
            AlterColumn("dbo.PHIEUXUAT", "MADL", c => c.Int(nullable: false));
            CreateIndex("dbo.PHIEUTHU", "MADL");
            CreateIndex("dbo.PHIEUCHI", "MANXB");
            CreateIndex("dbo.PHIEUNHAP", "MANXB");
            CreateIndex("dbo.PHIEUXUAT", "MADL");
            AddForeignKey("dbo.PHIEUTHU", "MADL", "dbo.DAILY", "MADL", cascadeDelete: true);
            AddForeignKey("dbo.PHIEUXUAT", "MADL", "dbo.DAILY", "MADL", cascadeDelete: true);
            AddForeignKey("dbo.PHIEUCHI", "MANXB", "dbo.NXB", "MANXB", cascadeDelete: true);
            AddForeignKey("dbo.PHIEUNHAP", "MANXB", "dbo.NXB", "MANXB", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PHIEUNHAP", "MANXB", "dbo.NXB");
            DropForeignKey("dbo.PHIEUCHI", "MANXB", "dbo.NXB");
            DropForeignKey("dbo.PHIEUXUAT", "MADL", "dbo.DAILY");
            DropForeignKey("dbo.PHIEUTHU", "MADL", "dbo.DAILY");
            DropIndex("dbo.PHIEUXUAT", new[] { "MADL" });
            DropIndex("dbo.PHIEUNHAP", new[] { "MANXB" });
            DropIndex("dbo.PHIEUCHI", new[] { "MANXB" });
            DropIndex("dbo.PHIEUTHU", new[] { "MADL" });
            AlterColumn("dbo.PHIEUXUAT", "MADL", c => c.Int());
            AlterColumn("dbo.PHIEUNHAP", "MANXB", c => c.Int());
            AlterColumn("dbo.PHIEUCHI", "MANXB", c => c.Int());
            AlterColumn("dbo.PHIEUTHU", "MADL", c => c.Int());
            CreateIndex("dbo.PHIEUXUAT", "MADL");
            CreateIndex("dbo.PHIEUNHAP", "MANXB");
            CreateIndex("dbo.PHIEUCHI", "MANXB");
            CreateIndex("dbo.PHIEUTHU", "MADL");
            AddForeignKey("dbo.PHIEUNHAP", "MANXB", "dbo.NXB", "MANXB");
            AddForeignKey("dbo.PHIEUCHI", "MANXB", "dbo.NXB", "MANXB");
            AddForeignKey("dbo.PHIEUXUAT", "MADL", "dbo.DAILY", "MADL");
            AddForeignKey("dbo.PHIEUTHU", "MADL", "dbo.DAILY", "MADL");
        }
    }
}
