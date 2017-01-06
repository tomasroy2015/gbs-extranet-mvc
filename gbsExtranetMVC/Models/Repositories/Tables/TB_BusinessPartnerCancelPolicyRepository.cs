using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_BusinessPartnerCancelPolicyRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_BusinessPartnerCancelPolicyExt> ReadAll(int TableID)
        {
            List<TB_BusinessPartnerCancelPolicyExt> list = new List<TB_BusinessPartnerCancelPolicyExt>();
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_DisplayTableNew_BizTbl_Table_Sp", SQLCon);
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
                    TB_BusinessPartnerCancelPolicyExt model = new TB_BusinessPartnerCancelPolicyExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.BusinessPartnerID = Convert.ToInt32(dr["BusinessPartnerID"]);
                    model.BusinessPartner = dr["FK_BusinessPartnerID_ID"].ToString();
                    model.PartID = Convert.ToInt32(dr["PartID"]);
                    model.Part = dr["FK_PartID_ID"].ToString();
                    model.CancelTypeID = Convert.ToInt32(dr["CancelTypeID"]);
                    model.CancelType = dr["FK_CancelTypeID_ID"].ToString();
                    model.RefundableDayCount = Convert.ToInt32(dr["RefundableDayCount"]);
                    if (dr["PenaltyRateTypeID"].ToString() != null && dr["PenaltyRateTypeID"].ToString() != "")
                    {
                        model.PenaultyRateID = Convert.ToInt32(dr["PenaltyRateTypeID"]);
                    }
                    else
                    {
                        model.PenaultyRateID = 0;
                    }

                    model.PenaultyRate = dr["FK_PenaltyRateTypeID_ID"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_BusinessPartnerCancelPolicyExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            TB_BusinessPartnerCancelPolicy obj = new TB_BusinessPartnerCancelPolicy();
           // obj.ID = model.ID;
            obj.BusinessPartnerID = model.BusinessPartnerID;
            obj.PartID = model.PartID;
            obj.CancelTypeID = model.CancelTypeID;
            obj.RefundableDayCount = model.RefundableDayCount;
            if (model.PenaultyRateID != 0)
            {
                obj.PenaltyRateTypeID = model.PenaultyRateID;
            }
            else
            {
                obj.PenaltyRateTypeID = null;
            }
            
            obj.Active = model.Active;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.TB_BusinessPartnerCancelPolicy.Add(obj);
            db.SaveChanges();

            int id = Convert.ToInt32(obj.ID);

            return status;
        }

        public bool Delete(TB_BusinessPartnerCancelPolicyExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_BusinessPartnerCancelPolicy.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_BusinessPartnerCancelPolicy.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(TB_BusinessPartnerCancelPolicyExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_BusinessPartnerCancelPolicy.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.ID = model.ID;
            obj.BusinessPartnerID = Convert.ToInt32(model.BusinessPartnerID);
            obj.PartID = Convert.ToInt32(model.PartID);
            obj.CancelTypeID = Convert.ToInt32(model.CancelTypeID);
            obj.RefundableDayCount = model.RefundableDayCount;
           // obj.PenaltyRateTypeID = Convert.ToInt32(model.PenaltyRateID);
            if (model.PenaultyRateID != 0)
            {
                obj.PenaltyRateTypeID = model.PenaultyRateID;
            }
            else
            {
                obj.PenaltyRateTypeID = null;
            }
            obj.Active = model.Active;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);

            db.SaveChanges();

            return status;
        }


    }

    public class TB_BusinessPartnerCancelPolicyExt
    {

        public int ID { get; set; }
        public int BusinessPartnerID { get; set; }
        public string BusinessPartner { get; set; }
        public int PartID { get; set; }
        public string Part { get; set; }
        public int CancelTypeID { get; set; }
        public string CancelType { get; set; }
        public int RefundableDayCount { get; set; }
        public int PenaultyRateID { get; set; }
        public string PenaultyRate { get; set; }
        public bool Active { get; set; }
        

    }
}