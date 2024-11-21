using eShopUpdate.Entities;
using System.Data.Entity;

namespace eShopUpdate.Api
{
	public class ProductDataContext : DbContext
	{

		public ProductDataContext() : base("Products")
		{
		}

		public System.Data.Entity.DbSet<Product> Products { get; set; }


	}
}