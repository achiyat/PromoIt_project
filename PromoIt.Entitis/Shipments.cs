using PromoIt.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PromoIt.Entitis
{
    public class Shipments
    {
        Shipping shipping = new Shipping();
        public Hashtable hash = new Hashtable();

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
                Shipping GetShipping = new Shipping();
                GetShipping.IDShipments = reader.GetInt32(reader.GetOrdinal("IDShipments"));
                GetShipping.donated = reader.GetBoolean(reader.GetOrdinal("donated"));
                GetShipping.bought = reader.GetBoolean(reader.GetOrdinal("bought"));
                
                GetShipping.IDProduct = reader.GetInt32(reader.GetOrdinal("IDProduct"));
                GetShipping.ProductName = reader.GetString(reader.GetOrdinal("ProductName"));
                GetShipping.Price = reader.GetInt32(reader.GetOrdinal("Price"));
                GetShipping.Inventory = reader.GetInt32(reader.GetOrdinal("Inventory"));
                GetShipping.SelectedProduct = reader.GetBoolean(reader.GetOrdinal("SelectedProduct"));
                GetShipping.StatusProduct = reader.GetBoolean(reader.GetOrdinal("StatusProduct"));

                GetShipping.IDcampaign = reader.GetInt32(reader.GetOrdinal("IDcampaign"));
                GetShipping.NameCampaign = reader.GetString(reader.GetOrdinal("NameCampaign"));
                GetShipping.IDassn = reader.GetInt32(reader.GetOrdinal("IDassn"));
                GetShipping.NameAssn = reader.GetString(reader.GetOrdinal("NameAssn"));

                GetShipping.EmailAssn = reader.GetString(reader.GetOrdinal("EmailAssn"));
                GetShipping.Fundraising = reader.GetInt32(reader.GetOrdinal("Fundraising"));
                

                GetShipping.linkURL = reader.GetString(reader.GetOrdinal("linkURL"));
                GetShipping.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
                GetShipping.SelectedCampaign = reader.GetBoolean(reader.GetOrdinal("SelectedCampaign"));
                GetShipping.StatusCampaign = reader.GetBoolean(reader.GetOrdinal("StatusCampaign"));

                GetShipping.IDCompany = reader.GetInt32(reader.GetOrdinal("IDCompany"));
                GetShipping.NameCompany = reader.GetString(reader.GetOrdinal("NameCompany"));
                GetShipping.OwnerCompany = reader.GetString(reader.GetOrdinal("OwnerCompany"));
                GetShipping.EmailCompany = reader.GetString(reader.GetOrdinal("EmailCompany"));
                GetShipping.PhoneCompany = reader.GetString(reader.GetOrdinal("PhoneCompany"));


                GetShipping.IDactivist = reader.GetInt32(reader.GetOrdinal("IDactivist"));
                GetShipping.NameActivist = reader.GetString(reader.GetOrdinal("NameActivist"));
                GetShipping.EmailActivist = reader.GetString(reader.GetOrdinal("EmailActivist"));
                GetShipping.AddressActivist = reader.GetString(reader.GetOrdinal("AddressActivist"));
                GetShipping.phoneActivist = reader.GetString(reader.GetOrdinal("phoneActivist"));
                GetShipping.MoneyActivist = reader.GetInt32(reader.GetOrdinal("MoneyActivist"));

                //Cheking If Hashtable contains the key
                if (hash.ContainsKey(GetShipping.IDShipments))
                {
                    //key already exists
                }
                else
                {
                    //Filling a hashtable
                    hash.Add(GetShipping.IDShipments, GetShipping);
                }
            }
        }

        // ייצוא נתונים - 1
        // Gives a command to DAL to create a connection with SQL for Export
        public void ExportFromDB(string SqlQuery, Shipping Class)
        {
            shipping = Class;
            DAL.PromoItQuery.InputToDB(SqlQuery, changeTheDB);
        }

        // ייצוא נתונים - 2
        // Exports the data from the server into the database
        public void changeTheDB(SqlCommand command)
        {
            //@donated,@bought,@IDProd,@Name,@Price,@Inventory,@SelectedProd,@StatusProd,@IDcamp,@Namecamp,@IDAssn,@NameAssn,@EmailAssn,@Fund,@Link,@Hashtag,@Selected,@StatusCamp,@IDCom,@NameCom,@Owner,@EmailComp,@Phone,@IDActiv,@NameActiv,@EmailActiv,@Address,@PhoneActiv,@Money
            command.Parameters.AddWithValue("@ID", shipping.IDShipments);
            command.Parameters.AddWithValue("@donated", shipping.donated);
            command.Parameters.AddWithValue("@bought", shipping.bought);

            command.Parameters.AddWithValue("@IDProd", shipping.IDProduct);
            command.Parameters.AddWithValue("@Name", shipping.ProductName);
            command.Parameters.AddWithValue("@Price", shipping.Price);
            command.Parameters.AddWithValue("@Inventory", shipping.Inventory);
            command.Parameters.AddWithValue("@SelectedProd", shipping.SelectedProduct);
            command.Parameters.AddWithValue("@StatusProd", shipping.StatusProduct);

            command.Parameters.AddWithValue("@IDcamp", shipping.IDcampaign);
            command.Parameters.AddWithValue("@Namecamp", shipping.NameCampaign);
            command.Parameters.AddWithValue("@IDAssn", shipping.IDassn);
            command.Parameters.AddWithValue("@NameAssn", shipping.NameAssn);

            command.Parameters.AddWithValue("@EmailAssn", shipping.EmailAssn);
            command.Parameters.AddWithValue("@Fund", shipping.Fundraising);

            command.Parameters.AddWithValue("@Link", shipping.linkURL);
            command.Parameters.AddWithValue("@Hashtag", shipping.Hashtag);
            command.Parameters.AddWithValue("@Selected", shipping.SelectedCampaign);
            command.Parameters.AddWithValue("@StatusCamp", shipping.StatusCampaign);

            command.Parameters.AddWithValue("@IDCom", shipping.IDCompany);
            command.Parameters.AddWithValue("@NameCom", shipping.NameCompany);
            command.Parameters.AddWithValue("@Owner", shipping.OwnerCompany);
            command.Parameters.AddWithValue("@EmailComp", shipping.EmailCompany);
            command.Parameters.AddWithValue("@Phone", shipping.PhoneCompany);


            command.Parameters.AddWithValue("@IDActiv", shipping.IDactivist);
            command.Parameters.AddWithValue("@NameActiv", shipping.NameActivist);
            command.Parameters.AddWithValue("@EmailActiv", shipping.EmailActivist);
            command.Parameters.AddWithValue("@Address", shipping.AddressActivist);
            command.Parameters.AddWithValue("@PhoneActiv", shipping.phoneActivist);
            command.Parameters.AddWithValue("@Money", shipping.MoneyActivist);
            command.ExecuteNonQuery();
        }
    }
}
