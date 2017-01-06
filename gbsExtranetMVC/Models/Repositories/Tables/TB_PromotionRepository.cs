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
    public class TB_PromotionRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<TB_PromotionExt> GetAllTypeFirmRequest(int TableID)
        {
            List<TB_PromotionExt> list = new List<TB_PromotionExt>();

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
                    TB_PromotionExt model = new TB_PromotionExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.PartID = Convert.ToInt32(dr["PartID"]);
                    model.PartName = dr["FK_PartID_ID"].ToString();
                    model.Name = dr["Name"].ToString();
                    model.Type = dr["Type"].ToString();
                    model.Description = dr["Description"].ToString();
                    if (dr["GeneralPromotion"].ToString() != "")
                    { 
                    model.GeneralPromotion = Convert.ToBoolean(dr["GeneralPromotion"]);
                    }
                    else
                    {
                        model.GeneralPromotion =false;
                    }
                    if (dr["StartDate"].ToString() != "")
                    {
                        model.StartDate = Convert.ToDateTime(dr["StartDate"]);
                    }
                    else
                    {
                        model.StartDate = null;
                    }
                    if (dr["EndDate"].ToString() != "")
                    {
                        model.EndDate = Convert.ToDateTime(dr["EndDate"]);
                    }
                    else
                    {
                        model.EndDate = null;
                    }
                    if (dr["TargetStartDate"].ToString() != "")
                    {
                        model.TargetStartDate = Convert.ToDateTime(dr["TargetStartDate"]);
                    }
                    else
                    {
                        model.TargetStartDate = null;
                    }
                    if (dr["TargetEndDate"].ToString() != "")
                    {
                        model.TargetEndDate = Convert.ToDateTime(dr["TargetEndDate"]);
                    }
                    else
                    {
                        model.TargetEndDate = null;
                    }
                    if (dr["Count"].ToString() != "")
                    {
                        model.Count = Convert.ToInt32(dr["Count"]);
                    }
                    else
                    {
                        model.Count = null;
                    }
                    if (dr["DiscountPercentage"].ToString() != "")
                    {
                        model.DiscountPercentage = Convert.ToInt32(dr["DiscountPercentage"]);
                    }
                    else
                    {
                        model.DiscountPercentage = null;
                    }
                    if (dr["RegionID"].ToString() != "")
                    {
                        model.RegionID = Convert.ToInt64(dr["RegionID"]);
                    }
                    else
                    {
                        model.RegionID = null;
                    }
                    //model.StartDate = Convert.ToDateTime(dr["StartDate"]);
                    //model.EndDate = Convert.ToDateTime(dr["EndDate"]);
                    //model.TargetStartDate = Convert.ToDateTime(dr["TargetStartDate"]);
                    //model.TargetEndDate = Convert.ToDateTime(dr["TargetEndDate"]);
                    //model.Count = Convert.ToInt32(dr["Count"]);
                    //model.DiscountPercentage = Convert.ToInt32(dr["DiscountPercentage"]);
                    //model.RegionID = Convert.ToInt64(dr["RegionID"]);

                    model.Region = dr["FK_RegionID_ID"].ToString();
                    model.Sorts = dr["Sort"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_PromotionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            TB_Promotion DepObj = new TB_Promotion();
            DepObj.PartID = model.PartID;
            switch (CultureCode)
            {
                case "en": //ID from BizTbl_Table for this Table
                    DepObj.Name_en = model.Name;
                    DepObj.Description_en = model.Description;
                    break;
                case "tr":
                       DepObj.Name_tr = model.Name;
                       DepObj.Description_tr = model.Description;
                    break;
                case "de":
                    DepObj.Name_de = model.Name;
                    DepObj.Description_de = model.Description;
                    break;
                case "es":
                       DepObj.Name_es = model.Name;
                    DepObj.Description_es = model.Description;
                    break;
                case "fr":
                      DepObj.Name_fr = model.Name;
                    DepObj.Description_fr = model.Description;
                    break;
                case "ru":
                       DepObj.Name_ru = model.Name;
                    DepObj.Description_ru = model.Description;
                    break;
                case "it":
                      DepObj.Name_it = model.Name;
                    DepObj.Description_it = model.Description;
                    break;
                case "ar":
                       DepObj.Name_ar = model.Name;
                       DepObj.Description_ar = model.Description;
                    break;
                case "ja":
                      DepObj.Name_ja = model.Name;
                      DepObj.Description_ja = model.Description;
                    break;
                case "pt":
                      DepObj.Name_pt = model.Name;
                      DepObj.Description_pt = model.Description;
                    break;
                case "zh":
                       DepObj.Name_zh = model.Name;
                       DepObj.Description_zh = model.Description;
                    break;
                default:
                    break;
            }
            DepObj.Type = model.Type;
            DepObj.GeneralPromotion = model.GeneralPromotion;
            DepObj.StartDate = model.StartDate;
            DepObj.TargetStartDate = model.TargetStartDate;
            DepObj.TargetEndDate = model.TargetEndDate;
            DepObj.Count = model.Count;
            DepObj.DiscountPercentage = model.DiscountPercentage;
            DepObj.RegionID = model.RegionID;
            DepObj.Sort = Convert.ToInt16(model.Sorts);
            DepObj.Active = model.Active;
            DepObj.OpDateTime = DateTime.Now;
            DepObj.OpUserID = 0;
            insertentity.TB_Promotion.Add(DepObj);
            insertentity.SaveChanges();
            int id = DepObj.ID;
            return status;
        }

        public bool Update(TB_PromotionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            using (DBEntities DE = new DBEntities())
            {
                var DepObj = DE.TB_Promotion.Where(x => x.ID == model.ID).FirstOrDefault();
                DepObj.PartID = model.PartID;
                switch (CultureCode)
                {
                    case "en": //ID from BizTbl_Table for this Table
                        DepObj.Name_en = model.Name;
                        DepObj.Description_en = model.Description;
                        break;
                    case "tr":
                        DepObj.Name_tr = model.Name;
                        DepObj.Description_tr = model.Description;
                        break;
                    case "de":
                        DepObj.Name_de = model.Name;
                        DepObj.Description_de = model.Description;
                        break;
                    case "es":
                        DepObj.Name_es = model.Name;
                        DepObj.Description_es = model.Description;
                        break;
                    case "fr":
                        DepObj.Name_fr = model.Name;
                        DepObj.Description_fr = model.Description;
                        break;
                    case "ru":
                        DepObj.Name_ru = model.Name;
                        DepObj.Description_ru = model.Description;
                        break;
                    case "it":
                        DepObj.Name_it = model.Name;
                        DepObj.Description_it = model.Description;
                        break;
                    case "ar":
                        DepObj.Name_ar = model.Name;
                        DepObj.Description_ar = model.Description;
                        break;
                    case "ja":
                        DepObj.Name_ja = model.Name;
                        DepObj.Description_ja = model.Description;
                        break;
                    case "pt":
                        DepObj.Name_pt = model.Name;
                        DepObj.Description_pt = model.Description;
                        break;
                    case "zh":
                        DepObj.Name_zh = model.Name;
                        DepObj.Description_zh = model.Description;
                        break;
                    default:
                        break;
                }
                DepObj.Type = model.Type;
                DepObj.GeneralPromotion = model.GeneralPromotion;
                DepObj.StartDate = model.StartDate;
                DepObj.TargetStartDate = model.TargetStartDate;
                DepObj.TargetEndDate = model.TargetEndDate;
                DepObj.Count = model.Count;
                DepObj.DiscountPercentage = model.DiscountPercentage;
                DepObj.RegionID = model.RegionID;
                DepObj.Sort = Convert.ToInt16(model.Sorts);
                DepObj.Active = model.Active;
                DepObj.OpDateTime = DateTime.Now;
                DepObj.OpUserID = 0;
                DE.SaveChanges();
            }
            return status;
        }

        public bool Delete(TB_PromotionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            using (DBEntities DE = new DBEntities())
            {
                var DepObj = DE.TB_Promotion.Where(x => x.ID == model.ID).FirstOrDefault();
                DE.TB_Promotion.Remove(DepObj);
                DE.SaveChanges();
            }
            return status;

        }
    }

    public class TB_PromotionExt
    {
        public int ID { get; set; }
        public int PartID { get; set; }
        public string PartName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool GeneralPromotion { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? TargetStartDate { get; set; }
        public DateTime? TargetEndDate { get; set; }
        public int? Count { get; set; }
        public int? DiscountPercentage { get; set; }
        public Int64? RegionID { get; set; }
        public string Region { get; set; }
        public string Sorts { get; set; }
        public string Type { get; set; }
        public bool Active { get; set; }
    }
}