using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;

namespace dotNetDataBaseApp
{
    public class MySqlProductDal : IProductDal
    {
        private MySqlConnection GetMySqlConnection()
        {
            string connectionString = @"server=localhost;port=3306;database=northwind;user=root;password=mysql1234";
            return new MySqlConnection(connectionString);
        }
        public int Create(Product p)
        {
            int result = 0;
            using (MySqlConnection connection = GetMySqlConnection())
            {
                try
                {
                    connection.Open();
                    string sql = "insert into products(product_name,list_price,discontinued) values (@productName,@listPrice,@discontinued)";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.Add("@productName", MySqlDbType.String).Value = p.Name;
                    command.Parameters.AddWithValue("@listPrice", p.Price);
                    command.Parameters.AddWithValue("@discontinued", 1);

                    result = command.ExecuteNonQuery();

                    Console.WriteLine($"{result}");
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
            return result;
        }

        public int Delete(int product_id)
        {
            int result = 0;
            using (var connection = GetMySqlConnection())
            {
                try
                {
                    connection.Open();
                    string sql = "delete from products where id=@id";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@id", product_id);

                    result = command.ExecuteNonQuery();
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
            return result;
        }

        public List<Product> GetAllProducts()
        {
            List<Product> products = null;
            using (var connection = GetMySqlConnection())
            {
                try
                {
                    connection.Open();
                    string sql = "select * from products";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    MySqlDataReader reader = command.ExecuteReader();
                    products = new List<Product>();

                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductId = int.Parse(reader["id"].ToString()),
                            Name = reader["product_name"].ToString(),
                            Price = double.Parse(reader["list_price"]?.ToString())
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
            Product product = null;
            using (var connection = GetMySqlConnection())
            {
                try
                {
                    connection.Open();
                    /******************/
                    //sql injection
                    //string sql = "select * from products where id="+id;
                    /******************/
                    string sql = "select * from products where id=@productid";

                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.Add("@productid", MySqlDbType.Int32).Value = id;

                    MySqlDataReader reader = command.ExecuteReader();
                    reader.Read();

                    if (reader.HasRows)
                    {
                        product = new Product();
                        product.ProductId = int.Parse(reader["id"].ToString());
                        product.Name = reader["product_name"].ToString();
                        product.Price = double.Parse(reader["list_price"]?.ToString());
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
            return product;
        }

        public int Update(Product p)
        {


            int result = 0;
            using (var connection = GetMySqlConnection())
            {
                try
                {
                    connection.Open();
                    string sql = "update products SET product_name=@productName, list_price=@listPrice WHERE id=@id ";
                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.AddWithValue("@productName", p.Name);
                    command.Parameters.AddWithValue("@listPrice", p.Price);
                    command.Parameters.AddWithValue("@id", p.ProductId);

                    result = command.ExecuteNonQuery();
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
            return result;
        }

        public List<Product> Find(string productName)
        {
            List<Product> products = null;
            using (var connection = GetMySqlConnection())
            {
                try
                {
                    connection.Open();
                    /******************/
                    //sql injection
                    //string sql = "select * from products where id="+id;
                    /******************/
                    string sql = "select * from products where product_name LIKE @productName";

                    MySqlCommand command = new MySqlCommand(sql, connection);
                    command.Parameters.Add("@productName", MySqlDbType.String).Value = "%" + productName + "%";

                    MySqlDataReader reader = command.ExecuteReader();
                    products = new List<Product>();

                    while (reader.Read())
                    {
                        products.Add(new Product
                        {
                            ProductId = int.Parse(reader["id"].ToString()),
                            Name = reader["product_name"].ToString(),
                            Price = double.Parse(reader["list_price"]?.ToString())
                        });
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

        public int Count()
        {
            int count = 0;
            using (var connection = GetMySqlConnection())
            {
                try
                {
                    connection.Open();
                    string sql = "select count(*) from products";

                    MySqlCommand command = new MySqlCommand(sql, connection);
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        count = Convert.ToInt32(result);
                    }
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
            return count;
        }
    }

}