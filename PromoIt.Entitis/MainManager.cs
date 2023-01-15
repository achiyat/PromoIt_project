using PromoIt.Entitis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PromoIt.Entities
{
    public class MainManager
    {
        // singelton
        private MainManager() { }
        private static readonly MainManager instance = new MainManager();
        public static MainManager Instance { get { return instance; } }

        public Companies Companies = new Companies();
        public Associations Associations = new Associations();
        public Activists Activists = new Activists();
        public Messages Messages = new Messages();
        public Campaigns_Of_Asso CampaignsAsso = new Campaigns_Of_Asso();
        public Campaign_Of_Company CampaignCompany = new Campaign_Of_Company();
        public Campaign_Of_Activists CampaignActivists = new Campaign_Of_Activists();
        public DonatedProducts DonatedProducts = new DonatedProducts();
        public Inner_Joins InnerJoins = new Inner_Joins();
        public Shipments Shipments = new Shipments();
        //public Products Products = new Products();
    }
}
