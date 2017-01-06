using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;


namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_FirmRequestHistoryRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_FirmRequestHistoryExt> ReadAll(int TableID)
        {
            List<TB_FirmRequestHistoryExt> list = new List<TB_FirmRequestHistoryExt>();

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
                    TB_FirmRequestHistoryExt model = new TB_FirmRequestHistoryExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.FirmRequestID = Convert.ToInt32(dr["FirmRequestID"]);
                    model.Firm = dr["FK_FirmID"].ToString();
                    model.RequestType = dr["FK_RequestTypeID"].ToString();
                    model.ReservationID = Convert.ToInt32(dr["FK_ReservationID"].ToString());
                    model.FirmRequestStatus = dr["FK_FirmRequestStatusID"].ToString();
                    model.CheckinDate = dr["CheckInDate"].ToString();
                    model.CheckoutDate = dr["CheckOutDate"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    model.IPAddress = dr["IPAddress"].ToString();
                    model.LogDate = Convert.ToDateTime(dr["LogDateTime"].ToString());
                    model.LogUser = dr["FK_LogUserID"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }
    }
    public class TB_FirmRequestHistoryExt
    {
        public int ID { get; set; }
        public int FirmRequestID { get; set; }
        public string Firm { get; set; }
        public string RequestType { get; set; }
        public int ReservationID { get; set; }
        public string FirmRequestStatus { get; set; }
        public string CheckinDate { get; set; }
        public string CheckoutDate { get; set; }
        public bool Active { get; set; }
        public string IPAddress { get; set; }
        public DateTime LogDate { get; set; }
        public string LogUser { get; set; }
    }
}