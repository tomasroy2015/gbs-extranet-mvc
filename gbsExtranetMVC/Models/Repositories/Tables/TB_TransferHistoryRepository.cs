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
    public class TB_TransferHistoryRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<TB_TransferHistoryExt> GetAllTableValue(int TableID)
        {
            List<TB_TransferHistoryExt> list = new List<TB_TransferHistoryExt>();

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
                    TB_TransferHistoryExt model = new TB_TransferHistoryExt();
                    model.ID = Convert.ToInt64(dr["ID"]);
                    model.TransferID = Convert.ToInt32(dr["TransferID"]);
                    model.BusinessPartnerName = dr["FK_BusinessPartnerID_ID"].ToString();
                    model.TransferPeriodName = dr["FK_TransferPeriodID_ID"].ToString();
                    model.TransferPaxName = dr["FK_TransferPaxID_ID"].ToString();
                    model.DepartureRegionName = dr["FK_DepartureRegionID_ID"].ToString();
                    model.DestinationRegionName = dr["FK_DestinationRegionID_ID"].ToString();
                    model.Amount = Convert.ToDecimal(dr["Amount"]);
                    model.Cost = Convert.ToDecimal(dr["Cost"]);
                    model.Deposit = Convert.ToDecimal(dr["Deposit"]);
                    model.CurrencyName = dr["FK_CurrencyID_ID"].ToString();
                    model.CostCurrencyName = dr["FK_CostCurrencyID_ID"].ToString();
                    model.DepositCurrencyName = dr["FK_DepositCurrencyID_ID"].ToString();
                    model.HitCount = Convert.ToInt64(dr["HitCount"]);
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    model.LogDateTime = Convert.ToDateTime(dr["LogDateTime"]);
                    model.LoggedUserID = dr["FK_LogUserID_ID"].ToString();
                    
                    
                    list.Add(model);
                }
            }

            return list;
        }

    
    }

    public class TB_TransferHistoryExt
    {
        public Int64 ID { get; set; }
        public int TransferID { get; set; }
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
        public string CurrencyName { get; set; }
        public int CostCurrencyID { get; set; }
        public string CostCurrencyName { get; set; }
        public int DepositCurrencyID { get; set; }
        public string DepositCurrencyName { get; set; }
        public Int64 HitCount { get; set; }
        public bool Active { get; set; }
        public DateTime LogDateTime { get; set; }
        public string LoggedUserID { get; set; }
        
    }
}