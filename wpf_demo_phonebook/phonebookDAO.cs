using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace wpf_demo_phonebook
{
    class PhonebookDAO
    {
        private DbConnection conn;

        public PhonebookDAO()
        {
            conn = new DbConnection();
        }

        /// <summary>
        /// Méthode permettant de rechercher un contact par nom
        /// </summary>
        /// <param name="_name">Nom de famille ou prénom</param>
        /// <returns>Une DataTable</returns>
        /// 

        public DataTable GetAll()
        {
            string _query =
                $"SELECT * " +
                $"FROM [Contacts] ";

            SqlParameter[] parameters = new SqlParameter[0];
            return conn.ExecuteSelectQuery(_query, parameters);
        }

        public void DeleteContact(int _id)
        {

            string _query =
                $"DELETE " +
                $"FROM [Contacts] " +
                $"WHERE ContactID LIKE @_id ";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@_id", SqlDbType.Int);
            parameters[0].Value = _id;

            conn.ExecutUpdateQuery(_query, parameters);

        }

        public int UpdateContact(ContactModel cm)
        {

            if(cm.ContactID == 0)
            {
                string _query =
                $"INSERT " +
                $"INTO [Contacts] (FirstName, LastName, Email, Phone, Mobile)" +
                $"VALUES (@firstName, @lastName, @email, @phone, @mobile)";

                SqlParameter[] parameters = new SqlParameter[5];

                parameters[0] = new SqlParameter("@firstName", SqlDbType.NVarChar);
                parameters[1] = new SqlParameter("@lastName", SqlDbType.NVarChar);
                parameters[2] = new SqlParameter("@email", SqlDbType.NVarChar);
                parameters[3] = new SqlParameter("@phone", SqlDbType.NVarChar);
                parameters[4] = new SqlParameter("@mobile", SqlDbType.NVarChar);

                parameters[0].Value = cm.FirstName;
                parameters[1].Value = cm.LastName;
                parameters[2].Value = cm.Email;
                parameters[3].Value = cm.Phone;
                parameters[4].Value = cm.Mobile;

                return conn.ExecutInsertQuery(_query, parameters);
            }
            else
            {

                string _query =
                $"UPDATE " +
                $"INTO [Contacts] (FirstName, LastName, Email, Phone, Mobile)" +
                $"VALUES (@firstname, @lastname, @email, @phone, @mobile)";

                SqlParameter[] parameters = new SqlParameter[5];

                return conn.ExecutInsertQuery(_query, parameters);
            }
            


            

        }
        public DataTable SearchByName(string _name)
        {
            string _query =
                $"SELECT * " +
                $"FROM [Contacts] " +
                $"WHERE FirstName LIKE @firstName OR LastName LIKE @lastName ";

            SqlParameter[] parameters = new SqlParameter[2];
            parameters[0] = new SqlParameter("@firstName", SqlDbType.NVarChar);
            parameters[0].Value = _name;

            parameters[1] = new SqlParameter("@lastName", SqlDbType.NVarChar);
            parameters[1].Value = _name;

            return conn.ExecuteSelectQuery(_query, parameters);
        }

        /// <summary>
        /// Méthode permettant de rechercher un contact par id
        /// </summary>
        /// <param name="_name">Nom de famille ou prénom</param>
        /// <returns>Une DataTable</returns>
        public DataTable SearchByID(int _id)
        {
            string _query =
                $"SELECT * " +
                $"FROM [Contacts] " +
                $"WHERE ContactID = @_id ";

            SqlParameter[] parameters = new SqlParameter[1];
            parameters[0] = new SqlParameter("@_id", SqlDbType.Int);
            parameters[0].Value = _id;

            return conn.ExecuteSelectQuery(_query, parameters);
        }
    }
}
