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
    public class TB_TourReservationHistoryRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<TB_TourReservationHistoryExt> GetAllTableValue(int TableID)
        {
            List<TB_TourReservationHistoryExt> list = new List<TB_TourReservationHistoryExt>();

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
                    TB_TourReservationHistoryExt model = new TB_TourReservationHistoryExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.FirmID = Convert.ToInt32(dr["FirmID"]);
                    model.Reservation = dr["FK_ReservationID_ID"].ToString();
                    model.TourReservationID = dr["TourReservationID"].ToString();
                    model.Firm = dr["FK_FirmID_ID"].ToString();
                    model.TourID = dr["FK_TourID_ID"].ToString();
                    model.StartDate = Convert.ToDateTime(dr["StartDate"]);
                    model.EndDate = Convert.ToDateTime(dr["EndDate"]);
                    model.GuestFullName = dr["GuestFullName"].ToString();
                    model.PreferredTourDateTime = Convert.ToDateTime(dr["PreferredTourDateTime"]);
                    model.PeopleCount = Convert.ToInt32(dr["PeopleCount"]);
                    model.ChildCount = Convert.ToInt32(dr["ChildCount"]);
                    model.TurkeyAddress = dr["TurkeyAddress"].ToString();
                    model.BusinessPartnerCancelPolicy = dr["FK_BusinessPartnerCancelPolicyID_ID"].ToString();
                    model.NonRefundable = Convert.ToBoolean(dr["NonRefundable"]);
                    model.Amount = Convert.ToDecimal(dr["Amount"]);
                    model.GeneralPromotionDiscountPercentage = Convert.ToInt32(dr["GeneralPromotionDiscountPercentage"]);
                    model.PromotionDiscountPercentage = Convert.ToInt32(dr["PromotionDiscountPercentage"]);
                    model.PayableAmount = Convert.ToDecimal(dr["PayableAmount"]);
                    model.Currency = dr["FK_CurrencyID_ID"].ToString();
                    model.Cost = Convert.ToDecimal(dr["Cost"]);
                    model.CostCurrency = dr["FK_CostCurrencyID_ID"].ToString();
                    model.ComissionRate = Convert.ToInt32(dr["ComissionRate"]);
                    model.ComissionAmount = Convert.ToDecimal(dr["ComissionAmount"]);
                    model.ComissionCurrency = dr["FK_ComissionCurrencyID_ID"].ToString();
                    model.Deposit = Convert.ToDecimal(dr["Deposit"]);
                    model.DepositType = dr["FK_DepositTypeID_ID"].ToString();
                    model.DepositCurrency = dr["FK_DepositCurrencyID_ID"].ToString();
                    model.Status = dr["FK_StatusID_ID"].ToString();
                    model.ReservationOperation = dr["FK_ReservationOperationID_ID"].ToString();
                    model.DepositInTL = Convert.ToDecimal(dr["DepositInTL"]);
                    model.CancelDateTime = Convert.ToDateTime(dr["CancelDateTime"]);
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    model.LogDateTime = Convert.ToDateTime(dr["LogDateTime"]);
                    model.LogUser = dr["FK_LogUserID_ID"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }

      
    }

    public class TB_TourReservationHistoryExt
    {
        public int ID { get; set; }
        public int FirmID { get; set; }
        public string Reservation { get; set; }
        public string TourReservationID { get; set; }
        public string Firm { get; set; }
        public string TourID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string GuestFullName { get; set; }
        public DateTime PreferredTourDateTime { get; set; }
        public int PeopleCount { get; set; }
        public int ChildCount { get; set; }
        public string TurkeyAddress { get; set; }
        public string BusinessPartnerCancelPolicy { get; set; }
        public bool NonRefundable { get; set; }
        public decimal Amount { get; set; }
        public int GeneralPromotionDiscountPercentage { get; set; }
        public int PromotionDiscountPercentage { get; set; }
        public decimal PayableAmount { get; set; }
        public string Currency { get; set; }
        public decimal Cost { get; set; }
        public string CostCurrency { get; set; }
        public int ComissionRate { get; set; }
        public decimal ComissionAmount { get; set; }
        public string ComissionCurrency { get; set; }
        public decimal Deposit { get; set; }
        public string DepositType { get; set; }
        public string DepositCurrency { get; set; }
        public string Status { get; set; }
        public string ReservationOperation { get; set; }
        public decimal DepositInTL { get; set; }
        public DateTime CancelDateTime { get; set; }
        public bool Active { get; set; } 
        public DateTime LogDateTime { get; set; }
        public string LogUser { get; set; }
    }
}