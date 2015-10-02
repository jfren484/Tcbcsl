namespace Tcbcsl.Data.Migrations
{
    public partial class AddPageContentTitleColumn : Migration
    {
        public override void Up()
        {
            AddColumn("dbo.PageContents", "Title", c => c.String(nullable: false, maxLength: 50));
            Sql("UPDATE dbo.PageContents SET Title = PageTag, PageTag = REPLACE(PageTag, ' ', '')");
        }
        
        public override void Down()
        {
            Sql("UPDATE dbo.PageContents SET PageTag = Title");
            DropColumn("dbo.PageContents", "Title");
        }
    }
}
