using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class PropertyCancelPolicyRepository : BaseRepository
    {
        public  string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public int UpdatePropertyCancelPolicyInfo(int HotelID, string CanceltypeID, string PenaltyRateType, string RefundableDayCount, Controller ctrl)
        {
            Int64 UserID = Convert.ToInt64(ctrl.Session["UserID"]); 
            SQLCon.Open();
            int status = 0;
            SqlCommand cmd = new SqlCommand("B_Ex_UpdateHotelCancelPolicy_TB_HotelCancelPolicy_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            cmd.Parameters.AddWithValue("@CanceltypeID", CanceltypeID);
            cmd.Parameters.AddWithValue("@PenaltyRateType", PenaltyRateType);
            cmd.Parameters.AddWithValue("@RefundableDayCount", RefundableDayCount);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            status = Convert.ToInt32(cmd.ExecuteNonQuery());
            SQLCon.Close();
            return status;
            
        }

        public List<PropertyCancelPolicyExt> GetHotelCancelPolicyinfo(int HotelID)
        {

            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelCancelPolicy", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            List<PropertyCancelPolicyExt> ListOfModel = new List<PropertyCancelPolicyExt>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyCancelPolicyExt HotelcancelpolicyObj = new PropertyCancelPolicyExt();
                  
                    HotelcancelpolicyObj.CancelTypeID = Convert.ToInt32(dr["CancelTypeID"]);
                    HotelcancelpolicyObj.CancelTypeName = dr["CancelTypeName"].ToString();
                    HotelcancelpolicyObj.Refundable = dr["Refundable"].ToString();
                    HotelcancelpolicyObj.RefundableDayCount = dr["RefundableDayCount"].ToString();
                    string PenaltyRateType = dr["PenaltyRateTypeID"].ToString();
                    if (PenaltyRateType != "")
                    {
                        HotelcancelpolicyObj.PenaltyRateTypeID = Convert.ToInt32(PenaltyRateType);
                    }
                    else
                    {
                        HotelcancelpolicyObj.PenaltyRateTypeID = 0;
                    }
                    HotelcancelpolicyObj.PenaltyRateTypeName = dr["PenaltyRateTypeName"].ToString();
                    ListOfModel.Add(HotelcancelpolicyObj);
                }

            }
            return ListOfModel;
        }
          

        public List<PropertyCancelPolicyExt> GetHotelCancelPolicy()
        {

            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelCancelPolicy", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            List<PropertyCancelPolicyExt> ListOfModel = new List<PropertyCancelPolicyExt>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyCancelPolicyExt HotelcancelpolicyObj = new PropertyCancelPolicyExt();
                    HotelcancelpolicyObj.HotelID = Convert.ToInt32(dr["HotelID"]);
                    HotelcancelpolicyObj.CancelTypeID = Convert.ToInt32(dr["CancelTypeID"]);
                    HotelcancelpolicyObj.CancelTypeName = dr["CancelTypeName"].ToString();
                    HotelcancelpolicyObj.Refundable = dr["Refundable"].ToString();
                    HotelcancelpolicyObj.RefundableDayCount = dr["RefundableDayCount"].ToString();
                    string PenaltyRateType = dr["PenaltyRateTypeID"].ToString();
                    if (PenaltyRateType !="")
                    {
                        HotelcancelpolicyObj.PenaltyRateTypeID = Convert.ToInt32(PenaltyRateType);
                    }
                    else
                    {
                        HotelcancelpolicyObj.PenaltyRateTypeID = 0;
                    }
                    HotelcancelpolicyObj.PenaltyRateTypeName = dr["PenaltyRateTypeName"].ToString();
                    ListOfModel.Add(HotelcancelpolicyObj);
                }

            }
            return ListOfModel;
        }
          
          
    }

    public class PropertyCancelPolicyExt
    {

        public int HotelID { get; set; }
        public int CancelTypeID { get; set; }
        public string CancelTypeName { get; set; }
        public string Refundable { get; set; }
        public string RefundableDayCount { get; set; }
        public int PenaltyRateTypeID { get; set; }
        public string PenaltyRateTypeName { get; set; }
    }
    
}