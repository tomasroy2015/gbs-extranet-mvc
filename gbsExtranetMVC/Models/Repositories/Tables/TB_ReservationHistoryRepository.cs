using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;


namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_ReservationHistoryRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_ReservationHistoryExt> ReadAll(int TableID)
        {
            List<TB_ReservationHistoryExt> list = new List<TB_ReservationHistoryExt>();

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
                    TB_ReservationHistoryExt PageObj = new TB_ReservationHistoryExt();
                    PageObj.ID = Convert.ToInt32(dr["FK_ID_ID"]);
                    PageObj.ReservationID = dr["ReservationID"].ToString();
                    PageObj.PINCode = dr["PinCode"].ToString();
                    PageObj.Part = dr["FK_PartID_ID"].ToString();
                    PageObj.Firm = dr["FK_FirmID_ID"].ToString();
                    PageObj.User = dr["FK_UserID_ID"].ToString();
                    PageObj.ReservationDate = dr["ReservationDate"].ToString();
                    PageObj.Status = dr["FK_StatusID_ID"].ToString();
                    PageObj.Country = dr["FK_CountryID_ID"].ToString();
                    PageObj.City = dr["City"].ToString();
                    PageObj.Salutation = dr["FK_SalutationTypeID_ID"].ToString();
                    PageObj.Name = dr["Name"].ToString();
                    PageObj.Surname = dr["Surname"].ToString();
                    PageObj.Email = dr["Email"].ToString();
                    PageObj.PostCode = dr["PostCode"].ToString();
                    PageObj.Address = dr["Address"].ToString();
                    PageObj.Phone = dr["Phone"].ToString();
                    PageObj.Amount = dr["Amount"].ToString();
                    PageObj.GeneralPromotionDiscountPercentage = dr["GeneralPromotionDiscountPercentage"].ToString();
                    PageObj.PromotionDiscountPercentage = dr["PromotionDiscountPercentage"].ToString();

                    PageObj.PayableAmount = dr["PayableAmount"].ToString();
                    PageObj.ActualAmount = Convert.ToBoolean(dr["ActualAmount"].ToString());
                    PageObj.Currency = dr["FK_CurrencyID_ID"].ToString();
                    PageObj.ComissionRate = dr["ComissionRate"].ToString();
                    PageObj.ComissionAmount = dr["ComissionAmount"].ToString();
                    PageObj.ComissionCurrency = dr["FK_ComissionCurrencyID_ID"].ToString();
                    PageObj.Deposit = dr["Deposit"].ToString();
                    PageObj.DepositCurrency = dr["FK_DepositCurrencyID_ID"].ToString();
                    //PageObj.DepositTL = dr["DepositInTL"].ToString();
                    PageObj.Note = dr["Note"].ToString();
                    PageObj.CreditCardUsed = Convert.ToBoolean(dr["CreditCardUsed"].ToString());

                    PageObj.CreditCardType = dr["FK_CCTypeID_ID"].ToString();
                    PageObj.NameontheCard = dr["CCFullName"].ToString();
                    PageObj.CreditCardNumber = dr["CCNo"].ToString();

                    PageObj.ExpirationDate = dr["CCExpiration"].ToString();
                    PageObj.CVC = dr["CCCVC"].ToString();
                    PageObj.ReservationOperation = dr["FK_ReservationOperationID_ID"].ToString();
                    PageObj.ChargedAmount = dr["ChargedAmount"].ToString();
                    PageObj.ChargedAmountCurrency = dr["FK_ChargedAmountCurrencyID_ID"].ToString();
                    PageObj.ChargeDate = dr["ChargeDate"].ToString();
                    PageObj.Active = Convert.ToBoolean(dr["Active"].ToString());
                    PageObj.Culture = dr["FK_CultureID_ID"].ToString();
                    PageObj.IPAddress = dr["IPAddress"].ToString();
                    PageObj.CancelDate = dr["CancelDateTime"].ToString();
                    PageObj.EncryptedReservationID = dr["EncReservationID"].ToString();
                    PageObj.EncryptedPINCode = dr["EncPinCode"].ToString();
                    PageObj.UserSessionID = dr["FK_UserSessionID_ID"].ToString();
                    PageObj.LogDateTime = Convert.ToDateTime(dr["LogDateTime"].ToString());
                    PageObj.LogUserID = dr["FK_LogUserID_ID"].ToString();

                    list.Add(PageObj);
                }
            }

            return list;
        }
    }
    public class TB_ReservationHistoryExt
    {
        public int ID { get; set; }
        public string ReservationID { get; set; }

        public string PINCode { get; set; }
        public string Part { get; set; }
        public string Firm { get; set; }
        public string User { get; set; }
        public string ReservationDate { get; set; }
        public string Status { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Salutation { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string PostCode { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Amount { get; set; }
        public string GeneralPromotionDiscountPercentage { get; set; }
        public string PromotionDiscountPercentage { get; set; }

        public string PayableAmount { get; set; }
        public bool ActualAmount { get; set; }
        public string Currency { get; set; }
        public string ComissionRate { get; set; }
        public string ComissionAmount { get; set; }
        public string ComissionCurrency { get; set; }
        public string Deposit { get; set; }
        public string DepositCurrency { get; set; }
        public string DepositTL { get; set; }

        public string Note { get; set; }
        public bool CreditCardUsed { get; set; }
        public string CreditCardType { get; set; }
        public string NameontheCard { get; set; }
        public string CreditCardNumber { get; set; }

        public string ExpirationDate { get; set; }
        public string CVC { get; set; }
        public string ReservationOperation { get; set; }
        public string ChargedAmount { get; set; }
        public string ChargedAmountCurrency { get; set; }

        public string ChargeDate { get; set; }
        public bool Active { get; set; }
        public string Culture { get; set; }
        public string IPAddress { get; set; }
        public string CancelDate { get; set; }

        public string EncryptedReservationID { get; set; }
        public string EncryptedPINCode { get; set; }
        public string UserSessionID { get; set; }

        public DateTime LogDateTime { get; set; }
        public string LogUserID { get; set; }
    }
}