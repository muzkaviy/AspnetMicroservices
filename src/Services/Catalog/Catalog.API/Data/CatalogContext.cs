using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContext : ICatalogContext
	{
        public CatalogContext(IConfiguration configuration)
        {
            var connStr = configuration.GetValue<string>("DatabaseSettings:ConnectionString");
            var client = new MongoClient(connStr);

            var dbName = configuration.GetValue<string>("DatabaseSettings:DatabaseName");
            var database = client.GetDatabase(dbName);

            var collectionName = configuration.GetValue<string>("DatabaseSettings:CollectionName");
            Products = database.GetCollection<Product>(collectionName);
        }

        public IMongoCollection<Product> Products { get; }
    }
}

