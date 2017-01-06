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
    public class TB_MessageRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_MessageExt> ReadAll(int TableID)
        {
            List<TB_MessageExt> list = new List<TB_MessageExt>();

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
                    TB_MessageExt model = new TB_MessageExt();
                    model.ID = Convert.ToInt64(dr["ID"]);
                    model.MessageSubjectName = dr["FK_MessageSubjectTypeID_ID"].ToString();
                    model.MessageSubjectID = Convert.ToInt32(dr["MessageSubjectID"].ToString());
                    model.MessageStatusName = dr["FK_MessageStatusID_ID"].ToString();
                    model.MessageStatusID = Convert.ToInt32(dr["MessageStatusID"].ToString());
                    model.TitleName = dr["FK_SalutationTypeID_ID"].ToString();
                    model.TitleID = Convert.ToInt32(dr["SalutationID"].ToString());
                    model.Name = dr["Name"].ToString();
                    model.Surname = dr["Surname"].ToString();
                    model.Email = dr["Email"].ToString();
                    model.Phone = dr["Phone"].ToString();
                    model.CountryName = dr["FK_CountryID_ID"].ToString();
                    model.CountryID = Convert.ToInt32(dr["CountryID"].ToString());
                    model.Text = dr["Text"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"].ToString());
                    model.IPAddress = dr["IPAddress"].ToString();                  
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_MessageExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            TB_Message obj = new TB_Message();
            // MailTable.MailTemplateID =model.MailTemplateID;  
            //obj.ID = model.ID;
            obj.MessageSubjectTypeID = model.MessageSubjectID;
            obj.MessageStatusID = model.MessageStatusID;
            obj.SalutationTypeID = model.TitleID;
            obj.Name = model.Name;
            obj.Surname = model.Surname;
            obj.Email = model.Email;
            obj.Phone = model.Phone;
            obj.CountryID = model.CountryID;
            obj.Text = model.Text;
            obj.Active = model.Active;
            obj.IPAddress = model.IPAddress;          
            obj.CreateDateTime = DateTime.Now;            
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.TB_Message.Add(obj);
            db.SaveChanges();
            Int64 id = obj.ID;
            return status;
        }
        public bool Update(TB_MessageExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var obj = db.TB_Message.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.MessageSubjectTypeID = model.MessageSubjectID;
            obj.MessageStatusID = model.MessageStatusID;
            obj.SalutationTypeID = model.TitleID;
            obj.Name = model.Name;
            obj.Surname = model.Surname;
            obj.Email = model.Email;
            obj.Phone = model.Phone;
            obj.CountryID = model.CountryID;
            obj.Text = model.Text;
            obj.Active = model.Active;
            obj.IPAddress = model.IPAddress;
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();
            return status;
        }
        public bool Delete(TB_MessageExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_Message.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_Message.Remove(obj);
            db.SaveChanges();
            return status;
        }
    }

    public class TB_MessageExt
    {
        public Int64 ID { get; set; }
        public int MessageSubjectID { get; set; }
        public string MessageSubjectName { get; set; }
        public int MessageStatusID { get; set; }
        public string MessageStatusName { get; set; }
        public int TitleID { get; set; }
        public string TitleName { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Name { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Surname { get; set; }
         [Required(ErrorMessage = "This field is required!")]
        public string Email { get; set; }
        public string Phone { get; set; }
        public int CountryID { get; set; }
        public string CountryName { get; set; }
         [Required(ErrorMessage = "This field is required!")]
        public string Text { get; set; }
        public Boolean Active { get; set; }
         [Required(ErrorMessage = "This field is required!")]
        public string IPAddress { get; set; }
        public DateTime CreateDateTime { get; set; }
        public DateTime OpDateTime { get; set; }
        public Int64 OpUserID { get; set; }
    }
}