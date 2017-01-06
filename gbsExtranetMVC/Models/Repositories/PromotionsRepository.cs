using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Resources;
namespace gbsExtranetMVC.Models.Repositories
{
    public class PromotionsRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<PromotionExt> ReadAll(int HotelID)
        {
            List<PromotionExt> list = new List<PromotionExt>();

            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("[TB_SP_GetHotelPromotions]", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelIDs", HotelID);
            cmd.Parameters.AddWithValue("@Culture", CultureCode);
            cmd.Parameters.AddWithValue("@OrderBy", "ID");
            cmd.Parameters.AddWithValue("@Active", true);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            string Alltypes = Resources.Resources.AllRoomTypes;
            string RoomNames = GetHotelRooms(HotelID);
            string DiscountText = Resources.Resources.Discount;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PromotionExt PageObj = new PromotionExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.HotelID = Convert.ToInt32(dr["HotelID"]);
                    PageObj.PromotionID = Convert.ToInt32(dr["PromotionID"]);
                    PageObj.PromotionType = dr["PromotionType"].ToString();
                    PageObj.PromotionDescription = dr["PromotionDescription"].ToString();
                    PageObj.PromotionName = dr["PromotionName"].ToString();
                    PageObj.PromotionSort = Convert.ToInt32(dr["PromotionSort"]);
                    PageObj.Alltypes =Alltypes;
                    if (Convert.ToBoolean(dr["ValidForAllRoomTypes"].ToString())==true)
                    {
                        PageObj.RoomNames = "";
                    }
                    else
                    {
                        PageObj.RoomNames = GetHotelRooms(Convert.ToInt32(dr["ID"]));
                    }
                    PageObj.DiscountText = DiscountText;
                    PageObj.StartDate = Convert.ToDateTime(dr["StartDate"].ToString());
                    PageObj.EndDate = Convert.ToDateTime(dr["EndDate"].ToString());
                    PageObj.AccommodationStartDate = Convert.ToDateTime(dr["AccommodationStartDate"].ToString());
                    PageObj.AccommodationEndDate = Convert.ToDateTime(dr["AccommodationEndDate"].ToString());
                    PageObj.HasDiscount = Convert.ToBoolean(dr["HasDiscount"].ToString());
                    PageObj.DiscountPercentage = "% " + dr["DiscountPercentage"].ToString() +" "+ DiscountText;
                    //PageObj.DayID = Convert.ToInt32(dr["DayID"].ToString());
                    PageObj.DayName = dr["DayName"].ToString();
                    //PageObj.DayCount = Convert.ToInt32(dr["DayCount"]);
                   // PageObj.EarlyBookerMargin = Convert.ToInt32(dr["EarlyBookerMargin"].ToString());
                   //PageObj.LastMinuteMargin = Convert.ToInt32(dr["LastMinuteMargin"].ToString());
                   // PageObj.BookingDate = Convert.ToDateTime(dr["BookingDate"].ToString());
                   //  PageObj.PricePolicyID = Convert.ToInt32(dr["PricePolicyID"].ToString());
                    PageObj.PricePolicyName = dr["PricePolicyName"].ToString();
                    PageObj.SecretDeal = Convert.ToBoolean(dr["SecretDeal"].ToString());
                   // PageObj.Region = dr["Region"].ToString();
                    PageObj.ValidForAllRoomTypes = Convert.ToBoolean(dr["ValidForAllRoomTypes"].ToString());
                    PageObj.Active = Convert.ToBoolean(dr["Active"].ToString());
                    PageObj.CreateDate = Convert.ToDateTime(dr["CreateDateTime"].ToString());
                    
                    list.Add(PageObj);
                }
            }
            return list;
        }
        public string GetHotelRooms(int PromotionID)
        {
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelPromotionRooms", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureCode);
            cmd.Parameters.AddWithValue("@OrderBy", "RoomTypeName");
            cmd.Parameters.AddWithValue("@HotelPromotionID", PromotionID);
            //cmd.Parameters.AddWithValue("@Active", true);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            string Roomname = "";

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    if(Roomname=="")
                    {
                        Roomname = "<li>" + dr["RoomTypeName"].ToString() + "</li>";
                    }
                    else
                    {
                        Roomname = Roomname + "<li>" + dr["RoomTypeName"].ToString() + "</li>";
                    }
                    //PropertyPhotosExt HotelObj = new PropertyPhotosExt();
                    //HotelObj.RoomID = Convert.ToInt32(dr["ID"]);
                    //HotelObj.RoomTypeID = Convert.ToInt32(dr["RoomTypeID"]);
                    //HotelObj.RoomTypeName = dr["RoomTypeName"].ToString();
                    //ListOfModel.Add(HotelObj);
                }
            }
            return Roomname;
        }


        //public bool Update(PromotionExt model, ref string Msg, Controller ctrl)
        //{
        //    bool status = true;
        //    var PageObj = db.TB_HotelPromotion.Where(x => x.ID == model.ID).FirstOrDefault();
        //    PageObj.ID = model.ID;
        //    PageObj.HotelID = model.HotelID;
        //    PageObj.PromotionID = model.PromotionID;
        //    PageObj.StartDate = model.StartDate;
        //    PageObj.EndDate = model.EndDate;
        //    PageObj.HasDiscount = model.HasDiscount;
        //    PageObj.AccommodationEndDate = model.AccommodationEndDate;
        //    PageObj.AccommodationStartDate = model.AccommodationStartDate;
        //    PageObj.DayID = Convert.ToString(model.DayID);
        //    PageObj.DayCount = model.DayCount;
        //    PageObj.EarlyBookerMargin = model.EarlyBookerMargin;
        //    PageObj.LastMinuteMargin = model.LastMinuteMargin;
        //    PageObj.BookingDate = model.BookingDate;
        //    PageObj.PricePolicyID = Convert.ToString(model.PricePolicyID);
        //    PageObj.SecretDeal = model.SecretDeal;
        //    PageObj.Region = model.Region;
        //    PageObj.ValidForAllRoomTypes = model.ValidForAllRoomTypes;
        //    PageObj.Active = model.Active;
        //    PageObj.CreateDateTime = DateTime.Now;
        //    PageObj.CreateUserID = Convert.ToInt64(ctrl.Session["UserID"]); 
        //    PageObj.OpDateTime = DateTime.Now;
        //    PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]); 
        //    db.SaveChanges();
        //    return status;
        //}

    }
    

    public class PromotionExt
    {
        public int ID { get; set; }
        public int HotelID { get; set; }
        public int PromotionID { get; set; }
        public string PromotionType { get; set; }
        public string PromotionName { get; set; }
        public string PromotionDescription { get; set; }
        public int PromotionSort { get; set; }
        public bool HasDiscount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime AccommodationStartDate { get; set; }
        public DateTime AccommodationEndDate { get; set; }
        public string DiscountPercentage { get; set; }
        public int DayCount { get; set; }
        public int DayID { get; set; }
        public string DayName { get; set; }
        public string PricePolicyName { get; set; }
        public int PricePolicyID { get; set; }
        public bool SecretDeal { get; set; }
        public bool ValidForAllRoomTypes { get; set; }
        public bool Active { get; set; }
        public int EarlyBookerMargin { get; set; }
        public int LastMinuteMargin { get; set; }
        public DateTime BookingDate { get; set; }
        public string Region { get; set; }
        public string Alltypes { get; set; }
        public string RoomNames { get; set; }
        public string DiscountText { get; set; }
        public DateTime CreateDate { get; set; }
    }
}