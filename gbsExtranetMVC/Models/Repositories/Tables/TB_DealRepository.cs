using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_DealRepository : BaseRepository
    {
        public string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_DealExt> ReadAll(int TableID)
        {
            List<TB_DealExt> list = new List<TB_DealExt>();
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
                    TB_DealExt model = new TB_DealExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.BusinessPartnerID = Convert.ToInt32(dr["BusinessPartnerID"]);
                    model.BusinessPartner = dr["FK_BusinessPartnerID_ID"].ToString();
                    model.RegionID = Convert.ToInt32(dr["RegionID"]);
                    model.Region = dr["FK_RegionID_ID"].ToString();
                    model.Name = dr["Name"].ToString();
                    model.Description = dr["Description"].ToString();
                    model.SpecialNote = dr["SpecialNote"].ToString();
                    model.StartDate = Convert.ToDateTime(dr["StartDate"]);
                    model.EndDate = Convert.ToDateTime(dr["EndDate"]);
                    model.Quota = Convert.ToInt32(dr["Quota"]);
                    model.Amount = dr["Amount"].ToString();
                    model.CurrencyID = Convert.ToInt32(dr["CurrencyID"]);
                    model.Currency = dr["FK_CurrencyID_ID"].ToString();
                    model.Cost = dr["Cost"].ToString();
                    model.CostCurrencyID = Convert.ToInt32(dr["CostCurrencyID"]);
                    model.CostCurrency = dr["FK_CostCurrencyID_ID"].ToString();
                    model.Deposit = dr["Deposit"].ToString();
                    model.DepositCurrencyID = Convert.ToInt32(dr["DepositCurrencyID"]);
                    model.DepositCurrency = dr["FK_DepositCurrencyID_ID"].ToString();
                    model.TypeDepositID = Convert.ToInt32(dr["DepositTypeID"]);
                    model.TypeDeposit = dr["FK_DepositTypeID_ID"].ToString();
                    model.BusinessPartnerCancelPolicyID = dr["BusinessPartnerCancelPolicyID"].ToString();
                    model.BusinessPartnerCancelPolicy = dr["FK_BusinessPartnerCancelPolicyID_ID"].ToString();
                    model.HitCount = Convert.ToInt32(dr["HitCount"]);
                    model.IsPopular = Convert.ToBoolean(dr["IsPopular"]);
                    model.Latitude = dr["Latitude"].ToString();
                    model.Longitude = dr["Longitude"].ToString();
                    model.MapZoomIndex = Convert.ToInt32(dr["MapZoomIndex"]);
                    model.Sorts = Convert.ToInt32(dr["Sort"]);
                    model.RoutingName = dr["RoutingName"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    model.IPAddress = dr["IPAddress"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_DealExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            TB_Deal obj = new TB_Deal();
            obj.ID = model.ID;
            obj.BusinessPartnerID = Convert.ToInt32(model.BusinessPartnerID);
            obj.RegionID = Convert.ToInt32(model.RegionID);
            obj.Name_en = model.Name;
            obj.Description_en = model.Description;
            obj.SpecialNote_en = model.SpecialNote;
            obj.StartDate = model.StartDate;
            obj.EndDate = model.EndDate;
            obj.Quota = Convert.ToInt16(model.Quota);
            obj.Amount = Convert.ToDecimal(model.Amount);
            if (model.CurrencyID != 0)
            {
                obj.CurrencyID = model.CurrencyID;
            }
            else
            {
                obj.CurrencyID = null;
            }

            obj.Cost = Convert.ToDecimal(model.Cost);
            obj.CostCurrencyID = model.CostCurrencyID;
            obj.Deposit = Convert.ToDecimal(model.Deposit);

            if (model.DepositCurrencyID != 0)
            {
                obj.DepositCurrencyID = Convert.ToInt32(model.DepositCurrencyID);
            }
            else
            {
                obj.DepositCurrencyID = null;
            }


            if (model.TypeDepositID != 0)
            {
                obj.DepositTypeID = Convert.ToInt32(model.TypeDepositID);
            }
            else
            {
                obj.DepositTypeID = null;
            }

            if (model.BusinessPartnerCancelPolicyID != "")
            {
                obj.BusinessPartnerCancelPolicyID = Convert.ToInt32(model.BusinessPartnerCancelPolicyID);
            }
            else
            {
                obj.BusinessPartnerCancelPolicyID = null;
            }
            obj.HitCount = Convert.ToInt32(model.HitCount);
            obj.IsPopular = model.IsPopular;
            obj.Latitude = model.Latitude;
            obj.Longitude = model.Longitude;
            obj.MapZoomIndex = Convert.ToInt32(model.MapZoomIndex);
            obj.Sort = Convert.ToInt32(model.Sorts);
            obj.RoutingName = model.RoutingName;
            obj.Active = model.Active;
            obj.IPAddress = model.IPAddress;
            obj.CreateDateTime = DateTime.Now;
            obj.CreateUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.TB_Deal.Add(obj);
            db.SaveChanges();

            int id = Convert.ToInt32(obj.ID);

            return status;
        }

        public bool Delete(TB_DealExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_Deal.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_Deal.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(TB_DealExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_Deal.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.BusinessPartnerID = Convert.ToInt32(model.BusinessPartnerID);
            obj.RegionID = Convert.ToInt32(model.RegionID);
            obj.Name_en = model.Name;
            obj.Description_en = model.Description;
            obj.SpecialNote_en = model.SpecialNote;
            obj.StartDate = model.StartDate;
            obj.EndDate = model.EndDate;
            obj.Quota = Convert.ToInt16(model.Quota);
            obj.Amount = Convert.ToDecimal(model.Amount);
            if (model.CurrencyID!=0)
            {
                obj.CurrencyID = model.CurrencyID;
            }
            else
            {
                obj.CurrencyID = null;
            }

            obj.Cost = Convert.ToDecimal(model.Cost);
            obj.CostCurrencyID = model.CostCurrencyID;
            obj.Deposit = Convert.ToDecimal(model.Deposit);
          
            if (model.DepositCurrencyID != 0)
            {
                obj.DepositCurrencyID = Convert.ToInt32(model.DepositCurrencyID);
            }
            else
            {
                obj.DepositCurrencyID = null;
            }


            if (model.TypeDepositID != 0)
            {
                obj.DepositTypeID = Convert.ToInt32(model.TypeDepositID);
            }
            else
            {
                obj.DepositTypeID = null;
            }

            if (model.BusinessPartnerCancelPolicyID != "")
            {
                obj.BusinessPartnerCancelPolicyID = Convert.ToInt32(model.BusinessPartnerCancelPolicyID);
            }
            else
            {
                obj.BusinessPartnerCancelPolicyID = null;
            }
          
            obj.HitCount = Convert.ToInt32(model.HitCount);
            obj.IsPopular = model.IsPopular;
            obj.Latitude = model.Latitude;
            obj.Longitude = model.Longitude;
            obj.MapZoomIndex = Convert.ToInt32(model.MapZoomIndex);
            obj.Sort = Convert.ToInt32(model.Sorts);
            obj.RoutingName = model.RoutingName;
            obj.Active = model.Active;
            obj.IPAddress = model.IPAddress;
            obj.CreateDateTime = DateTime.Now;
            obj.CreateUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();

            return status;
        }


    }

    public class TB_DealExt
    {
        public int ID { get; set; }
        public int BusinessPartnerID { get; set; }
        public string BusinessPartner { get; set; }
        public int RegionID { get; set; }
        public string Region { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string SpecialNote { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public int Quota { get; set; }
        public string Amount { get; set; }
        public int CurrencyID { get; set; }
        public string Currency{ get; set; }
        [Required(ErrorMessage = "This field is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "This field must be numeric!")]

        public string Cost { get; set; }
        public int CostCurrencyID { get; set; }
        public string CostCurrency { get; set; }
        public string Deposit { get; set; }
        public int DepositCurrencyID { get; set; }
        public string DepositCurrency { get; set; }
        public int TypeDepositID { get; set; }
        public string TypeDeposit { get; set; }
        public string BusinessPartnerCancelPolicyID { get; set; }
        public string BusinessPartnerCancelPolicy { get; set; }
        public int HitCount { get; set; }
        public bool IsPopular { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public int MapZoomIndex { get; set; }
        public int Sorts { get; set; }
        public string RoutingName { get; set; }
        public bool Active { get; set; }
          [Required(ErrorMessage = "This field is required!")]
        public string IPAddress { get; set; }
    }
}