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
    public class TB_TransferRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<TB_TransferExt> GetAllTableValue(int TableID)
        {
            List<TB_TransferExt> list = new List<TB_TransferExt>();

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
                    TB_TransferExt model = new TB_TransferExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.BusinessPartnerID = Convert.ToInt32(dr["BusinessPartnerID"]);
                    model.BusinessPartnerName = dr["FK_BusinessPartnerID_ID"].ToString();
                    model.TransferPeriodName = dr["FK_TransferPeriodID_ID"].ToString();
                    model.TransferPeriodID = Convert.ToInt32(dr["TransferPeriodID"]);
                    model.TransferPaxID = Convert.ToInt32(dr["TransferPaxID"]);
                    model.TransferPaxName = dr["FK_TransferPaxID_ID"].ToString();
                    model.DepartureRegionID = Convert.ToInt64(dr["DepartureRegionID"]);
                    model.DepartureRegionName = dr["FK_DepartureRegionID_ID"].ToString();
                    model.DestinationRegionID = Convert.ToInt64(dr["DestinationRegionID"]);
                    model.DestinationRegionName = dr["FK_DestinationRegionID_ID"].ToString();
                    model.Amount = Convert.ToDecimal(dr["Amount"]);
                    model.Cost = Convert.ToDecimal(dr["Cost"]);
                    model.Deposit = Convert.ToDecimal(dr["Deposit"]);
                    model.CurrencyID = Convert.ToInt32(dr["CurrencyID"]);
                    model.Currency = dr["FK_CurrencyID_ID"].ToString();
                    model.CostCurrencyID = Convert.ToInt32(dr["CostCurrencyID"]);
                    model.CostCurrencyName = dr["FK_CostCurrencyID_ID"].ToString();
                    model.DepositCurrencyID = Convert.ToInt32(dr["DepositCurrencyID"]);
                    model.DepositCurrencyName = dr["FK_DepositCurrencyID_ID"].ToString();
                    model.HitCount = Convert.ToInt64(dr["HitCount"]);
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_TransferExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            TB_Transfer DepObj = new TB_Transfer();
            //DepObj.ID = model.ID;
            DepObj.BusinessPartnerID = model.BusinessPartnerID;
            DepObj.CostCurrencyID = model.CostCurrencyID;
            DepObj.CurrencyID = model.CurrencyID;
            DepObj.TransferPaxID = model.TransferPaxID;
            DepObj.DepartureRegionID = model.DepartureRegionID;
            DepObj.DepositCurrencyID = model.DepositCurrencyID;
            DepObj.TransferPeriodID = model.TransferPeriodID;
            DepObj.Deposit = model.Deposit;
            DepObj.Amount = model.Amount;
            DepObj.Cost = model.Amount;
            DepObj.HitCount = model.HitCount;
            DepObj.Active = model.Active;
            DepObj.OpDateTime = DateTime.Now;
            DepObj.CreateDateTime = DateTime.Now;
            DepObj.OpUserID = 0;
            DepObj.CreateUserID = 0;
            insertentity.TB_Transfer.Add(DepObj);
            insertentity.SaveChanges();
            int id = DepObj.ID;
            return status;
        }

        public bool Update(TB_TransferExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            using (DBEntities DE = new DBEntities())
            {
                var DepObj = DE.TB_Transfer.Where(x => x.ID == model.ID).FirstOrDefault();
                DepObj.BusinessPartnerID = model.BusinessPartnerID;
                DepObj.CostCurrencyID = model.CostCurrencyID;
                DepObj.CurrencyID = model.CurrencyID;
                DepObj.TransferPaxID = model.TransferPaxID;
                DepObj.DepartureRegionID = model.DepartureRegionID;
                DepObj.DepositCurrencyID = model.DepositCurrencyID;
                DepObj.TransferPeriodID = model.TransferPeriodID;
                DepObj.Deposit = model.Deposit;
                DepObj.Amount = model.Amount;
                DepObj.Cost = model.Amount;
                DepObj.HitCount = model.HitCount;
                DepObj.Active = model.Active;
                DepObj.OpDateTime = DateTime.Now;
                DepObj.OpUserID = 0;
                DE.SaveChanges();
            }
            return status;
        }

        public bool Delete(TB_TransferExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            using (DBEntities DE = new DBEntities())
            {
                var DepObj = DE.TB_Transfer.Where(x => x.ID == model.ID).FirstOrDefault();
                DE.TB_Transfer.Remove(DepObj);
                DE.SaveChanges();
            }
            return status;

        }
    }

    public class TB_TransferExt
    {
        public int ID { get; set; }
        public string BusinessPartnerName { get; set; }
        public int BusinessPartnerID { get; set; }
        public int TransferPeriodID { get; set; }
        public string TransferPeriodName { get; set; }
        public int TransferPaxID { get; set; }
        public string TransferPaxName { get; set; }
        public Int64 DepartureRegionID { get; set; }
        public string DepartureRegionName { get; set; }
        public Int64 DestinationRegionID { get; set; }
        public string DestinationRegionName { get; set; }
        public decimal Amount { get; set; }
        public decimal Cost { get; set; }
        public decimal Deposit { get; set; }
        public int CurrencyID { get; set; }
        public string Currency { get; set; }
        public int CostCurrencyID { get; set; }
        public string CostCurrencyName { get; set; }
        public int DepositCurrencyID { get; set; }
        public string DepositCurrencyName { get; set; }
        public Int64 HitCount { get; set; }
        public bool Active { get; set; }
    }
}