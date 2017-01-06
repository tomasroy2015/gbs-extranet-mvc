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
using System.Collections.Generic;
using System;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_AttributeHeaderRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<TB_AttributeHeaderExt> GetAllTableValue(int TableID)
        {
            List<TB_AttributeHeaderExt> list = new List<TB_AttributeHeaderExt>();

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
                    TB_AttributeHeaderExt model = new TB_AttributeHeaderExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.Code = dr["Code"].ToString();
                    model.Name = dr["Name"].ToString();
                    model.Sorts = dr["Sort"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_AttributeHeaderExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            DBEntities insertentity = new DBEntities();
            TB_AttributeHeader DepObj = new TB_AttributeHeader();
           // DepObj.ID = model.ID;
            DepObj.Code = model.Code;
            DepObj.Name_en = model.Name;
            DepObj.Sort = Convert.ToInt16(model.Sorts);
            DepObj.Active = model.Active;
            DepObj.OpDateTime = DateTime.Now;
            DepObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            insertentity.TB_AttributeHeader.Add(DepObj);
            insertentity.SaveChanges();
            int id = DepObj.ID;
            return status;
        }

        public bool Update(TB_AttributeHeaderExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            using (DBEntities DE = new DBEntities())
            {
                var DepObj = DE.TB_AttributeHeader.Where(x => x.ID == model.ID).FirstOrDefault();
                DepObj.Code = model.Code;
                DepObj.Name_en = model.Name;
                DepObj.Sort = Convert.ToInt16(model.Sorts);
                DepObj.Active = model.Active;
                DepObj.OpDateTime = DateTime.Now;
                DepObj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
                DE.SaveChanges();
            }
            return status;
        }

        public bool Delete(TB_AttributeHeaderExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            using (DBEntities DE = new DBEntities())
            {
                var DepObj = DE.TB_AttributeHeader.Where(x => x.ID == model.ID).FirstOrDefault();
                DE.TB_AttributeHeader.Remove(DepObj);
                DE.SaveChanges();
            }
            return status;

        }
    }

    public class TB_AttributeHeaderExt
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Sorts { get; set; }
        public bool Active { get; set; }
    }
}