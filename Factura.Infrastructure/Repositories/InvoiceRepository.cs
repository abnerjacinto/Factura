using Factura.Core.Entities;
using Factura.Core.Interfaces;
using Factura.Infrastructure.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Factura.Infrastructure.Repositories
{
    public class InvoiceRepository : IRepository<Invoice>
    {
        #region Atributtes
        private Connection _Conn;
        private MySqlCommand _Command;
        #endregion
        #region Constructor
        public InvoiceRepository()
        {
            _Conn = Connection.IsConnected();
        }
        #endregion
        #region Methods
        public void Create(Invoice entity)
        {
            string Create = "INSERT INTO Invoice(CreatedTime,  CreatedUserId,  CustomerId, Serie, InvoiceDate, InvoiceNumber, Tax, Total) " +
                  "VALUES (" + entity.CreatedTime + "," + entity.CreatedUserId + "," + entity.CustmerId + ",'" + entity.Serie + "','" + entity.InvoiceDate + "','" + entity.InvoiceNumber + "'," + entity.Tax + "," + entity.Total + ")";
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
            string Delete = "DELETE FROM Invoice WHERE Id='" + id + "'";

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

        public Invoice FindbyId(int id)
        {
            Invoice _invoice = new Invoice();
            string Find = "SELECT Id,InvoiceDate, CustomerId, Serie, InvoiceNumber, Tax, Total, Status FROM Invoice WHERE Id='" + id + "'";
            try
            {

                _Command = new MySqlCommand(Find, _Conn.GetConn());
                _Conn.GetConn().Open();
                MySqlDataReader read = _Command.ExecuteReader();

                if (read.Read())
                {
                    _invoice.Id = Convert.ToInt32(read[0].ToString());
                    _invoice.InvoiceDate = read[1].ToString();
                    _invoice.CustmerId = Convert.ToInt32(read[2].ToString());
                    _invoice.Serie = read[3].ToString();
                    _invoice.InvoiceNumber = read[4].ToString();
                    _invoice.Tax = Convert.ToDouble(read[5].ToString());
                    _invoice.Total = Convert.ToDouble(read[6].ToString());
                    _invoice.Status = Convert.ToInt32(read[7].ToString());
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
            return _invoice;
        }

        public List<Invoice> GetAll()
        {
            List<Invoice> invoices = new List<Invoice>();
            string All = "SELECT Id,InvoiceDate, CustomerId, Serie, InvoiceNumber, Tax, Total,Status FROM Invoice";
            try
            {

                _Command = new MySqlCommand(All, _Conn.GetConn());
                _Conn.GetConn().Open();
                MySqlDataReader read = _Command.ExecuteReader();

                while (read.Read())
                {
                    Invoice _invoice = new Invoice
                    {
                        Id = Convert.ToInt32(read[0].ToString()),
                        InvoiceDate = read[1].ToString(),
                        CustmerId = Convert.ToInt32(read[2].ToString()),
                        Serie = read[3].ToString(),
                        InvoiceNumber = read[4].ToString(),
                        Tax = Convert.ToDouble(read[5].ToString()),
                        Total = Convert.ToDouble(read[6].ToString()),
                        Status = Convert.ToInt32(read[7].ToString())

                    };
                    invoices.Add(_invoice);
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
            return invoices;
        }

        public int GetLastId()
        {
            int Id = 0;
            string LastId = "SELECT MAX(Id) FROM Invoice";

            try
            {

                _Command = new MySqlCommand(LastId, _Conn.GetConn());
                _Conn.GetConn().Open();
                MySqlDataReader read = _Command.ExecuteReader();
                if (read.Read()) {
                    Id = Convert.ToInt32(read["MAX(Id)"].ToString());
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
            return Id;
        }

        public void Update(Invoice entity)
        {
            string Update = "UPDATE Invoice SET Status=" + entity.Status + ", InvoiceDate='" + entity.InvoiceDate + "', CustomerId=" + entity.CustmerId + ", Serie='" + entity.Serie + "', InvoiceNumber='" + entity.InvoiceNumber + "', Tax=" + entity.Tax + ", Total=" + entity.Total + ", ModifiedTime=" + entity.ModifiedTime + ", ModifiedUserId=" + entity.ModifiedUserId + " WHERE id='" + entity.Id + "' ";
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
