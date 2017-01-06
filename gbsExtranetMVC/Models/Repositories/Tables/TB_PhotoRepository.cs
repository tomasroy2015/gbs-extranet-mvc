using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{   
    public class TB_PhotoRepository : BaseRepository 
    {
        public static string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_PhotoExt> ReadAll(int TableID)
        {
            List<TB_PhotoExt> list = new List<TB_PhotoExt>();

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
                    TB_PhotoExt model = new TB_PhotoExt();
                    model.ID = Convert.ToInt64(dr["ID"]);
                    model.PartName = dr["FK_PartID_ID"].ToString();
                    model.PartID = Convert.ToInt32(dr["PartID"]);
                    model.RecordID = Convert.ToInt64(dr["RecordID"].ToString());
                    model.Name = dr["Name"].ToString();
                    try
                    {
                        model.MainPhoto = Convert.ToBoolean(dr["MainPhoto"].ToString());
                    }
                    catch
                    {
                        model.MainPhoto = false;
                    }
                   
                    model.Sorts = dr["Sort"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"].ToString());
                    model.Operation = dr["Operation"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Delete(TB_PhotoExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_Photo.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_Photo.Remove(obj);
            db.SaveChanges();
            return status;
        }
        public bool Create(TB_PhotoExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            TB_Photo obj = new TB_Photo();
            // MailTable.MailTemplateID =model.MailTemplateID;  
           // obj.ID = model.ID;
            obj.PartID = model.PartID;
            obj.RecordID = model.RecordID;
            obj.Name = model.Name;
            obj.MainPhoto = model.MainPhoto;
            obj.Sort = Convert.ToInt32(model.Sorts);
            obj.Active = model.Active;
            obj.CreateDateTime = DateTime.Now;
            obj.CreateUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.TB_Photo.Add(obj);
            db.SaveChanges();
            Int64 id = obj.ID;
            return status;
        }
        public bool Update(TB_PhotoExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var obj = db.TB_Photo.Where(x => x.ID == model.ID).FirstOrDefault();
            // MailTable.MailTemplateID =model.MailTemplateID;
            obj.ID = model.ID;
            obj.PartID = model.PartID;
            obj.RecordID = model.RecordID;
            obj.Name = model.Name;
            obj.MainPhoto = model.MainPhoto;
            obj.Sort = Convert.ToInt32(model.Sorts);
            obj.Active = model.Active;            
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }

    }

    public class TB_PhotoExt
    {
        public Int64 ID { get; set; }
        public int PartID { get; set; }
        public string PartName { get; set; }
        public Int64 RecordID { get; set; }
        public string Name { get; set; }
        public Boolean MainPhoto { get; set; }
        public string Sorts { get; set; }
        public Boolean Active { get; set; }
        public string Operation { get; set; }
    }

}