using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using WebApiWithDapper.Models;

namespace WebApiWithDapper.Data
{
    public class ProductRepository : IProductRepository
    {
        private readonly IConfiguration _configuration;
        private readonly string connectionString;
        private IDbConnection _connection { get { return new SqlConnection(connectionString); } }

        public ProductRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            //connectionString = _configuration.GetConnectionString("ConnectionString");
            connectionString = "Server=DESKTOP-6A045A9\\SQLEXPRESS;Database=WebApiDapper;Trusted_Connection=True;";
        }
        public async Task<Product> GetAsync(long id)
        {
            using (IDbConnection dbConnection = _connection)
            {
                string query = @"SELECT [Id] ,[CategoryId]
                                ,[Name]
                                ,[Description]
                                ,[Price]
                                ,[CreatedDate]
                                FROM [dbo].[Products]
                                WHERE [Id] = @Id";

                var product = await dbConnection.QueryFirstOrDefaultAsync<Product>(query, new { @Id = id });

                return product;
            }
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            using (IDbConnection dbConnection = _connection)
            {
                string query = @"SELECT [Id] ,[CategoryId]
                                ,[Name]
                                ,[Description]
                                ,[Price]
                                ,[CreatedDate]
                                FROM [dbo].[Products]";

                var product = await dbConnection.QueryAsync<Product>(query);

                return product;
            }
        }

        public async Task AddAsync(Product product)
        {
            try
            {
                using (IDbConnection dbConnection = _connection)
                {
                    string query = @"INSERT INTO [dbo].[Products] (
                                [CategoryId],
                                [Name],
                                [Description],
                                [Price]) VALUES (
                                @CategoryId,
                                @Name,
                                @Description,
                                @Price)";

                    await dbConnection.ExecuteAsync(query, product);

                }
            }
            catch (Exception excp)
            {
                // (excp.Message); 
            }

        }

        public bool DeleteAsync(long id)
        {
            using (IDbConnection dbConnection = _connection)
            {
                int rowsAffected = dbConnection.Execute(@"DELETE FROM [dbo].[Products] WHERE Id = @Id", new { Id = id });

                if (rowsAffected > 0)
                {
                    return true;
                }
                return false;
            }
        }

        public async Task UpdateAsync(Product product)
        {
            using (IDbConnection dbConnection = _connection)
            {
                int result = await dbConnection.ExecuteAsync("UPDATE [dbo].[Products] SET [CategoryId] = @CategoryId, [Name] = @Name, [Description] = @Description, [Price] = @Price WHERE Id = " + product.Id, product);
            }
        }
    }
}
