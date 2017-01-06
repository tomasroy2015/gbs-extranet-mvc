using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class BizTbl_TableColumnRepository : BaseRepository
    {
        public string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<BizTbl_TableColumnExt> ReadAll(int TableID)
        {
            List<BizTbl_TableColumnExt> list = new List<BizTbl_TableColumnExt>();
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
                    BizTbl_TableColumnExt model = new BizTbl_TableColumnExt();
                    model.ID = Convert.ToInt16(dr["ID"]);
                    model.TableID= Convert.ToInt32(dr["TableID"]);
                    model.Table = dr["FK_TableID_ID"].ToString();
                    model.Name = dr["Name"].ToString();
                    model.Caption = dr["Caption"].ToString();
                    model.Description = dr["Description"].ToString();
                    model.MultilingualStatus = dr["Multilingual"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(BizTbl_TableColumnExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            BizTbl_TableColumn obj = new BizTbl_TableColumn();
            obj.Name = model.Name;
            obj.TableID = model.TableID;
            obj.Caption_en = model.Caption;
            obj.Description = model.Description;
            obj.Multilingual = Convert.ToBoolean( model.MultilingualStatus);

            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);

            db.BizTbl_TableColumn.Add(obj);
            db.SaveChanges();

            int id = obj.ID;

            return status;
        }

        public bool Delete(BizTbl_TableColumnExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.BizTbl_TableColumn.Where(x => x.ID == model.ID).FirstOrDefault();
            db.BizTbl_TableColumn.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(BizTbl_TableColumnExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            var DepObj = db.BizTbl_TableColumn.Where(x => x.ID == model.ID).FirstOrDefault();
            DepObj.Name = model.Name;
            DepObj.TableID = model.TableID;
            DepObj.Caption_en = model.Caption;
            DepObj.Description = model.Description;
            DepObj.Multilingual = Convert.ToBoolean(model.MultilingualStatus);
            DepObj.OpDateTime = DateTime.Now;
            DepObj.OpUserID = 0;
            db.SaveChanges();
            return status;
        }


        public List<HitcountExt> GetTableValue()
        {

            DBEntities entity = new DBEntities();
            DataTable dt = new DataTable();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            dt.Columns.Add("ID");
            dt.Columns.Add("Tables");
            List<HitcountExt> ListOfModel = new List<HitcountExt>();
            var result = entity.Database.SqlQuery<GetTablesdropdown_Result>("B_Ex_GetTables_BizTbl_Table_SP @CultureCode", Culture).ToList();


            foreach (GetTablesdropdown_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Tables);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    HitcountExt HitObj = new HitcountExt();
                    HitObj.ID = Convert.ToInt32(dr["ID"]);
                    HitObj.Name = dr["Tables"].ToString();
                    ListOfModel.Add(HitObj);
                }
            }
            return ListOfModel;
        }
    }
    public class BizTbl_TableColumnExt
    {
        public long ID { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public int TableID { get; set; }
        public string Table { get; set; }
        [Required(ErrorMessage = "This field is required!")]
        public string Name { get; set; }
        public string Caption { get; set; }
        public string Description { get; set; }
        public string MultilingualStatus { get; set; }
        public DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
    }
}