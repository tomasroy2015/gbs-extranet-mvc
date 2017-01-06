using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelRoomAttributeHistoryRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelRoomAttributeHistoryExt> ReadAll(int TableID)
        {
            List<TB_HotelRoomAttributeHistoryExt> list = new List<TB_HotelRoomAttributeHistoryExt>();

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
                    TB_HotelRoomAttributeHistoryExt PageObj = new TB_HotelRoomAttributeHistoryExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.HotelRoomAttributeID = Convert.ToInt32(dr["HotelRoomAttributeID"].ToString());
                    PageObj.HotelRoomID = Convert.ToInt32(dr["FK_HotelRoomID_ID"].ToString());
                    PageObj.Attribute = dr["FK_AttributeID_ID"].ToString();
                    PageObj.Charged = Convert.ToInt32(dr["Charged"]);
                    PageObj.Unitvalue = dr["UnitValue"].ToString();
                    PageObj.Charge = dr["Charge"].ToString();
                    PageObj.CurrencyID = dr["CurrencyID"].ToString();
                    PageObj.LogDateTime = dr["LogDateTime"].ToString();
                    PageObj.LogUser = dr["FK_LogUserID_ID"].ToString();
                    list.Add(PageObj);
                }
            }


            return list;
        }

      
    }
    public class TB_HotelRoomAttributeHistoryExt
    {
        public int ID { get; set; }
        public int HotelRoomID { get; set; }
        public int Charged { get; set; }
        public string Unitvalue { get; set; }
        public string Attribute { get; set; }
        public string Charge { get; set; }
        public string CurrencyID { get; set; }
        public int HotelRoomAttributeID { get; set; }
        public string LogDateTime { get; set; }
        public string LogUser { get; set; }
        

       
    }
}