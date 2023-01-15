using PromoIt.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoIt.Entitis
{
    public class Campaign_Of_Activists
    {
        public Hashtable hash = new Hashtable();
        public CampaignActivist campaign = new CampaignActivist();

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
                CampaignActivist newCampaign = new CampaignActivist();
                newCampaign.IDcampaign = reader.GetInt32(reader.GetOrdinal("IDcampaign"));
                newCampaign.NameCampaign = reader.GetString(reader.GetOrdinal("NameCampaign"));
                newCampaign.IDassn = reader.GetInt32(reader.GetOrdinal("IDassn"));
                newCampaign.NameAssn = reader.GetString(reader.GetOrdinal("NameAssn"));

                newCampaign.EmailAssn = reader.GetString(reader.GetOrdinal("EmailAssn"));
                newCampaign.Fundraising = reader.GetInt32(reader.GetOrdinal("Fundraising"));
                newCampaign.MoneyActivist = reader.GetInt32(reader.GetOrdinal("MoneyActivist"));


                newCampaign.linkURL = reader.GetString(reader.GetOrdinal("linkURL"));
                newCampaign.Hashtag = reader.GetString(reader.GetOrdinal("Hashtag"));
                newCampaign.SelectedCampaign = reader.GetBoolean(reader.GetOrdinal("SelectedCampaign"));
                newCampaign.StatusCampaign = reader.GetBoolean(reader.GetOrdinal("StatusCampaign"));

                newCampaign.IDactivist = reader.GetInt32(reader.GetOrdinal("IDactivist"));
                newCampaign.NameActivist = reader.GetString(reader.GetOrdinal("NameActivist"));
                newCampaign.EmailActivist = reader.GetString(reader.GetOrdinal("EmailActivist"));
                newCampaign.AddressActivist = reader.GetString(reader.GetOrdinal("AddressActivist"));
                newCampaign.phoneActivist = reader.GetString(reader.GetOrdinal("phoneActivist"));

                //Cheking If Hashtable contains the key
                if (hash.ContainsKey(newCampaign.IDcampaign))
                {
                    //key already exists
                }
                else
                {
                    //Filling a hashtable
                    hash.Add(newCampaign.IDcampaign, newCampaign);
                }
            }
        }

        // ייצוא נתונים - 1
        // Gives a command to DAL to create a connection with SQL for Export
        public void ExportFromDB(string SqlQuery, CampaignActivist Class)
        {
            campaign = Class;
            DAL.PromoItQuery.InputToDB(SqlQuery, changeTheDB);
        }

        // ייצוא נתונים - 2
        // Exports the data from the server into the database
        public void changeTheDB(SqlCommand command)
        {
            //"@ID,@Name,@IDAssn,@NameAssn,@EmailAssn,@Fund,@Money,@Link,@Hashtag,@Selected,@Status,@IDactiv,@Nameactiv,@EmailActiv,@Address,@Phone"
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

            command.Parameters.AddWithValue("@IDactiv", campaign.IDactivist);
            command.Parameters.AddWithValue("@Nameactiv", campaign.NameActivist);
            command.Parameters.AddWithValue("@EmailActiv", campaign.EmailActivist);
            command.Parameters.AddWithValue("@Address", campaign.AddressActivist);
            command.Parameters.AddWithValue("@Phone", campaign.phoneActivist);
            command.Parameters.AddWithValue("@Money", campaign.MoneyActivist);
            command.ExecuteNonQuery();
        }
    }
}
