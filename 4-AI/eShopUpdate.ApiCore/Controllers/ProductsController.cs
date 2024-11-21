using eShopUpdate.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace eShopUpdate.Api.Controllers
{
	[ApiController]
	[Route("api/[controller]")]
	public class ProductsController : ControllerBase
	{
		private readonly ProductDataContext _db;

        public ProductsController(ProductDataContext db)
        {
            _db = db;
        }

        [HttpGet]
		// GET api/products
		public IEnumerable<Product> Get()
		{
			return _db.Products;
		}

		[HttpGet("{id?}")]

		// GET api/products/5
		public async Task<Product> Get(int id)
		{
			return await _db.Products.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
		}

		[HttpPost]

		// generate the other CRUD methods for this controller


		// POST api/products
		public async Task Post([FromBody] Product product)
		{
            _db.Products.Add(product);
			await _db.SaveChangesAsync();
		}

		[HttpPut("{id?}")]

		// PUT api/products/5
		public async Task Put(int id, [FromBody] Product product)
		{
            _db.Entry(product).State = EntityState.Modified;
			await _db.SaveChangesAsync();
		}

		[HttpDelete("{id?}")]

		// DELETE api/products/5
		public async Task Delete(int id)
		{
			var product = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
            _db.Products.Remove(product);
			await _db.SaveChangesAsync();
		}
	}
}