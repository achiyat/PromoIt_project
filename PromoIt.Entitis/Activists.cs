using PromoIt.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PromoIt.Entitis
{
    public class Activists
    {
        public Hashtable hash = new Hashtable();
        public Activist Activ = new Activist();

        // ייבוא נתונים - 1
        // Gives a command to DAL to create a connection with SQL for Import
        public object ImportData(string SqlQuery)
        {
            DAL.PromoItQuery.ImportDataFromDB(SqlQuery, ReadFromDb);
            return hash;
        }

        // ייבוא נתונים - 2
        // Imports the data from the database into the server
        public void ReadFromDb(SqlDataReader reader)
        {
            //Clear Hashtable Before Inserting Information From Sql Server
            hash.Clear();
            while (reader.Read())
            {
                Activist newActivist = new Activist();
                newActivist.IDactivist = reader.GetInt32(reader.GetOrdinal("IDactivist"));
                newActivist.NameActivist = reader.GetString(reader.GetOrdinal("NameActivist"));
                newActivist.EmailActivist = reader.GetString(reader.GetOrdinal("EmailActivist"));
                newActivist.AddressActivist = reader.GetString(reader.GetOrdinal("AddressActivist"));
                newActivist.phoneActivist = reader.GetString(reader.GetOrdinal("phoneActivist"));

                //Cheking If Hashtable contains the key
                if (hash.ContainsKey(newActivist.IDactivist))
                {
                    //key already exists
                }
                else
                {
                    //Filling a hashtable
                    hash.Add(newActivist.IDactivist, newActivist);
                }
            }
        }

        // ייצוא נתונים - 1
        // Gives a command to DAL to create a connection with SQL for Export
        public void ExportFromDB(string SqlQuery, Activist Class)
        {
            Activ = Class;
            DAL.PromoItQuery.InputToDB(SqlQuery, changeTheDB);
        }

        // ייצוא נתונים - 2
        // Exports the data from the server into the database
        public void changeTheDB(SqlCommand command)
        {
            command.Parameters.AddWithValue("@ID", Activ.IDactivist);
            command.Parameters.AddWithValue("@Name", Activ.NameActivist);
            command.Parameters.AddWithValue("@Email", Activ.EmailActivist);
            command.Parameters.AddWithValue("@Address", Activ.AddressActivist);
            command.Parameters.AddWithValue("@Phone", Activ.phoneActivist);
            command.ExecuteNonQuery();
        }
    }
}
