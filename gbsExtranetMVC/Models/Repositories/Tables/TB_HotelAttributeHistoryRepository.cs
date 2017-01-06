using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelAttributeHistoryRepository : BaseRepository
    {

        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelAttributeHistoryExt> ReadAll(int TableID)
        {
            List<TB_HotelAttributeHistoryExt> list = new List<TB_HotelAttributeHistoryExt>();

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
                    TB_HotelAttributeHistoryExt model = new TB_HotelAttributeHistoryExt();
                    model.ID = Convert.ToInt32(dr["ID"]);
                    model.HotelAttributeID = Convert.ToInt32(dr["HotelAttributeID"].ToString());
                    model.Hotel = dr["FK_HotelID"].ToString();
                    model.Attribute = dr["FK_AttributeID"].ToString();
                    model.Charged = Convert.ToBoolean(dr["Charged"].ToString());
                    model.Unit = dr["FK_UnitID"].ToString();
                    model.UnitValue = dr["UnitValue"].ToString();
                    model.Charge = dr["Charge"].ToString();
                    model.Currency = dr["FK_CurrencyID"].ToString();
                    model.StartDate = dr["StartDate"].ToString();
                    model.EndDate = dr["EndDate"].ToString();
                    model.Active = Convert.ToBoolean(dr["Active"].ToString());
                    model.LogDate = dr["LogDateTime"].ToString();
                    model.LogUser = dr["FK_LogUserID"].ToString();
                    list.Add(model);
                }
            }

            return list;
        }

    }

    public class TB_HotelAttributeHistoryExt
    {
        public int ID { get; set; }
        public int HotelAttributeID { get; set; }
        public string Hotel { get; set; }
        public string Attribute { get; set; }
        public bool Charged { get; set; }
        public string Unit { get; set; }
        public string UnitValue { get; set; } 
        public string Charge { get; set; }
        public string Currency { get; set; }      
        public string StartDate { get; set; }
        public string EndDate { get; set; }     
        public bool Active { get; set; }
        public string LogDate { get; set; }
        public string LogUser { get; set; }
    }
}