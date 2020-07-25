using Factura.Core.Entities;
using Factura.Core.Interfaces;
using Factura.Infrastructure.Data;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace Factura.Infrastructure.Repositories
{
    public class UserRepository : IRepository<User>
    {
        #region Atributtes
        private Connection _Conn;
        private MySqlCommand _Command;
        #endregion
        #region Constructor
        public UserRepository()
        {
            _Conn = Connection.IsConnected();
        }
        #endregion
        #region Methods
        public void Create(User entity)
        {
            string Create = "INSERT INTO User(CreatedTime,  CreatedUserId, FirstName, LastName, UserName, Password, RolId) " +
                  "VALUES (" + entity.CreatedTime + "," + entity.CreatedUserId + ",'" + entity.FirstName + "','" + entity.LastName + "','" + entity.UserName + "','" + entity.Password + "'," + entity.RolId + ")";
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
            string Delete = "DELETE FROM User WHERE Id='" + id + "'";

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

        public User FindbyId(int id)
        {
            User _user = new User();
            string Find = "SELECT Id,FirstName, LastName, UserName, Password, RolId WHERE Id='" + id + "'";
            try
            {

                _Command = new MySqlCommand(Find, _Conn.GetConn());
                _Conn.GetConn().Open();
                MySqlDataReader read = _Command.ExecuteReader();

                if (read.Read())
                {
                    _user.Id = Convert.ToInt32(read[0].ToString());
                    _user.FirstName = read[1].ToString();
                    _user.LastName = read[2].ToString();
                    _user.UserName = read[3].ToString();
                    _user.Password = read[4].ToString();
                    _user.RolId = Convert.ToInt32(read[5].ToString());
                    
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
            return _user;
        }

        public List<User> GetAll()
        {
            List<User> users = new List<User>();
            string All = "SELECT Id,FirstName, LastName, UserName, Password, RolId FROM User";
            try
            {

                _Command = new MySqlCommand(All, _Conn.GetConn());
                _Conn.GetConn().Open();
                MySqlDataReader read = _Command.ExecuteReader();

                while (read.Read())
                {
                    User _user = new User
                    {
                        Id = Convert.ToInt32(read[0].ToString()),
                        FirstName = read[1].ToString(),
                        LastName = read[2].ToString(),
                        UserName = read[3].ToString(),
                        Password = read[4].ToString(),
                        RolId = Convert.ToInt32(read[5].ToString()),

                };
                    users.Add(_user);
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
            return users;
        }

        public int GetLastId()
        {
            int Id = 0;
            string LastId = "SELECT MAX(Id) FROM User";

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

        public void Update(User entity)
        {
            string Update = "UPDATE User SET FirstName='" + entity.FirstName + "', LastName='" + entity.LastName + "', UserName='" + entity.UserName + "', Password='" + entity.Password + "', RolId=" + entity.RolId + ", ModifiedTime=" + entity.ModifiedTime + ", ModifiedUserId=" + entity.ModifiedUserId + " WHERE id='" + entity.Id + "' ";
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
        public User FindbyUserName(string userName)
        {
            User _user = new User();
            string Find = "SELECT Id,FirstName, LastName, UserName, Password, RolId FROM User WHERE UserName='" + userName + "'";
            try
            {

                _Command = new MySqlCommand(Find, _Conn.GetConn());
                _Conn.GetConn().Open();
                MySqlDataReader read = _Command.ExecuteReader();

                if (read.Read())
                {
                    _user.Id = Convert.ToInt32(read[0].ToString());
                    _user.FirstName = read[1].ToString();
                    _user.LastName = read[2].ToString();
                    _user.UserName = read[3].ToString();
                    _user.Password = read[4].ToString();
                    _user.RolId = Convert.ToInt32(read[5].ToString());

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
            return _user;
        }
        #endregion

    }
}
