using eShopUpdate.Entities;
using eShopUpdateCore.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Newtonsoft.Json;

namespace eShopUpdate.Controllers
{
	public class HomeController(IHttpClientFactory ClientFactory, AISummaryService SummaryService) : Controller
	{


		[OutputCache(Duration = 30)]
		public async Task<ActionResult> Index()
		{

			var client = ClientFactory.CreateClient("api");

			var response = await client.GetAsync("api/products");

			if (response.IsSuccessStatusCode)
			{
				var productString = await response.Content.ReadAsStringAsync();
				var products = JsonConvert.DeserializeObject<IEnumerable<Product>>(productString);
				ViewBag.Products = products;
			}


			return View();
		}

		public ActionResult About()
		{
			return View();
		}

        [OutputCache(Duration = 60)]
        public async Task<ActionResult> Product(int id)
		{
            var client = ClientFactory.CreateClient("api");

            var response = await client.GetAsync($"api/products/{id}");

            if (response.IsSuccessStatusCode)
            {
                var productString = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<Product>(productString);
				var reviewSummary = await SummaryService.SummarizeReviews(products);
                ViewBag.Product = products;
                ViewBag.ReviewSummary = reviewSummary.Text;
            }

			return View();
        }
	}
}