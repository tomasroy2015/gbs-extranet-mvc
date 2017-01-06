using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;


namespace gbsExtranetMVC.Models.Repositories
{

    public class ReservationExt
    {
        public Int64 ReservationID { get; set; }
        public string PinCode { get; set; }
        public string ReservationDate { get; set; }
        public string ReservationOwner { get; set; }
        public string Reservation { get; set; }
        public string Sum { get; set; }
        public double PayableAmount { get; set; }
        public string Cost { get; set; }
        public string Deposit { get; set; }
        public double ChargedAmount { get; set; }
        public string StatusName { get; set; }
        public int StatusID { get; set; }
        public int ReservationOperationID { get; set; }
        
        public string ReservationOperation { get; set; }
        public string EncryptReservationID { get; set; }
        public string Encryptcc { get; set; }
        public string Encrypthistory { get; set; }
        public string CheckInDate { get; set; }
        public string CheckOutDate { get; set; }
        public int CCTypeID { get; set; }
        public string CreditCardProvider { get; set; }
        public string NameontheCreditCard { get; set; }
        public string CreditCardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CVCCode { get; set; }

    }
    public class ReservationRepository : BaseRepository
    {
        public  string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<ReservationExt> GetReservationOperations()
        {
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_GetReservations_TB_Reservation_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CultureCode", CultureValue);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            List<ReservationExt> list = new List<ReservationExt>();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ReservationExt ReservationObj = new ReservationExt();
                    Encryption64 objEncryptreservation = new Encryption64();
                    string EncryptReservationID = dr["ReservationID"].ToString();
                    EncryptReservationID = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(EncryptReservationID, "58421043")));
                    ReservationObj.EncryptReservationID = EncryptReservationID;
                    string Encryptcc = "CC";
                    Encryptcc = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(Encryptcc, "58421043")));
                    ReservationObj.Encryptcc = Encryptcc;
                    string Encrypthistory = "History";
                    Encrypthistory = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(Encrypthistory, "58421043")));
                    ReservationObj.Encrypthistory = Encrypthistory;
                    ReservationObj.ReservationID = Convert.ToInt64(dr["ReservationID"]);
                    ReservationObj.PinCode = dr["PinCode"].ToString();
                    DateTime dt1 = Convert.ToDateTime(dr["ReservationDate"]);
                    ReservationObj.ReservationDate = (dt1.ToString("d"));
                   // ReservationObj.ReservationDate = Convert.ToDateTime(dr["ReservationDate"]);
                    ReservationObj.ReservationOwner = dr["FullName"].ToString();
                    ReservationObj.Reservation = dr["Reservation"].ToString();
                    ReservationObj.Sum = dr["Sum"].ToString();
                    ReservationObj.PayableAmount = Convert.ToDouble(dr["PayableAmount"]);
                    ReservationObj.Cost = dr["Cost"].ToString();
                    ReservationObj.Deposit = '(' + dr["CurrencyName"].ToString() + dr["CurrencySymbol"].ToString() + ' ' + dr["Deposit"].ToString() + ')';
                    ReservationObj.ChargedAmount = Convert.ToDouble(dr["ChargedAmount"]);
                    ReservationObj.StatusName = dr["StatusName"].ToString();
                    ReservationObj.ReservationOperationID = Convert.ToInt32(dr["ReservationOperationID"]);
                    ReservationObj.StatusID = Convert.ToInt32(dr["StatusID"]);
                    
                    ReservationObj.ReservationOperation = dr["ReservationOperationName"].ToString();

                    list.Add(ReservationObj);
                }
            }
            return list;
        }



        //public static string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
       

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
    }

   
}