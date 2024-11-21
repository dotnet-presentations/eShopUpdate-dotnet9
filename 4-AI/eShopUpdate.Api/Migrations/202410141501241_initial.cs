namespace eShopUpdate.Api.Migrations
{
	using System.Data.Entity.Migrations;

	public partial class initial : DbMigration
	{
		public override void Up()
		{
			CreateTable(
					"dbo.Products",
					c => new
					{
						Id = c.Int(nullable: false, identity: true),
						Name = c.String(),
						Description = c.String(),
						Price = c.Decimal(nullable: false, precision: 18, scale: 2),
						ImageUrl = c.String(),
					})
					.PrimaryKey(t => t.Id);

		}

		public override void Down()
		{
			DropTable("dbo.Products");
		}
	}
}
