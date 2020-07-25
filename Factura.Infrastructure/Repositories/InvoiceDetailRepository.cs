using Factura.Core.Entities;
using Factura.Core.Interfaces;
using Factura.Infrastructure.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Factura.Infrastructure.Repositories
{
    public class InvoiceDetailRepository : IRepository<InvoiceDetail>
    {
        #region Atributtes
        private Connection _Conn;
        private MySqlCommand _Command;
        #endregion
        #region Constructor
        public InvoiceDetailRepository()
        {
            _Conn = Connection.IsConnected();
        }
        #endregion
        #region Methods
        public void Create(InvoiceDetail entity)
        {
            string Create = "INSERT INTO InvoiceDetail(CreatedTime, CreatedUserId, InvoiceId, ProductId, Price, Qty) " +
                  "VALUES (" + entity.CreatedTime + "," + entity.CreatedUserId + "," + entity.InvoiceId + "," + entity.ProductId + "," + entity.Price + "," + entity.Qty + ")";
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
            string Delete = "DELETE FROM InvoiceDetail WHERE id='" + id + "'";

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

        public InvoiceDetail FindbyId(int id)
        {
            InvoiceDetail _invoiceDetail = new InvoiceDetail();
            string Find = "SELECT Id, InvoiceId, ProductId, Price, Qty FROM InvoiceDetail WHERE id='" + id + "'";
            try
            {

                _Command = new MySqlCommand(Find, _Conn.GetConn());
                _Conn.GetConn().Open();
                MySqlDataReader read = _Command.ExecuteReader();

                if (read.Read())
                {
                    _invoiceDetail.Id = Convert.ToInt32(read[0].ToString());
                    _invoiceDetail.InvoiceId = Convert.ToInt32(read[1].ToString());
                    _invoiceDetail.ProductId = Convert.ToInt32(read[2].ToString());
                    _invoiceDetail.Price = Convert.ToDouble(read[3].ToString());
                    _invoiceDetail.Qty = Convert.ToDouble(read[4].ToString());
                    
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
            return _invoiceDetail;
        }

        public List<InvoiceDetail> GetAll()
        {
            List<InvoiceDetail> InvoiceDetails = new List<InvoiceDetail>();
            string All = "SELECT Id, InvoiceId, ProductId, Price, Qty FROM InvoiceDetail";
            try
            {

                _Command = new MySqlCommand(All, _Conn.GetConn());
                _Conn.GetConn().Open();
                MySqlDataReader read = _Command.ExecuteReader();

                while (read.Read())
                {
                    InvoiceDetail _invoiceDetail = new InvoiceDetail
                    {
                        Id = Convert.ToInt32(read[0].ToString()),
                        InvoiceId = Convert.ToInt32(read[1].ToString()),
                        ProductId = Convert.ToInt32(read[2].ToString()),
                        Price = Convert.ToDouble(read[3].ToString()),
                        Qty = Convert.ToDouble(read[4].ToString()),

                };
                    InvoiceDetails.Add(_invoiceDetail);
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
            return InvoiceDetails;
        }

        public int GetLastId()
        {
            int Id = 0;
            string LastId = "SELECT MAX(Id) FROM InvoiceDetail";

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

        public void Update(InvoiceDetail entity)
        {
            string Update = "UPDATE InvoiceDetail SET InvoiceId=" + entity.InvoiceId + ", ProductId=" + entity.ProductId + ", Price=" + entity.Price + ", Qty=" + entity.Qty + ", ModifiedTime=" + entity.ModifiedTime + ", ModifiedUserId=" + entity.ModifiedUserId + " WHERE id='" + entity.Id + "' ";
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
