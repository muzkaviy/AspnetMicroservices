using Npgsql;

namespace Discount.API.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static WebApplicationBuilder InitializeDatabase(this WebApplicationBuilder host, int? retry = 0)
        {
            var retryAvailability = retry.Value;
            var connString = host.Configuration.GetValue<string>("DatabaseSettings:ConnectionString");

            try
            {
                using var connection = new NpgsqlConnection(connString);
                connection.Open();

                using var command = new NpgsqlCommand { Connection = connection };
                command.CommandText = "DROP TABLE IF EXISTS coupon";
                command.ExecuteNonQuery();

                command.CommandText = @"
CREATE TABLE coupon
(
  Id          SERIAL PRIMARY KEY,
  ProductName VARCHAR(24) NOT NULL,
  Description TEXT,
  Amount      INT
)";
                command.ExecuteNonQuery();

                command.CommandText = @"
INSERT INTO coupon
       (ProductName,       Description,                Amount)
VALUES ('IPhone 13',       'IPhone 13 Discount',       150),
       ('Samsung Note 20', 'Samsung Note 20 Discount', 120)";
                command.ExecuteNonQuery();
                connection.Close();
            }
            catch (NpgsqlException ex)
            {
                if (retryAvailability < 50)
                {
                    retryAvailability++;
                    Thread.Sleep(2000);
                    host.InitializeDatabase(retryAvailability);
                }
            }

            return host;
        }
    }
}

