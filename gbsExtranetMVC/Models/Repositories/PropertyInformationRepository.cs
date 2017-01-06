using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Models.Repositories
{
    public class PropertyInformationRepository : BaseRepository
    {
       // CommonRepository ObjCommon = new CommonRepository();
        public int UpdateHotelGeneralInfo(int HotelID, string CheckinStart, string CheckinEnd,string CheckoutStart, string CheckoutEnd,  string SelectedCards )
        {

            int status = 1;
             // Object valu=ObjCommon.CheckEmptyStringDBParameter(CheckinStart);
            
            var obj = db.TB_Hotel.Where(x => x.ID == HotelID).FirstOrDefault();
            obj.CheckinStart = CheckinStart;
            obj.CheckinEnd = CheckinEnd;
            obj.CheckoutStart = CheckoutStart;
            obj.CheckoutEnd = CheckoutEnd;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = 0;
            db.SaveChanges();
            var HotelIDParameter = new SqlParameter("@HotelID", HotelID);
            var SelectedCardsParameter = new SqlParameter("@SelectedCards", SelectedCards);
            int i = db.Database.ExecuteSqlCommand("B_Ex_UpdateHotelCreditCard_TB_HotelCreditCard_SP @HotelID,@SelectedCards", HotelIDParameter, SelectedCardsParameter);
      
            return status;
        }
    }
}