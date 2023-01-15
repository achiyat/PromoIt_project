using PromoIt.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PromoIt.Entitis
{
    public class DonatedProducts
    {
        public Hashtable hash = new Hashtable();
        public DonatedProduct DonatedProduct = new DonatedProduct();

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
                DonatedProduct Donated = new DonatedProduct();
                Donated.IDProduct = reader.GetInt32(reader.GetOrdinal("IDProduct"));
                Donated.ProductName = reader.GetString(reader.GetOrdinal("ProductName"));
                Donated.Price = reader.GetInt32(reader.GetOrdinal("Price"));
                Donated.Inventory = reader.GetInt32(reader.GetOrdinal("Inventory"));
                Donated.SelectedProduct = reader.GetBoolean(reader.GetOrdinal("SelectedProduct"));
                Donated.StatusProduct = reader.GetBoolean(reader.GetOrdinal("StatusProduct"));


                Donated.IDcampaign = reader.GetInt32(reader.GetOrdinal("IDcampaign"));
                Donated.NameCampaign = reader.GetString(reader.GetOrdinal("NameCampaign"));
                Donated.IDassn = reader.GetInt32(reader.GetOrdinal("IDassn"));
                Donated.NameAssn = reader.GetString(reader.GetOrdinal("NameAssn"));

                Donated.EmailAssn = reader.GetString(reader.GetOrdinal("EmailAssn"));
                Donated.Fundraising = reader.GetInt32(reader.GetOrdinal("Fundraising"));


                Donated.linkURL = reader.GetString(reader.GetOrdinal("linkURL"));
                Donated.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
                Donated.SelectedCampaign = reader.GetBoolean(reader.GetOrdinal("SelectedCampaign"));
                Donated.StatusCampaign = reader.GetBoolean(reader.GetOrdinal("StatusCampaign"));

                Donated.IDCompany = reader.GetInt32(reader.GetOrdinal("IDCompany"));
                Donated.NameCompany = reader.GetString(reader.GetOrdinal("NameCompany"));
                Donated.OwnerCompany = reader.GetString(reader.GetOrdinal("OwnerCompany"));
                Donated.PhoneCompany = reader.GetString(reader.GetOrdinal("PhoneCompany"));
                Donated.EmailCompany = reader.GetString(reader.GetOrdinal("EmailCompany"));

                //Cheking If Hashtable contains the key
                if (hash.ContainsKey(Donated.IDProduct))
                {
                    //key already exists
                }
                else
                {
                    //Filling a hashtable
                    hash.Add(Donated.IDProduct, Donated);
                }
            }
        }

        // ייצוא נתונים - 1
        // Gives a command to DAL to create a connection with SQL for Export
        public void ExportFromDB(string SqlQuery, DonatedProduct Class)
        {
            DonatedProduct = Class;
            DAL.PromoItQuery.InputToDB(SqlQuery, changeTheDB);
        }

        // ייצוא נתונים - 2
        // Exports the data from the server into the database
        public void changeTheDB(SqlCommand command)
        {
            //@Name,@Price,@Inventory,@SelectedProd,@StatusProd,@IDcamp,@Namecamp,@IDAssn,@NameAssn,@EmailAssn,@Fund,@Link,@Hashtag,@Selected,@StatusCamp,@IDCom,@NameCom,@Owner,@Phone,@EmailComp
            command.Parameters.AddWithValue("@ID", DonatedProduct.IDProduct);
            command.Parameters.AddWithValue("@Name", DonatedProduct.ProductName);
            command.Parameters.AddWithValue("@Price", DonatedProduct.Price);
            command.Parameters.AddWithValue("@Inventory", DonatedProduct.Inventory);
            command.Parameters.AddWithValue("@SelectedProd", DonatedProduct.SelectedProduct); 
            command.Parameters.AddWithValue("@StatusProd", DonatedProduct.StatusProduct);

            command.Parameters.AddWithValue("@IDcamp", DonatedProduct.IDcampaign);
            command.Parameters.AddWithValue("@Namecamp", DonatedProduct.NameCampaign);
            command.Parameters.AddWithValue("@IDAssn", DonatedProduct.IDassn);
            command.Parameters.AddWithValue("@NameAssn", DonatedProduct.NameAssn);

            command.Parameters.AddWithValue("@EmailAssn", DonatedProduct    .EmailAssn);
            command.Parameters.AddWithValue("@Fund", DonatedProduct.Fundraising);

            command.Parameters.AddWithValue("@Link", DonatedProduct.linkURL);
            command.Parameters.AddWithValue("@Hashtag", DonatedProduct.Hashtag);
            command.Parameters.AddWithValue("@Selected", DonatedProduct.SelectedCampaign);
            command.Parameters.AddWithValue("@StatusCamp", DonatedProduct.StatusCampaign);

            command.Parameters.AddWithValue("@IDCom", DonatedProduct.IDCompany);
            command.Parameters.AddWithValue("@NameCom", DonatedProduct.NameCompany);
            command.Parameters.AddWithValue("@Owner", DonatedProduct.OwnerCompany);
            command.Parameters.AddWithValue("@Phone", DonatedProduct.PhoneCompany);
            command.Parameters.AddWithValue("@EmailComp", DonatedProduct.EmailCompany);
            command.ExecuteNonQuery();
        }
    }
}
