using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Transactions;
using System.Net;

namespace gbsExtranetMVC.Models.Repositories
{
    public class SystemConfigurationRepository : BaseRepository 
    {
        public List<SystemConfigurationExt> GetSystemConfiguration(int id)
        {
            string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            List<SystemConfigurationExt> list = new List<SystemConfigurationExt>();
           // DataSet ds = new DataSet();

            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetHotelByHotelID_Tb_Hotel_SP", SQLCon);
            //cmd.Parameters.AddWithValue("@Culture", CultureValue);
            //cmd.Parameters.AddWithValue("@OrderBy", "ID");
            //cmd.Parameters.AddWithValue("@PagingSize", 1);
            //cmd.Parameters.AddWithValue("@PageIndex", 1);
            cmd.Parameters.AddWithValue("@HotelID", Convert.ToInt32(id));
            cmd.CommandType = CommandType.StoredProcedure;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            //dt = ds.Tables[1];
            SQLCon.Close();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SystemConfigurationExt EmailObj = new SystemConfigurationExt();
                 //   EmailObj.CultureID = Convert.ToInt64(dr["ID"]);
                    EmailObj.HotelID = Convert.ToInt32(dr["ID"]);
                    EmailObj.CultureID = dr["CultureID"].ToString();
                    EmailObj.Creditcards = Convert.ToBoolean(dr["CreditCardNotRequired"]);
                    EmailObj.Secret = Convert.ToBoolean(dr["IsSecret"]);
                    list.Add(EmailObj);
                }
            }
            return list;
        }

        public int SysConfiguration(string Secret, string CreditCard, string NotificationCulture, Controller ctrl,int id)
        {
            DBEntities obj = new DBEntities();
            var SecretParameter = new SqlParameter("@Secret", Secret);
            var CreditCardParameter = new SqlParameter("@CreditCard", CreditCard);
            var NotificationCultureParameter = new SqlParameter("@NotificationCulture", NotificationCulture);
            var OpUserIDParameter = new SqlParameter("@OpUserID", Convert.ToInt64(ctrl.Session["UserID"]));
            var HotelIDParameter = new SqlParameter("@ID", id);
            int i = obj.Database.ExecuteSqlCommand("B_Ex_UpdateSystemConfig_TB_Hotel_SP @Secret,@CreditCard,@NotificationCulture,@OpUserID,@ID", SecretParameter, CreditCardParameter, NotificationCultureParameter, OpUserIDParameter, HotelIDParameter);
            return i;
        }
    }

    public class SystemConfigurationExt
    {
        public bool Secret { get; set; }

        public bool Creditcards { get; set; }

        public string CultureCode { get; set; }

        public string CultureID { get; set; }

        public int HotelID { get; set; }
    }
}