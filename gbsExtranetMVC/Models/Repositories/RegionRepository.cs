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

    public class RegionExt
    {

        // [Required(ErrorMessage = "This field is required")]

        [Required(ErrorMessage = "This field is required")]

        [Range(1, long.MaxValue, ErrorMessage = "This field is required")]
        public long ID { get; set; }
        public string CountryID { get; set; }
        
        public string Country { get; set; }
        public string ParentID { get; set; }
        public string secondParentID { get; set; }
        public string RegionType { get; set; }
        public string SubRegionType { get; set; }
        public string Name { get; set; }
        public string NameASCII { get; set; }
        public string Name_tr { get; set; }
        public string Name_en { get; set; }
        public string Name_de { get; set; }
        public string Name_es { get; set; }
        public string Name_fr { get; set; }
        public string Name_ru { get; set; }
        public string Name_it { get; set; }
        public string Name_ar { get; set; }
        public string Name_ja { get; set; }
        public string Name_pt { get; set; }
        public string Name_zh { get; set; }
        public string Code { get; set; }
        public string Population { get; set; }
        public bool IsIncludedInSearch { get; set; }
        public bool IsCity { get; set; }
        public bool IsPopular { get; set; }
        public bool IsFilter { get; set; }
        public bool IsMainPageDisplay { get; set; }
        public string MainPageDisplaySort { get; set; }
        public string HitCount { get; set; }
        public string Sort { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string MapZoomIndex { get; set; }
        public bool CityTax { get; set; }
        public bool Active { get; set; }
    }
    public class RegionRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<RegionExt> GetAllRegions()
        {
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetAllRegions_TB_Region_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;           
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            List<RegionExt> list = new List<RegionExt>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    RegionExt RegObj = new RegionExt();
                    RegObj.ID = Convert.ToInt64(dr["ID"].ToString());
                    RegObj.Country = dr["FK_CountryID_ID"].ToString();
                    RegObj.ParentID = dr["ParentID"].ToString();
                    RegObj.secondParentID = dr["SecondParentID"].ToString();
                    RegObj.RegionType = dr["RegionType"].ToString();
                    RegObj.SubRegionType = dr["SubRegionType"].ToString();
                    RegObj.Name = dr["Name"].ToString();
                    RegObj.NameASCII = dr["NameASCII"].ToString();
                    RegObj.Name_en = dr["Name_en"].ToString();
                    RegObj.Name_tr = dr["Name_tr"].ToString();
                    RegObj.Name_de = dr["Name_de"].ToString();
                    RegObj.Name_es = dr["Name_es"].ToString();
                    RegObj.Name_fr = dr["Name_fr"].ToString();
                    RegObj.Name_ru = dr["Name_ru"].ToString();
                    RegObj.Name_it = dr["Name_it"].ToString();
                    RegObj.Name_ar = dr["Name_ar"].ToString();
                    RegObj.Name_ja = dr["Name_ja"].ToString();
                    RegObj.Name_pt = dr["Name_pt"].ToString();
                    RegObj.Name_zh = dr["Name_zh"].ToString();
                    RegObj.Code = dr["Code"].ToString();
                    RegObj.Population = dr["Population"].ToString();
                    RegObj.IsIncludedInSearch = Convert.ToBoolean(dr["IsIncludedInDestinationSearch"].ToString());
                    RegObj.IsCity = Convert.ToBoolean(dr["IsCity"].ToString());
                    RegObj.IsPopular = Convert.ToBoolean(dr["IsPopular"].ToString());
                    RegObj.IsFilter = Convert.ToBoolean(dr["IsFilter"].ToString());
                    RegObj.IsMainPageDisplay = Convert.ToBoolean(dr["IsMainPageDisplay"].ToString());
                    RegObj.MainPageDisplaySort = dr["MainPageDisplaySort"].ToString();
                    RegObj.HitCount = dr["HitCount"].ToString();
                    RegObj.Sort = dr["Sort"].ToString();
                    RegObj.Latitude = dr["Latitude"].ToString();
                    RegObj.Longitude = dr["Longitude"].ToString();
                    RegObj.MapZoomIndex = dr["MapZoomIndex"].ToString();
                    string citytax = dr["HasCityTax"].ToString();
                    if (citytax != null && citytax != "")
                    {
                        RegObj.CityTax = Convert.ToBoolean(dr["HasCityTax"].ToString());
                    }
                    else
                    {
                        RegObj.CityTax = false;
                    }

                    RegObj.Active = Convert.ToBoolean(dr["Active"].ToString());
                    RegObj.CountryID = dr["CountryID"].ToString();
                    list.Add(RegObj);
                }
            }
            return list;
            
        }


        public List<RegionExt> ReadAll()
             
        {
            List<RegionExt> list = new List<RegionExt>();

            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetRegionsDropdown_TB_Region_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
          
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    RegionExt RegionObj = new RegionExt();
                    RegionObj.ID = Convert.ToInt64(dr["ID"]);
                    RegionObj.Name = dr["Region"].ToString();
                    list.Add(RegionObj);
                }
            }

            return list;

        }

        public bool Create(RegionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            TB_Region RegionTable = new TB_Region();
            RegionTable.ID = model.ID;
            RegionTable.CountryID = Convert.ToInt32(model.CountryID);
            RegionTable.ParentID = Convert.ToInt64(model.ParentID);
            RegionTable.SecondParentID = Convert.ToInt64(model.secondParentID);
            RegionTable.RegionType = model.RegionType;
            RegionTable.SubRegionType = model.SubRegionType;
            RegionTable.Name = model.Name;
            RegionTable.NameASCII = model.NameASCII;
            RegionTable.Name_en = model.Name_en;
            RegionTable.Name_tr = model.Name_tr;
            RegionTable.Name_de = model.Name_de;
            RegionTable.Name_es = model.Name_es;
            RegionTable.Name_fr = model.Name_fr;
            RegionTable.Name_ru = model.Name_ru;
            RegionTable.Name_it = model.Name_it;
            RegionTable.Name_ar = model.Name_ar;
            RegionTable.Name_ja = model.Name_ja;
            RegionTable.Name_pt = model.Name_pt;
            RegionTable.Name_zh = model.Name_zh;
            RegionTable.Code = model.Code;
            RegionTable.Population = Convert.ToInt64(model.Population);
            RegionTable.IsIncludedInDestinationSearch = model.IsIncludedInSearch;
            RegionTable.IsCity = model.IsCity;
            RegionTable.IsPopular = model.IsPopular;
            RegionTable.IsFilter = model.IsFilter;
            RegionTable.IsMainPageDisplay = model.IsMainPageDisplay;
            RegionTable.MainPageDisplaySort = Convert.ToInt32(model.MainPageDisplaySort);
            RegionTable.HitCount = Convert.ToInt64(model.HitCount);
            RegionTable.Sort = Convert.ToInt16(model.Sort);
            RegionTable.Latitude = model.Latitude;
            RegionTable.Longitude = model.Longitude;
            RegionTable.MapZoomIndex = Convert.ToInt32(model.MapZoomIndex);
            RegionTable.HasCityTax = model.CityTax;
            RegionTable.Active = model.Active;
            RegionTable.OpDateTime = DateTime.Now;
            RegionTable.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            insertentity.TB_Region.Add(RegionTable);
            insertentity.SaveChanges();
            return status;
        }

        public bool Update(RegionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            using (DBEntities DE = new DBEntities())
            {
                var RegionTable = DE.TB_Region.Where(x => x.ID == model.ID).FirstOrDefault();
                RegionTable.ID = model.ID;
                RegionTable.CountryID = Convert.ToInt32(model.CountryID);
                RegionTable.ParentID = Convert.ToInt64(model.ParentID);
                RegionTable.SecondParentID = Convert.ToInt64(model.secondParentID);
                RegionTable.RegionType = model.RegionType;
                RegionTable.SubRegionType = model.SubRegionType;
                RegionTable.Name = model.Name;
                RegionTable.NameASCII = model.NameASCII;
                RegionTable.Name_en = model.Name_en;
                RegionTable.Name_tr = model.Name_tr;
                RegionTable.Name_de = model.Name_de;
                RegionTable.Name_es = model.Name_es;
                RegionTable.Name_fr = model.Name_fr;
                RegionTable.Name_ru = model.Name_ru;
                RegionTable.Name_it = model.Name_it;
                RegionTable.Name_ar = model.Name_ar;
                RegionTable.Name_ja = model.Name_ja;
                RegionTable.Name_pt = model.Name_pt;
                RegionTable.Name_zh = model.Name_zh;
                RegionTable.Code = model.Code;
                RegionTable.Population = Convert.ToInt64(model.Population);
                RegionTable.IsIncludedInDestinationSearch = model.IsIncludedInSearch;
                RegionTable.IsCity = model.IsCity;
                RegionTable.IsPopular = model.IsPopular;
                RegionTable.IsFilter = model.IsFilter;
                RegionTable.IsMainPageDisplay = model.IsMainPageDisplay;
                RegionTable.MainPageDisplaySort = Convert.ToInt32(model.MainPageDisplaySort);
                RegionTable.HitCount = Convert.ToInt64(model.HitCount);
                RegionTable.Sort = Convert.ToInt16(model.Sort);
                RegionTable.Latitude = model.Latitude;
                RegionTable.Longitude = model.Longitude;
                RegionTable.MapZoomIndex = Convert.ToInt32(model.MapZoomIndex);
                RegionTable.HasCityTax = model.CityTax;
                RegionTable.Active = model.Active;
                DE.SaveChanges();
            }
            return status;
        }

        public bool Delete(RegionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;            
            using (DBEntities DE = new DBEntities())
            {
                var RegionTable = DE.TB_Region.Where(x => x.ID == model.ID).FirstOrDefault();
                DE.TB_Region.Remove(RegionTable);
                DE.SaveChanges();
            }
            return status;
        }


    }     
}