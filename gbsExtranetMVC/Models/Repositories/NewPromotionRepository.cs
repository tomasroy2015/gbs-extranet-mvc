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
    public class NewPromotionRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        CommonRepository ObjCommon = new CommonRepository();
        public List<NewPromotionExt> ReadAll()
        {
            List<NewPromotionExt> list = new List<NewPromotionExt>();

            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("[B_Ex_GetPromotionDetails_TB_Promotion_SP]", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
            cmd.Parameters.AddWithValue("@Part", 1);
            cmd.Parameters.AddWithValue("@GeneralPromotion", false);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    NewPromotionExt PageObj = new NewPromotionExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.PartID = Convert.ToInt32(dr["PartID"]);
                    if (dr["PromotionType"].ToString()=="BasicDeal")
                    {
                        PageObj.Color = "#f2ca85";
                    }
                    else if (dr["PromotionType"].ToString() == "MinimumStay")
                    {
                        PageObj.Color = "#c8d9f7";
                    }
                    else if (dr["PromotionType"].ToString() == "EarlyBooker")
                    {
                        PageObj.Color = "#f1a19f";
                    }
                    else if (dr["PromotionType"].ToString() == "LastMinute")
                    {
                        PageObj.Color = "#a7abe8";
                    }
                    else if (dr["PromotionType"].ToString() == "TwentyFourHourPromotion")
                    {
                        PageObj.Color = "#c2a3e2";
                    }
                    else if (dr["PromotionType"].ToString() == "Genius")
                    {
                        PageObj.Color = "#c4dabd";
                    }
                    PageObj.PromotionType = dr["PromotionType"].ToString();
                    PageObj.PromotionDescription = dr["Description"].ToString();
                    PageObj.PromotionName = dr["Name"].ToString();
                    PageObj.PromotionSort = Convert.ToInt32(dr["Sort"]);
                    if (dr["StartDate"].ToString() != "")
                    {
                        PageObj.StartDate = Convert.ToDateTime(dr["StartDate"].ToString());
                    }
                    if (dr["EndDate"].ToString() != "")
                    {
                        PageObj.EndDate = Convert.ToDateTime(dr["EndDate"].ToString());
                    }
                    if (dr["TargetStartDate"].ToString() != "")
                    {
                        PageObj.TargetStartDate = Convert.ToDateTime(dr["TargetStartDate"].ToString());
                    }
                    if (dr["TargetEndDate"].ToString() != "")
                    {
                        PageObj.TargetEndDate = Convert.ToDateTime(dr["TargetEndDate"].ToString());
                    }
                    PageObj.RegionID = dr["RegionID"].ToString();
                    PageObj.Count = dr["Count"].ToString();
                    PageObj.DiscountPercentage = dr["DiscountPercentage"].ToString();
                    PageObj.GeneralPromotion = Convert.ToBoolean(dr["GeneralPromotion"].ToString());

                    list.Add(PageObj);
                }
            }
            return list;
        }

        public List<NewPromotionExt> GetDay()
        {
           // List<NewPromotionExt> list = new List<NewPromotionExt>();
            //GetDaysDetails_Result
            
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            var result = entity.Database.SqlQuery<GetDaysDetails_Result>("[B_Ex_GetDay_TB_TypeDays_SP] @CultureCode", Culture).ToList();

            List<NewPromotionExt> list = (from dr in result
                                           select new NewPromotionExt
                                             {
                                                 ID = Convert.ToInt32(dr.ID),
                                                 Name = dr.Name,
                                             }).ToList();
            return list;
        }

        public string HotelPromotion(int HotelID, string AccommodationStartDate, string AccommodationEndDate, string PromotionRoomType)
        {

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
            string Status ="NoConflict";
            DateTime AccommodationStartDateFromUser = Convert.ToDateTime(AccommodationStartDate);
            DateTime AccommodationEndDateFromUser = Convert.ToDateTime(AccommodationEndDate);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DateTime AccommodationStartDateFromDB = Convert.ToDateTime(dr["AccommodationStartDate"].ToString());
                    DateTime AccommodationEndDateFromDB = Convert.ToDateTime(dr["AccommodationEndDate"].ToString());

                    if (dr["PromotionType"].ToString() == PromotionRoomType.ToString() && !(AccommodationStartDateFromUser < AccommodationStartDateFromDB || AccommodationEndDateFromUser > AccommodationEndDateFromDB))
                    {
                        Status = "Conflict";
                        return Status;
                    }
                 
                }
            }
            return Status;
        }

        public int GetParameterValue(string Parameter)
        {
            int Status = 0;
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetParameter_BizTbl_Parameter_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Code", Parameter);
            Status = Convert.ToInt32(cmd.ExecuteScalar());
            SQLCon.Close();
            return Status;
        }
         public List<PropertyPhotosExt> GetHotelRooms(int HotelID)
        {
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("[TB_SP_GetHotelRooms]", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureCode);
            cmd.Parameters.AddWithValue("@OrderBy", "Sort,RoomTypeName");
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            cmd.Parameters.AddWithValue("@Active", true);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
             List<PropertyPhotosExt> ListOfModel = new List<PropertyPhotosExt>();
            string Roomname = "";

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {

                    PropertyPhotosExt HotelObj = new PropertyPhotosExt();
                    HotelObj.RoomID = Convert.ToInt32(dr["ID"]);
                    HotelObj.RoomTypeID = Convert.ToInt32(dr["RoomTypeID"]);
                    HotelObj.RoomTypeName = dr["RoomTypeName"].ToString();
                    ListOfModel.Add(HotelObj);
                }
            }
            return ListOfModel;
        }

        public string ReplaceComma( string text)
         { string value="";
         if (text.Contains(','))
         {
             value = text.Replace(",", ";");
             value = value + ";";
         }
         else
         {
             value = text;
         }
            return value;
        }
         public string PromotionInsert(string DiscountPercentage, string AccommodationStartDate, string AccommodationEndDate,
                string WeekDay, string PricePolicy, string RoomCount, string RoomType, string MinimumStayDayCount, string EarlyBookerMargin,
                string LastMinuteMargin, string BookingDate, string PromotionID, string HasDiscount, string validForAllRoomTypes,
                int HotelID,int SecretDeal,Controller ctrl)
         {
             if(MinimumStayDayCount=="")
             {
                 MinimumStayDayCount = "1";
             }
             string status = "Success";
             DBEntities insertentity = new DBEntities();
             TB_HotelPromotion PageObj = new TB_HotelPromotion();
             //  PageObj.ID = model.ID;
             PageObj.HotelID = HotelID;
             PageObj.DiscountPercentage = Convert.ToInt32(DiscountPercentage);
             PageObj.PromotionID = Convert.ToInt32(PromotionID);
             PageObj.StartDate = DateTime.Now; ;
             PageObj.EndDate = Convert.ToDateTime("31.12.2100");
             PageObj.HasDiscount = Convert.ToBoolean(Convert.ToInt32(HasDiscount));
             PageObj.AccommodationEndDate = Convert.ToDateTime(AccommodationEndDate);
             PageObj.AccommodationStartDate = Convert.ToDateTime(AccommodationStartDate);
             PageObj.DayID = ReplaceComma(WeekDay);
             if (MinimumStayDayCount!="")
             {
                 PageObj.DayCount = Convert.ToInt32(MinimumStayDayCount);
             }
             else
             {
                 PageObj.DayCount = null;
             }
             if (EarlyBookerMargin != "")
             {
                 PageObj.EarlyBookerMargin = Convert.ToInt32(EarlyBookerMargin);
             }
             else
             {
                 PageObj.EarlyBookerMargin = null;
             }
             if (LastMinuteMargin != "")
             {
                 PageObj.LastMinuteMargin = Convert.ToInt32(LastMinuteMargin);
             }
             else
             {
                 PageObj.LastMinuteMargin = null;
             }
             if (BookingDate != "")
             {
                 PageObj.BookingDate =  Convert.ToDateTime(BookingDate);
             }
             else
             {
                 PageObj.BookingDate = null;
             }
             PageObj.PricePolicyID = ReplaceComma(PricePolicy);
             //PageObj.Region = model.Region;
             PageObj.ValidForAllRoomTypes = Convert.ToBoolean(validForAllRoomTypes);
             PageObj.Active = true;
             PageObj.SecretDeal = Convert.ToBoolean(Convert.ToInt32(SecretDeal));
             PageObj.OpDateTime = DateTime.Now;
             PageObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]); ;
             PageObj.CreateDateTime = DateTime.Now;
             PageObj.CreateUserID = Convert.ToInt64(ctrl.Session["UserID"]); ;
             insertentity.TB_HotelPromotion.Add(PageObj);
             insertentity.SaveChanges();
             int id = PageObj.ID;
            // DBEntities insertRoom = new DBEntities();
             TB_HotelPromotionRoom RoomObj = new TB_HotelPromotionRoom();
             // PageObj.ID = model.ID;
             foreach (string RoomID in RoomCount.Split(','))
             {
                 RoomObj.HotelPromotionID = id;
                 RoomObj.HotelRoomID = Convert.ToInt32(RoomID);
                 RoomObj.CreateDateTime = DateTime.Now;
                 RoomObj.CreateUserID = Convert.ToInt64(ctrl.Session["UserID"]); ;
                 RoomObj.OpDateTime = DateTime.Now;
                 RoomObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]); ;
                 RoomObj.Active = true;
                 insertentity.TB_HotelPromotionRoom.Add(RoomObj);
                 insertentity.SaveChanges();
             }
             

             return status;
         }
        
    }

    

    public class NewPromotionExt
    {
        public int ID { get; set; }
        public int PartID { get; set; }
        public string PromotionType { get; set; }
        public string PromotionName { get; set; }
        public string PromotionDescription { get; set; }
        public int PromotionSort { get; set; }
        public bool GeneralPromotion { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime TargetStartDate { get; set; }
        public DateTime TargetEndDate { get; set; }
        public string DiscountPercentage { get; set; }
        public string Count { get; set; }
        public string RegionID { get; set; }
        public string Color { get; set; }
        public string Name { get; set; }
    }
}