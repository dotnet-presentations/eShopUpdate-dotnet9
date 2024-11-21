using eShopUpdate.Entities;
using System.Collections.Generic;
using System.Data.Entity;
using System.Threading.Tasks;
using System.Web.Http;

namespace eShopUpdate.Api.Controllers
{
	public class ProductsController : ApiController
	{

		private ProductDataContext db = new ProductDataContext();

		// GET api/products
		public IEnumerable<Product> Get()
		{
			return db.Products;
		}

		// GET api/products/5
		public async Task<Product> Get(int id)
		{
			return await db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
		}

		// generate the other CRUD methods for this controller


		// POST api/products
		public async Task Post([FromBody] Product product)
		{
			db.Products.Add(product);
			await db.SaveChangesAsync();
		}

		// PUT api/products/5
		public async Task Put(int id, [FromBody] Product product)
		{
			db.Entry(product).State = EntityState.Modified;
			await db.SaveChangesAsync();
		}

		// DELETE api/products/5
		public async Task Delete(int id)
		{
			var product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
			db.Products.Remove(product);
			await db.SaveChangesAsync();
		}

	}
}