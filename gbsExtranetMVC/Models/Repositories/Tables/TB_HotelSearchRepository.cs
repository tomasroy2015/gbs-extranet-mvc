using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelSearchRepository:BaseRepository
    {
        public static string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        public List<TB_HotelSearchExt> ReadAll(int TableID)
        {
            List<TB_HotelSearchExt> list = new List<TB_HotelSearchExt>();

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
                    TB_HotelSearchExt PageObj = new TB_HotelSearchExt();
                    PageObj.ID = Convert.ToInt32(dr["ID"]);
                    PageObj.SearchParameterID = Convert.ToInt32(dr["SearchParameterID"].ToString());
                    PageObj.HotelID = Convert.ToInt32(dr["HotelID"].ToString());
                    PageObj.Hotel = dr["Hotel"].ToString();
                    PageObj.HotelRoomID = Convert.ToInt32(dr["HotelRoomID"].ToString());
                    PageObj.MaxChildrenCount = Convert.ToInt32(dr["MaxChildrenCount"].ToString());
                    PageObj.MaxPeopleCount = Convert.ToInt32(dr["MaxPeopleCount"].ToString());
                    PageObj.ChildrenAllowed = Convert.ToBoolean(dr["ChildrenAllowed"].ToString());
                    PageObj.MinumumRoomRate = dr["MinumumRoomRate"].ToString();
                    PageObj.TotalRoomRate = dr["TotalRoomRate"].ToString();
                    PageObj.TotalRoomRateHistory = dr["TotalRoomRateHistory"].ToString();
                    PageObj.AvailableRoomCount = dr["AvailableRoomCount"].ToString();
                    PageObj.AllocatedRoomCount = dr["AllocatedRoomCount"].ToString();
                    PageObj.Date = dr["Date"].ToString();

                    list.Add(PageObj);
                }
            }


            return list;
        }


    }
    public class TB_HotelSearchExt
    {
        public int ID { get; set; }
       
        public string Date { get; set; }

        public int SearchParameterID { get; set; }

        public int HotelID { get; set; }

        public string Hotel { get; set; }

        public int HotelRoomID { get; set; }

        public int MaxChildrenCount { get; set; }

        public int MaxPeopleCount { get; set; }

        public bool ChildrenAllowed { get; set; }

        public string MinumumRoomRate { get; set; }

        public string TotalRoomRate { get; set; }

        public string TotalRoomRateHistory { get; set; }

        public string AvailableRoomCount { get; set; }

        public string AllocatedRoomCount { get; set; }
    }

}