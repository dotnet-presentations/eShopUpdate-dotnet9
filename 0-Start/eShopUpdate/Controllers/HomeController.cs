using eShopUpdate.Entities;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace eShopUpdate.Controllers
{
	public class HomeController : Controller
	{
		public async Task<ActionResult> Index()
		{

			var baseAddress = ConfigurationManager.AppSettings["ApiAddress"];


			var client = new HttpClient()
			{
				BaseAddress = new Uri(baseAddress)
			};

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
	}
}