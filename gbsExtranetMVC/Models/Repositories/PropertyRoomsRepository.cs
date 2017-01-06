using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{
    public class PropertyRoomsRepository : BaseRepository
    {
        public  string CultureValue = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

        DataTable dt = new DataTable();


        public List<PropertyRoomsExt> GetPropertyRooms(int HotelID)
        {        
           // long HotelID = 100003;
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelRooms", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureValue);
            cmd.Parameters.AddWithValue("@OrderBy", "ID");
            cmd.Parameters.AddWithValue("@HotelID",HotelID);
            cmd.Parameters.AddWithValue("@Active", true);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();

            List<PropertyRoomsExt> ListOfModel = new List<PropertyRoomsExt>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    //if (!ArrayID.Contains(Convert.ToInt32(dr["ID"])))
                    //{
                    PropertyRoomsExt HotelRoom = new PropertyRoomsExt();

                    HotelRoom.ID = Convert.ToInt32(dr["ID"]);
                    HotelRoom.HotelID = Convert.ToInt64(dr["HotelID"]);
                    HotelRoom.RoomTypeID = Convert.ToInt32(dr["RoomTypeID"]);
                    HotelRoom.RoomCount = dr["RoomCount"].ToString();

                    HotelRoom.RoomTypeName = dr["RoomTypeName"].ToString();
                    HotelRoom.RoomSize = dr["RoomSize"].ToString();
                    HotelRoom.MaxPeopleCount = dr["MaxPeopleCount"].ToString();

                    HotelRoom.IDWithMaxPeopleCount = dr["IDWithMaxPeopleCount"].ToString();
                    HotelRoom.MaxChildrenCount = dr["MaxChildrenCount"].ToString();
                    HotelRoom.BabyCotCount = dr["BabyCotCount"].ToString();

                    HotelRoom.ExtraBedCount = dr["ExtraBedCount"].ToString();
                    HotelRoom.SmokingTypeID = dr["SmokingTypeID"].ToString();
                    HotelRoom.SmokingTypeName = dr["SmokingTypeName"].ToString();

                    HotelRoom.ViewTypeID = dr["ViewTypeID"].ToString();
                    HotelRoom.ViewTypeName = dr["ViewTypeName"].ToString();
                    HotelRoom.IncludedInRoomTypeCaption = dr["IncludedInRoomTypeCaption"].ToString();

                    HotelRoom.Active = Convert.ToBoolean(dr["Active"]);
                    HotelRoom.CreateDateTime = dr["CreateDateTime"].ToString();
                    HotelRoom.CreateUserID = dr["CreateUserID"].ToString();

                    HotelRoom.OpDateTime = dr["OpDateTime"].ToString();
                    HotelRoom.OpUserID = dr["OpUserID"].ToString();


                    Encryption64 objEncryptreservation = new Encryption64();
                    string EncryptRoomID = dr["ID"].ToString();
                    EncryptRoomID = System.Web.HttpContext.Current.Server.UrlEncode(ConvertStringToHex(objEncryptreservation.Encrypt(EncryptRoomID, "58421043")));
                    HotelRoom.EncryptRoomID = EncryptRoomID;

                    ListOfModel.Add(HotelRoom);
                    //}

                }
            }
            return ListOfModel;
        }

        public int DeleteHotelRoom(int HotelRoomID,Controller ctrl)
        {
            int status = 0;
            Int64 OpUserID = Convert.ToInt64(ctrl.Session["UserID"]);
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_DeleteHotelRoom", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@HotelRoomID", HotelRoomID);
            cmd.Parameters.AddWithValue("@OpUserID", OpUserID);

            status = Convert.ToInt32(cmd.ExecuteNonQuery());
       
            return status;

        }

        public string ConvertStringToHex(string asciiString)
        {
            string hex = "";
            foreach (char c in asciiString)
            {
                int tmp = c;
                hex += String.Format("{0:x2}", (uint)System.Convert.ToUInt32(tmp.ToString()));
            }
            return hex;
        }

        public class Encryption64
        {

            private byte[] key = { };
            //private byte[] IV = { 10, 20, 30, 40, 50, 60, 70, 80 };

            private byte[] IV = { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };

          



            public string Encrypt(string stringToEncrypt, string sEncryptionKey)
            {
                try
                {
                    key = Encoding.UTF8.GetBytes(sEncryptionKey.Substring(0, 8));
                    DESCryptoServiceProvider des = new DESCryptoServiceProvider();
                    Byte[] inputByteArray = Encoding.UTF8.GetBytes(stringToEncrypt);
                    MemoryStream ms = new MemoryStream();
                    CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(key, IV),
                                                                      CryptoStreamMode.Write);
                    cs.Write(inputByteArray, 0, inputByteArray.Length);
                    cs.FlushFinalBlock();
                    return Convert.ToBase64String(ms.ToArray());
                }
                catch (Exception ex)
                {
                    return ex.Message;
                }
            }
        }       

    }
    public class PropertyRoomsExt
    {
        public Int64 ID { get; set; }

        public Int64 HotelID { get; set; }

        public int RoomTypeID { get; set; }

        public string RoomCount { get; set; }

        public string RoomTypeName { get; set; }

        public string RoomSize { get; set; }

        public string MaxPeopleCount { get; set; }

        public string IDWithMaxPeopleCount { get; set; }

        public string MaxChildrenCount { get; set; }

        public string BabyCotCount { get; set; }

        public string ExtraBedCount { get; set; }

        public string SmokingTypeID { get; set; }

        public string SmokingTypeName { get; set; }

        public string ViewTypeID { get; set; }

        public string ViewTypeName { get; set; }

        public string IncludedInRoomTypeCaption { get; set; }

        public bool Active { get; set; }

        public string CreateDateTime { get; set; }

        public string CreateUserID { get; set; }

        public string OpDateTime { get; set; }

        public string OpUserID { get; set; }


        public string BedId { get; set; }

        public string BedName { get; set; }
        public string OptionNo { get; set; }

        public string BedTypeId { get; set; }

        public string BedCount { get; set; }

        public string AttributeHeaderId { get; set; }
        public string AttributeHeaderName { get; set; }

        public int AttributeId { get; set; }
        public string AttributeName { get; set; }
        public bool AttributeIsSelected { get; set; }

        public List<PropertyRoomsExt> AttributeList { get; set; }


        public string EncryptRoomID { get; set; }
    }

    public class AttributeModel
    {
        
    }
}