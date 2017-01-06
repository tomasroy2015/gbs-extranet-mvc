using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;
namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelCancelPolicyRepository : BaseRepository 
    {

        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelCancelPolicyExt> ReadAll(int TableID)
        {
            List<TB_HotelCancelPolicyExt> list = new List<TB_HotelCancelPolicyExt>();

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
                    TB_HotelCancelPolicyExt model = new TB_HotelCancelPolicyExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.Hotel = dr["FK_HotelID"].ToString();
                    model.HotelID = dr["HotelID"].ToString();
                    model.CancelType = dr["FK_CancelTypeID"].ToString();
                    model.RefundaleDayCount = dr["RefundableDayCount"].ToString();
                    model.PenaultyRate = dr["FK_PenaltyRateTypeID"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"].ToString());
                    model.CancelTypeID = Convert.ToInt32(dr["CancelTypeID"]);
                    model.PenaultyRateID = dr["PenaltyRateTypeID"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Update(TB_HotelCancelPolicyExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_HotelCancelPolicy.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.HotelID = Convert.ToInt32(model.HotelID);
            obj.CancelTypeID = Convert.ToInt32(model.CancelTypeID);
            obj.RefundableDayCount = Convert.ToInt32(model.RefundaleDayCount);
            obj.PenaltyRateTypeID = Convert.ToInt32(model.PenaultyRateID);
            obj.Active = model.Active;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }
        public bool Delete(TB_HotelCancelPolicyExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_HotelCancelPolicy.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_HotelCancelPolicy.Remove(obj);
            db.SaveChanges();
            return status;
        }

        public bool Create(TB_HotelCancelPolicyExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            TB_HotelCancelPolicy obj = new TB_HotelCancelPolicy();
            obj.ID = model.ID;
            obj.HotelID = Convert.ToInt32(model.HotelID);
            obj.CancelTypeID = Convert.ToInt32(model.CancelTypeID);
            obj.RefundableDayCount = Convert.ToInt32(model.RefundaleDayCount);
            obj.PenaltyRateTypeID = Convert.ToInt32(model.PenaultyRateID);
            obj.Active = model.Active;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.TB_HotelCancelPolicy.Add(obj);
            db.SaveChanges();
            int id = obj.ID;

            return status;
        }
    }

    public class TB_HotelCancelPolicyExt
    {
        public int ID { get; set; }
        public string Hotel { get; set; }
        public string CancelType { get; set; }
        public string RefundaleDayCount { get; set; }
        public string PenaultyRate { get; set; }
        public bool Active { get; set; }

        public int CancelTypeID { get; set; }

        public string PenaultyRateID { get; set; }

        public string HotelID { get; set; }
    }
}