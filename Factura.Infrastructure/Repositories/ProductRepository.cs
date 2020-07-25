using Factura.Core.Entities;
using Factura.Core.Interfaces;
using Factura.Infrastructure.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Factura.Infrastructure.Repositories
{
    public class ProductRepository : IRepository<Product>
    {
        #region Atributtes
        private Connection _Conn;
        private MySqlCommand _Command;
        #endregion
        #region Constructor
        public ProductRepository()
        {
            _Conn = Connection.IsConnected();
        }
        #endregion
        #region Methods
        public void Create(Product entity)
        {
            string Create = "INSERT INTO Product(CreatedTime,  CreatedUserId, Name, Description, Unit, Price, Stock) " +
                  "VALUES (" + entity.CreatedTime + "," + entity.CreatedUserId + ",'" + entity.Name + "','" + entity.Description + "','" + entity.Unit + "'," + entity.Price + "," + entity.Stock + ")";
            try
            {
                _Command = new MySqlCommand(Create, _Conn.GetConn());
                _Conn.GetConn().Open();
                _Command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _Conn.GetConn().Close();
                _Conn.CloseConnection();
            }
        }

        public bool Delete(int id)
        {
            bool IsDelete = false;
            string Delete = "DELETE FROM Product WHERE Id='" + id + "'";

            try
            {
                _Command = new MySqlCommand(Delete, _Conn.GetConn());
                _Conn.GetConn().Open();
                if (_Command.ExecuteNonQuery() > 0)
                {
                    IsDelete = true;
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _Conn.GetConn().Close();
                _Conn.CloseConnection();
            }
            return IsDelete;
        }

        public Product FindbyId(int id)
        {
            Product _product = new Product();
            string Find = "SELECT Id,Name, Description, Unit, Price, Stock FROM Product WHERE Id='" + id + "'";
            try
            {

                _Command = new MySqlCommand(Find, _Conn.GetConn());
                _Conn.GetConn().Open();
                MySqlDataReader read = _Command.ExecuteReader();

                if (read.Read())
                {
                    _product.Id = Convert.ToInt32(read[0].ToString());
                    _product.Name = read[1].ToString();
                    _product.Description = read[2].ToString();
                    _product.Unit = read[3].ToString();
                    _product.Price = Convert.ToDouble(read[4].ToString());
                    _product.Stock = Convert.ToDouble(read[5].ToString());
                    
                }
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _Conn.GetConn().Close();
                _Conn.CloseConnection();
            }
            return _product;
        }

        public List<Product> GetAll()
        {
            List<Product> products = new List<Product>();
            string All = "SELECT Id,Name, Description, Unit, Price, Stock FROM Product";
            try
            {

                _Command = new MySqlCommand(All, _Conn.GetConn());
                _Conn.GetConn().Open();
                MySqlDataReader read = _Command.ExecuteReader();

                while (read.Read())
                {
                    Product _product = new Product
                    {
                        Id = Convert.ToInt32(read[0].ToString()),
                        Name = read[1].ToString(),
                        Description = read[2].ToString(),
                        Unit = read[3].ToString(),
                        Price = Convert.ToDouble(read[4].ToString()),
                        Stock = Convert.ToDouble(read[5].ToString()),

                };
                    products.Add(_product);
                };
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _Conn.GetConn().Close();
                _Conn.CloseConnection();
            }
            return products;
        }

        public int GetLastId()
        {
            int Id = 0;
            string LastId = "SELECT MAX(Id) FROM Product";

            try
            {

                _Command = new MySqlCommand(LastId, _Conn.GetConn());
                _Conn.GetConn().Open();
                MySqlDataReader read = _Command.ExecuteReader();
                Id = Convert.ToInt32(read[0].ToString());

            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _Conn.GetConn().Close();
                _Conn.CloseConnection();
            }
            return Id;
        }

        public void Update(Product entity)
        {
            string Update = "UPDATE Product SET Name='" + entity.Name + "', Description='" + entity.Description + "', Unit='" + entity.Unit + "', Price=" + entity.Price + ", Stock=" + entity.Stock + ", ModifiedUserId=" + entity.ModifiedUserId + " WHERE id='" + entity.Id + "' ";
            try
            {
                _Command = new MySqlCommand(Update, _Conn.GetConn());
                _Conn.GetConn().Open();
                _Command.ExecuteNonQuery();
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                _Conn.GetConn().Close();
                _Conn.CloseConnection();
            }
        }
        #endregion

    }
}
