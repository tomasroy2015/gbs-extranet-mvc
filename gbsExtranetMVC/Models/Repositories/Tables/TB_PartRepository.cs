using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories.Tables
{
    public class TB_PartRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_PartExt> ReadAll(int TableID)
        {
            List<TB_PartExt> list = new List<TB_PartExt>();

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
                    TB_PartExt model = new TB_PartExt();
                    model.ID = Convert.ToInt32(dr["ID"]);                    
                    model.Name = dr["Name"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"].ToString());
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_PartExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            TB_Part obj = new TB_Part();
            // MailTable.MailTemplateID =model.MailTemplateID;  
            obj.ID = model.ID;
            obj.Name = model.Name;
            obj.Active = model.Active;          
            db.TB_Part.Add(obj);
            db.SaveChanges();
            Int64 id = obj.ID;
            return status;
        }

        public bool Update(TB_PartExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var obj = db.TB_Part.Where(x => x.ID == model.ID).FirstOrDefault();
            // MailTable.MailTemplateID =model.MailTemplateID;
            obj.ID = model.ID;
            obj.Name = model.Name;
            obj.Active = model.Active;           
            db.SaveChanges();
            return status;
        }
        public bool Delete(TB_PartExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_Part.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_Part.Remove(obj);
            db.SaveChanges();
            return status;
        }
    }
    public class TB_PartExt
    {
        [Required(ErrorMessage = "This field is required!")]
        [Range(1, int.MaxValue, ErrorMessage = "This field must be numeric!")]
        public int ID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Name { get; set; }
        public Boolean Active { get; set; }
    }
}