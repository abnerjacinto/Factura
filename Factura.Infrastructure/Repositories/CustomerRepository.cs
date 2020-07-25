using Factura.Core.Entities;
using Factura.Core.Interfaces;
using System;
using System.Collections.Generic;
using MySql.Data.MySqlClient;
using Factura.Infrastructure.Data;

namespace Factura.Infrastructure.Repositories
{
    public class CustomerRepository : IRepository<Customer>
    {
        #region atributes
        private Connection _Conn;
        private MySqlCommand _Command;
        #endregion
        #region Constructor
        public CustomerRepository()
        {
            _Conn = Connection.IsConnected();
        }
        #endregion
        #region Methods
        public void Create(Customer entity)
        {
            string Create = "INSERT INTO Customer(CreatedTime,  CreatedUserId, FirstName, LastName, NIT, Address, Phone, Email) " +
                  "VALUES (" + entity.CreatedTime + "," + entity.CreatedUserId + ",'" + entity.FirstName + "','" + entity.LastName + "','" + entity.NIT + "','" + entity.Address + "','" + entity.Phone + "','" + entity.Email + "')";
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
            string Delete = "DELETE FROM Customer WHERE id='" + id + "'";

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

        public Customer FindbyId(int id)
        {
            Customer _customer = new Customer();
            string Find = "SELECT Id, FirstName, LastName, NIT, Address, Phone, Email FROM Customer WHERE id='" + id + "'";
            try
            {

                _Command = new MySqlCommand(Find, _Conn.GetConn());
                _Conn.GetConn().Open();
                MySqlDataReader read = _Command.ExecuteReader();

                if (read.Read())
                {
                    _customer.Id = Convert.ToInt32(read[0].ToString());
                    _customer.FirstName = read[1].ToString();
                    _customer.LastName = read[2].ToString();
                    _customer.NIT = read[3].ToString();
                    _customer.Address = read[4].ToString();
                    _customer.Phone = read[5].ToString();
                    _customer.Email = read[6].ToString();
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
            return _customer;
        }

        public List<Customer> GetAll()
        {
            List<Customer> customers = new List<Customer>();
            string All = "SELECT Id, FirstName, LastName, NIT, Address, Phone, Email FROM Customer";
            try
            {

                _Command = new MySqlCommand(All, _Conn.GetConn());
                _Conn.GetConn().Open();
                MySqlDataReader read = _Command.ExecuteReader();

                while (read.Read())
                {
                    Customer _customer = new Customer
                    {
                        Id = Convert.ToInt32(read[0].ToString()),
                        FirstName = read[1].ToString(),
                        LastName = read[2].ToString(),
                        NIT = read[3].ToString(),
                        Address = read[4].ToString(),
                        Phone = read[5].ToString(),
                        Email = read[6].ToString(),

                    };
                    customers.Add(_customer);
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
            return customers;
        }

        public int GetLastId()
        {
            int Id = 0;
            string LastId = "SELECT MAX(Id) FROM Customer";
            
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

        public void Update(Customer entity)
        {
            string Update = "UPDATE Customer SET FirstName='" + entity.FirstName + "', LastName='" + entity.LastName + "', NIT='" + entity.NIT + "', Address='" + entity.Address + "', Phone='" + entity.Phone + "', Email='" + entity.Email + "', ModifiedTime=" + entity.ModifiedTime + ", ModifiedUserId=" + entity.ModifiedUserId + " WHERE id='" + entity.Id + "' ";
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
        public Customer FindByNIT(string nit) {
            Customer _customer = new Customer();
            string Find = "SELECT Id, FirstName, LastName, NIT, Address, Phone, Email FROM Customer WHERE NIT='" + nit + "'";
            try
            {

                _Command = new MySqlCommand(Find, _Conn.GetConn());
                _Conn.GetConn().Open();
                MySqlDataReader read = _Command.ExecuteReader();

                if (read.Read())
                {
                    _customer.Id = Convert.ToInt32(read[0].ToString());
                    _customer.FirstName = read[1].ToString();
                    _customer.LastName = read[2].ToString();
                    _customer.NIT = read[3].ToString();
                    _customer.Address = read[4].ToString();
                    _customer.Phone = read[5].ToString();
                    _customer.Email = read[6].ToString();
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
            return _customer;
        }
        #endregion

    }
}
