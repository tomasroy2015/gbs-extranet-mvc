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
    public class PropertyStatisticsRepository : BaseRepository
    {
        public  string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
         //public List<PropertyStatisticsExt> DisplayPropertyStatistics(string PartID, string RecordID, string HitCountPeriodID, string Year, string StartDate, string EndDate)
        public List<PropertyStatisticsExt> DisplayPropertyStatistics(string PartID, string HitCountPeriodID, string Startdate, string EndDate, int HotelID)
        {
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
         
            List<PropertyStatisticsExt> list = new List<PropertyStatisticsExt>();
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHitCounts", SQLCon);
            cmd.Parameters.AddWithValue("@PartID", Convert.ToInt32(PartID));
            cmd.Parameters.AddWithValue("@RecordID", Convert.ToInt64(HotelID));
            cmd.Parameters.AddWithValue("@HitCountPeriodID", Convert.ToInt32(HitCountPeriodID));
            cmd.Parameters.AddWithValue("@StartDate", Startdate);
            cmd.Parameters.AddWithValue("@EndDate", EndDate);
            cmd.Parameters.AddWithValue("@OrderBy", "Sort");
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
           
            SQLCon.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyStatisticsExt FirmObj = new PropertyStatisticsExt();
                    FirmObj.PartID = dr["PartID"].ToString();
                    FirmObj.RecordID = dr["RecordID"].ToString();
                    FirmObj.ReservationCount = dr["ReservationCount"].ToString();
                    FirmObj.Date = dr["Date"].ToString();
                    FirmObj.HitCount = dr["HitCount"].ToString();
                    FirmObj.Month = dr["Month"].ToString();
                    FirmObj.MonthName = dr["MonthName"].ToString();
                    FirmObj.Day = dr["Day"].ToString();
                    FirmObj.DayName = dr["DayName"].ToString();                  
                    list.Add(FirmObj);
                }
            }
            return list;
        }

        public DataTable DisplaydatewisePropertyStatistics(string PartID, string HitCountPeriodID, string Startdate, string EndDate, int HotelID)
        {
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
               
            List<PropertyStatisticsExt> list = new List<PropertyStatisticsExt>();
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHitCounts", SQLCon);
            cmd.Parameters.AddWithValue("@PartID", Convert.ToInt32(PartID));
            cmd.Parameters.AddWithValue("@RecordID", Convert.ToInt64(HotelID));
            cmd.Parameters.AddWithValue("@HitCountPeriodID", Convert.ToInt32(HitCountPeriodID));
            cmd.Parameters.AddWithValue("@StartDate", Startdate);
            cmd.Parameters.AddWithValue("@EndDate", EndDate);
            cmd.Parameters.AddWithValue("@OrderBy", "Sort");
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            return dt;
        }

        public DataTable MonthlyPropertyStatistics(string PartID, int HotelID, string Year)
        {
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            //HotelID = "100002";
            string HitCountPeriodID = "2";
            List<PropertyStatisticsExt> list = new List<PropertyStatisticsExt>();
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHitCounts", SQLCon);
            cmd.Parameters.AddWithValue("@PartID", Convert.ToInt32(PartID));
            cmd.Parameters.AddWithValue("@RecordID", Convert.ToInt64(HotelID));
            cmd.Parameters.AddWithValue("@HitCountPeriodID", Convert.ToInt32(HitCountPeriodID));
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@Year", Convert.ToInt32(Year));
            cmd.Parameters.AddWithValue("@OrderBy", "Month");
            
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            return dt;
          
        }
        //public List<PropertyStatisticsExt> MonthlyPropertyStatistics(string PartID, string HotelID,string Year)
        //{
        //    string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        //    HotelID = "100002";
        //    string HitCountPeriodID = "2";
        //    List<PropertyStatisticsExt> list = new List<PropertyStatisticsExt>();
        //    DataTable dt = new DataTable();
        //    SQLCon.Open();
        //    SqlCommand cmd = new SqlCommand("TB_SP_GetHitCounts", SQLCon);
        //    cmd.Parameters.AddWithValue("@PartID", Convert.ToInt32(PartID));
        //    cmd.Parameters.AddWithValue("@RecordID", Convert.ToInt64(HotelID));
        //    cmd.Parameters.AddWithValue("@HitCountPeriodID", Convert.ToInt32(HitCountPeriodID));
        //    cmd.Parameters.AddWithValue("@Culture", CultureValue);
        //    cmd.Parameters.AddWithValue("@Year", Convert.ToInt32(Year));
        //    cmd.CommandType = CommandType.StoredProcedure;
        //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //    sda.Fill(dt);
        //    SQLCon.Close();
        //    // return dt;
        //    if (dt.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dt.Rows)
        //        {
        //            PropertyStatisticsExt FirmObj = new PropertyStatisticsExt();
        //            FirmObj.PartID = dr["PartID"].ToString();
        //            FirmObj.RecordID = dr["RecordID"].ToString();
        //            FirmObj.ReservationCount = dr["ReservationCount"].ToString();
        //            FirmObj.HitCount = dr["HitCount"].ToString();
        //            FirmObj.Month = dr["Month"].ToString();
        //            FirmObj.MonthName = dr["MonthName"].ToString();
                                   
        //            list.Add(FirmObj);
        //        }
        //    }
        //    return list;
        //}


        public List<PropertyStatisticsExt> YearlyPropertyStatistics(string PartID, int HotelID)
        {
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
            //HotelID = "100002";
            string HitCountPeriodID = "1";
            List<PropertyStatisticsExt> list = new List<PropertyStatisticsExt>();
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHitCounts", SQLCon);
            cmd.Parameters.AddWithValue("@PartID", Convert.ToInt32(PartID));
            cmd.Parameters.AddWithValue("@RecordID", Convert.ToInt64(HotelID));
            cmd.Parameters.AddWithValue("@HitCountPeriodID", Convert.ToInt32(HitCountPeriodID));
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            // return dt;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyStatisticsExt FirmObj = new PropertyStatisticsExt();
                    FirmObj.PartID = dr["PartID"].ToString();
                    FirmObj.RecordID = dr["RecordID"].ToString();
                    FirmObj.ReservationCount = dr["ReservationCount"].ToString();
                    FirmObj.HitCount = dr["HitCount"].ToString();
                    FirmObj.Year = dr["Year"].ToString();
                    list.Add(FirmObj);
                }
            }
            return list;
        }


    }
    public class PropertyStatisticsExt
    {
        public string PartID { get; set; }

        public string RecordID { get; set; }

        public string HitCount { get; set; }

        public string Year { get; set; }

        public string Month { get; set; }

        public string MonthName { get; set; }

        public string Day { get; set; }

        public string DayName { get; set; }

        public string Date { get; set; }

        public string StartDate { get; set; }

        public string EndDate { get; set; }
        public string ReservationCount { get; set; }
    }
}