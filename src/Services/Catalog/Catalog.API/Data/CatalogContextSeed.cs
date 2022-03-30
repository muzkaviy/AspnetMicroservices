using System;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
	public class CatalogContextSeed
	{
		public static async Task SeedData(IMongoCollection<Product> productCollection)
        {
			bool existProduct = productCollection.Find(p => true).Any();

            if (!existProduct)
            {
                await productCollection.InsertManyAsync(GetPreconfiguredProducts());
            }
        }

        private static IEnumerable<Product> GetPreconfiguredProducts()
        {
            var products = new List<Product> {
                new Product
                {
                    Id = "602d2149e773f2a3990b47f5",
                    Name = "HTC U11+ Plus",
                    Summary = "Summary",
                    Description = "Description",
                    ImageFile = "product-5.png",
                    Price = 380.00M,
                    Category = "Smart Phone"
                },
                new Product
                {
                    Id = "602d2149e773f2a3990b47f6",
                    Name = "Xiaomi Mi 9",
                    Summary = "Summary",
                    Description = "Description",
                    ImageFile = "product-6.png",
                    Price = 470.00M,
                    Category = "White Appliances"
                },
                new Product
                {
                    Id = "602d2149e773f2a3990b47f7",
                    Name = "Huawei Plus",
                    Summary = "Summary",
                    Description = "Description",
                    ImageFile = "product-6.png",
                    Price = 650.00M,
                    Category = "White Appliances"
                }
            };

            return products;
        }
    }
}

