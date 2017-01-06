using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_TourRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_TourExt> ReadAll(int TableID)
        {
            List<TB_TourExt> list = new List<TB_TourExt>();

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
                    TB_TourExt PageObj = new TB_TourExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.BusinessPartner = dr["FK_BusinessPartnerID_ID"].ToString();

                    PageObj.Code = dr["Code"].ToString();
                    PageObj.StartDate = Convert.ToDateTime(dr["StartDate"].ToString());
                    PageObj.EndDate = Convert.ToDateTime(dr["EndDate"].ToString());
                    PageObj.Name_en = dr["Name_en"].ToString();
                    PageObj.Description_en = dr["Description_en"].ToString();
                    PageObj.SpecialNote_en = dr["SpecialNote_en"].ToString();
                    PageObj.Quota = dr["Quota"].ToString();
                    PageObj.TourFrequency = dr["FK_TourFrequencyID_ID"].ToString();
                    PageObj.Duration = dr["Duration"].ToString();
                    PageObj.Unit = dr["FK_DurationUnitID_ID"].ToString();
                    PageObj.TourStartDateTime = dr["TourStartDateTime"].ToString();
                    PageObj.Region = dr["FK_StartRegionID_ID"].ToString();
                    PageObj.ChildAge = dr["ChildAge"].ToString();
                    PageObj.Amount = dr["Amount"].ToString();
                    PageObj.Currency = dr["FK_CurrencyID_ID"].ToString();
                    PageObj.Cost = dr["Cost"].ToString();
                    PageObj.CostCurrencyName = dr["FK_CostCurrencyID_ID"].ToString();
                    PageObj.Deposit = dr["Deposit"].ToString();
                    PageObj.DepositCurrencyName = dr["FK_DepositCurrencyID_ID"].ToString();
                    PageObj.TypeDeposit = dr["FK_DepositTypeID_ID"].ToString();
                    PageObj.BusinessPartnerCancelPolicyID = dr["FK_BusinessPartnerCancelPolicyID_ID"].ToString();
                    PageObj.HitCount = dr["HitCount"].ToString();
                    PageObj.IsPopular = Convert.ToBoolean(dr["IsPopular"]);
                    PageObj.Sorts = dr["Sort"].ToString();
                    PageObj.RoutingName = dr["RoutingName"].ToString();
                    PageObj.Active = Convert.ToBoolean(dr["Active"]);
                    PageObj.IPAddress = dr["IPAddress"].ToString();
                    //PageObj.Operation = dr["Operation"].ToString();


                    PageObj.BusinessPartnerID = dr["BusinessPartnerID"].ToString();
                    PageObj.TourFrequencyID = dr["TourFrequencyID"].ToString();
                    PageObj.UnitID = dr["DurationUnitID"].ToString();
                    PageObj.RegionID = dr["StartRegionID"].ToString();
                    PageObj.CurrencyID = dr["CurrencyID"].ToString();
                    PageObj.CostCurrencyID = dr["CostCurrencyID"].ToString();
                    PageObj.DepositCurrencyID = dr["DepositCurrencyID"].ToString();
                    PageObj.TypeDepositID = dr["DepositTypeID"].ToString();
                  //  PageObj.BusinessPartnerCancelPolicyID = Convert.ToInt32(dr["BusinessPartnerCancelPolicyID"]);

                    list.Add(PageObj);
                }
            }

            return list;
        }


        public bool Update(TB_TourExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_Tour.Where(x => x.ID == model.ID).FirstOrDefault();

            obj.ID = model.ID;
            obj.BusinessPartnerID = Convert.ToInt32(model.BusinessPartnerID);
            obj.Code = model.Code;
            obj.StartDate = Convert.ToDateTime(model.StartDate);
            obj.EndDate = Convert.ToDateTime(model.EndDate);           

            obj.Name_en = model.Name_en;
            obj.Description_en = model.Description_en;
            obj.SpecialNote_en = model.SpecialNote_en;

            obj.Quota = Convert.ToInt16(model.Quota);
            obj.TourFrequencyID = Convert.ToInt32(model.TourFrequencyID);

            if (model.Duration != null)
            {
                obj.Duration = Convert.ToInt16(model.Duration);
            }
            else
            {
                obj.Duration = null;
            }
            if (model.UnitID != "0" && model.UnitID != null)
            {
                obj.DurationUnitID = Convert.ToInt32(model.UnitID);
            }
            else
            {
                obj.DurationUnitID = null;
            }

            
            obj.TourStartDateTime = model.TourStartDateTime;
            obj.StartRegionID = Convert.ToInt64(model.RegionID);


            obj.ChildAge = Convert.ToInt32(model.ChildAge);
            obj.Amount = Convert.ToDecimal(model.Amount);
            if (model.CurrencyID != "0" && model.CurrencyID != null)
            {
                obj.CurrencyID = Convert.ToInt32(model.CurrencyID);
            }
            else
            {
                obj.CurrencyID = null;
            }            

            obj.Cost = Convert.ToDecimal(model.Cost);
            obj.CostCurrencyID = Convert.ToInt32(model.CostCurrencyID);

            if (model.Deposit != null)
            {
                obj.Deposit = Convert.ToDecimal(model.Deposit);
            }
            else
            {
                obj.Deposit = null;
            }

            if (model.DepositCurrencyID != "0" && model.DepositCurrencyID != null)
            {
                obj.DepositCurrencyID = Convert.ToInt32(model.DepositCurrencyID);
            }
            else
            {
                obj.DepositCurrencyID = null;
            }

            if (model.TypeDepositID != "0" && model.TypeDepositID != null)
            {
                obj.DepositTypeID = Convert.ToInt32(model.TypeDepositID);
            }
            if (model.BusinessPartnerCancelPolicyID != "0" && model.BusinessPartnerCancelPolicyID != null)
            {
                obj.BusinessPartnerCancelPolicyID = Convert.ToInt32(model.BusinessPartnerCancelPolicyID);
            }
            else
            {
                obj.BusinessPartnerCancelPolicyID = null;
            }
            

         //   obj.CreditCardRequired = null;
            if(model.HitCount!=null)
            {
                obj.HitCount = Convert.ToInt64(model.HitCount);
            }
            else
            {
                obj.HitCount = null;
            }
            
            obj.IsPopular = model.IsPopular;

            if(model.Sorts!=null)
            {
                obj.Sort = Convert.ToInt32(model.Sorts);
            }
            else
            {
                obj.Sort = null;
            }
            if(model.RoutingName!=null)
            {
                obj.RoutingName = model.RoutingName;
            }
            else
            {
                obj.RoutingName = null;
            }
            
            obj.Active = model.Active;

            obj.CreateDateTime = DateTime.Now;
            obj.CreateUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            obj.IPAddress = model.IPAddress;
            db.SaveChanges();
            return status;
        }
        public bool Delete(TB_TourExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_Tour.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_Tour.Remove(obj);
            db.SaveChanges();
            return status;
        }

        public bool Create(TB_TourExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            TB_Tour obj = new TB_Tour();

            obj.BusinessPartnerID = Convert.ToInt32(model.BusinessPartnerID);
            obj.Code = model.Code;
            obj.StartDate = Convert.ToDateTime(model.StartDate);
            obj.EndDate = Convert.ToDateTime(model.EndDate);

            obj.Name_en = model.Name_en;
            obj.Description_en = model.Description_en;
            obj.SpecialNote_en = model.SpecialNote_en;

            obj.Quota = Convert.ToInt16(model.Quota);
            obj.TourFrequencyID = Convert.ToInt32(model.TourFrequencyID);

            if (model.Duration != null)
            {
                obj.Duration = Convert.ToInt16(model.Duration);
            }
            else
            {
                obj.Duration = null;
            }
            if (model.UnitID != "0" && model.UnitID != null)
            {
                obj.DurationUnitID = Convert.ToInt32(model.UnitID);
            }
            else
            {
                obj.DurationUnitID = null;
            }


            obj.TourStartDateTime = model.TourStartDateTime;
            obj.StartRegionID = Convert.ToInt64(model.RegionID);


            obj.ChildAge = Convert.ToInt32(model.ChildAge);
            obj.Amount = Convert.ToDecimal(model.Amount);
            if (model.CurrencyID != "0" && model.CurrencyID != null)
            {
                obj.CurrencyID = Convert.ToInt32(model.CurrencyID);
            }
            else
            {
                obj.CurrencyID = null;
            }

            obj.Cost = Convert.ToDecimal(model.Cost);
            obj.CostCurrencyID = Convert.ToInt32(model.CostCurrencyID);

            if (model.Deposit != null)
            {
                obj.Deposit = Convert.ToDecimal(model.Deposit);
            }
            else
            {
                obj.Deposit = null;
            }

            if (model.DepositCurrencyID != "0" && model.DepositCurrencyID != null)
            {
                obj.DepositCurrencyID = Convert.ToInt32(model.DepositCurrencyID);
            }
            else
            {
                obj.DepositCurrencyID = null;
            }

            if (model.TypeDepositID != "0" && model.TypeDepositID != null)
            {
                obj.DepositTypeID = Convert.ToInt32(model.TypeDepositID);
            }
            if (model.BusinessPartnerCancelPolicyID != "0" && model.BusinessPartnerCancelPolicyID != null)
            {
                obj.BusinessPartnerCancelPolicyID = Convert.ToInt32(model.BusinessPartnerCancelPolicyID);
            }
            else
            {
                obj.BusinessPartnerCancelPolicyID = null;
            }


            //   obj.CreditCardRequired = null;
            if (model.HitCount != null)
            {
                obj.HitCount = Convert.ToInt64(model.HitCount);
            }
            else
            {
                obj.HitCount = null;
            }

            obj.IsPopular = model.IsPopular;

            if (model.Sorts != null)
            {
                obj.Sort = Convert.ToInt32(model.Sorts);
            }
            else
            {
                obj.Sort = null;
            }
            if (model.RoutingName != null)
            {
                obj.RoutingName = model.RoutingName;
            }
            else
            {
                obj.RoutingName = null;
            }

            obj.Active = model.Active;

            obj.CreateDateTime = DateTime.Now;
            obj.CreateUserID = Convert.ToInt64(ctrl.Session["UserID"]);

            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            obj.IPAddress = model.IPAddress;
         
            db.TB_Tour.Add(obj);
            db.SaveChanges();
            int id = obj.ID;

            return status;
        }
    }
    public class TB_TourExt
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string BusinessPartner { get; set; }
        public string Code { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? StartDate { get; set; }

        [Required(ErrorMessage = "This field is required")]
        [DataType(DataType.Date), DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime? EndDate { get; set; }
        public string Name_en { get; set; }
        public string Description_en { get; set; }
        public string SpecialNote_en { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Quota { get; set; }
        public string TourFrequency { get; set; }
        public string Duration { get; set; }
        public string Unit { get; set; }
        public string TourStartDateTime { get; set; }
        public string Region { get; set; }
        public string ChildAge { get; set; }
        public string Amount { get; set; }
        public string Currency { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string Cost { get; set; }
        public string CostCurrencyName { get; set; }
        public string Deposit { get; set; }
        public string DepositCurrencyName { get; set; }
        public string TypeDeposit { get; set; }
        public string BusinessPartnerCancelPolicyID { get; set; }
        public string HitCount { get; set; }
        public bool IsPopular { get; set; }
        public string Sorts { get; set; }
        public string RoutingName { get; set; }
        public bool Active { get; set; }
        public string Operation { get; set; }

        [Required(ErrorMessage = "This field is required")]
        public string IPAddress { get; set; }

        public string BusinessPartnerID { get; set; }
        public string TourFrequencyID { get; set; }
        public string UnitID { get; set; }
        public string CurrencyID { get; set; }
        public string CostCurrencyID { get; set; }
        public string DepositCurrencyID { get; set; }
        public string TypeDepositID { get; set; }
        public string RegionID { get; set; }

        

    }
}