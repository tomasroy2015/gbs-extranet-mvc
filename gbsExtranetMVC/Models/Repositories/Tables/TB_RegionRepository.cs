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
    public class TB_RegionRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;


        public List<TB_RegionExt> ReadAll(int TableID)
        {
            List<TB_RegionExt> list = new List<TB_RegionExt>();

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
                    TB_RegionExt model = new TB_RegionExt();
                    model.ID = Convert.ToInt64(dr["ID"]);

                    //model.Country = dr["FK_CountryID_ID"].ToString();
                    //model.ParentID = dr["ParentID"].ToString();                  
                    //model.SecondParentID = dr["SecondParentID"].ToString();

                    model.Country = dr["FK_CountryID_ID"].ToString();
                    model.ParentID = (Nullable<long>)CheckEmptyStringDBParameter(dr["ParentID"].ToString(),false,false,false,false,false,true);
                    model.SecondParentID = (Nullable<long>)CheckEmptyStringDBParameter(dr["SecondParentID"].ToString(), false, false, false, false, false, true);

                    model.RegionType = dr["RegionType"].ToString();
                    model.SubRegionType = dr["SubRegionType"].ToString();
                    model.Name = dr["Name"].ToString();
                    model.ASCIIName = dr["NameASCII"].ToString();
                    model.Name_en = dr["Name_en"].ToString();
                    model.Name_tr = dr["Name_tr"].ToString();
                    model.Name_de = dr["Name_de"].ToString();
                    model.Name_es = dr["Name_es"].ToString();
                    model.Name_fr = dr["Name_fr"].ToString();
                    model.Name_ru = dr["Name_ru"].ToString();
                    model.Name_it = dr["Name_it"].ToString();
                    model.Name_ar = dr["Name_ar"].ToString();
                    model.Name_ja = dr["Name_ja"].ToString();
                    model.Name_pt = dr["Name_pt"].ToString();
                    model.Name_zh = dr["Name_zh"].ToString();
                    model.Code = dr["Code"].ToString();
                    //model.Population = dr["Population"].ToString();
                    model.Population = (Nullable<long>)CheckEmptyStringDBParameter(dr["Population"].ToString(), false, false, false, false, false, true);

                    model.IsIncludedInDestinationSearch = Convert.ToBoolean(dr["IsIncludedInDestinationSearch"]);
                    model.IsCity = Convert.ToBoolean(dr["IsCity"]);
                    model.IsPopular = Convert.ToBoolean(dr["IsPopular"]);
                    model.IsFilter = Convert.ToBoolean(dr["IsFilter"]);
                    model.IsMainPageDisplay = Convert.ToBoolean(dr["IsMainPageDisplay"]);
                    //model.MainPageDisplaySort = dr["MainPageDisplaySort"].ToString();
                    //model.HitCount = dr["HitCount"].ToString();
                    //model.Sort = dr["Sort"].ToString();
                    model.MainPageDisplaySort = (Nullable<int>)CheckEmptyStringDBParameter(dr["MainPageDisplaySort"].ToString(), true);
                    model.HitCount = (Nullable<int>)CheckEmptyStringDBParameter(dr["HitCount"].ToString(), true);
                    model.Sorts = (Nullable<short>)CheckEmptyStringDBParameter(dr["Sort"].ToString(),false, false, false, false, false, false, true);

                    model.Latitude = dr["Latitude"].ToString();
                    model.Longitude = dr["Longitude"].ToString();
                    //model.MapZoomIndex = dr["MapZoomIndex"].ToString();
                    model.MapZoomIndex = (Nullable<int>)CheckEmptyStringDBParameter(dr["MapZoomIndex"].ToString(), true);
                    if (dr["HasCityTax"].ToString() != null && dr["HasCityTax"].ToString() != "")
                    {
                        model.HasCityTax = Convert.ToBoolean(dr["HasCityTax"]);
                    }
                    else
                    {
                        model.HasCityTax = false;
                    }
                    model.Active = Convert.ToBoolean(dr["Active"]);

                    model.CountryID = dr["CountryID"].ToString();


                    list.Add(model);
                }
            }

            return list;
        }

        public bool Update(TB_RegionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_Region.Where(x => x.ID == model.ID).FirstOrDefault();

            obj.ID = model.ID;
            obj.CountryID = Convert.ToInt32(model.CountryID);
            obj.ParentID = model.ParentID;
            obj.SecondParentID = model.SecondParentID;
            obj.RegionType = model.RegionType;
            obj.SubRegionType = model.SubRegionType;
            obj.Name = model.Name;
            obj.NameASCII = model.ASCIIName;
            obj.Name_en = model.Name_en;
            obj.Name_tr = model.Name_tr;
            obj.Name_de = model.Name_de;
            obj.Name_es = model.Name_es;
            obj.Name_fr = model.Name_fr;
            obj.Name_ru = model.Name_ru;
            obj.Name_it = model.Name_it;
            obj.Name_ar = model.Name_ar;
            obj.Name_ja = model.Name_ja;
            obj.Name_pt = model.Name_pt;
            obj.Name_zh = model.Name_zh;
            obj.Code = model.Code;
            obj.Population = model.Population;
            obj.IsIncludedInDestinationSearch = Convert.ToBoolean(model.IsIncludedInDestinationSearch);
            obj.IsCity = Convert.ToBoolean(model.IsCity);
            obj.IsPopular = Convert.ToBoolean(model.IsPopular);
            obj.IsFilter = Convert.ToBoolean(model.IsFilter);
            obj.IsMainPageDisplay = Convert.ToBoolean(model.IsMainPageDisplay);
            obj.MainPageDisplaySort = model.MainPageDisplaySort;
            obj.HitCount = model.HitCount;
            obj.Sort = model.Sorts;
            obj.Latitude = model.Latitude;
            obj.Longitude = model.Longitude;
            obj.MapZoomIndex = model.MapZoomIndex;
            obj.HasCityTax = Convert.ToBoolean(model.HasCityTax);
            obj.Active = model.Active;

            db.SaveChanges();
            return status;
        }
        public bool Delete(TB_RegionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_Region.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_Region.Remove(obj);
            db.SaveChanges();
            return status;
        }
        public bool Create(TB_RegionExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            TB_Region obj = new TB_Region();

            obj.ID = model.ID;
            obj.CountryID = Convert.ToInt32(model.CountryID);
            obj.ParentID = model.ParentID;
            obj.SecondParentID = model.SecondParentID;
            obj.RegionType = model.RegionType;
            obj.SubRegionType = model.SubRegionType;
            obj.Name = model.Name;
            obj.NameASCII = model.ASCIIName;
            obj.Name_en = model.Name_en;
            obj.Name_tr = model.Name_tr;
            obj.Name_de = model.Name_de;
            obj.Name_es = model.Name_es;
            obj.Name_fr = model.Name_fr;
            obj.Name_ru = model.Name_ru;
            obj.Name_it = model.Name_it;
            obj.Name_ar = model.Name_ar;
            obj.Name_ja = model.Name_ja;
            obj.Name_pt = model.Name_pt;
            obj.Name_zh = model.Name_zh;
            obj.Code = model.Code;
            obj.Population = model.Population;
            obj.IsIncludedInDestinationSearch = Convert.ToBoolean(model.IsIncludedInDestinationSearch);
            obj.IsCity = Convert.ToBoolean(model.IsCity);
            obj.IsPopular = Convert.ToBoolean(model.IsPopular);
            obj.IsFilter = Convert.ToBoolean(model.IsFilter);
            obj.IsMainPageDisplay = Convert.ToBoolean(model.IsMainPageDisplay);
            obj.MainPageDisplaySort = model.MainPageDisplaySort;
            obj.HitCount = model.HitCount;
            obj.Sort = model.Sorts;
            obj.Latitude = model.Latitude;
            obj.Longitude = model.Longitude;
            obj.MapZoomIndex = model.MapZoomIndex;
            obj.HasCityTax = Convert.ToBoolean(model.HasCityTax);
            obj.Active = model.Active;
            // obj.Operation = model.Operation;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.TB_Region.Add(obj);
            db.SaveChanges();

            Int64 id = obj.ID;

            return status;
        }

        public static object CheckEmptyStringDBParameter(object Value, bool ReturnInteger = false, bool ReturnDate = false, bool ReturnDouble = false, bool ReturnDecimal = false, bool ReturnBoolean = false, bool ReturnLong = false, bool ReturnShort=false)
        {

            if (Value == string.Empty)
            {
                return null;
            }

            if (ReturnInteger)
            {
                return Convert.ToInt32(Value);
            }

            //if (ReturnDate)
            //{

            //    return DateTime.ParseExact(Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            //}


            if (ReturnDouble)
            {
                return Convert.ToDouble(Value);
            }

            if (ReturnDecimal)
            {
                return Convert.ToDecimal(Value);
            }

            if (ReturnBoolean)
            {
                return Convert.ToBoolean(Value);
            }

            if (ReturnLong)
            {
                return Convert.ToInt64(Value);
            }
            if(ReturnShort)
            {
                return Convert.ToInt16(Value);
            }

            return Value;

        }
    }
    public class TB_RegionExt
    {
        [Required(ErrorMessage = "This field is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "This field must be numeric!")]
        public Int64 ID { get; set; }
        public string CountryID { get; set; }
        public string Country { get; set; }
        public Nullable<long> ParentID { get; set; }
        public Nullable<long> SecondParentID { get; set; }
        public string RegionType { get; set; }
        public string SubRegionType { get; set; }
        public string Name { get; set; }
        public string ASCIIName { get; set; }
        public string Name_en { get; set; }
        public string Name_tr { get; set; }
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
        public Nullable<long> Population { get; set; }
        public bool IsIncludedInDestinationSearch { get; set; }
        public bool IsCity { get; set; }
        public bool IsPopular { get; set; }
        public bool IsFilter { get; set; }
        public bool IsMainPageDisplay { get; set; }
        public Nullable<int> MainPageDisplaySort { get; set; }
        public Nullable<long> HitCount { get; set; }
        public Nullable<short> Sorts { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public Nullable<int> MapZoomIndex { get; set; }
        public bool HasCityTax { get; set; }
        public bool Active { get; set; }
        public string Operation { get; set; }

    }

}