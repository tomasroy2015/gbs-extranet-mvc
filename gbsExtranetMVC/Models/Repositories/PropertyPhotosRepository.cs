using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class PropertyPhotosExt
    {
        public int RoomID { get; set; }
        public int ID { get; set; }
        public int PartID { get; set; }
        public int RecordID { get; set; }
        public int RoomTypeID { get; set; }
        public string RoomTypeName { get; set; }
        public string Name { get; set; }
        public bool MainPhoto { get; set; }
        public string Path { get; set; }
       

        public List<PropertyPhotosExt> AllPhotos { get; set; }
    }
    public class PropertyPhotosRepository : BaseRepository
    {
        public  string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<PropertyPhotosExt> GetHotelRooms(int HotelID)
        {
              DataTable dt = new DataTable();
              SQLCon.Open();
              SqlCommand cmd = new SqlCommand("[TB_SP_GetHotelRooms]", SQLCon);
              cmd.CommandType = CommandType.StoredProcedure;
              cmd.Parameters.AddWithValue("@Culture", CultureValue);
              cmd.Parameters.AddWithValue("@OrderBy", "RoomTypeName");
              cmd.Parameters.AddWithValue("@HotelID", HotelID);
              cmd.Parameters.AddWithValue("@Active", true);
            
              SqlDataAdapter sda = new SqlDataAdapter(cmd);
              sda.Fill(dt);
              SQLCon.Close();

              List<PropertyPhotosExt> ListOfModel = new List<PropertyPhotosExt>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyPhotosExt HotelObj = new PropertyPhotosExt();
                    HotelObj.RoomID = Convert.ToInt32(dr["ID"]);
                    HotelObj.RoomTypeID = Convert.ToInt32(dr["RoomTypeID"]);
                    HotelObj.RoomTypeName = dr["RoomTypeName"].ToString();
                    ListOfModel.Add(HotelObj);
                }
            }
            return ListOfModel;
        }
        public List<PropertyPhotosExt> LoadPhoto(int PartID, int HotelID)
        {
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetPhotos_TB_Photo_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@PartID", PartID);
            cmd.Parameters.AddWithValue("@RecordID", HotelID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();

            List<PropertyPhotosExt> ListOfModel = new List<PropertyPhotosExt>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    PropertyPhotosExt HotelObj = new PropertyPhotosExt();
                    HotelObj.ID = Convert.ToInt32(dr["ID"]);
                    HotelObj.PartID = Convert.ToInt32(dr["PartID"]);
                    HotelObj.RecordID = Convert.ToInt32(dr["RecordID"]);
                    HotelObj.Name = dr["Name"].ToString();
                    if (dr["MainPhoto"].ToString() != "")
                    {
                        HotelObj.MainPhoto = Convert.ToBoolean(dr["MainPhoto"]);
                    }
                    ListOfModel.Add(HotelObj);
                }
            }
            PropertyPhotosExt HotelObjAll = new PropertyPhotosExt();
            HotelObjAll.AllPhotos = ListOfModel;
            return ListOfModel;
        }

        public string GetParameterValue(string Parameter)
        {
            string Status = "";
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetParameter_BizTbl_Parameter_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Code", Parameter);
            Status = cmd.ExecuteScalar().ToString();
            SQLCon.Close();
            return Status;
        }

        public int MainPhoto(string ID, string RecordID, string PartID,Controller Ctrl)
        {         
            DBEntities obj = new DBEntities();
            var OpUserIDParameter = new SqlParameter("@OpUserID", Convert.ToInt64(Ctrl.Session["UserID"]));
            var IDParameter = new SqlParameter("@ID",Convert.ToInt32(ID));
            var RecordIDParameter = new SqlParameter("@RecordID",Convert.ToInt64(RecordID));
            var PartIDParameter = new SqlParameter("@PartID", Convert.ToInt64(PartID));
            int i = obj.Database.ExecuteSqlCommand("B_Ex_SetAsMainPhoto_TB_Photo_SP @PartID,@RecordID,@ID,@OpUserID", PartIDParameter, RecordIDParameter, IDParameter, OpUserIDParameter);
            return i;
        }

        public string DeletePhotos(string PhotoID, Controller Ctrl)
        {
            DBEntities obj = new DBEntities();
            var PhotoIDParameter = new SqlParameter("@PhotoID", Convert.ToInt32(PhotoID));        
            var OpUserIDParameter = new SqlParameter("@OpUserID", Convert.ToInt64(Ctrl.Session["UserID"]));
            int i = obj.Database.ExecuteSqlCommand("B_Ex_DeletePhotos_TB_Photo_SP @PhotoID,@OpUserID", PhotoIDParameter, OpUserIDParameter);
            return Convert.ToString(i);
        }

        //public string UploadImage(string HotelID, string Hotelname, Controller Ctrl)
        //{
        //    DBEntities obj = new DBEntities();
        //    var IDParameter = new SqlParameter("@PhotoID", Convert.ToInt32(HotelID));
        //    var NameParameter = new SqlParameter("@PhotoID", Hotelname);
        //    var OpUserIDParameter = new SqlParameter("@OpUserID", Convert.ToInt64(Ctrl.Session["UserID"]));
        //    int i = obj.Database.ExecuteSqlCommand("B_Ex_DeletePhotos_TB_Photo_SP @PhotoID,@OpUserID", IDParameter,NameParameter,OpUserIDParameter);
        //    return Convert.ToString(i);
        //}
        public string UpdateSort(string PhID, string PhValue, Controller Ctrl)
        {                   
            SQLCon.Open();

            SqlCommand cmd = new SqlCommand("B_Ex_UpdateSort_TB_Photo_SP", SQLCon);

            cmd.CommandType = CommandType.StoredProcedure;

            cmd.Parameters.AddWithValue("@PhotoID", Convert.ToInt64(PhID));

            cmd.Parameters.AddWithValue("@Sort", Convert.ToInt32(PhValue));

            cmd.Parameters.AddWithValue("@OpUserID", Convert.ToInt64(Ctrl.Session["UserID"]));

            string var = Convert.ToString(cmd.ExecuteScalar());

            SQLCon.Close();
          
            return var;
                   
        }
        public string AddImage(int part, string FName, int id, Controller Ctrl)
        {
            string status = "Success";
            DBEntities insertentity = new DBEntities();
            TB_Photo Obj = new TB_Photo();
            Obj.PartID = part;
            Obj.RecordID = Convert.ToInt64(id);
            Obj.Name = FName;
            // Obj.MainPhoto = Convert.ToBoolean(0);
            Obj.Active = true;
            Obj.CreateDateTime = DateTime.Now;
            Obj.CreateUserID = Convert.ToInt64(Ctrl.Session["UserID"]);
            Obj.OpDateTime = DateTime.Now;
            Obj.OpUserID = Convert.ToInt64(Ctrl.Session["UserID"]);
            insertentity.TB_Photo.Add(Obj);
            insertentity.SaveChanges();
            int ID = Convert.ToInt32(Obj.ID);


            //SQLCon.Open();

            //SqlCommand cmd = new SqlCommand("B_Ex_InsertHotelPhotos_TB_Photo_SP", SQLCon);

            //cmd.CommandType = CommandType.StoredProcedure;

            //cmd.Parameters.AddWithValue("@PartID", Convert.ToInt32(part));
            //cmd.Parameters.AddWithValue("@RecordID", Convert.ToInt64(id));
            //cmd.Parameters.AddWithValue("@Name",Convert.ToString(FName));
            //cmd.Parameters.AddWithValue("@MainPhoto", false);
            //cmd.Parameters.AddWithValue("@Active", true);
            //cmd.Parameters.AddWithValue("@Sort", 0);
            //cmd.Parameters.AddWithValue("@OpUserID", Convert.ToInt64(Ctrl.Session["UserID"]));

            //int var = Convert.ToInt32(cmd.ExecuteNonQuery());

            //SQLCon.Close();
            return status;
        }

    }
}