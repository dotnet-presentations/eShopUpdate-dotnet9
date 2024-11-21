using Newtonsoft.Json;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace eShopUpdate.Entities
{

	public class Product
	{
		[JsonPropertyName("id")]
		public int Id { get; set; }

		[JsonPropertyName("name")]
		public string Name { get; set; }

		[JsonPropertyName("description")]
		public string Description { get; set; }

		[JsonPropertyName("price")]
		public decimal Price { get; set; }

		[JsonPropertyName("imageUrl")]
		public string ImageUrl { get; set; }

        [JsonPropertyName("reviews")]
        public List<Review> Reviews { get; set; }
    }

    public class Review
    {
        public string Text { get; set; }

        public float Rating { get; set; }

        public string Sentiment { get; set; }

        public List<string> Keywords { get; set; }
    }
}