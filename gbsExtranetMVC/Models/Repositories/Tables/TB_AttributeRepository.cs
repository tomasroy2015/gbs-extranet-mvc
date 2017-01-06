using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories 
{
    public class TB_AttributeRepository : BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_AttributeExt> ReadAll(int TableID)
        {
            List<TB_AttributeExt> list = new List<TB_AttributeExt>();
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
                    TB_AttributeExt model = new TB_AttributeExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.Part = dr["FK_PartID_ID"].ToString();
                    model.PartID =dr["PartID"].ToString();
                    model.AttributeType = dr["FK_AttributeTypeID_ID"].ToString();
                    model.AttributeTypeID = dr["AttributeTypeID"].ToString();
                    model.AttributeCategory = dr["FK_AttributeHeaderID_ID"].ToString();
                    model.AttributeHeaderID = dr["AttributeHeaderID"].ToString();
                    model.Name = dr["Name"].ToString();
                    model.Description = dr["Description"].ToString();
                    //model.DataType= dr["DataTypeID"].ToString();
                    model.DataTypeID = (Nullable<int>)CheckEmptyStringDBParameter(dr["DataTypeID"].ToString(), true); ;
                    model.Unit = dr["FK_UnitID_ID"].ToString();
                    model.UnitID = dr["UnitID"].ToString();
                    model.MinValue = dr["MinValue"].ToString();
                    model.MaxValue = dr["MaxValue"].ToString();
                    //model.Chargeable = dr["Chargeable"].ToString();
                    model.Chargeable = (Nullable<bool>)CheckEmptyStringDBParameter(dr["Chargeable"].ToString(), false,false,false,false,true);
                    model.Sorts = dr["Sort"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"]);
                    model.RelatedAttribute = dr["FK_RelatedAttributeID_ID"].ToString();
                    model.RelatedAttributeID = dr["RelatedAttributeID"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }

        public bool Create(TB_AttributeExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            TB_Attribute obj = new TB_Attribute();
           // obj.ID = model.ID;
            obj.PartID = Convert.ToInt32(model.PartID);
            obj.AttributeTypeID = Convert.ToInt32(model.AttributeTypeID);
            if (model.AttributeHeaderID != "0" && model.AttributeHeaderID != null)
            {
                obj.AttributeHeaderID = Convert.ToInt32(model.AttributeHeaderID);
            }
            else
            {
                obj.AttributeHeaderID = null;
            }
            obj.Name_en = model.Name;
            obj.Description_en = model.Description;
            obj.DataTypeID = model.DataTypeID;
            //if (model.DataType != "0" && model.DataType != null)
            //{
            //    obj.DataTypeID = Convert.ToInt32(model.DataType);
            //}
            //else
            //{
            //    obj.DataTypeID = null;
            //}
            if (model.UnitID != "0" && model.UnitID != null)
            {
                obj.UnitID = Convert.ToInt32(model.UnitID);
            }
            else
            {
                obj.UnitID = null;
            }
            
            
            obj.MinValue = model.MinValue;
            obj.MaxValue = model.MaxValue;
            obj.Chargeable =Convert.ToBoolean( model.Chargeable);
            obj.Sort = Convert.ToInt16(model.Sorts);
            obj.Active = Convert.ToBoolean(model.Active);
            if (model.RelatedAttributeID != "0" && model.RelatedAttributeID != null)
            {
                obj.RelatedAttributeID = Convert.ToInt32(model.RelatedAttributeID);
            }
            else
            {
                obj.RelatedAttributeID = null;
            }
            
            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);

            db.TB_Attribute.Add(obj);
            db.SaveChanges();

            int id = Convert.ToInt32(obj.ID);

            return status;
        }

        public bool Delete(TB_AttributeExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_Attribute.Where(x => x.ID == model.ID).FirstOrDefault();
            db.TB_Attribute.Remove(obj);
            db.SaveChanges();

            return status;
        }

        public bool Update(TB_AttributeExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;

            var obj = db.TB_Attribute.Where(x => x.ID == model.ID).FirstOrDefault();
            obj.ID = model.ID;
            obj.PartID = Convert.ToInt32(model.PartID);
            obj.AttributeTypeID = Convert.ToInt32(model.AttributeTypeID);
            if (model.AttributeHeaderID != "0" && model.AttributeHeaderID != null)
            {
                obj.AttributeHeaderID = Convert.ToInt32(model.AttributeHeaderID);
            }
            else
            {
                obj.AttributeHeaderID = null;
            }
            obj.Name_en = model.Name;
            obj.Description_en = model.Description;
            obj.DataTypeID = model.DataTypeID;
            //if (model.DataType != "0" && model.DataType != null)
            //{
            //    obj.DataTypeID = Convert.ToInt32(model.DataType);
            //}
            //else
            //{
            //    obj.DataTypeID = null;
            //}
            if (model.UnitID != "0" && model.UnitID != null)
            {
                obj.UnitID = Convert.ToInt32(model.UnitID);
            }
            else
            {
                obj.UnitID = null;
            }

            obj.MinValue = model.MinValue;
            obj.MaxValue = model.MaxValue;
            obj.Chargeable = Convert.ToBoolean(model.Chargeable);
            obj.Sort = Convert.ToInt16(model.Sorts);
            obj.Active = Convert.ToBoolean(model.Active);
            if (model.RelatedAttributeID != "0" && model.RelatedAttributeID != null)
            {
                obj.RelatedAttributeID = Convert.ToInt32(model.RelatedAttributeID);
            }
            else
            {
                obj.RelatedAttributeID = null;
            }

            obj.OpDateTime = DateTime.Now;
            obj.OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            db.SaveChanges();

            return status;
        }

        public static object CheckEmptyStringDBParameter(object Value, bool ReturnInteger = false, bool ReturnDate = false, bool ReturnDouble = false, bool ReturnDecimal = false, bool ReturnBoolean = false, bool ReturnLong = false)
        {

            if (Value == string.Empty)
            {
                return null;
            }

            if (ReturnInteger)
            {
                return Convert.ToInt32(Value);
            }

            //if (ReturnDate)
            //{

            //    return DateTime.ParseExact(Value, "yyyy-MM-dd", CultureInfo.InvariantCulture);

            //}


            if (ReturnDouble)
            {
                return Convert.ToDouble(Value);
            }

            if (ReturnDecimal)
            {
                return Convert.ToDecimal(Value);
            }

            if (ReturnBoolean)
            {
                return Convert.ToBoolean(Value);
            }

            if (ReturnLong)
            {
                return Convert.ToInt64(Value);
            }

            return Value;

        }


    }

    public class TB_AttributeExt
    {
        public int ID { get; set; }
        public string Part { get; set; }
        public string PartID { get; set; }
        public string AttributeType { get; set; }
        public string AttributeTypeID { get; set; }
        public string AttributeCategory { get; set; }
        public string AttributeHeaderID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string DataType { get; set; }
        public Nullable<int> DataTypeID { get; set; }
        public string Unit { get; set; }
        public string UnitID { get; set; }
        public string MinValue { get; set; }
        public string MaxValue { get; set; }
        public Nullable<bool> Chargeable { get; set; }
        public string Sorts { get; set; }
        public int SortID { get; set; }
        public bool Active { get; set; }
        public string RelatedAttribute { get; set; }
        public string RelatedAttributeID { get; set; }
        public DateTime OpDateTime { get; set; }
        public long OpUserID { get; set; }
    }
}