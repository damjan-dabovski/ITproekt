namespace ITproekt.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddPostAuthorAndProductImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Posts", "AuthorName", c => c.String());
            AddColumn("dbo.Products", "ImageUrl", c => c.String());
            AlterColumn("dbo.Comments", "AuthorName", c => c.String(nullable: false));
            AlterColumn("dbo.Posts", "Title", c => c.String(nullable: false, maxLength: 200));
            AlterColumn("dbo.Products", "Name", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Products", "Name", c => c.String());
            AlterColumn("dbo.Posts", "Title", c => c.String());
            AlterColumn("dbo.Comments", "AuthorName", c => c.String());
            DropColumn("dbo.Products", "ImageUrl");
            DropColumn("dbo.Posts", "AuthorName");
        }
    }
}
