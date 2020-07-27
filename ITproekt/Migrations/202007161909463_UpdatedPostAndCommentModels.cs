namespace ITproekt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdatedPostAndCommentModels : DbMigration
    {
        public override void Up()
        {
            CreateIndex("dbo.Comments", "PostID");
            AddForeignKey("dbo.Comments", "PostID", "dbo.Posts", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Comments", "PostID", "dbo.Posts");
            DropIndex("dbo.Comments", new[] { "PostID" });
        }
    }
}
