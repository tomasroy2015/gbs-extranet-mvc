using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_BusinessPartnerHistoryRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<TB_BusinessPartnerHistoryExt> ReadAll(int TableID)
        {
            List<TB_BusinessPartnerHistoryExt> list = new List<TB_BusinessPartnerHistoryExt>();
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
                    TB_BusinessPartnerHistoryExt model = new TB_BusinessPartnerHistoryExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.BusinessPartnerID = Convert.ToInt32(dr["BusinessPartnerID"]);
                    model.BusinessPartnerType = dr["FK_BusinessPartnerTypeID_ID"].ToString();
                    model.Firm = dr["FK_FirmID_ID"].ToString();
                    model.Country = dr["FK_CountryID_ID"].ToString();
                    model.City = dr["FK_CityID_ID"].ToString();
                    model.Name = dr["Name"].ToString();
                    model.Description = dr["Description"].ToString();
                    model.Address = dr["Address"].ToString();
                    model.Phone = dr["Phone"].ToString();
                    model.Fax = dr["Fax"].ToString();
                    model.PostCode = dr["PostCode"].ToString();
                    model.WebAddress = dr["WebAddress"].ToString();
                    model.Email = dr["Email"].ToString();
                    model.TransferCostCurrency = dr["TransferCostCurrencyID"].ToString();
                    model.TransferCurrency = dr["FK_TransferCurrencyID_ID"].ToString();
                    model.TransferDepositType = dr["FK_TransferDepositTypeID_ID"].ToString();
                    model.TourCostCurrency = dr["FK_TourCostCurrencyID_ID"].ToString();
                    model.TourCurrency = dr["FK_TourCurrencyID_ID"].ToString();
                    model.TourDepositType = dr["FK_TourDepositTypeID_ID"].ToString();
                    model.DealCostCurrency = dr["FK_DealCostCurrencyID_ID"].ToString();
                    model.DealCurrency = dr["FK_DealCurrencyID_ID"].ToString();
                    model.DealDepositType = dr["FK_DealDepositTypeID_ID"].ToString();
                    model.HitCount = dr["HitCount"].ToString();
                    model.Sorts = dr["Sort"].ToString();
                    model.Status = dr["FK_StatusID_ID"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    model.CreateDate = Convert.ToDateTime(dr["CreateDateTime"]);
                    model.IPAddress = dr["IPAddress"].ToString();
                    model.LogDate = Convert.ToDateTime(dr["LogDateTime"]);
                    model.LogUser = dr["FK_LogUserID_ID"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }
    }

    public class TB_BusinessPartnerHistoryExt
    {
        public int ID { get; set; }
        public int BusinessPartnerID { get; set; }
        public string BusinessPartnerType { get; set; }
        public string Firm { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public string Fax { get; set; }
        public string PostCode { get; set; }
        public string WebAddress { get; set; }
        public string Email { get; set; }
        public string TransferCostCurrency { get; set; }
        public string TransferCurrency { get; set; }
        public string TransferDepositType { get; set; }
        public string TourCostCurrency { get; set; }
        public string TourCurrency { get; set; }
        public string TourDepositType { get; set; }
        public string DealCostCurrency { get; set; }
        public string DealCurrency { get; set; }
        public string DealDepositType { get; set; }
        public string HitCount { get; set; }
        public string Sorts { get; set; }
        public string Status { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDate { get; set; }
        public string IPAddress { get; set; }
        public DateTime LogDate { get; set; }
        public string LogUser { get; set; }
    }
}