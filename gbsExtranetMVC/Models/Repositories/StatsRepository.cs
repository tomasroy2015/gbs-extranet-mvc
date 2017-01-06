using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using gbsExtranetMVC.Models;

namespace gbsExtranetMVC.Models.Repositories
{
    public class StatsRepository : BaseRepository
    {
        public SelectStats_Result ReadStats()
        {
            return db.SelectStats().FirstOrDefault();
        }
    }
}