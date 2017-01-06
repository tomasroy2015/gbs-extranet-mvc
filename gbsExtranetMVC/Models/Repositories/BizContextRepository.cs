using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Models.Repositories
{
    [Serializable()]
    public class BizContextRepository
    {
       private int _firmID;

       private int _hotelID;
        public int HotelID
        {
            get { return _hotelID; }
            set { _hotelID = value; }
        }

        public int FirmID
        {
            get { return _firmID; }
            set { _firmID = value; }
        }
    }
}