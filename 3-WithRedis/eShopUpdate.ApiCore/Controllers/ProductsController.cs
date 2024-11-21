using eShopUpdate.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;

namespace eShopUpdate.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController : ControllerBase
	{

		private ProductDataContext db = new ProductDataContext();

		[HttpGet]
		// GET api/products
		public IEnumerable<Product> Get()
		{
			return db.Products;
		}

		[HttpGet("{id?}")]

		// GET api/products/5
		public async Task<Product> Get(int id)
		{
			return await db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
		}

		[HttpPost]

		// generate the other CRUD methods for this controller


		// POST api/products
		public async Task Post([FromBody] Product product)
		{
			db.Products.Add(product);
			await db.SaveChangesAsync();
		}

		[HttpPut("{id?}")]

		// PUT api/products/5
		public async Task Put(int id, [FromBody] Product product)
		{
			db.Entry(product).State = EntityState.Modified;
			await db.SaveChangesAsync();
		}

		[HttpDelete("{id?}")]

		// DELETE api/products/5
		public async Task Delete(int id)
		{
			var product = await db.Products.FirstOrDefaultAsync(p => p.Id == id);
			db.Products.Remove(product);
			await db.SaveChangesAsync();
		}
	}
}