using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace dotNetDataBaseApp
{
     public class MsSqlProductDal : IProductDal
    {
        private SqlConnection GetMsSqlConnection()
        {
            string connectionString = @".\SQLEXPRESS;Initial Catalog=Northwind;Integrated Security=SSPI";
            return new SqlConnection(connectionString);
        }
        public int Create(Product p)
        {
            throw new NotImplementedException();
        }

        public int Delete(int product_id)
        {
            throw new NotImplementedException();
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = null;
            using (var connection = GetMsSqlConnection())
            {
                try
                {
                    connection.Open();
                    string sql = "select * from products";
                    SqlCommand command = new SqlCommand(sql, connection);
                    SqlDataReader reader = command.ExecuteReader();
                    products = new List<Product>();

                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductId = int.Parse(reader["ProductID"].ToString()),
                            Name = reader["ProductName"].ToString(),
                            Price = double.Parse(reader["UnitPrice"]?.ToString())
                        }
                    );
                    }
                    reader.Close();

                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            return products;
        }

        public Product GetProductById(int id)
        {
            throw new NotImplementedException();
        }

        public int Update(Product p)
        {
            throw new NotImplementedException();
        }

        public List<Product> Find(string productName)
        {
            throw new NotImplementedException();
        }

        public int Count()
        {
            throw new NotImplementedException();
        }
    }
}