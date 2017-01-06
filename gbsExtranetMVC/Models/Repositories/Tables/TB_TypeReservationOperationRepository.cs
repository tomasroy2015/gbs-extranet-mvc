﻿using gbsExtranetMVC.Helpers;
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
    public class TB_TypeReservationOperationRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<TB_TypeReservationOperationExt> GetAllTableValue(int TableID)
        {
            List<TB_TypeReservationOperationExt> list = new List<TB_TypeReservationOperationExt>();

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
                    TB_TypeReservationOperationExt model = new TB_TypeReservationOperationExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.PartID = Convert.ToInt32(dr["PartID"]);
                    model.PartName = dr["FK_PartID_ID"].ToString();
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
                    model.Level = Convert.ToInt32(dr["Level"]);
                    model.Sorts = dr["Sort"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    model.Admin = Convert.ToBoolean(dr["Admin"]);
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_TypeReservationOperationExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            TB_TypeReservationOperation DepObj = new TB_TypeReservationOperation();
            DepObj.ID = model.ID;
            DepObj.PartID = model.PartID;
            DepObj.Name_en = model.Name_en;
            DepObj.Name_tr = model.Name_tr;
            DepObj.Name_de = model.Name_de;
            DepObj.Name_es = model.Name_es;
            DepObj.Name_fr = model.Name_fr;
            DepObj.Name_ru = model.Name_ru;
            DepObj.Name_it = model.Name_it;
            DepObj.Name_ar = model.Name_ar;
            DepObj.Name_ja = model.Name_ja;
            DepObj.Name_pt = model.Name_pt;
            DepObj.Name_zh = model.Name_zh;
            DepObj.Level = model.Level;
            DepObj.Sort = Convert.ToInt16(model.Sorts);
            DepObj.Admin = model.Admin;
            DepObj.Active = model.Active;
            DepObj.OpDateTime = DateTime.Now;
            DepObj.OpUserID = 0;
            insertentity.TB_TypeReservationOperation.Add(DepObj);
            insertentity.SaveChanges();
            int id = DepObj.ID;
            return status;
        }

        public bool Update(TB_TypeReservationOperationExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            using (DBEntities DE = new DBEntities())
            {
                var DepObj = DE.TB_TypeReservationOperation.Where(x => x.ID == model.ID).FirstOrDefault();
                DepObj.PartID = model.PartID;
                DepObj.Name_en = model.Name_en;
                DepObj.Name_tr = model.Name_tr;
                DepObj.Name_de = model.Name_de;
                DepObj.Name_es = model.Name_es;
                DepObj.Name_fr = model.Name_fr;
                DepObj.Name_ru = model.Name_ru;
                DepObj.Name_it = model.Name_it;
                DepObj.Name_ar = model.Name_ar;
                DepObj.Name_ja = model.Name_ja;
                DepObj.Name_pt = model.Name_pt;
                DepObj.Name_zh = model.Name_zh;
                DepObj.Level = model.Level;
                DepObj.Sort = Convert.ToInt16(model.Sorts);
                DepObj.Admin = model.Admin;
                DepObj.Active = model.Active;
                DepObj.OpDateTime = DateTime.Now;
                DepObj.OpUserID = 0;
                DE.SaveChanges();
            }
            return status;
        }

        public bool Delete(TB_TypeReservationOperationExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            using (DBEntities DE = new DBEntities())
            {
                var DepObj = DE.TB_TypeReservationOperation.Where(x => x.ID == model.ID).FirstOrDefault();
                DE.TB_TypeReservationOperation.Remove(DepObj);
                DE.SaveChanges();
            }
            return status;

        }
    }

    public class TB_TypeReservationOperationExt
    {
        [Required(ErrorMessage = "This field is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "This field must be numeric!")]
        public int ID { get; set; }
        public int PartID { get; set; }
        public int Level { get; set; }
        public string PartName { get; set; }
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
        public string Sorts { get; set; }
        public bool Active { get; set; }
        public bool Admin { get; set; }
    }
}