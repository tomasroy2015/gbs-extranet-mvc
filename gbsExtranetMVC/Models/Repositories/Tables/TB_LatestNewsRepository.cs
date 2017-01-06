using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_LatestNewsRepository : BaseRepository
    {
        public string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_LatestNewsExt> ReadAll(int TableID)
        {
            List<TB_LatestNewsExt> list = new List<TB_LatestNewsExt>();
            DBEntities entity = new DBEntities();
            var TableIDparam = new SqlParameter("@TableID", TableID);
            var Cultureparam = new SqlParameter("@CultureCode", CultureCode);
            var result = entity.Database.SqlQuery<TB_LatestNews>("B_DisplayTableNew_BizTbl_Table_Sp @TableID,@CultureCode", TableIDparam, Cultureparam).ToList();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("UserID");
            dt.Columns.Add("Title_tr");
            dt.Columns.Add("Title_en");
            dt.Columns.Add("Title_de");
            dt.Columns.Add("Title_fr");
            dt.Columns.Add("Title_ru");
            dt.Columns.Add("Title_ar");
            dt.Columns.Add("Description_tr");
            dt.Columns.Add("Description_en");
            dt.Columns.Add("Description_de");
            dt.Columns.Add("Description_fr");
            dt.Columns.Add("Description_ru");
            dt.Columns.Add("Description_ar");
            dt.Columns.Add("PostImage");
            dt.Columns.Add("Travel");
            dt.Columns.Add("Active");
            dt.Columns.Add("Createddate");
            foreach (TB_LatestNews dr in result)
            {
                dt.Rows.Add(dr.ID, dr.UserID, dr.Title_tr, dr.Title_en, dr.Title_de, dr.Title_fr, dr.Title_ru, dr.Title_ar,dr.Description_tr,dr.Description_en,dr.Description_de,
                    dr.Description_fr,dr.Description_ru,dr.Description_ar,dr.PostImage,dr.Travel,dr.Active,dr.Createddate);
            }

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    TB_LatestNewsExt model = new TB_LatestNewsExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.UserID = Convert.ToInt32(dr["UserID"]);
                    model.Title_tr = dr["Title_tr"].ToString();
                    model.Title_en = dr["Title_en"].ToString();
                    model.Title_de = dr["Title_de"].ToString();
                    model.Title_fr = dr["Title_fr"].ToString();
                    model.Title_ru = dr["Title_ru"].ToString();
                    model.Title_ar = dr["Title_ar"].ToString();
                    model.Description_tr = dr["Description_tr"].ToString();
                    model.Description_en = dr["Description_en"].ToString();
                    model.Description_de = dr["Description_de"].ToString();
                    model.Description_fr = dr["Description_fr"].ToString();
                    model.Description_ru = dr["Description_ru"].ToString();
                    model.Description_ar = dr["Description_ar"].ToString();
                    model.PostImage = dr["PostImage"].ToString();
                    model.Travel = dr["Travel"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    model.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                   // model.OpUserID = Convert.ToInt64(dr["OpUserID"]);
                    list.Add(model);
                }
            }

            return list;
        }
        //public bool Create(TB_LatestNewsExt model, ref string Msg, Controller ctrl)
        //{
        //    bool status = true;

        //    TB_LatestNewsExt obj = new TB_LatestNewsExt();
        //    obj.ID = model.ID;
        //    obj.UserID = model.UserID;
        //    obj.Title_tr = model.Title_tr;
        //    obj.Title_en = model.Title_en;
        //    obj.Title_de = model.Title_de;
        //    obj.Title_fr = model.Title_fr;
        //    obj.Title_ru = model.Title_ru;
        //    obj.Title_ar = model.Title_ar;
        //    obj.Description_tr = model.Description_tr;
        //    obj.Description_en = model.Description_en;
        //    obj.Description_de = model.Description_de;
        //    obj.Description_fr = model.Description_fr;
        //    obj.Description_ru = model.Description_ru;
        //    obj.Description_ar = model.Description_ar;
        //    obj.PostImage = model.PostImage;
        //    obj.Travel = model.Travel;
        //    obj.Active = model.Active;
        //    obj.CreatedDate = DateTime.Now;
        //    //obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);

        //    db.TB_LatestNews.Add(obj);
        //    db.SaveChanges();

        //    int id = obj.ID;

        //    return status;
        //}

        public bool Delete(TB_LatestNewsExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_LatestNews.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_LatestNews.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(TB_LatestNewsExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_LatestNews.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.UserID = Convert.ToInt64(model.UserID);
            obj.Title_tr = model.Title_tr;
            obj.Title_en = model.Title_en;
            obj.Title_de = model.Title_de;
            obj.Title_fr = model.Title_fr;
            obj.Title_ru = model.Title_ru;
            obj.Title_ar = model.Title_ar;
            obj.Description_tr = model.Description_tr;
            obj.Description_en = model.Description_en;
            obj.Description_de = model.Description_de;
            obj.Description_fr = model.Description_fr;
            obj.Description_ru = model.Description_ru;
            obj.Description_ar = model.Description_ar;
            obj.PostImage = model.PostImage;
            obj.Travel = model.Travel;
            obj.Active = model.Active;
            obj.Createddate = model.CreatedDate;
            db.SaveChanges();

            return status;
        }


        public TB_LatestNewsExt GetLatestNewsByID(int LatestNewsID)
        {
          

            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_GetLatestNewsByID_TB_LatestNews_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@CultureCode", CultureCode);
            cmd.Parameters.AddWithValue("@LatestNewsID", LatestNewsID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();
            TB_LatestNewsExt LatestNewsObj = new TB_LatestNewsExt();
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                   
                    LatestNewsObj.ID = Convert.ToInt32(dr["ID"]);
                    LatestNewsObj.UserID = Convert.ToInt32(dr["UserID"]);
                    LatestNewsObj.Title_tr = dr["Title_tr"].ToString();
                    LatestNewsObj.Title_en = dr["Title_en"].ToString();
                    LatestNewsObj.Title_de = dr["Title_de"].ToString();
                    LatestNewsObj.Title_fr = dr["Title_fr"].ToString();
                    LatestNewsObj.Title_ru = dr["Title_ru"].ToString();
                    LatestNewsObj.Title_ar = dr["Title_ar"].ToString();
                    LatestNewsObj.Description_tr = dr["Description_tr"].ToString();
                    LatestNewsObj.Description_en = dr["Description_en"].ToString();
                    LatestNewsObj.Description_de = dr["Description_de"].ToString();
                    LatestNewsObj.Description_fr = dr["Description_fr"].ToString();
                    LatestNewsObj.Description_ru = dr["Description_ru"].ToString();
                    LatestNewsObj.Description_ar = dr["Description_ar"].ToString();
                    LatestNewsObj.PostImage = dr["PostImage"].ToString();
                    //LatestNewsObj.Travel = dr["Travel"].ToString();
                    LatestNewsObj.Active = Convert.ToBoolean(dr["Active"]);
                    LatestNewsObj.CreatedDate = Convert.ToDateTime(dr["CreatedDate"]);
                    //EmailObj.Operation = dr["Operation"].ToString();
                }
            }

            return LatestNewsObj;
        }



        public int InsertLatestNews(string UserID, string Title_tr, string Title_en, string Title_de, string Title_fr, string Title_ru, string Title_ar,
            string Description_tr, string Description_en, string Description_de, string Description_fr, string Description_ru, string Description_ar,
            string Active, string ContentfileName)
          {
            int status;

            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_InsertLatestNews_TB_LatestNews_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@Title_tr", Title_tr);
            cmd.Parameters.AddWithValue("@Title_en", Title_en);
            cmd.Parameters.AddWithValue("@Title_de", Title_de);
            cmd.Parameters.AddWithValue("@Title_fr", Title_fr);
            cmd.Parameters.AddWithValue("@Title_ru", Title_ru);
            cmd.Parameters.AddWithValue("@Title_ar", Title_ar);
            cmd.Parameters.AddWithValue("@Description_tr", Description_tr);
            cmd.Parameters.AddWithValue("@Description_en", Description_en);
            cmd.Parameters.AddWithValue("@Description_de", Description_de);
            cmd.Parameters.AddWithValue("@Description_fr", Description_fr);
            cmd.Parameters.AddWithValue("@Description_ru", Description_ru);
            cmd.Parameters.AddWithValue("@Description_ar", Description_ar);
            cmd.Parameters.AddWithValue("@Active", Active);
            cmd.Parameters.AddWithValue("@ImageName", ContentfileName);

            //status = Convert.ToInt32(cmd.ExecuteNonQuery());
            status = Convert.ToInt32(cmd.ExecuteNonQuery());
            SQLCon.Close();
            return status;

         }


        public int UpdateLatestNews(string ID, string UserID, string Title_tr, string Title_en, string Title_de, string Title_fr, string Title_ru, string Title_ar,
           string Description_tr, string Description_en, string Description_de, string Description_fr, string Description_ru, string Description_ar,
           string Active, string ContentfileName)
        {
            int status;

            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_UpdateLatestNews_TB_LatestNews_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@ID", ID);
            cmd.Parameters.AddWithValue("@UserID", UserID);
            cmd.Parameters.AddWithValue("@Title_tr", Title_tr);
            cmd.Parameters.AddWithValue("@Title_en", Title_en);
            cmd.Parameters.AddWithValue("@Title_de", Title_de);
            cmd.Parameters.AddWithValue("@Title_fr", Title_fr);
            cmd.Parameters.AddWithValue("@Title_ru", Title_ru);
            cmd.Parameters.AddWithValue("@Title_ar", Title_ar);
            cmd.Parameters.AddWithValue("@Description_tr", Description_tr);
            cmd.Parameters.AddWithValue("@Description_en", Description_en);
            cmd.Parameters.AddWithValue("@Description_de", Description_de);
            cmd.Parameters.AddWithValue("@Description_fr", Description_fr);
            cmd.Parameters.AddWithValue("@Description_ru", Description_ru);
            cmd.Parameters.AddWithValue("@Description_ar", Description_ar);
            cmd.Parameters.AddWithValue("@Active", Active);
            cmd.Parameters.AddWithValue("@ImageName", ContentfileName);

            //status = Convert.ToInt32(cmd.ExecuteNonQuery());
            status = Convert.ToInt32(cmd.ExecuteNonQuery());
            SQLCon.Close();
            return status;

        }
       
    }

    public class TB_LatestNewsExt
    {
       // //[Required(ErrorMessage = "This field is required!")]
       // [Range(1, int.MaxValue, ErrorMessage = "This field must be numeric!")]
       // //public int ID { get; set; }
       // //[Required(ErrorMessage = "This field is required!")]
       // [StringLength(3, ErrorMessage = "This field cannot be longer than 3 characters.")]
       // public string Code { get; set; }
       //// [Required(ErrorMessage = "This field is required!")]
       // public string SystemCode { get; set; }
       // //[Required(ErrorMessage = "This field is required!")]
       // public string Description { get; set; }
       // public bool Active { get; set; }
       // public DateTime OpDateTime { get; set; }
       // public long OpUserID { get; set; }

        public int ID { get; set; }
        public int UserID { get; set; }
        public string Title_tr { get; set; }
        public string Title_en { get; set; }
        public string Title_de { get; set; }
        public string Title_fr { get; set; }
        public string Title_ru { get; set; }
        public string Title_ar { get; set; }
        public string Description_tr { get; set; }
        public string Description_en { get; set; }
        public string Description_de { get; set; }
        public string Description_fr { get; set; }
        public string Description_ru { get; set; }
        public string Description_ar { get; set; }
        public string PostImage { get; set; }
        public string Travel { get; set; }
        public bool Active { get; set; }
        public string Upload { get; set; }
        public DateTime CreatedDate { get; set; }

    }
}