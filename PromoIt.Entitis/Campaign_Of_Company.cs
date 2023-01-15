using PromoIt.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace PromoIt.Entitis
{
    public class Campaign_Of_Company
    {
        public Hashtable hash = new Hashtable();
        public CampaignOfCompany campaign = new CampaignOfCompany();

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
                CampaignOfCompany newCampaign = new CampaignOfCompany();
                newCampaign.IDcampaign = reader.GetInt32(reader.GetOrdinal("IDcampaign"));
                newCampaign.NameCampaign = reader.GetString(reader.GetOrdinal("NameCampaign"));
                newCampaign.IDassn = reader.GetInt32(reader.GetOrdinal("IDassn"));
                newCampaign.NameAssn = reader.GetString(reader.GetOrdinal("NameAssn"));

                newCampaign.EmailAssn = reader.GetString(reader.GetOrdinal("EmailAssn"));
                newCampaign.Fundraising = reader.GetInt32(reader.GetOrdinal("Fundraising"));


                newCampaign.linkURL = reader.GetString(reader.GetOrdinal("linkURL"));
                newCampaign.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
                newCampaign.SelectedCampaign = reader.GetBoolean(reader.GetOrdinal("SelectedCampaign"));
                newCampaign.StatusCampaign = reader.GetBoolean(reader.GetOrdinal("StatusCampaign"));

                newCampaign.IDCompany = reader.GetInt32(reader.GetOrdinal("IDCompany"));
                newCampaign.NameCompany = reader.GetString(reader.GetOrdinal("NameCompany"));
                newCampaign.OwnerCompany = reader.GetString(reader.GetOrdinal("OwnerCompany"));
                newCampaign.PhoneCompany = reader.GetString(reader.GetOrdinal("PhoneCompany"));
                newCampaign.EmailCompany = reader.GetString(reader.GetOrdinal("EmailCompany"));

                //Cheking If Hashtable contains the key
                if (hash.ContainsKey(newCampaign.NameCampaign))
                {
                    //key already exists
                }
                else
                {
                    //Filling a hashtable
                    hash.Add(newCampaign.NameCampaign, newCampaign);
                }
            }
        }


        // ייצוא נתונים - 1
        // Gives a command to DAL to create a connection with SQL for Export
        public void ExportFromDB(string SqlQuery, CampaignOfCompany Class)
        {
            campaign = Class;
            DAL.PromoItQuery.InputToDB(SqlQuery, changeTheDB);
        }

        // ייצוא נתונים - 2
        // Exports the data from the server into the database
        public void changeTheDB(SqlCommand command)
        {
            //@ID,@Name,@IDAssn,@NameAssn,@EmailAssn,@Fund,@Link,@Hashtag,@Selected,@Status,@IDCom,@NameCom,@Owner,@Phone,@EmailComp
            command.Parameters.AddWithValue("@ID", campaign.IDcampaign);
            command.Parameters.AddWithValue("@Name", campaign.NameCampaign);
            command.Parameters.AddWithValue("@IDAssn", campaign.IDassn);
            command.Parameters.AddWithValue("@NameAssn", campaign.NameAssn);

            command.Parameters.AddWithValue("@EmailAssn", campaign.EmailAssn);
            command.Parameters.AddWithValue("@Fund", campaign.Fundraising);

            command.Parameters.AddWithValue("@Link", campaign.linkURL);
            command.Parameters.AddWithValue("@Hashtag", campaign.Hashtag);
            command.Parameters.AddWithValue("@Selected", campaign.SelectedCampaign);
            command.Parameters.AddWithValue("@Status", campaign.StatusCampaign);

            command.Parameters.AddWithValue("@IDCom", campaign.IDCompany);
            command.Parameters.AddWithValue("@NameCom", campaign.NameCompany);
            command.Parameters.AddWithValue("@Owner", campaign.OwnerCompany);
            command.Parameters.AddWithValue("@Phone", campaign.PhoneCompany);
            command.Parameters.AddWithValue("@EmailComp", campaign.EmailCompany);
            command.ExecuteNonQuery();
        }
    }
}