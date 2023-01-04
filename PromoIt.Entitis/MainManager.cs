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

        //public Products Products = new Products();
    }
}
