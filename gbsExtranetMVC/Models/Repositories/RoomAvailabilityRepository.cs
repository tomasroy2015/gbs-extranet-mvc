using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class RoomAvailabilityRepository:BaseRepository
    {
        public  string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        DataTable HotelRooms = new DataTable();
        DataTable Dates = new DataTable();
        DataTable HotelAvailabilityTable = new DataTable();

       // public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public string ReplaceComma(string text)
        {
            string value = "";
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
       
        public int CreateHotelRoomAvailability(string HotelRoomID,string RoomCount, string StartDate, string EndDate)
        {
            //IFormatProvider culture = new CultureInfo("en-US", true);
            //DateTime dtStart = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //DateTime dtEnd = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
         
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_CreateHotelRoomAvailability", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelRoomID", HotelRoomID);
            cmd.Parameters.AddWithValue("@StartDate", StartDate);
            cmd.Parameters.AddWithValue("@EndDate", EndDate);
            cmd.Parameters.AddWithValue("@RoomCount", RoomCount);
            int i = Convert.ToInt32(cmd.ExecuteNonQuery());
            SQLCon.Close();
            return i;
        }

       
        public DataTable GetHotelRooms(string OrderBy, int HotelID)
        {
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
         
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelRooms", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", OrderBy);
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            cmd.Parameters.AddWithValue("@Active", 1);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(HotelRooms);
            SQLCon.Close();
            return HotelRooms;
        }

        public DataTable GetDates(string OrderBy, string startDate, string endDate)
        {
           // IFormatProvider culture = new CultureInfo("en-US", true);
            //DateTime dtStart = DateTime.ParseExact(startDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //DateTime dtEnd = DateTime.ParseExact(endDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
         
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetDates", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", OrderBy);
            cmd.Parameters.AddWithValue("@StartDate", startDate);
            cmd.Parameters.AddWithValue("@EndDate", endDate);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(Dates);
            SQLCon.Close();
            return Dates;
        }


        public DataTable GetHotelAvilabilty(string StartDate, string EndDate, int HotelID)
        {
            //DateTime dtStart = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //DateTime dtEnd = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
         
            SQLCon.Open();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelAvailability", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@StartDate", StartDate);
            cmd.Parameters.AddWithValue("@EndDate", EndDate);
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            return dt;
        }



        public void SaveHotelAvailabilty(string HotelRoomID, string DateIDArray, string AvailRoomcountArray,DateTime Date, Controller ctrl)
        {

            string[] DateID = DateIDArray.Split(',');
            string[] AvailableRoomcount = AvailRoomcountArray.Split(',');
     

            for (int i = 0; i < DateID.Length; i++)
            {
               
                int dateId = Convert.ToInt32(DateID[i]);
                int RoomID = Convert.ToInt32(HotelRoomID);
              
                var HotelAvailability = db.TB_HotelAvailability.Where(x => x.DateID == dateId && x.HotelRoomID == RoomID).FirstOrDefault();

                if (AvailableRoomcount[i] != "")
                {
                    HotelAvailability.RoomCount = Convert.ToInt32(AvailableRoomcount[i]);
                }
                
                HotelAvailability.OpDateTime = Date;
                HotelAvailability.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
                db.SaveChanges();
                int id = HotelAvailability.ID;
            }

        }


        public void SaveRoomAvailability(string StartDate, string Enddate,string HotelRoomID, string availabiltyValue, string CloseOpenAvailabilityValue, DateTime Date, Controller ctrl)
        {
            bool CloseOpenAvailability = false;

            if(CloseOpenAvailabilityValue=="")
            {
                CloseOpenAvailabilityValue = "0";

            }
            if(CloseOpenAvailabilityValue == "0")
            {
                CloseOpenAvailability = true;
            }
            else if(CloseOpenAvailabilityValue == "1")
            {
                CloseOpenAvailability =false;
            }
            int HotelRoomIDValue = Convert.ToInt32(HotelRoomID);
            
            //DateTime DStartdate = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            //DateTime DEndDate = DateTime.ParseExact(Enddate, "dd/MM/yyyy", CultureInfo.InvariantCulture);

            SQLCon.Open();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("B_GetRoomAvailability_TB_HotelAvailability_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Startdate", StartDate);
            cmd.Parameters.AddWithValue("@EndDate", Enddate);
            cmd.Parameters.AddWithValue("@HotelRoomID", HotelRoomID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        int HotelRoom = Convert.ToInt32(dr["HotelRoomID"]);
                        int IDDate = Convert.ToInt32(dr["DateID"]);
                        var obj = db.TB_HotelAvailability.Where(x => x.HotelRoomID == HotelRoom && x.DateID == IDDate).FirstOrDefault();
                        //int Closedvalue = Convert.ToInt32(CloseOpenAvailabilityValue);
                        obj.Closed = CloseOpenAvailability;
                        obj.RoomCount = Convert.ToInt32(availabiltyValue);
                        obj.OpDateTime = Date;
                        obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
                        int ID = obj.ID;
                        db.SaveChanges();
                    }
                }
            }

        }
        
         

        public DataTable GetHotelAvailability(int HotelID)
        {
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("[TB_SP_GetHotelRooms]", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "RoomTypeName");
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            cmd.Parameters.AddWithValue("@Active", true);

            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();

            return dt;
        }
       

        public bool SaveRoomAvailabilityAndRate(int AccommodationType, int PricePolicy, int RoomID, int MaximamPeopleCountstring, string DateIDArray, string SinglePriceNameArray, string DoublePriceNameArray, string RoomPriceNameArray,
            string AvailableRoomCountNameArray, string MinimumStayNameArray, string CloseToArrivalArray, string CloseToDepartureArray, string ClosedArray, Controller ctrl)
       
        {

            bool status = true;
              string [] DateID= DateIDArray.Split(',');
              string [] SinglePrice=SinglePriceNameArray.Split(',');
              string [] DoublePrice=DoublePriceNameArray.Split(',');
              string [] RoomPrice=RoomPriceNameArray.Split(',');
              string [] AvailableRoomCount=AvailableRoomCountNameArray.Split(',');
              string [] MinimumStay=MinimumStayNameArray.Split(',');
              string [] CloseToArrival=CloseToArrivalArray.Split(',');
              string [] CloseToDeparture=CloseToDepartureArray.Split(',');
              string [] Closed=ClosedArray.Split(',');
            //using (DBEntities DE = new DBEntities())
            //{
            for(int i=0;i<DateID.Length;i++)
            {
                int dateId=Convert.ToInt32(DateID[i]);
                var AvailabilityTable = db.TB_HotelAvailability.Where(x => x.DateID == dateId && x.HotelRoomID == RoomID).FirstOrDefault();
                AvailabilityTable.RoomCount = Convert.ToInt32(AvailableRoomCount[i]);
                AvailabilityTable.CloseToArrival = Convert.ToBoolean(Convert.ToInt32(CloseToArrival[i]));
                AvailabilityTable.CloseToDeparture = Convert.ToBoolean(Convert.ToInt32(CloseToDeparture[i]));
                AvailabilityTable.Closed = Convert.ToBoolean(Convert.ToInt32(Closed[i]));
                if(MinimumStay[i]=="")
                {
                    AvailabilityTable.MinimumStay = 1;
                }
                else
                {
                    AvailabilityTable.MinimumStay = Convert.ToInt32(MinimumStay[i]);
                }
                AvailabilityTable.OpDateTime = DateTime.Now;
                AvailabilityTable.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);

                var RateTable = db.TB_HotelRate.Where(x => x.DateID == dateId && x.HotelRoomID == RoomID && x.PricePolicyTypeID == PricePolicy && x.HotelAccommodationTypeID == AccommodationType).FirstOrDefault();
                RateTable.SinglePrice = Convert.ToDecimal(SinglePrice[i]);
                RateTable.DoublePrice = Convert.ToDecimal(DoublePrice[i]);
                RateTable.RoomPrice = Convert.ToDecimal(RoomPrice[i]);
                RateTable.OpDateTime = DateTime.Now;
                RateTable.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
                db.SaveChanges();
            }
                
            //}
               return status;
        }
        public DataTable GetHotelAvailability(int HotelID, string StartDate, string EndDate,string RoomType)
        {
            DateTime dtStart = DateTime.ParseExact(StartDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DateTime dtEnd = DateTime.ParseExact(EndDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("[TB_SP_GetHotelAvailability]", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            cmd.Parameters.AddWithValue("@HotelRoomID",Convert.ToInt32(RoomType));
            cmd.Parameters.AddWithValue("@StartDate", dtStart);
            cmd.Parameters.AddWithValue("@EndDate", dtEnd);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
           
            return dt;
        }


        public DataTable GetHotelRoomstable(int HotelID)
        {
           
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("[TB_SP_GetHotelRooms]", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "RoomTypeName");
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            cmd.Parameters.AddWithValue("@Active", true);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
          
            SQLCon.Close();

            return dt;
        }

       
}
    public class RoomAvailabilityExt
    {
        public int RoomID { get; set; }
        public int RoomTypeID { get; set; }
        public int MaxPeopleCount { get; set; }
        public int RoomCount { get; set; }
        public string RoomTypeName { get; set; }

        public string CssClass { get; set; }

        //For Date
        public int DateID { get; set; }
        public DateTime Date { get; set; }
        public int DayID { get; set; }
        public int Day { get; set; }
        public int WeekDay { get; set; }
        public string DayName { get; set; }
        public int MonthID { get; set; }
        public string MonthName { get; set; }
        public int Year { get; set; }

        //For HotelAvailability
        public int HotelAvailabityID { get; set; }
        public int AvailableRoomCount { get; set; }
        public int MinimumStay { get; set; }
        public bool RoomRateMissing { get; set; }
        public bool CloseToArrival { get; set; }
        public bool CloseToDeparture { get; set; }
        public bool Closed { get; set; }
        public int HotelRateID { get; set; }

        public int PricePolicyTypeID { get; set; }
        public decimal SinglePrice { get; set; }
        public decimal DoublePrice { get; set; }
        public decimal RoomPrice { get; set; }

        public string RoomTypeNames { get; set; }

        public int HotelRoomID { get; set; }

        public int RoomTypeIDCount { get; set; }

        public string hotelRoomAvailabilityText { get; set; }

        public bool txtAvailableRoomCount { get; set; }
    }
}