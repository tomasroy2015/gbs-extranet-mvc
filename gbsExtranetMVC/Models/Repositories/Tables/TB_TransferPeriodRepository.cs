using gbsExtranetMVC.Helpers;
using gbsExtranetMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Business;
using System.Collections;
using gbsExtranetMVC.Models;
using Extension;
namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_TransferPeriodRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<TB_TransferPeriodExt> GetAllTableValue(int TableID)
        {
            List<TB_TransferPeriodExt> list = new List<TB_TransferPeriodExt>();

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
                    TB_TransferPeriodExt model = new TB_TransferPeriodExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.BusinessPartnerID = Convert.ToInt32(dr["BusinessPartnerID"]);
                    model.UserID = Convert.ToInt32(dr["OpUserID"]);
                    model.BusinessPartner = dr["FK_BusinessPartnerID_ID"].ToString();
                    model.StartDate = Convert.ToDateTime(dr["StartDate"]);
                    model.EndDate = Convert.ToDateTime(dr["EndDate"]);
                    model.OpDateTime = Convert.ToDateTime(dr["OpDateTime"]);
                    model.User = dr["FK_OpUserID_ID"].ToString();
                    model.Period = dr["Period"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_TransferPeriodExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            TB_TransferPeriod DepObj = new TB_TransferPeriod();
            //DepObj.ID = model.ID;
            DepObj.BusinessPartnerID = model.BusinessPartnerID;
            DepObj.StartDate = model.StartDate;
            DepObj.EndDate = model.EndDate;
            DepObj.OpDateTime = model.OpDateTime;
            DepObj.OpUserID = model.UserID;
            DepObj.Period = model.Period;
            DepObj.Active = model.Active;
            insertentity.TB_TransferPeriod.Add(DepObj);
            insertentity.SaveChanges();
            int id = DepObj.ID;
            return status;
        }

        public bool Update(TB_TransferPeriodExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            using (DBEntities DE = new DBEntities())
            {
                var DepObj = DE.TB_TransferPeriod.Where(x => x.ID == model.ID).FirstOrDefault();
                DepObj.BusinessPartnerID = model.BusinessPartnerID;
                DepObj.StartDate = model.StartDate;
                DepObj.EndDate = model.EndDate;
                DepObj.OpDateTime = model.OpDateTime;
                DepObj.OpUserID = model.UserID;
                DepObj.Period = model.Period;
                DepObj.Active = model.Active;
                DE.SaveChanges();
            }
            return status;
        }

        public bool Delete(TB_TransferPeriodExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            using (DBEntities DE = new DBEntities())
            {
                var DepObj = DE.TB_TransferPeriod.Where(x => x.ID == model.ID).FirstOrDefault();
                DE.TB_TransferPeriod.Remove(DepObj);
                DE.SaveChanges();
            }
            return status;

        }
    }

    public class TB_TransferPeriodExt
    {
        public int ID { get; set; }
        public string BusinessPartner { get; set; }
        public int BusinessPartnerID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Period { get; set; }
        public bool Active { get; set; }
        public DateTime OpDateTime { get; set; }
        public int UserID { get; set; }
        public string User{ get; set; }
    }
}