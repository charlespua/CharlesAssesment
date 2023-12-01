using CharlesAssesment.Models;
using System.Data.SqlClient;

namespace CharlesAssesment.Service
{
    public class ProductService : IProductService
    {
        private readonly IConfiguration _configuration;

        public ProductService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        private SqlConnection GetConnection()
        {
            var sqlConnectionStringBuilder = new SqlConnectionStringBuilder(_configuration.GetConnectionString("SQLConnection"));
            return new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
        }

        public async Task<IList<Product>> GetProductsAsync()
        {
            var products = new List<Product>();
            using (var sqlConn = GetConnection())
            {
                sqlConn.Open();
                var sql = "Select * From Product";
                var cmd = new SqlCommand(sql, sqlConn);

                using (SqlDataReader dr = await cmd.ExecuteReaderAsync().ConfigureAwait(false))
                {
                    while (await dr.ReadAsync().ConfigureAwait(false))
                    {
                        products.Add(new Product
                        {
                            Id = dr.GetInt32(0),
                            Name = dr.GetString(1),
                            Description = dr.GetString(2),
                        });
                    }
                }
            }
            return products;
        }
    }
}
