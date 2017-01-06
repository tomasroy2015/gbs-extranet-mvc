using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Transactions;
using System.Net;
using System.Security.Cryptography;
using System.IO;
using System.Text;
using Business;

namespace gbsExtranetMVC.Models.Repositories
{
    public class ChangePasswordRepository : BaseRepository
    {
        string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<ChangePasswordExt> GetCode()
        {
            List<ChangePasswordExt> list = new List<ChangePasswordExt>();
            string code = "PasswordLength";
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetContactUs_BizTbl_Parameter_SP", SQLCon);
            cmd.Parameters.AddWithValue("@code", code);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ChangePasswordExt EmailObj = new ChangePasswordExt();

                       EmailObj.PasswordLen = dr["value"].ToString();
                       EmailObj.Valid ="^.*(?=.{" + EmailObj.PasswordLen + ",})(?=.*d)(?=.*[a-zA-Z]).*$";
                       list.Add(EmailObj);
                }
            }
            return list;
        }


        public DataTable GetUser(Controller Ctrl)
        {
           // string Pass = "";
            List<ChangePasswordExt> list = new List<ChangePasswordExt>();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("BizSp_GetUsers", SQLCon);
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "ID");
            cmd.Parameters.AddWithValue("@PagingSize", 1);
            cmd.Parameters.AddWithValue("@PageIndex", 1);
            cmd.Parameters.AddWithValue("@UserID", Convert.ToInt64(Ctrl.Session["UserID"]));
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            sda.Fill(ds);
            if (ds != null)
            {
                dt = ds.Tables[1];
            }
            SQLCon.Close();
            //if (dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        ChangePasswordExt EmailObj = new ChangePasswordExt();
            //        Pass = dr["Password"].ToString();
            //        string Decryptedoperations = Decrypt128New(Pass);
            //        EmailObj.Password = Decryptedoperations;
            //        Pass = EmailObj.Password;
            //        EmailObj.EmailID = dr["Email"].ToString();
            //        EmailObj.Username = dr["UserName"].ToString();
            //        list.Add(EmailObj);
            //    }
            //    if (Pass == CurrentPassword)
            //    {
            //        int updatePassword = Update(NewPassword, Ctrl);
            //    }
            //}
            return dt;
        }


        public List<ChangePasswordExt> GetUserDetails(Controller Ctrl, string CurrentPassword, string NewPassword, string Verification)
        {
            string Pass = "";
            List<ChangePasswordExt> list = new List<ChangePasswordExt>();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("BizSp_GetUsers", SQLCon);
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "ID");
            cmd.Parameters.AddWithValue("@PagingSize", 1);
            cmd.Parameters.AddWithValue("@PageIndex", 1);
            cmd.Parameters.AddWithValue("@UserID", Convert.ToInt64(Ctrl.Session["UserID"]));
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);

            sda.Fill(ds);
            if (ds != null)
            {
                dt = ds.Tables[1];
            }
            SQLCon.Close();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    ChangePasswordExt EmailObj = new ChangePasswordExt();
                    Pass = dr["Password"].ToString();
                    string Decryptedoperations = Decrypt128New(Pass);
                    EmailObj.Password = Decryptedoperations;
                    Pass = EmailObj.Password;
                    EmailObj.EmailID = dr["Email"].ToString();
                    EmailObj.Username = dr["UserName"].ToString();
                    list.Add(EmailObj);
                }
              
            }
            return list;
        }

        public string Decrypt128New(string Pass)
        {
                      
            //  string EncryptionKey = "MAKV2SPBNI99212";
            byte[] cipherBytes = Convert.FromBase64String(Pass);
            using (Aes encryptor = Aes.Create())
            {
                encryptor.Key = Encoding.UTF8.GetBytes("2428598755421637");
                encryptor.IV = Encoding.UTF8.GetBytes("5369523205842148");

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    Pass = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return Pass;
        }

        public string Encrypt(string clearText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                encryptor.Key = Encoding.UTF8.GetBytes("2428598755421637");
                encryptor.IV = Encoding.UTF8.GetBytes("5369523205842148");

                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }

     
        public int Update(string CurrentPassword,Controller Ctrl)
        {
            //string Encryptedoperations = Encrypt(CurrentPassword);
            string Encryptedoperations = (new BizCrypto.AES128()).Encrypt(CurrentPassword);  
            DBEntities obj = new DBEntities();
            var OpUserIDParameter = new SqlParameter("@OpUserID", Convert.ToInt64(Ctrl.Session["UserID"]));
            var PasswordParameter = new SqlParameter("@Password", Encryptedoperations);
            int i = obj.Database.ExecuteSqlCommand("B_Ex_UpdatePassword_BizTbl_User_SP @OpUserID,@Password", OpUserIDParameter, PasswordParameter);
            return i;
        }

        public DataTable GetMailTemplates(string MaiTemplateCode, string Culturecode)
        {
            SQLCon.Open();
            DataTable dt = new DataTable();
            SqlCommand cmd = new SqlCommand("B_GetMailTemplate_BizTbl_MailTemplate_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@MailTemplateCode", MaiTemplateCode);
            cmd.Parameters.AddWithValue("@Culture", Culturecode);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            return dt;
        }
        public string GetParameter(string Parameter)
        {
            string Status = "";
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetParameter_BizTbl_Parameter_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Code", Parameter);
            Status = cmd.ExecuteScalar().ToString();
            SQLCon.Close();
            return Status;
        }
         
    }

    public class ChangePasswordExt
    {
        public string PasswordLen { get; set; }

        public string Valid { get; set; }

        public string Password { get; set; }

        public string EmailID { get; set; }

        public string Username { get; set; }

       
    }
}