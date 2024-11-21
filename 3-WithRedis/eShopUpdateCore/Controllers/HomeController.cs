﻿using eShopUpdate.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;
using Newtonsoft.Json;

namespace eShopUpdate.Controllers
{
	public class HomeController(IHttpClientFactory ClientFactory) : Controller
	{


		[OutputCache(Duration = 300)]
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
	}
}