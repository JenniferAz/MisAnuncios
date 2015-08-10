namespace MisAnuncios.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddCategoriesClass : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                   "dbo.Categorias",
                   c => new
                   {
                       ID = c.Int(nullable: false, identity: true),
                       Nombre = c.String(maxLength: 200),

                   })
                   .PrimaryKey(t => t.ID)
                   .Index(p => p.Nombre, unique: true);

        }

        public override void Down()
        {

            DropIndex("dbo.Categorias", new[] { "Nombre" });
            DropTable("dbo.Categorias");
        }
    }
}
