using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_DealReservationHistoryRepository : BaseRepository
    {
        public string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_DealReservationHistoryExt> ReadAll(int TableID)
        {
            List<TB_DealReservationHistoryExt> list = new List<TB_DealReservationHistoryExt>();
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
                    TB_DealReservationHistoryExt model = new TB_DealReservationHistoryExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.DealReservationID = Convert.ToInt64(dr["DealReservationID"]);
                    model.ReservationID = dr["FK_ReservationID_ID"].ToString();
                    model.Firm = dr["FK_FirmID_ID"].ToString();
                    model.Deal = dr["FK_DealID_ID"].ToString();
                    model.GuestFullName = dr["GuestFullName"].ToString();
                    model.PromotionCode = dr["PromotionCode"].ToString();
                    model.StartDate = Convert.ToDateTime(dr["StartDate"]);
                    model.EndDate = Convert.ToDateTime(dr["EndDate"]);
                    model.PeopleCount = Convert.ToInt32(dr["PeopleCount"]);
                    model.BusinessPartnerCancelPolicy = dr["FK_BusinessPartnerCancelPolicyID_ID"].ToString();
                    model.NonRefundable = dr["NonRefundable"].ToString();
                    model.Amount = dr["Amount"].ToString();
                    model.GeneralPromotionDiscountPercentage = Convert.ToInt32(dr["GeneralPromotionDiscountPercentage"]);
                    model.PromotionDiscountPercentage = Convert.ToInt32(dr["PromotionDiscountPercentage"]);
                    model.PayableAmount = dr["PayableAmount"].ToString();
                    model.Currency = dr["FK_CurrencyID_ID"].ToString();
                    model.Cost = dr["Cost"].ToString();
                    model.CostCurrency = dr["FK_CostCurrencyID_ID"].ToString();
                    model.ComissionRate = Convert.ToInt32(dr["ComissionRate"]);
                    model.ComissionAmount = dr["ComissionAmount"].ToString();
                    model.ComissionCurrency = dr["FK_ComissionCurrencyID_ID"].ToString();
                    model.Deposit = dr["Deposit"].ToString();
                    model.DepositType = dr["FK_DepositTypeID_ID"].ToString();
                    model.DepositCurrency = dr["FK_DepositCurrencyID_ID"].ToString();
                    model.DepositInTL = dr["DepositInTL"].ToString();
                    model.Status = dr["FK_StatusID_ID"].ToString();
                    model.ReservationOperation = dr["FK_ReservationOperationID_ID"].ToString();
                    model.CancelDate = Convert.ToDateTime(dr["CancelDateTime"]);
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    model.LogDate = Convert.ToDateTime(dr["LogDateTime"]);
                    model.LogUser = dr["LogUserID"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }
    }
    public class TB_DealReservationHistoryExt
    {
        public int ID { get; set; }
        public Int64 DealReservationID { get; set; }
        public string ReservationID { get; set; }
        public string Firm { get; set; }
        public string Deal { get; set; }
        public string GuestFullName { get; set; }
        public string PromotionCode { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int PeopleCount { get; set; }
        public string BusinessPartnerCancelPolicy { get; set; }
        public string NonRefundable { get; set; }
        public string Amount { get; set; }
        public int GeneralPromotionDiscountPercentage { get; set; }
        public int PromotionDiscountPercentage { get; set; }
        public string PayableAmount { get; set; }
        public string Currency { get; set; }
        public string Cost { get; set; }
        public string CostCurrency { get; set; }
        public int ComissionRate { get; set; }
        public string ComissionAmount { get; set; }
        public string ComissionCurrency { get; set; }
        public string Deposit { get; set; }
        public string DepositType { get; set; }
        public string DepositCurrency { get; set; }
        public string DepositInTL { get; set; }
        public string Status { get; set; }
        public string ReservationOperation { get; set; }
        public DateTime CancelDate { get; set; }
        public bool Active { get;set; }
        public DateTime LogDate { get; set; }
        public string LogUser { get; set; }
    }
}