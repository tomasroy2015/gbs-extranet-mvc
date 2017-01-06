using gbsExtranetMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace gbsExtranetMVC.Models
{
    public class Home : BaseRepository
    {
        public string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<HomeExt> GetCulturecode()
        {
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_Getculture_BizTbl_Culture", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            SQLCon.Close();
            List<HomeExt> CultureList = new List<HomeExt>();

            if (dt != null)
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow dr in dt.Rows)
                    {
                        HomeExt obj = new HomeExt();
                        obj.Code = dr["Code"].ToString();
                        obj.SystemCode = dr["SystemCode"].ToString();
                        obj.Description = dr["Description"].ToString();
                        CultureList.Add(obj);
                    }
                }
            }

            return CultureList;
        }
        Encryption64 objEncryptreservation = new Encryption64();
        public List<HomeExt> ReadCheckInFuture(int HotelID, DateTime CheckInStartDate, DateTime CheckInEndDate)
        {
            List<HomeExt> list = new List<HomeExt>();

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelReservations", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderBy", "CheckInDate");
            cmd.Parameters.AddWithValue("@Culture", CultureCode);
            cmd.Parameters.AddWithValue("@PagingSize", int.MaxValue);
            cmd.Parameters.AddWithValue("@PageIndex", 1);
            cmd.Parameters.AddWithValue("@ReservationStatusID", 1);
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            cmd.Parameters.AddWithValue("@CheckInStartDate", CheckInStartDate.Date);
            cmd.Parameters.AddWithValue("@CheckInEndDate", CheckInEndDate.Date);
            cmd.Parameters.AddWithValue("@Active", 1);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            sda.Fill(ds);
            SQLCon.Close();
            dt = ds.Tables[1].DefaultView.ToTable(true, "ReservationID", "PinCode", "ReservationDate", "FullName", "CheckInDate", "CheckOutDate", "StatusID", "StatusName");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    HomeExt model = new HomeExt();
                    model.ReservationID = dr["ReservationID"].ToString();
                    string EncryptReservationID = dr["ReservationID"].ToString();
                    EncryptReservationID = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(EncryptReservationID, "58421043")));
                    model.EncryptReservationID = EncryptReservationID;
                    model.ReservationOwener = dr["FullName"].ToString();
                    DateTime CheckinDateDB = Convert.ToDateTime(dr["CheckInDate"]).Date;
                    DateTime Nowdate = DateTime.Now.Date;
                    if (CheckinDateDB == Nowdate)
                    {
                        model.CheckinDate = Resources.Resources.Today;
                    }
                    else if (CheckinDateDB == Nowdate.AddDays(1))
                    {
                        model.CheckinDate = Resources.Resources.Tomorrow;
                    }
                    else
                    {
                        model.CheckinDate = CheckinDateDB.ToString("dd MMMM yyyy, dddd");
                    }
                    list.Add(model);
                }
            }

            return list;
        }

        public List<HomeExt> ReadCheckOutFuture(int HotelID, DateTime CheckOutStartDate, DateTime CheckOutEndDate)
        {
            List<HomeExt> list = new List<HomeExt>();

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelReservations", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderBy", "CheckOutDate");
            cmd.Parameters.AddWithValue("@Culture", CultureCode);
            cmd.Parameters.AddWithValue("@PagingSize", int.MaxValue);
            cmd.Parameters.AddWithValue("@PageIndex", 1);
            cmd.Parameters.AddWithValue("@ReservationStatusID", 1);
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            cmd.Parameters.AddWithValue("@CheckOutStartDate", CheckOutStartDate.Date);
            cmd.Parameters.AddWithValue("@CheckOutEndDate", CheckOutEndDate.Date);
            cmd.Parameters.AddWithValue("@Active", 1);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            sda.Fill(ds);
            SQLCon.Close();
            dt = ds.Tables[1].DefaultView.ToTable(true, "ReservationID", "PinCode", "ReservationDate", "FullName", "CheckInDate", "CheckOutDate", "StatusID", "StatusName"); ;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    HomeExt model = new HomeExt();
                    model.ReservationID = dr["ReservationID"].ToString();
                    model.ReservationOwener = dr["FullName"].ToString();
                    DateTime CheckoutDateDB = Convert.ToDateTime(dr["CheckOutDate"]).Date;
                    string EncryptReservationID = dr["ReservationID"].ToString();
                    EncryptReservationID = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(EncryptReservationID, "58421043")));
                    model.EncryptReservationID = EncryptReservationID;
                    DateTime Nowdate = DateTime.Now.Date;
                    if (CheckoutDateDB == Nowdate)
                    {
                        model.CheckoutDate = Resources.Resources.Today;
                    }
                    else if (CheckoutDateDB == Nowdate.AddDays(1))
                    {
                        model.CheckoutDate = Resources.Resources.Tomorrow;
                    }
                    else
                    {
                        model.CheckoutDate = CheckoutDateDB.ToString("dd MMMM yyyy, dddd");
                    }
                    list.Add(model);
                }
            }

            return list;
        }
         public List<HomeExt> recentHotelReservations(int HotelID, DateTime ReservationStartDate, DateTime ReservationEndDate)
        {
            List<HomeExt> list = new List<HomeExt>();

            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelReservations", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@OrderBy", "ReservationDate");
            cmd.Parameters.AddWithValue("@Culture", CultureCode);
            cmd.Parameters.AddWithValue("@PagingSize", int.MaxValue);
            cmd.Parameters.AddWithValue("@PageIndex", 1);
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            cmd.Parameters.AddWithValue("@ReservationStartDate", ReservationStartDate.Date);
            cmd.Parameters.AddWithValue("@ReservationEndDate", ReservationEndDate.Date);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            sda.Fill(ds);
            SQLCon.Close();
            dt = ds.Tables[1].DefaultView.ToTable(true, "ReservationID", "PinCode", "ReservationDate", "FullName", "CheckInDate", "CheckOutDate", "StatusID", "StatusName");
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    HomeExt model = new HomeExt();
                    model.ReservationID = dr["ReservationID"].ToString();
                    model.ReservationOwener = dr["FullName"].ToString();
                    string EncryptReservationID = dr["ReservationID"].ToString();
                    EncryptReservationID = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(EncryptReservationID, "58421043")));
                    model.EncryptReservationID = EncryptReservationID;
                    DateTime Resrvationdate = Convert.ToDateTime(dr["ReservationDate"]).Date;
                    DateTime Nowdate = DateTime.Now.Date;
                    if (Resrvationdate == Nowdate)
                    {
                        model.Resrvationdate = Resources.Resources.Today;
                    }
                    else if (Resrvationdate == Nowdate.AddDays(-1))
                    {
                        model.Resrvationdate = Resources.Resources.Yesterday;
                    }
                    else
                    {
                        model.Resrvationdate = Resrvationdate.ToString("dd MMMM yyyy, dddd");
                    }
                    list.Add(model);
                }
            }

            return list;
        }
         public List<HomeExt> recentHotelReservationCancels(int HotelID, DateTime CancelStartDate, DateTime CancelEndDate)
         {
             List<HomeExt> list = new List<HomeExt>();

             DataTable dt = new DataTable();
             DataSet ds = new DataSet();
             SQLCon.Open();
             SqlCommand cmd = new SqlCommand("TB_SP_GetHotelReservations", SQLCon);
             cmd.CommandType = CommandType.StoredProcedure;
             cmd.Parameters.AddWithValue("@OrderBy", "CancelDateTime");
             cmd.Parameters.AddWithValue("@Culture", CultureCode);
             cmd.Parameters.AddWithValue("@PagingSize", int.MaxValue);
             cmd.Parameters.AddWithValue("@PageIndex", 1);
             cmd.Parameters.AddWithValue("@HotelID", HotelID);
             cmd.Parameters.AddWithValue("@CancelStartDate", CancelStartDate.Date);
             cmd.Parameters.AddWithValue("@CancelEndDate", CancelEndDate.Date);
             cmd.Parameters.AddWithValue("@Active", 0);
             SqlDataAdapter sda = new SqlDataAdapter(cmd);

             sda.Fill(ds);
             SQLCon.Close();
             dt = ds.Tables[1].DefaultView.ToTable(true, "ReservationID", "PinCode", "ReservationDate", "CancelDateTime", "FullName", "CheckInDate", "CheckOutDate", "StatusID", "StatusName");
             if (dt.Rows.Count > 0)
             {
                 foreach (DataRow dr in dt.Rows)
                 {
                     HomeExt model = new HomeExt();
                     model.ReservationID = dr["ReservationID"].ToString();
                     model.ReservationOwener = dr["FullName"].ToString();
                     string EncryptReservationID = dr["ReservationID"].ToString();
                     EncryptReservationID = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(EncryptReservationID, "58421043")));
                     model.EncryptReservationID = EncryptReservationID;
                     DateTime CancelDateTime = Convert.ToDateTime(dr["CancelDateTime"]).Date;
                     DateTime Nowdate = DateTime.Now.Date;
                     if (CancelDateTime == Nowdate)
                     {
                         model.CancelDate = Resources.Resources.Today;
                     }
                     else if (CancelDateTime == Nowdate.AddDays(-1))
                     {
                         model.CancelDate = Resources.Resources.Yesterday;
                     }
                     else
                     {
                         model.CancelDate = CancelDateTime.ToString("dd MMMM yyyy, dddd");
                     }
                     int statusID=Convert.ToInt32(dr["StatusID"]);
                     if (statusID==1)
                     {
                         model.CancelDate = model.CancelDate + "<br>" + Resources.Resources.PartialHotelReservationCancelWarning;
                     }
                     list.Add(model);
                 }
             }

             return list;
         }
         public string ConvertStringToHex(string asciiString)
         {
             string hex = "";
             foreach (char c in asciiString)
             {
                 int tmp = c;
                 hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
             }
             return hex;
         }
    
    }
    public class Encryption64
    {

        private byte[] key = { };
        //private byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };

        private byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

        //public string Decrypt(string stringToDecrypt, string sEncryptionKey)
        //{
        //    byte[] inputByteArray = new byte[stringToDecrypt.Length];
        //    try
        //    {
        //        key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
        //        DESCryptoServiceProvider des = new DESCryptoServiceProvider();
        //        inputByteArray = Convert.FromBase64String(stringToDecrypt);
        //        MemoryStream ms = new MemoryStream();
        //        CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV),
        //                                                          CryptoStreamMode.Write);
        //        cs.Write(inputByteArray, 0, inputByteArray.Length);
        //        cs.FlushFinalBlock();
        //        Encoding encoding = Encoding.UTF8;
        //        return encoding.GetString(ms.ToArray());
        //    }
        //    catch (Exception ex)
        //    {
        //        return ex.Message;
        //    }
        //}


        public string Encrypt(string stringToEncrypt, string sEncryptionKey)
        {
            try
            {
                key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
                DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                Byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                MemoryStream ms = new MemoryStream();
                CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV),
                                                                  CryptoStreamMode.Write);
                cs.Write(inputByteArray, 0, inputByteArray.Length);
                cs.FlushFinalBlock();
                return Convert.ToBase64String(ms.ToArray());
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
    public class HomeExt
    {
        public string Code { get; set; }
        public string SystemCode { get; set; }
        public string Description { get; set; }
        public string ReservationID { get; set; }
        public string ReservationOwener { get; set; }
        public string CheckinDate { get; set; }
        public string CheckoutDate { get; set; }
        public string Resrvationdate { get; set; }
        public string CancelDate { get; set; }
        public string EncryptReservationID { get; set; }
        
    }
   
}