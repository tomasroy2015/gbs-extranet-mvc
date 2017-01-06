using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;


namespace gbsExtranetMVC.Models.Repositories
{
    public class BizTbl_SecurityRepository : BaseRepository
    {
        public string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;


        public List<BizTbl_SecurityExt> ReadAll(int TableID)
        {
            List<BizTbl_SecurityExt> list = new List<BizTbl_SecurityExt>();

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
                    BizTbl_SecurityExt model = new BizTbl_SecurityExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.Code = dr["Code"].ToString();                 
                    model.Description = dr["Description"].ToString();                 
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Update(BizTbl_SecurityExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.BizTbl_Security.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.Code = model.Code;
            obj.Description_en = model.Description;   
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }
        public bool Delete(BizTbl_SecurityExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.BizTbl_Security.Where(x => x.ID == model.ID).FirstOrDefault();
            db.BizTbl_Security.Remove(obj);
            db.SaveChanges();
            return status;
        }

        public bool Create(BizTbl_SecurityExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            BizTbl_Security obj = new BizTbl_Security();
            obj.Code = model.Code;         
            obj.Description_en = model.Description;       
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.BizTbl_Security.Add(obj);
            db.SaveChanges();

            int id = obj.ID;

            return status;
        }

    }
    public class BizTbl_SecurityExt
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Code { get; set; }    
        public string Description { get; set; }
    }
}