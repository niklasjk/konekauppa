namespace Konekauppa.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class etunimi : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "etunimi", c => c.String());
            AddColumn("dbo.AspNetUsers", "sukunimi", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "sukunimi");
            DropColumn("dbo.AspNetUsers", "etunimi");
        }
    }
}
