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
    public class TB_BusinessPartnerRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_BusinessPartnerExt> ReadAll(int TableID)
        {
            List<TB_BusinessPartnerExt> list = new List<TB_BusinessPartnerExt>();
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
                    TB_BusinessPartnerExt model = new TB_BusinessPartnerExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.BusinessPartnerTypeID = Convert.ToInt32(dr["BusinessPartnerTypeID"]);
                    model.BusinessPartnerType = dr["FK_BusinessPartnerTypeID_ID"].ToString();
                    model.FirmID = Convert.ToInt32(dr["FirmID"]);
                    model.Firm = dr["FK_FirmID_ID"].ToString();
                    model.CountryID = Convert.ToInt32(dr["CountryID"]);
                    model.Country = dr["FK_CountryID_ID"].ToString();
                    model.City = dr["FK_CityID_ID"].ToString();
                    if (dr["CityID"].ToString() != null && dr["CityID"].ToString() != "")
                    {
                        model.CityID = Convert.ToInt32(dr["CityID"]);
                    }
                    else
                    {
                        model.CityID = 0;
                    }
                    model.Name = dr["Name"].ToString();
                    model.Description = dr["Description"].ToString();
                    model.Address = dr["Address"].ToString();
                    model.Phone = dr["Phone"].ToString();
                    model.Fax = dr["Fax"].ToString();
                    model.PostCode = dr["PostCode"].ToString();
                    model.WebAddress = dr["WebAddress"].ToString();
                    model.Email = dr["Email"].ToString();
                    string value = Convert.ToString(dr["TransferCostCurrencyID"]);
                    if (dr["TransferCostCurrencyID"].ToString() != null && dr["TransferCostCurrencyID"].ToString() != "")
                    {
                        model.TransferCostCurrencyID = Convert.ToInt32(dr["TransferCostCurrencyID"].ToString());
                    }
                    else
                    {
                        model.TransferCostCurrencyID = 0;
                    }
                    
                    model.TransferCostCurrency = dr["FK_TransferCostCurrencyID_ID"].ToString();
                    if (dr["TransferCurrencyID"].ToString() != null && dr["TransferCurrencyID"].ToString() != "")
                    {
                        model.TransferCurrencyID = Convert.ToInt32(dr["TransferCurrencyID"]);
                    }
                    else
                    {
                        model.TransferCurrencyID = 0;
                    }
                    
                    model.TransferCurrency = dr["FK_TransferCurrencyID_ID"].ToString();
                    if (dr["TransferDepositTypeID"].ToString() != null && dr["TransferDepositTypeID"].ToString() != "")
                    {
                        model.TransferDepositTypeID = Convert.ToInt32(dr["TransferDepositTypeID"]);
                    }
                    else
                    {
                        model.TransferDepositTypeID = 0;
                    }
                   
                    model.TransferDepositType = dr["FK_TransferDepositTypeID_ID"].ToString();
                    if (dr["TourCostCurrencyID"].ToString() != null && dr["TourCostCurrencyID"].ToString() != "")
                    {
                        model.TourCostCurrencyID = Convert.ToInt32(dr["TourCostCurrencyID"]);
                    }
                    else
                    {
                        model.TourCostCurrencyID = 0; 
                    }
                    
                    model.TourCostCurrency = dr["FK_TourCostCurrencyID_ID"].ToString();
                    if (dr["TourCurrencyID"].ToString() != null && dr["TourCurrencyID"].ToString() != "")
                    {
                        model.TourCurrencyID = Convert.ToInt32(dr["TourCurrencyID"]);
                    }
                    else
                    {
                        model.TourCurrencyID = 0;
                    }
                    model.TourCurrency = dr["FK_TourCurrencyID_ID"].ToString();
                    if (dr["TourDepositTypeID"].ToString() != null && dr["TourDepositTypeID"].ToString() != "")
                    {
                        model.TourDepositTypeID = Convert.ToInt32(dr["TourDepositTypeID"]);
                    }
                    else
                    {
                        model.TourDepositTypeID = 0;
                    }
                    
                    model.TourDepositType = dr["FK_TourDepositTypeID_ID"].ToString();
                    if (dr["DealCostCurrencyID"].ToString() != null && dr["DealCostCurrencyID"].ToString() != "")
                    {
                        model.DealCostCurrencyID = Convert.ToInt32(dr["DealCostCurrencyID"]);
                    }
                    else
                    {
                        model.DealCostCurrencyID = 0;

                    }
                    
                    model.DealCostCurrency = dr["FK_DealCostCurrencyID_ID"].ToString();
                    if (dr["DealCurrencyID"].ToString() != null && dr["DealCurrencyID"].ToString() != "")
                    {
                        model.DealCurrencyID = Convert.ToInt32(dr["DealCurrencyID"]);
                    }
                    else
                    {
                        model.DealCurrencyID = 0;
                    }
                    
                    model.DealCurrency = dr["FK_DealCurrencyID_ID"].ToString();
                    if (dr["DealDepositTypeID"].ToString() != null && dr["DealDepositTypeID"].ToString() != "")
                    {
                        model.DealDepositTypeID = Convert.ToInt32(dr["DealDepositTypeID"]);
                    }
                    else
                    {
                        model.DealDepositTypeID = 0;
                    }
                    
                    model.DealDepositType = dr["FK_DealDepositTypeID_ID"].ToString();
                    model.HitCount = dr["HitCount"].ToString();
                    model.Sorts = dr["Sort"].ToString();
                    model.StatusID = Convert.ToInt32(dr["StatusID"]);
                    model.Status = dr["FK_StatusID_ID"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    model.IPAddress = dr["IPAddress"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_BusinessPartnerExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            TB_BusinessPartner obj = new TB_BusinessPartner();
         //   obj.ID = model.ID;
            obj.BusinessPartnerTypeID = Convert.ToInt32(model.BusinessPartnerTypeID);
            obj.FirmID = Convert.ToInt32(model.FirmID);
            obj.CountryID = Convert.ToInt32(model.CountryID);
            if (model.CityID != 0)
            {
                obj.CityID = Convert.ToInt32(model.CityID);
            }
            else
            {
                obj.CityID = null;
            }
            
            obj.Name = model.Name;
            obj.Description_en = model.Description;
            obj.Address = model.Address;
            obj.Phone = model.Phone;
            obj.Fax = model.Fax;
            obj.PostCode = model.PostCode;
            obj.WebAddress = model.WebAddress;
            obj.Email=model.Email;

            if (model.TransferCostCurrencyID != 0)
            {
                obj.TransferCostCurrencyID = model.TransferCostCurrencyID;
            }
            else
            {
                obj.TransferCostCurrencyID = null;
            }
            if(model.TransferCurrencyID!=0)
            {
                obj.TransferCurrencyID = model.TransferCurrencyID;
            }
            else
            {
                obj.TransferCurrencyID = null;
            }
            
            
            if (model.TransferDepositTypeID!=0)
            {
                obj.TransferDepositTypeID = model.TransferDepositTypeID;
            }
            else
            {
                obj.TransferDepositTypeID = null;
            }

            if (model.TourCostCurrencyID != 0)
            {
                obj.TourCostCurrencyID = model.TourCostCurrencyID;
            }
            else
            {
                obj.TourCostCurrencyID = null;
            }

            if (model.TourCurrencyID != 0)
            {
                obj.TourCurrencyID = model.TourCurrencyID;
            }
            else
            {
                obj.TourCurrencyID = null;
            }

            if (model.TourDepositTypeID != 0)
            {
                obj.TourDepositTypeID = model.TourDepositTypeID;
            }
            else
            {
                obj.TourDepositTypeID = null;
            }

            if (model.DealCostCurrencyID != 0)
            {
                obj.DealCostCurrencyID = model.DealCostCurrencyID;
            }
            else
            {
                obj.DealCostCurrencyID = null;
            }

            if (model.DealCurrencyID != 0)
            {
                obj.DealCurrencyID = model.DealCurrencyID;
            }
            else
            {
                obj.DealCurrencyID = null;
            }

            if (model.DealDepositTypeID != 0)
            {
                obj.DealDepositTypeID = model.DealDepositTypeID;
            }
            else
            {
                obj.DealDepositTypeID = null;
            }

            if (model.HitCount != "" && model.HitCount!=null)
            {
                obj.HitCount = Convert.ToInt64(model.HitCount);
            }
            else
            {
                obj.HitCount = null;
            }

            if(model.Sorts!="" && model.Sorts!=null)
            {
                obj.Sort = Convert.ToInt32(model.Sorts);
            }
            else
            {
                obj.Sort = null;
            }
            
            obj.StatusID = Convert.ToInt32(model.StatusID);
            obj.Active = model.Active;
            obj.IPAddress = model.IPAddress;
            obj.CreateDateTime = DateTime.Now;
            obj.CreateUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.TB_BusinessPartner.Add(obj);
            db.SaveChanges();

            int id = Convert.ToInt32(obj.ID);

            return status;
        }

        public bool Delete(TB_BusinessPartnerExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_BusinessPartner.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_BusinessPartner.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(TB_BusinessPartnerExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_BusinessPartner.Where(x => x.ID == model.ID).FirstOrDefault();

            obj.ID = model.ID;
            obj.BusinessPartnerTypeID = Convert.ToInt32(model.BusinessPartnerTypeID);
            obj.FirmID = Convert.ToInt32(model.FirmID);
            obj.CountryID = Convert.ToInt32(model.CountryID);
            if (model.CityID != 0)
            {
                obj.CityID = Convert.ToInt32(model.CityID);
            }
            else
            {
                obj.CityID = null;
            }
            obj.Name = model.Name;
            obj.Description_en = model.Description;
            obj.Address = model.Address;
            obj.Phone = model.Phone;
            obj.Fax = model.Fax;
            obj.PostCode = model.PostCode;
            obj.WebAddress = model.WebAddress;
            obj.Email = model.Email;
            if (model.TransferCostCurrencyID != 0)
            {
                obj.TransferCostCurrencyID = model.TransferCostCurrencyID;
            }
            else
            {
                obj.TransferCostCurrencyID = null;
            }
            if (model.TransferCurrencyID != 0)
            {
                obj.TransferCurrencyID = model.TransferCurrencyID;
            }
            else
            {
                obj.TransferCurrencyID = null;
            }


            if (model.TransferDepositTypeID != 0)
            {
                obj.TransferDepositTypeID = model.TransferDepositTypeID;
            }
            else
            {
                obj.TransferDepositTypeID = null;
            }

            if (model.TourCostCurrencyID != 0)
            {
                obj.TourCostCurrencyID = model.TourCostCurrencyID;
            }
            else
            {
                obj.TourCostCurrencyID = null;
            }

            if (model.TourCurrencyID != 0)
            {
                obj.TourCurrencyID = model.TourCurrencyID;
            }
            else
            {
                obj.TourCurrencyID = null;
            }

            if (model.TourDepositTypeID != 0)
            {
                obj.TourDepositTypeID = model.TourDepositTypeID;
            }
            else
            {
                obj.TourDepositTypeID = null;
            }

            if (model.DealCostCurrencyID != 0)
            {
                obj.DealCostCurrencyID = model.DealCostCurrencyID;
            }
            else
            {
                obj.DealCostCurrencyID = null;
            }

            if (model.DealCurrencyID != 0)
            {
                obj.DealCurrencyID = model.DealCurrencyID;
            }
            else
            {
                obj.DealCurrencyID = null;
            }

            if (model.DealDepositTypeID != 0)
            {
                obj.DealDepositTypeID = model.DealDepositTypeID;
            }
            else
            {
                obj.DealDepositTypeID = null;
            }

            if (model.HitCount != "" && model.HitCount != null)
            {
                obj.HitCount = Convert.ToInt64(model.HitCount);
            }
            else
            {
                obj.HitCount = null;
            }

            if (model.Sorts != "" && model.Sorts != null)
            {
                obj.Sort = Convert.ToInt32(model.Sorts);
            }
            else
            {
                obj.Sort = null;
            }
            obj.StatusID = Convert.ToInt32(model.StatusID);
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

    public class TB_BusinessPartnerExt
    {
        public int ID { get; set; }
        public int BusinessPartnerTypeID { get; set; }
        public string BusinessPartnerType { get; set; }
        public int FirmID { get; set; }
        public string Firm { get; set; }
        public int CountryID { get; set; }
        public string Country { get; set; }
        public int CityID { get; set; }
        public string City { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string Name { get; set; }
        public string Description { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string Address { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string Phone { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string Fax { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string PostCode { get; set; }
        public string WebAddress { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string Email { get; set; }
        public int TransferCostCurrencyID { get; set; }
        public string TransferCostCurrency { get; set; }
        public int TransferCurrencyID { get; set; }
        public string TransferCurrency { get; set; }
        public int TransferDepositTypeID { get; set; }
        public string TransferDepositType { get; set; }
        public int TourCostCurrencyID { get; set; }
        public string TourCostCurrency { get; set; }
        public int TourCurrencyID { get; set; }
        public string TourCurrency { get; set; }
        public int TourDepositTypeID { get; set; }
        public string TourDepositType { get; set; }
        public int DealCostCurrencyID { get; set; }
        public string DealCostCurrency { get; set; }
        public int DealCurrencyID { get; set; }
        public string DealCurrency { get; set; }
        public int DealDepositTypeID { get; set; }
        public string DealDepositType { get; set; }
        public string HitCount { get; set; }
        public string Sorts { get; set; }
        public int StatusID { get; set; }
        public string Status { get; set; }
        public bool Active { get; set; }

        [Required(ErrorMessage = "This field is required!")]
        public string IPAddress { get; set; }

    }
}