namespace ElevenNote.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AssociatedNotesAndCategories : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Note", "CategoryId", c => c.Int());
            CreateIndex("dbo.Note", "CategoryId");
            AddForeignKey("dbo.Note", "CategoryId", "dbo.Category", "CategoryId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Note", "CategoryId", "dbo.Category");
            DropIndex("dbo.Note", new[] { "CategoryId" });
            DropColumn("dbo.Note", "CategoryId");
        }
    }
}
