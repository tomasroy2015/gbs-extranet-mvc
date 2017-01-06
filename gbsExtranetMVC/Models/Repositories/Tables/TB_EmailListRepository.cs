using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_EmailListRepository : BaseRepository
    {
        public string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_EmailListExt> ReadAll(int TableID)
        {
            List<TB_EmailListExt> list = new List<TB_EmailListExt>();
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
                    TB_EmailListExt model = new TB_EmailListExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.Email = dr["Email"].ToString();
                    model.IPAddress = dr["IPAddress"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_EmailListExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            TB_EmailList obj = new TB_EmailList();
            obj.ID = model.ID;
            obj.Email = model.Email;
            obj.IPAddress = model.IPAddress;
            obj.Active =  model.Active;
            obj.OpDateTime = DateTime.Now;
            obj.CreateDateTime = DateTime.Now;

            db.TB_EmailList.Add(obj);
            db.SaveChanges();

            int id = Convert.ToInt32(obj.ID);

            return status;
        }

        public bool Delete(TB_EmailListExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_EmailList.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_EmailList.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(TB_EmailListExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_EmailList.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.Email = model.Email;
            obj.IPAddress = model.IPAddress;
            obj.Active = model.Active;
            obj.OpDateTime = DateTime.Now;
            obj.CreateDateTime = DateTime.Now;
            db.SaveChanges();

            return status;
        }
    }
    public class TB_EmailListExt
    {
        public int ID { get; set; }

         [Required(ErrorMessage = "This field is required!")]
        public string Email { get; set; }

         [Required(ErrorMessage = "This field is required!")]
        public string IPAddress { get; set; }
        public bool Active { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime OpDateTime { get; set; }
    }
}