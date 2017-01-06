using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.Data.SqlClient;


namespace gbsExtranetMVC.Models.Repositories
{
    public class InvoiceRepository : BaseRepository
    {
        //public DataTable GetInvoice()
        //{
        //    DataTable dt = new DataTable();
        //    SQLCon.Open();
        //    SqlCommand cmd = new SqlCommand("B_GetInvoices_TB_Invoice_SP", SQLCon);
        //    cmd.CommandType = CommandType.StoredProcedure;        
        //    SqlDataAdapter sda = new SqlDataAdapter(cmd);
        //    sda.Fill(dt);
        //    SQLCon.Close();
        //    return dt;
        //}
        public List<InvoiceExt> GetInvoice(bool IsAdmin=false)
        {
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            List<InvoiceExt> list = new List<InvoiceExt>();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();
            
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetInvoices", SQLCon);
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "ID");
            cmd.Parameters.AddWithValue("@PagingSize", int.MaxValue);
            cmd.Parameters.AddWithValue("@PageIndex", 1);
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);

            if(ds!=null)
            {
                dt = ds.Tables[1];
            }

            SQLCon.Close();
            // return dt;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    InvoiceExt EmailObj = new InvoiceExt();
                    EmailObj.ID = Convert.ToInt32(dr["ID"].ToString());
                    EmailObj.FirmID = dr["FirmID"].ToString();
                    EmailObj.Firm = dr["FirmName"].ToString();
                    EmailObj.InvoiceDate = dr["InvoiceDate"].ToString();
                    EmailObj.Period = dr["period"].ToString();
                    EmailObj.DueDate = dr["DueDate"].ToString();
                    EmailObj.Invoice_Amount = dr["CurrencySymbol"].ToString() + " " + dr["Amount"].ToString();
                    EmailObj.status = dr["InvoiceStatusName"].ToString();
                    //if (EmailObj.ID != null && EmailObj.ID!=0)
                    //{
                    //    DataTable Invoice = new DataTable();
                    //    SqlCommand cmd1 = new SqlCommand("TB_SP_GetInvoiceDetail", SQLCon);
                    //    cmd.Parameters.AddWithValue("@InvoiceID", EmailObj.ID);
                    //    cmd.Parameters.AddWithValue("@OrderBy", "ID");
                    //    cmd.CommandType = CommandType.StoredProcedure;
                    //    SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                    //    sda1.Fill(Invoice);
                    //    if(Invoice!=null)
                    //    {
                    //        if(Invoice.Rows.Count>0)
                    //        {
                    //            //foreach (DataRow InvoiceDetail in Invoice.Rows)
                    //            //{
                    //                EmailObj.ReservationID = Invoice.Rows[0]["ReservationID"].ToString();
                    //                EmailObj.ReservationAmount = Invoice.Rows[0]["CurrencySymbol"].ToString() + " " + Invoice.Rows[0]["Amount"].ToString();
                    //                EmailObj.ComissionAmount = Invoice.Rows[0]["ComissionCurrencySymbol"].ToString() + " " + Invoice.Rows[0]["ComissionAmount"].ToString();
                    //                EmailObj.ComissionRate = Invoice.Rows[0]["ComissionRate"].ToString();
                    //                EmailObj.CheckOutDate = Invoice.Rows[0]["CheckOutDate"].ToString();
                    //                EmailObj.CheckInDate = Invoice.Rows[0]["CheckInDate"].ToString();
                    //                EmailObj.Name = Invoice.Rows[0]["ReservationOwnerFullName"].ToString();
                    //            //}
                            
                    //        }
                    //    }

                    //}                   
                    EmailObj.InvoiceStatusID = dr["InvoiceStatusID"].ToString();

                    EmailObj.IsAdmin = IsAdmin;

