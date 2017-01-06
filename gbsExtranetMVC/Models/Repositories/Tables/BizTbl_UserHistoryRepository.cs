using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class BizTbl_UserHistoryRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<BizTbl_UserHistoryExt> ReadAll(int TableID)
        {
            List<BizTbl_UserHistoryExt> list = new List<BizTbl_UserHistoryExt>();
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_DisplayTable_BizTbl_Table_Sp", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@TableID", TableID);
            cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    BizTbl_UserHistoryExt model = new BizTbl_UserHistoryExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.UserID = Convert.ToInt32(dr["UserID"]);
                    model.SalutationType = dr["FK_SalutationTypeID_ID"].ToString();
                    model.Name = dr["Name"].ToString();
                    model.Surname = dr["Surname"].ToString();
                    model.Country = dr["FK_CountryID_ID"].ToString();
                    model.CityID = dr["FK_CityID_ID"].ToString();
                    model.City = dr["City"].ToString();
                    model.Address = dr["Address"].ToString();
                    model.Email = dr["Email"].ToString();
                    model.Phone = dr["Phone"].ToString();
                    model.PostCode = dr["PostCode"].ToString();
                    model.UserName = dr["UserName"].ToString();
                    model.Password = dr["Password"].ToString();
                    model.Firm = dr["FK_FirmID_ID"].ToString();
                    model.Status = dr["FK_StatusID_ID"].ToString();
                    model.PromotionalEmail = dr["PromotionalEmail"].ToString();
                    model.VerificationCode = dr["VerificationCode"].ToString();
                    model.DisplayName = dr["DisplayName"].ToString();
                    model.Locked = dr["Locked"].ToString();
                    //model.Active = Convert.ToBoolean(dr["Active"]);
                    model.LogDateTime = Convert.ToDateTime(dr["LogDateTime"]);

                    string Active = dr["Active"].ToString();
                    if (Active=="")
                    {
                        Active = "False";
                        model.Active = Convert.ToBoolean(Active);
                    }
                    else
                    {
                        model.Active = Convert.ToBoolean(Active);
                    }
                    model.IPAddress = dr["IPAddress"].ToString();
                  
                    model.LogUserID = dr["LogUserID"].ToString();
                    
                    list.Add(model);
                }
            }

            return list;
        }
    }
    public class BizTbl_UserHistoryExt
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string SalutationType { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Country { get; set; }
        public string CityID { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string PostCode { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Firm { get; set; }
        public string Status { get; set; }
        public string PromotionalEmail { get; set; }
        public string VerificationCode { get; set; }
        public string DisplayName { get; set; }
        public string Locked { get; set; }
        public bool Active { get; set; }
        public string IPAddress { get; set; }
        public DateTime LogDateTime { get; set; }
        public string LogUserID { get; set; }
        public DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
    }
}