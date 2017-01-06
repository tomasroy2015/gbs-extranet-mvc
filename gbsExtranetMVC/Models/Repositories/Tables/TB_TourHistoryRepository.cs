using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_TourHistoryRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_TourHistoryExt> ReadAll(int TableID)
        {
            List<TB_TourHistoryExt> list = new List<TB_TourHistoryExt>();

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
                    TB_TourHistoryExt model = new TB_TourHistoryExt();
                    model.ID = Convert.ToInt64(dr["ID"]);
                    model.TourID = Convert.ToInt32(dr["TourID"]);
                    model.BusinessPartnerID = dr["FK_BusinessPartnerID_ID"].ToString();
                    model.StartDate = Convert.ToDateTime(dr["StartDate"]);
                    model.EndDate = Convert.ToDateTime(dr["EndDate"]);
                    model.Name = dr["Name"].ToString();
                    model.Description = dr["Description"].ToString();
                    model.SpecialNote_tr = dr["SpecialNote_tr"].ToString();
                    model.SpecialNote_en = dr["SpecialNote_en"].ToString();
                    model.SpecialNote_de = dr["SpecialNote_de"].ToString();
                    model.SpecialNote_es = dr["SpecialNote_es"].ToString();
                    model.SpecialNote_fr = dr["SpecialNote_fr"].ToString();
                    model.SpecialNote_ru = dr["SpecialNote_ru"].ToString();
                    model.SpecialNote_it = dr["SpecialNote_it"].ToString();
                    model.SpecialNote_ar = dr["SpecialNote_ar"].ToString();
                    model.SpecialNote_jp = dr["SpecialNote_jp"].ToString();
                    model.Quota = dr["Quota"].ToString();
                    model.TourFrequencyID = dr["FK_TourFrequencyID_ID"].ToString();
                    model.Duration = dr["Duration"].ToString();
                    model.DurationUnitID = dr["FK_DurationUnitID_ID"].ToString();
                    model.TourStartDateTime = dr["TourStartDateTime"].ToString();
                    model.StartRegionID = dr["FK_StartRegionID_ID"].ToString();
                    model.ChildAge = dr["ChildAge"].ToString();
                    model.Amount = dr["Amount"].ToString();
                    model.CurrencyID = dr["FK_CurrencyID_ID"].ToString();
                    model.Cost = dr["Cost"].ToString();
                    model.CostCurrencyID = dr["CostCurrencyID"].ToString();
                    model.Deposit = dr["Deposit"].ToString();
                    model.DepositCurrencyID = dr["FK_DepositCurrencyID_ID"].ToString();
                    model.DepositTypeID = dr["FK_DepositTypeID_ID"].ToString();
                    model.BusinessPartnerCancelPolicyID = dr["FK_BusinessPartnerCancelPolicyID_ID"].ToString();
                    model.HitCount = dr["HitCount"].ToString();
                    model.IsPopular = Convert.ToBoolean(dr["IsPopular"]);
                    model.Sort = dr["Sort"].ToString();
                    model.RoutingName = dr["RoutingName"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    model.IPAddress = dr["TourID"].ToString();                  
                    model.LogDateTime = Convert.ToDateTime(dr["LogDateTime"]);
                    model.LogUserID = dr["FK_LogUserID_ID"].ToString();

                    list.Add(model);
                }
            }

            return list;
        }
    }
    public class TB_TourHistoryExt
    {
        public Int64 ID { get; set; }
        public int TourID { get; set; }
        public string BusinessPartnerID { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
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
        public string Quota { get; set; }
        public string TourFrequencyID { get; set; }
        public string Duration { get; set; }
        public string DurationUnitID { get; set; }
        public string TourStartDateTime { get; set; }
        public string StartRegionID { get; set; }
        public string ChildAge { get; set; }
        public string Amount { get; set; }
        public string CurrencyID { get; set; }
        public string Cost { get; set; }
        public string CostCurrencyID { get; set; }
        public string Deposit { get; set; }
        public string DepositCurrencyID { get; set; }
        public string DepositTypeID { get; set; }
        public string BusinessPartnerCancelPolicyID { get; set; }
        public string HitCount { get; set; }
        public bool IsPopular { get; set; }
        public string Sort { get; set; }
        public string RoutingName { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDateTime { get; set; }
        public Int64 CreateUserID { get; set; }
        public DateTime OpDateTime { get; set; }
        public Int64 OpUserID { get; set; }
        public string IPAddress { get; set; }
        public DateTime LogDateTime { get; set; }
        public string LogUserID { get; set; }
    }
}