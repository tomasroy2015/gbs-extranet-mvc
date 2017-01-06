using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.Data.SqlClient;
namespace gbsExtranetMVC.Models.Repositories
{
    public class UserMessagesRepository : BaseRepository
    {

        public List<UserMessagesExt> GetUserMessages()
        {
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            List<UserMessagesExt> list = new List<UserMessagesExt>();
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_GetUserMessages_TB_Message_SP", SQLCon);
            cmd.Parameters.AddWithValue("@CultureCode", CultureValue);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            // return dt;
            //bool displayButton = false;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    UserMessagesExt EmailObj = new UserMessagesExt();
                    EmailObj.ID = dr["ID"].ToString();
                    EmailObj.Subject = dr["Subject"].ToString();
                    EmailObj.Title = dr["Title"].ToString();
                    EmailObj.Name = dr["Name"].ToString();
                    EmailObj.Surname = dr["Surname"].ToString();
                    EmailObj.EmailID = dr["Email"].ToString();
                    EmailObj.Phone = dr["Phone"].ToString();
                    EmailObj.Country = dr["Country"].ToString();
                    EmailObj.Message = dr["Text"].ToString();
                    EmailObj.Status = dr["Status"].ToString();
                    EmailObj.CreatedDate = dr["CreateDateTime"].ToString();
                    EmailObj.UpdatedDate = dr["OpDateTime"].ToString();
                    EmailObj.IPaddress = dr["IPAddress"].ToString();
                    if (dr["MessageStatusID"].ToString()=="1")
                    {
                        EmailObj.displayButton = true;
                    }
                    else
                    {
                        EmailObj.displayButton = false;
                    }
                    
                    list.Add(EmailObj);
                }
            }
            return list;

        }

        public int UpdateUserMessage(long id, Controller ctrl)
        {

            int status = 1;
            var obj = db.TB_Message.Where(x => x.ID == id).FirstOrDefault();
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            obj.MessageStatusID = 2;
            db.SaveChanges();
            //var InvoiceIDParameter = new SqlParameter("@InvoiceID", id);
            //int i = db.Database.ExecuteSqlCommand("B_UpdateInvoices_TB_Invoice_SP @InvoiceID", InvoiceIDParameter);
            return status;
        }

        public int DeleteUserMessage(long id, Controller ctrl)
        {

            int status = 1;
            var obj = db.TB_Message.Where(x => x.ID == id).FirstOrDefault();
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            obj.MessageStatusID = 3;
            db.SaveChanges();
            //var InvoiceIDParameter = new SqlParameter("@InvoiceID", id);
            //int i = db.Database.ExecuteSqlCommand("B_UpdateInvoices_TB_Invoice_SP @InvoiceID", InvoiceIDParameter);
            return status;
        }
    }

    public class UserMessagesExt
    {
        public string ID { get; set; }
        public string Subject { get; set; }
        public string Title { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool displayButton { get; set; }
        public string EmailID { get; set; }
        public string Phone { get; set; }
        public string Country { get; set; }

        public string Message { get; set; }
        public string Status { get; set; }

        public string CreatedDate { get; set; }

        public string UpdatedDate { get; set; }

        public string IPaddress { get; set; }    

    }
}