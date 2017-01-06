using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_DealHistoryRepository : BaseRepository
    {
        public string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_DealHistoryExt> ReadAll(int TableID)
        {
            List<TB_DealHistoryExt> list = new List<TB_DealHistoryExt>();
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
                    TB_DealHistoryExt model = new TB_DealHistoryExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                   // model.DealReservationID = Convert.ToInt64(dr["DealReservationID"]);
                    model.DealID = Convert.ToInt32(dr["DealID"]);
                    model.BusinessPartner = dr["FK_BusinessPartnerID_ID"].ToString();
                    model.RegionID = dr["FK_RegionID_ID"].ToString();
                    model.Name = dr["Name_en"].ToString();
                    model.Description = dr["Description_en"].ToString();
                    model.SpecialNote_tr = dr["SpecialNote_tr"].ToString();
                    model.SpecialNote_en = dr["SpecialNote_en"].ToString();
                    model.SpecialNote_de = dr["SpecialNote_de"].ToString();
                    model.SpecialNote_es = dr["SpecialNote_es"].ToString();
                    model.SpecialNote_fr = dr["SpecialNote_fr"].ToString();
                    model.SpecialNote_ru = dr["SpecialNote_ru"].ToString();
                    model.SpecialNote_it = dr["SpecialNote_it"].ToString();
                    model.SpecialNote_ar = dr["SpecialNote_ar"].ToString();
                    model.SpecialNote_jp = dr["SpecialNote_jp"].ToString();
                    model.StartDate = Convert.ToDateTime(dr["StartDate"]);
                    model.EndDate = Convert.ToDateTime(dr["EndDate"]);
                    model.Quota = Convert.ToInt32(dr["Quota"]);
                    model.Amount = dr["Amount"].ToString();
                    model.CurrencyID = Convert.ToInt32(dr["CurrencyID"]);
                    model.Cost = dr["Cost"].ToString();
                    model.CostCurrency = dr["CostCurrencyID"].ToString();
                    model.Deposit = dr["Deposit"].ToString();
                    model.DepositCurrency = dr["FK_DepositCurrencyID_ID"].ToString();
                    model.DepositType = dr["FK_DepositTypeID_ID"].ToString();
                    model.BusinessPartnerCancelPolicy = dr["FK_BusinessPartnerCancelPolicyID_ID"].ToString();
                    model.HitCount = Convert.ToInt32(dr["HitCount"]);
                    model.IsPopular = dr["IsPopular"].ToString();
                    model.Sort = Convert.ToInt32(dr["Sort"]);
                    model.RoutingName = dr["RoutingName"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    model.CreateDateTime = Convert.ToDateTime(dr["CreateDateTime"]);
                    model.CreateUserID = dr["CreateUserID"].ToString();
                    model.OpDateTime = Convert.ToDateTime(dr["OpDateTime"]);
                    model.OpUser = dr["FK_OpUserID_ID"].ToString();
                    model.IPAddress = dr["IPAddress"].ToString();
                    model.LogDateTime = Convert.ToDateTime(dr["LogDateTime"]);
                    model.LogUser = dr["FK_LogUserID_ID"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }
    }
    public class TB_DealHistoryExt
    {
        public int ID { get; set; }
        public int DealID { get; set; }
        public string BusinessPartner { get; set; }
        public string RegionID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SpecialNote_tr { get; set; }
        public string SpecialNote_en { get; set; }
        public string SpecialNote_de { get; set; }
        public string SpecialNote_es { get; set; }
        public string SpecialNote_fr { get; set; }
        public string SpecialNote_ru { get; set; }
        public string SpecialNote_it { get; set; }
        public string SpecialNote_ar { get; set; }
        public string SpecialNote_jp { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int Quota { get; set; }
        public string Amount { get; set; }
        public int CurrencyID { get; set; }
        public string Cost { get; set; }
        public string CostCurrency { get; set; }
        public string Deposit { get; set; }
        public string DepositCurrency { get; set; }
        public string DepositType { get; set; }
        public string BusinessPartnerCancelPolicy { get; set; }
        public int HitCount { get; set; }
        public string IsPopular { get; set; }
        public int Sort { get; set; }
        public string RoutingName { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string CreateUserID { get; set; }
        public DateTime OpDateTime { get; set; }
        public string OpUser { get; set; }
        public string IPAddress { get; set; }
        public DateTime LogDateTime { get; set; }
        public string LogUser { get; set; }
    }
    
}