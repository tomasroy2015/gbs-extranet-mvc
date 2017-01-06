using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Models.Repositories
{
    public class PropertyReviewExt
    {
        public int HotelID { get; set; }
        public decimal AveragePoint { get; set; }
        public int ReviewTypeID { get; set; }
        public string ReviewTypeName { get; set; }
        public int sort { get; set; }
        public decimal PointSum { get; set; }
        public decimal FirstAveragePoint { get; set; }
        public int ReviewCount { get; set; }
        public string ReviewTypeEvaluationName { get; set; }
        public string AvgPropertyPoint { get; set; }
        public double height { get; set; }
        public string height1 { get; set; }
        public double width { get; set; }
        public string width1 { get; set; }
        public double width2 { get; set; }
        public string width3 { get; set; }
        public string FirstReviewTypeEvaluationName { get; set; }
        public int TotalRecordCount { get; set; }
        public int ReservationID { get; set; }
        public int TravellerTypeID { get; set; }
        public string TravelerTypeName { get; set; }
        public string Review { get; set; }
        public int ReviewStatusID { get; set; }
        public string ReviewStatusName { get; set; }
        public bool Anonymous { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime OpDateTime { get; set; }
        public string IPAddress { get; set; }
        public decimal Point { get; set; }
        public string ReviewTypeScaleName { get;set; }
        public Int64 UserID { get; set; }
        public string UserName { get; set; }
        public string UserFullName { get; set; }
        public int PartID { get; set; }
        public string Part { get; set; }
        public int FirmID { get; set; }
        public string FirmName { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        public int ReviewTypeCount{get;set;}
        public float ReviewCount1 { get; set; }
        public string ReviewInfo { get; set; }

        public int ReservationReviewID { get; set; }

        public int ReviewTypeCount1 { get; set; }
    }
    public class PropertyReviewRepository : BaseRepository
    {
        public string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<PropertyReviewExt> GetReviews(int HotelID)
        {
            // string PropertyConditions = "";
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetReservationReviewAveragePoints", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "ReviewTypeEvaluationName");
            cmd.Parameters.AddWithValue("@PartID", 1);
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
           // string FirstAveragePoint = "";
            int ReservationReviewDetailCount = 0;
            int TypeReviewCount = 0;
            List<PropertyReviewExt> ListOfModel = new List<PropertyReviewExt>();
            DataSet ReviewTypeCount = GetReviewTypeCount(HotelID);

             DataTable dt1 = new DataTable();
            DataTable dt2 = new DataTable();
            if (ReviewTypeCount != null)
            {
                dt1 = ReviewTypeCount.Tables[0];
                dt2 = ReviewTypeCount.Tables[1];
            }
          
         

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt1.Rows)
                {
                    PropertyReviewExt ReviewsObj = new PropertyReviewExt();
                    ReservationReviewDetailCount = Convert.ToInt32(dr["ReservationReviewDetailCount"]);
                    //ListOfModel.Add(ReviewsObj);
                }
            }
            if (dt1.Rows.Count > 0)
            {
                foreach (DataRow dr in dt2.Rows)
                {
                    PropertyReviewExt ReviewsObj = new PropertyReviewExt();
                    TypeReviewCount = Convert.ToInt32(dr["TypeReviewCount"]);
                    //ListOfModel.Add(ReviewsObj);
                }
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyReviewExt ReviewsObj = new PropertyReviewExt();
                    ReviewsObj.HotelID = Convert.ToInt32(dr["HotelID"]);
                    ReviewsObj.AveragePoint = Convert.ToDecimal(dr["AveragePoint"]);
                    ReviewsObj.ReviewTypeID = Convert.ToInt32(dr["ReviewTypeID"]);
                    ReviewsObj.ReviewTypeName = dr["ReviewTypeName"].ToString();
                    ReviewsObj.sort = Convert.ToInt32(dr["Sort"]);
                    ReviewsObj.PointSum = Convert.ToDecimal(dr["PointSum"]);
                    ReviewsObj.ReviewCount = Convert.ToInt32(dr["ReviewCount"]);
                    ReviewsObj.FirstAveragePoint = Convert.ToDecimal(dt.Rows[0]["AveragePoint"]);
                    string FirstAveragePoint = dt.Rows[0]["AveragePoint"].ToString();
                    ReviewsObj.FirstReviewTypeEvaluationName = dt.Rows[0]["ReviewTypeEvaluationName"].ToString();
                    if (FirstAveragePoint != "")
                    {

                        ReviewsObj.FirstAveragePoint = Convert.ToDecimal(FormatToNumber(FirstAveragePoint));

                    }
                    double AveragePoint1 = Convert.ToDouble(Convert.ToDecimal(dr["PointSum"]) / Convert.ToInt32(dr["ReviewCount"]));
                    //ReviewsObj.AveragePoint = ;
                    ReviewsObj.AveragePoint= Math.Round(Convert.ToDecimal(AveragePoint1), 1);
                    if (AveragePoint1 != 0)
                    {
                        ReviewsObj.height = Convert.ToDouble(Math.Floor(AveragePoint1 * 20 ) );
                        ReviewsObj.height1 = ReviewsObj.height +"px";
                    }
                    ReviewsObj.ReviewTypeEvaluationName = dr["ReviewTypeEvaluationName"].ToString();
                    ReviewsObj.ReviewTypeCount1 = ReservationReviewDetailCount / TypeReviewCount;

                    ListOfModel.Add(ReviewsObj);
                }
            }
           

            return ListOfModel;
        }


        public string FormatToNumber(string Value)
        {
            Int16 MaxDecimalLength = 1;
            bool RemoveDecimalZeros = true;
            double NumericValue = 1;
            string numberStr = string.Empty;
            string InputNumberCultureCode="en-Gb";
            string FormatNumberCultureCode="en-Gb";

            System.Globalization.CultureInfo inputNumberCultureInfo = new System.Globalization.CultureInfo(InputNumberCultureCode);
            System.Globalization.CultureInfo formatNumberCultureInfo = new System.Globalization.CultureInfo(FormatNumberCultureCode);
            double d = 0;


            if (Value != null && !object.ReferenceEquals(Value, DBNull.Value) && double.TryParse(Value, System.Globalization.NumberStyles.Number, inputNumberCultureInfo, out d))
            {
                if (Value is double || Value is decimal || Value is int || Value is long)
                {
                    d = Convert.ToDouble(Value);
                }

                if (d == Math.Floor(d) && RemoveDecimalZeros)
                {
                    formatNumberCultureInfo.NumberFormat.NumberDecimalDigits = 0;
                }
                else
                {
                    formatNumberCultureInfo.NumberFormat.NumberDecimalDigits = MaxDecimalLength;
                }
                numberStr = d.ToString("n", formatNumberCultureInfo);

                //if (formatNumberCultureInfo.NumberFormat.NumberDecimalDigits > 0 && RemoveDecimalZeros)
                //{
                //    numberStr = numberStr.TrimEnd(0);
                //    numberStr = numberStr.TrimEnd(formatNumberCultureInfo.NumberFormat.NumberDecimalSeparator);
                //}

                numberStr = numberStr.Replace(formatNumberCultureInfo.NumberFormat.NumberGroupSeparator, string.Empty);
                NumericValue = double.Parse(numberStr, formatNumberCultureInfo);

            }

            return numberStr;

        }

        public List<PropertyReviewExt> GetReviews1(int HotelID)
        {
            // string PropertyConditions = "";
            DataSet ds = new DataSet();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetReservationReviews", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "CreateDateTime DESC");
            cmd.Parameters.AddWithValue("@PagingSize", int.MaxValue);
            cmd.Parameters.AddWithValue("@PageIndex", 1);
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);
            SQLCon.Close();

            List<PropertyReviewExt> ListOfModel1 = new List<PropertyReviewExt>();
           
            int TotalRecordCount=0;
   
            
            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            if (ds != null)
            {
                dt = ds.Tables[0];
                dt1 = ds.Tables[1];
            }
          
         

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyReviewExt ReviewsObj = new PropertyReviewExt();
                    TotalRecordCount = Convert.ToInt32(dr["TotalRecordCount"]);
                    ListOfModel1.Add(ReviewsObj);
                }
            }
            if (dt1.Rows.Count > 0)
            {
                foreach (DataRow dr in dt1.Rows)
                {
                    PropertyReviewExt ReviewsObj = new PropertyReviewExt();
                    ReviewsObj.ReservationReviewID = Convert.ToInt32(dr["ID"]);
                    ReviewsObj.ReservationID = Convert.ToInt32(dr["ReservationID"]);
                    ReviewsObj.TravellerTypeID = Convert.ToInt32(dr["TravellerTypeID"]);
                    ReviewsObj.TravelerTypeName = dr["TravelerTypeName"].ToString();
                    ReviewsObj.Review = dr["Review"].ToString();
                    ReviewsObj.AveragePoint = Convert.ToDecimal(dr["AveragePoint"]);
                    double AvgPoint = Convert.ToDouble(Convert.ToDecimal(dr["AveragePoint"]) / 5);
                    if (AvgPoint != 0)
                    {
                        ReviewsObj.width2 = Convert.ToDouble(Math.Floor(AvgPoint * 150));
                        ReviewsObj.width3 = ReviewsObj.width2 + "px";
                    }
                    ReviewsObj.ReviewStatusID = Convert.ToInt32(dr["ReviewStatusID"]);
                    ReviewsObj.ReviewStatusName = dr["ReviewStatusName"].ToString();
                    ReviewsObj.Anonymous = Convert.ToBoolean(dr["Anonymous"]);
                    ReviewsObj.Active = Convert.ToBoolean(dr["Active"]);
                    ReviewsObj.CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]);
                    ReviewsObj.OpDateTime = Convert.ToDateTime(dr["OpDateTime"]);
                    ReviewsObj.IPAddress = dr["IPAddress"].ToString();
                    ReviewsObj.ReviewTypeID = Convert.ToInt32(dr["ReviewTypeID"]);
                    ReviewsObj.ReviewTypeName = dr["ReviewTypeName"].ToString();
                    ReviewsObj.ReviewTypeEvaluationName = dr["ReviewTypeEvaluationName"].ToString();
                    ReviewsObj.sort = Convert.ToInt32(dr["Sort"]);
                    ReviewsObj.Point = Convert.ToDecimal(dr["Point"]);
                    double AveragePoint1 = Convert.ToDouble(Convert.ToDecimal(dr["Point"]) / 5);

                    if (AveragePoint1 != 0)
                    {
                        ReviewsObj.width = Convert.ToDouble(Math.Floor(AveragePoint1 * 75));
                        ReviewsObj.width1 = ReviewsObj.width + "px";
                    }
                    ReviewsObj.ReviewTypeScaleName = dr["ReviewTypeScaleName"].ToString();
                    ReviewsObj.UserID = Convert.ToInt32(dr["UserID"]);
                    ReviewsObj.UserName = dr["UserName"].ToString();
                    ReviewsObj.UserFullName = dr["UserFullName"].ToString();
                    ReviewsObj.PartID = Convert.ToInt32(dr["PartID"]);
                    ReviewsObj.Part = dr["Part"].ToString();
                    ReviewsObj.FirmID = Convert.ToInt32(dr["FirmID"]);
                    ReviewsObj.FirmName = dr["FirmName"].ToString();
                    ReviewsObj.CountryID = Convert.ToInt32(dr["CountryID"]);
                    ReviewsObj.CountryName = dr["CountryName"].ToString();
                    //ReviewsObj.ReviewCount1 = TotalRecordCount / ReviewTypeCount1;
                    ReviewsObj.ReviewInfo = dr["UserFullName"].ToString() +" - "+dr["CountryName"].ToString()+" - "+Convert.ToDateTime(dr["CreateDateTime"]);
                    ListOfModel1.Add(ReviewsObj);
                }
            }
            return ListOfModel1;
        }

        public DataSet GetReviewTypeCount(int HotelID)
        {
            DataSet ds = new DataSet();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_GetReviewTypeCount_TB_TypeReview_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);
            SQLCon.Close();
            return ds;
        }
    }
}