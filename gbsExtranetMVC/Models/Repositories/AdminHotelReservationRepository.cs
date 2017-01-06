using Business;
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
    public class AdminHotelReservationExt
    {
        public Int64 ReservationID { get; set; }
        public DateTime ReservationDate { get; set; }
        public string ReservationOwner { get; set; }
        public string StatusName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int CCTypeID { get; set; }
        public string CreditCardProvider { get; set; }
        public string NameontheCreditCard { get; set; }
        public string CreditCardNumber { get; set; }
        public string ExpirationDate { get; set; }
        public string CVCCode { get; set; }
       

    }
     
    public class AdminHotelReservationRepository : BaseRepository
    {
        public  string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<ReservationExt> GetReservations(Int64 ReservationID, Controller ctrl,bool systemadmin,string originaluserid)
        {
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetResevations_TB_Reservations_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "Name ASC,ID ASC");
            cmd.Parameters.AddWithValue("@PagingSize", int.MaxValue);
            cmd.Parameters.AddWithValue("@PageIndex", 1);
            cmd.Parameters.AddWithValue("@ReservationID", ReservationID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
           
            List<ReservationExt> ListOfModel = new List<ReservationExt>();
          
            Int64 OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            Int64 useroriginaluserid = Convert.ToInt64(originaluserid);
          
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ReservationExt ReservationObj = new ReservationExt();
                    ReservationObj.ReservationID = Convert.ToInt32(dr["ReservationID"]);
                    ReservationObj.ReservationOwner = dr["FullName"].ToString();
                    ReservationObj.StatusName = dr["StatusName"].ToString();
                    DateTime dt1 = Convert.ToDateTime(dr["ReservationDate"]);
                    DateTime dt2 = Convert.ToDateTime(dr["CheckInDate"]);
                    DateTime dt3 = Convert.ToDateTime(dr["CheckOutDate"]);
                    ReservationObj.ReservationDate = (dt1.ToString("d"));
                    ReservationObj.CheckInDate = (dt2.ToString("d"));
                    ReservationObj.ReservationOperation = dr["ReservationOperationName"].ToString();
                    DateTime CheckOutDate =Convert.ToDateTime( dr["CheckOutDate"]);
                    ReservationObj.CheckOutDate = (dt3.ToString("d"));
                
                    DBEntities insertentity = new DBEntities();
                 
                    string rowValue = "";
                    string rowValues = "";
                    if (((dt.Rows.Count > 0) && ((CheckOutDate.AddDays(7) >= DateTime.Now)) && Convert.ToInt32(dr["ReservationID"]) == ReservationID))

                    {
                        DataTable tbl = BizApplication.GetUserOperations(BizDB, "Date DESC", CultureValue, "", Convert.ToString(ReservationID), Convert.ToString(17), null);
                         if (tbl.Rows.Count > 0)
                         {
                             DataRow row = tbl.Rows[0];
                             rowValue = row["Date"].ToString();
                         }

                         DataTable tbl1 = BizApplication.GetUserOperations(BizDB, "Date DESC", CultureValue, Convert.ToString(OpUserID), Convert.ToString(ReservationID), Convert.ToString(17), null);
                        if (tbl1.Rows.Count > 0)
                        {
                            DataRow rows = tbl1.Rows[0];
                            rowValues = rows["Date"].ToString();
                        }

                        if (systemadmin == true || (Business.BizApplication.GetUserOperations(BizDB, "Date DESC", CultureValue, Convert.ToString(OpUserID), Convert.ToString(ReservationID), Convert.ToString(2), null).Rows.Count == 0 ||
                        BizApplication.GetUserOperations(BizDB, "Date DESC", CultureValue, "", Convert.ToString(ReservationID), Convert.ToString(17), null).Rows.Count == 1 &&
                        (DateTime.ParseExact(rowValue, "yyyy-MM-dd HH:mm tt", System.Globalization.CultureInfo.InvariantCulture)) >(DateTime.ParseExact(rowValue, "yyyy-MM-dd HH:mm tt", System.Globalization.CultureInfo.InvariantCulture))))

                        {
                            ReservationObj.CreditCardProvider = dr["CCTypeName"].ToString();
                            string NameOnCreditcard = dr["CCFullName"].ToString();
                            ReservationObj.NameontheCreditCard = Decrypt128New(NameOnCreditcard, "2164285821854754", "5436265039712626");
                            string CreditcardNumber = dr["CCNo"].ToString();
                            ReservationObj.CreditCardNumber = Decrypt128New(CreditcardNumber, "6164285828955421", "6485880454987489");
                            string CVCCode = dr["CCCVC"].ToString();
                            ReservationObj.CVCCode = Decrypt128New(CVCCode, "5267912096542731", "6359629697944359");
                            string CardExpriryDate = dr["CCExpiration"].ToString();
                            ReservationObj.ExpirationDate = Decrypt128New(CardExpriryDate, "5216428540391821", "6961584652179891");
                            // divCCDisplayWarning.Visible = true;
                            if (systemadmin != true)
                            {
                                BizTbl_UserOperation UserObj = new BizTbl_UserOperation();
                                UserObj.UserID = OpUserID;
                                UserObj.Date = DateTime.Now;
                                UserObj.OperationTypeID = 2;
                                UserObj.IPAddress = "";
                                UserObj.PartID = 1;
                                UserObj.RecordID =ReservationID;
                                UserObj.UserSessionID = null;
                                insertentity.BizTbl_UserOperation.Add(UserObj);
                                insertentity.SaveChanges();            
                               // Business.BizUser.AddUserOperation(BizDB, Convert.ToString(OpUserID), Convert.ToString(DateTime.Now),Business.BizCommon.Operation.CreditCardInfoViewed, "", Convert.ToString(ReservationID), "", "");
                            }

                        }
                        else
                        {
                            // MasterPage.AddMessage(BizDB, "CreditCardAlreadyDisplayedWarning", CultureValue, BizCommon.MessageType.Info);

                        }
                    }
                   




                    ListOfModel.Add(ReservationObj);

                    
                   
                    
                    
                }
            }
            return ListOfModel;
        }

        public string Decrypt128New(string cipherText, string key, string IV)
        {
            //  string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                encryptor.Key = Encoding.UTF8.GetBytes(key);
                encryptor.IV = Encoding.UTF8.GetBytes(IV);

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }

        //public static string Encrypt128New(string clearText, string key, string IV)
        //{
        //    //string EncryptionKey = "MAKV2SPBNI99212";
        //    byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
        //    using (Aes encryptor = Aes.Create())
        //    {

        //        encryptor.Key = Encoding.UTF8.GetBytes(key);
        //        encryptor.IV = Encoding.UTF8.GetBytes(IV);
        //        using (MemoryStream ms = new MemoryStream())
        //        {
        //            using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
        //            {
        //                cs.Write(clearBytes, 0, clearBytes.Length);
        //                cs.Close();
        //            }
        //            clearText = Convert.ToBase64String(ms.ToArray());
        //        }
        //    }
        //    return clearText;
        //}
        public class Encryption64
        {

            private byte[] key = { };
            //private byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };

            private byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

            public string Decrypt(string stringToDecrypt, string sEncryptionKey)
            {
                byte[] inputByteArray = new byte[stringToDecrypt.Length];
                try
                {
                    key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    inputByteArray = Convert.FromBase64String(stringToDecrypt);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(key, IV),
                                                                      CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    Encoding encoding = Encoding.UTF8;
                    return encoding.GetString(ms.ToArray());
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }


            
        }
    }

   
}