                    list.Add(EmailObj);
                }
            }
            //SqlCommand cmd = new SqlCommand("B_GetInvoices_TB_Invoice_SP", SQLCon);
            //cmd.Parameters.AddWithValue("@CultureCode", CultureValue);
            //cmd.CommandType = CommandType.StoredProcedure;
            //SqlDataAdapter sda = new SqlDataAdapter(cmd);
            //sda.Fill(dt);
            //SQLCon.Close();
            //// return dt;
            //if (dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        InvoiceExt EmailObj = new InvoiceExt();
            //        EmailObj.ID = Convert.ToInt32(dr["InvoiceID"].ToString());
            //        EmailObj.FirmID = dr["FirmID"].ToString();
            //        EmailObj.Firm = dr["Name"].ToString();
            //        EmailObj.InvoiceDate = dr["InvoiceDate"].ToString();
            //        EmailObj.Period = dr["period"].ToString();
            //        EmailObj.DueDate = dr["DueDate"].ToString();
            //        EmailObj.Invoice_Amount = dr["InvoiceAmount"].ToString();
            //        EmailObj.status = dr["Name_en"].ToString();
            //        EmailObj.ReservationID = dr["ReservationID"].ToString();
            //        EmailObj.ReservationAmount = dr["ReservationAmount"].ToString();
            //        EmailObj.ComissionAmount = dr["ComissionAmount"].ToString();
            //        EmailObj.ComissionRate = dr["ComissionRate"].ToString();
            //        EmailObj.CheckOutDate = dr["CheckOutDate"].ToString();
            //        EmailObj.CheckInDate = dr["CheckInDate"].ToString();
            //        EmailObj.Name = dr["ReservationOwner"].ToString();
            //        EmailObj.InvoiceStatusID = dr["InvoiceStatusID"].ToString();

            //        EmailObj.IsAdmin = IsAdmin;

            //        list.Add(EmailObj);
            //    }
            //}
            return list;

        }

        public List<InvoiceExt> GetInvoices(long ID)
        {
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            List<InvoiceExt> list = new List<InvoiceExt>();
            DataSet ds = new DataSet();
            DataTable dt = new DataTable();

            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetInvoices", SQLCon);
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "ID");
            cmd.Parameters.AddWithValue("@PagingSize", int.MaxValue);
            cmd.Parameters.AddWithValue("@PageIndex", 1);
            cmd.Parameters.AddWithValue("@InvoiceID", ID);           
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(ds);

            if (ds != null)
            {
                dt = ds.Tables[1];
            }

            SQLCon.Close();
            // return dt;
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    InvoiceExt EmailObj = new InvoiceExt();
                    EmailObj.ID = Convert.ToInt32(dr["ID"].ToString());
                    EmailObj.FirmID = dr["FirmID"].ToString();
                    EmailObj.Firm = dr["FirmName"].ToString();
                    EmailObj.InvoiceDate = dr["InvoiceDate"].ToString();
                    EmailObj.Period = dr["period"].ToString();
                    EmailObj.DueDate = dr["DueDate"].ToString();
                    EmailObj.Invoice_Amount = dr["CurrencySymbol"].ToString() + " " + dr["Amount"].ToString();
                    EmailObj.status = dr["InvoiceStatusName"].ToString();
                    
                        DataTable Invoice = new DataTable();
                        SqlCommand cmd1 = new SqlCommand("TB_SP_GetInvoiceDetail", SQLCon);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.AddWithValue("@InvoiceID", ID);
                        cmd1.Parameters.AddWithValue("@OrderBy", "ID");

                        SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                        sda1.Fill(Invoice);
                        if (Invoice != null)
                        {
                            if (Invoice.Rows.Count > 0)
                            {

                                EmailObj.ReservationID = Invoice.Rows[0]["ReservationID"].ToString();
                                EmailObj.ReservationAmount = Invoice.Rows[0]["CurrencySymbol"].ToString() + " " + Invoice.Rows[0]["Amount"].ToString();
                                EmailObj.ComissionAmount = Invoice.Rows[0]["ComissionCurrencySymbol"].ToString() + " " + Invoice.Rows[0]["ComissionAmount"].ToString();
                                EmailObj.ComissionRate = Invoice.Rows[0]["ComissionRate"].ToString();
                                EmailObj.CheckOutDate = Invoice.Rows[0]["CheckOutDate"].ToString();
                                EmailObj.CheckInDate = Invoice.Rows[0]["CheckInDate"].ToString();
                                EmailObj.Name = Invoice.Rows[0]["ReservationOwnerFullName"].ToString();

                            }
                        }

                 
                    EmailObj.InvoiceStatusID = dr["InvoiceStatusID"].ToString();

                    list.Add(EmailObj);
                }
            }
            return list;

        }  

        public int UpdateInvoice(long id,Controller ctrl)
        {

            int status = 1;
            var obj = db.TB_Invoice.Where(x => x.ID == id).FirstOrDefault();          
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            var InvoiceIDParameter = new SqlParameter("@InvoiceID", id);
            int i = db.Database.ExecuteSqlCommand("B_UpdateInvoices_TB_Invoice_SP @InvoiceID", InvoiceIDParameter);
            return status;
        }
        
        
    }
    public class InvoiceExt
    {
        public int ID { get; set; }
        public string Firm { get; set; }

        public string Name { get; set; }

        public string ReservationID { get; set; }

        public string ReservationAmount { get; set; }

        public string ComissionRate { get; set; }

        public string ComissionAmount { get; set; }
        public string InvoiceDate { get; set; }

        public string CheckInDate { get; set; }

        public string CheckOutDate { get; set; }

        public string Period { get; set; }

        public string DueDate { get; set; }

        public string Invoice_Amount { get; set; }

        public string status { get; set; }

        public string InvoiceStatusID { get; set; }



        public bool IsAdmin { get; set; }

        public string FirmID { get; set; }
    }
}