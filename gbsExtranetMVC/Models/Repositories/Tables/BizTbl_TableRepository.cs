using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class BizTbl_TableRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<BizTbl_TableExt> ReadAll(int TableID)
        {
            List<BizTbl_TableExt> list = new List<BizTbl_TableExt>();
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
                    BizTbl_TableExt model = new BizTbl_TableExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.Name = dr["Name"].ToString();
                    model.Description = dr["Description"].ToString();
                    model.LogStatus = Convert.ToBoolean(dr["Logged"]);
                    model.EditableStatus = Convert.ToBoolean(dr["Editable"]);
                    model.DescriptiveFields = dr["DescriptiveFields"].ToString();
                    model.ViewableFields = dr["ViewableFields"].ToString();
                    model.OrderFields = dr["OrderFields"].ToString();
                    model.FilterFields = dr["FilterFields"].ToString();
                    model.FilterExpression = dr["FilterExpression"].ToString();
                    model.PagingSize =  dr["PagingSize"].ToString();
                    model.NewRecordVisible = dr["NewRecordVisible"].ToString();
                    //model.OpDateTime = Convert.ToDateTime(dr["OpDateTime"]);
                   // model.OpUserID = Convert.ToInt64(dr["OpUserID"]);
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(BizTbl_TableExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            BizTbl_Table obj = new BizTbl_Table();
            obj.Name = model.Name;
            obj.Description_en = model.Description;
            obj.Logged = model.LogStatus;
            obj.Editable = model.EditableStatus;
            obj.DescriptiveFields = model.DescriptiveFields;
            obj.ViewableFields = model.ViewableFields;
            obj.OrderFields = model.OrderFields;
            obj.FilterFields = model.FilterFields;
            obj.OrderFields = model.OrderFields;
            obj.FilterExpression = model.FilterExpression;
            obj.PagingSize = Convert.ToInt16(model.PagingSize);
            obj.NewRecordVisible = Convert.ToBoolean(model.NewRecordVisible);
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);

            db.BizTbl_Table.Add(obj);
            db.SaveChanges();

            int id = obj.ID;

            return status;
        }

        public bool Delete(BizTbl_TableExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.BizTbl_Table.Where(x => x.ID == model.ID).FirstOrDefault();
            db.BizTbl_Table.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(BizTbl_TableExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.BizTbl_Table.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.Name = model.Name;
            obj.Description_en = model.Description;
            obj.Logged = model.LogStatus;
            obj.Editable = model.EditableStatus;
            obj.DescriptiveFields = model.DescriptiveFields;
            obj.ViewableFields = model.ViewableFields;
            obj.OrderFields = model.OrderFields;
            obj.FilterFields = model.FilterFields;
            obj.OrderFields = model.OrderFields;
            obj.FilterExpression = model.FilterExpression;
            obj.PagingSize = Convert.ToInt16 (model.PagingSize);
            obj.NewRecordVisible =Convert.ToBoolean( model.NewRecordVisible);
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = 0;
            db.SaveChanges();

            return status;
        }
    }
    public class BizTbl_TableExt
    {
        public int ID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Name { get; set; }
        public string Description { get; set; }
        public bool LogStatus { get; set; }
        public bool EditableStatus { get; set; }
        public string DescriptiveFields { get; set; }
        public string ViewableFields { get; set; }
        public string OrderFields { get; set; }
        public string FilterFields { get; set; }
        public string FilterExpression { get; set; }
        public string PagingSize { get; set; }
        public string NewRecordVisible { get; set; }
        public DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
    }
}