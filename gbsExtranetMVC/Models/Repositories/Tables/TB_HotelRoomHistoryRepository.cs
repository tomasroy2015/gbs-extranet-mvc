using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Models.Repositories
{
    public class TB_HotelRoomHistoryRepository : BaseRepository
    {
      
            public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            public List<TB_HotelRoomHistoryExt>ReadAll(int TableID)
            {
                List<TB_HotelRoomHistoryExt> list = new List<TB_HotelRoomHistoryExt>();

                DataTable dt = new DataTable();
                SQLCon.Open();
                SqlCommand cmd = new SqlCommand("B_DisplayTableNew_BizTbl_Table_Sp", SQLCon);
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
                        TB_HotelRoomHistoryExt PageObj = new TB_HotelRoomHistoryExt();
                        PageObj.ID = Convert.ToInt32(dr["ID"]);
                        PageObj.HotelRoomID = Convert.ToInt32(dr["HotelRoomID"]);
                        PageObj.Hotel = dr["FK_HotelID_ID"].ToString();
                        PageObj.Description = dr["Description"].ToString();
                        PageObj.RoomType = dr["FK_RoomTypeID_ID"].ToString();
                        PageObj.RoomCount = Convert.ToInt32(dr["RoomCount"].ToString());
                        PageObj.RoomSize = Convert.ToInt32(dr["RoomSize"].ToString());
                        PageObj.MaxPeopleCount = Convert.ToInt32(dr["MaxPeopleCount"].ToString());
                        PageObj.MaxChildrenCount = Convert.ToInt32(dr["MaxChildrenCount"].ToString());
                        PageObj.BabyCotCount = Convert.ToInt32(dr["BabyCotCount"].ToString());
                        PageObj.ExtraBedCount = Convert.ToInt32(dr["ExtraBedCount"].ToString());
                        PageObj.SmokingType = dr["FK_SmokingTypeID_ID"].ToString();
                        PageObj.ViewType = dr["FK_ViewTypeID_ID"].ToString();
                        PageObj.Sorts = dr["Sort"].ToString();
                        PageObj.Active = Convert.ToBoolean(dr["Active"].ToString());
                        PageObj.CreateDateTime = dr["CreateDateTime"].ToString();
                        PageObj.CreateUserID =  Convert.ToInt32(dr["CreateUserID"].ToString());
                        PageObj.LogDateTime = dr["LogDateTime"].ToString();
                        PageObj.LogUser = dr["FK_LogUserID_ID"].ToString();
                        list.Add(PageObj);
                    }
                }


                return list;
            }


        }
        public class TB_HotelRoomHistoryExt
        {
            public int ID { get; set; }
            public string LogDateTime { get; set; }
            public string LogUser { get; set; }
            public string Description { get; set; }
            public string Hotel { get; set; }
            public string RoomType { get; set; }
            public int RoomCount { get; set; }
            public int RoomSize { get; set; }
            public int MaxPeopleCount { get; set; }
            public int MaxChildrenCount { get; set; }
            public int BabyCotCount { get; set; }
            public int ExtraBedCount { get; set; }
            public string SmokingType { get; set; }
            public string ViewType { get; set; }
            public string Sorts { get; set; }
            public bool Active { get; set; }
            public string CreateDateTime { get; set; }
            public int CreateUserID { get; set; }
            public int HotelRoomID { get; set; }
        }
    
}