using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace gbsExtranetMVC.Models.Repositories
{
    public class DropDownListsRepository:BaseRepository
    {
        public  string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;


        public List<DropDownListsExt> ReadCurrencies()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetCurrencyDropdown_Result>("B_Ex_GetCurrencyDropdown_TB_Currency_SP @CultureCode", Culture).ToList();
            foreach (GetCurrencyDropdown_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }



        public List<DropDownListsExt> ReadPages()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Page");
            var result = entity.Database.SqlQuery<GetPagesdropdown_Result>("B_Ex_GetPagesDropdown_TB_Currency_SP @CultureCode", Culture).ToList();
            foreach (GetPagesdropdown_Result Val in result)
            {
                dt.Rows.Add(Val.id, Val.Page);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["id"]);
                    drpObj.Name = dr["Page"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }
        public List<DropDownListsExt> GetRegionsDropdown(string RCountryID, string ParentRegionID, string Latitude, string Longitude)
        {
            if (RCountryID == "")
            {
                RCountryID = null;
            }
            var cID = (RCountryID != null ? Convert.ToInt32(RCountryID.Trim()) : (object)DBNull.Value);
            var rID = (ParentRegionID != null ? Convert.ToInt64(ParentRegionID.Trim()) : (object)DBNull.Value);
            //List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@Culture", CultureCode);
            var CountryID = new SqlParameter("@CountryID", cID);
            var SecondParentID = new SqlParameter("@SecondParentID", rID);
            var OrderBy = new SqlParameter("@OrderBy", "Name Asc");
            List<DropDownListsExt> list = new  List<DropDownListsExt>();
            List<DropDownListsExt> listDw = new List<DropDownListsExt>();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("Latitude");
            dt.Columns.Add("Longitude");

            var result = entity.Database.SqlQuery<GetRegionsDropdown_Result4>("B_Ex_GetRegions_TB_Region_SP @CountryID,@SecondParentID,@Culture,@OrderBy", CountryID, SecondParentID, Culture, OrderBy).ToList();


            foreach (GetRegionsDropdown_Result4 Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name, Val.Latitude, Val.Longitude);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropDownListsExt drpObj = new DropDownListsExt();
                        drpObj.ID = Convert.ToInt64(dr["ID"]);
                        drpObj.Name = dr["Name"].ToString();
                        drpObj.Latitude = dr["Latitude"].ToString();
                        drpObj.Longitude = dr["Longitude"].ToString();
                        list.Add(drpObj);
                    }
                }
            }
            DataTable regionDW = new DataTable();
            if (Latitude != "" && Longitude != "" && Latitude != null && Longitude != null)
            {
                SQLCon.Open();
                SqlCommand cmd = new SqlCommand("[TB_SP_GetRegionsInVicinity]", SQLCon);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CenterLatitude", Latitude);
                cmd.Parameters.AddWithValue("@CenterLongitude", Longitude);
                cmd.Parameters.AddWithValue("@Radius", 50000);
                cmd.Parameters.AddWithValue("@Culture", CultureCode);
                cmd.Parameters.AddWithValue("@OrderBy", "Name Asc");
                cmd.Parameters.AddWithValue("@CountryID", RCountryID);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                sda.Fill(regionDW);

                SQLCon.Close();
            }

            if (Latitude != "" && Longitude != "" && Latitude != null && Longitude != null)
            {
                regionDW.Columns.Add("Distance");
                foreach (DataRow dr in regionDW.Rows)
                {
                   if(dr["Latitude"].ToString()!=""&&dr["Longitude"].ToString()!="")
                   {
                       double distance = GetDistanceBetweenCoordinates(Double.Parse(Latitude), Double.Parse(Longitude), Double.Parse(dr["Latitude"].ToString()), Double.Parse(dr["Longitude"].ToString()));
                      // regionDW
                       DataRow[] rows = regionDW.Select("ID = '"+dr["ID"].ToString()+"'"); 
                       distance = Math.Round(distance, 1);
                       rows[0]["Distance"] = distance;
                       rows[0]["Name"] = dr["Name"].ToString() + " - " + distance + " km";
                   }
                   else
                   {
                       DataRow[] rows = regionDW.Select("ID = '" + dr["ID"].ToString() + "'");
                       rows[0]["Distance"] = 0;
                       rows[0]["Name"] = dr["Name"].ToString();

                   }
                }
              
            }
            if (Latitude != "" && Longitude != "" && Latitude != null && Longitude != null)
            {
                
                if (regionDW.Rows.Count > 0)
                {
                    DataView dv = regionDW.DefaultView;
                    dv.Sort = "Distance";
                    regionDW = dv.ToTable();

                    foreach (DataRow dr in regionDW.Rows)
                    {
                        {
                            DropDownListsExt drpObj = new DropDownListsExt();
                            drpObj.ID = Convert.ToInt64(dr["ID"]);
                            drpObj.Name = dr["Name"].ToString();
                            drpObj.Latitude = dr["Latitude"].ToString();
                            drpObj.Longitude = dr["Longitude"].ToString();
                            listDw.Add(drpObj);
                        }
                    }
                    list = listDw;
                }

              
            }



            return list;

            //var result = entity.Database.SqlQuery<GetRegionsDropdown_Result4>("B_Ex_GetRegions_TB_Region_SP @CountryID,@SecondParentID,@Culture,@OrderBy", CountryID, SecondParentID, Culture, OrderBy).ToList();

            //List<DropDownListsExt> list = (from dr in result
            //                               select new DropDownListsExt
            //                                 {
            //                                     ID = Convert.ToInt64(dr.ID),
            //                                     Name = dr.Name,
            //                                     Latitude = dr.Latitude,
            //                                     Longitude = dr.Longitude,
                                              
            //                                 }).ToList();
         
           
        }
        public double GetDistanceBetweenCoordinates(double Latitude1, double Longitude1, double Latitude2, double Longitude2)
        {

            double e = (3.1415926538 * Latitude1 / 180);
            double f = (3.1415926538 * Longitude1 / 180);
            double g = (3.1415926538 * Latitude2 / 180);
            double h = (3.1415926538 * Longitude2 / 180);
            double i = (Math.Cos(e) * Math.Cos(g) * Math.Cos(f) * Math.Cos(h) + Math.Cos(e) * Math.Sin(f) * Math.Cos(g) * Math.Sin(h) + Math.Sin(e) * Math.Sin(g));
            double j = (Math.Acos(i));
            double k = (6371 * j);

            return k;

        }
        public List<DropDownListsExt> GetMainRegionsDropdown( string ParentRegionID)
        {
            //List<DropDownListsExt> list = new List<DropDownListsExt>();
            //DBEntities entity = new DBEntities();
            //var Culture = new SqlParameter("@Culture", CultureCode);
            //var SecondParentID = new SqlParameter("@SecondParentID", Convert.ToInt64(ParentRegionID));
            //var OrderBy = new SqlParameter("@OrderBy", "Name Asc");
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetRegions_TB_Region_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureCode);
            cmd.Parameters.AddWithValue("@OrderBy", "Name Asc");
            cmd.Parameters.AddWithValue("@SecondParentID", Convert.ToInt64(ParentRegionID));
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();

            List<DropDownListsExt> list = new List<DropDownListsExt>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt HotelObj = new DropDownListsExt();
                    HotelObj.ID = Convert.ToInt64(dr["ID"]);
                    HotelObj.Name = dr["Name"].ToString();
                    HotelObj.Longitude = dr["Longitude"].ToString();
                    HotelObj.Latitude = dr["Latitude"].ToString();

                    list.Add(HotelObj);
                }
            }
            //return ListOfModel;

            //var result = entity.Database.SqlQuery<GetRegionsDropdown_Result4>("B_Ex_GetRegions_TB_Region_SP @SecondParentID,@Culture,@OrderBy", SecondParentID, Culture, OrderBy).ToList();

            //List<DropDownListsExt> list = (from dr in result
            //                               select new DropDownListsExt
            //                               {
            //                                   ID = Convert.ToInt64(dr.ID),
            //                                   Name = dr.Name,
            //                                   Latitude = dr.Latitude,
            //                                   Longitude = dr.Longitude,

            //                               }).ToList();

            return list;
        }

        public List<DropDownListsExt> GetFirmDropdown()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetFirmsDropDown_Result1>("B_Ex_GetFirmDropdown_TB_Firm_SP").ToList();
            foreach (GetFirmsDropDown_Result1 Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropDownListsExt drpObj = new DropDownListsExt();
                        drpObj.ID = Convert.ToInt64(dr["ID"]);
                        drpObj.Name = dr["Name"].ToString();
                        list.Add(drpObj);
                    }
                }
            }
            return list;
        }

           public List<DropDownListsExt> GetPropertyType()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@Culture", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetHotelTypeDropdown_Result>("B_Ex_GetHotelType_TB_TypeHotel_SP @Culture", Culture).ToList();
            foreach (GetHotelTypeDropdown_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.HotelType);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropDownListsExt drpObj = new DropDownListsExt();
                        drpObj.ID = Convert.ToInt64(dr["ID"]);
                        drpObj.Name = dr["Name"].ToString();
                        list.Add(drpObj);
                    }
                }
            }
            return list;
        }

        public List<DropDownListsExt> GetPropertyClass()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@Culture", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetPropertyClassDropdown_Result>("B_Ex_GetHotelClass_TB_TypeHotelClass_SP @Culture", Culture).ToList();
            foreach (GetPropertyClassDropdown_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropDownListsExt drpObj = new DropDownListsExt();
                        drpObj.ID = Convert.ToInt64(dr["ID"]);
                        drpObj.Name = dr["Name"].ToString();
                        list.Add(drpObj);
                    }
                }
            }
            return list;
        }

        public List<DropDownListsExt> GetTypeHotelChain()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetHotelChainDropdown_Result>("B_Ex_GetHotelChain_TB_TypeHotelChain_SP").ToList();
            foreach (GetHotelChainDropdown_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropDownListsExt drpObj = new DropDownListsExt();
                        drpObj.ID = Convert.ToInt64(dr["ID"]);
                        drpObj.Name = dr["Name"].ToString();
                        list.Add(drpObj);
                    }
                }
            }
            return list;
        }



        public List<DropDownListsExt> GetTypeHotelAccommodation()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@Cultureid", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<TypeHotelAccommodationDropdown_Result1>("B_Ex_GetHotelAccommodation_TB_TypeHotelAccommodation_SP @Cultureid", Culture).ToList();
            foreach (TypeHotelAccommodationDropdown_Result1 Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropDownListsExt drpObj = new DropDownListsExt();
                        drpObj.ID = Convert.ToInt64(dr["ID"]);
                        drpObj.Name = dr["Name"].ToString();
                        list.Add(drpObj);
                    }
                }
            }
            return list;
        }
        public List<DropDownListsExt> GetTypeHotelAccommodationByID(int Id)
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@Cultureid", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<TypeHotelAccommodationDropdown_Result1>("B_Ex_GetHotelAccommodation_TB_TypeHotelAccommodation_SP @Cultureid", Culture).ToList();
            foreach (TypeHotelAccommodationDropdown_Result1 Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    {
                        if (Convert.ToInt32(dr["ID"]) == Id)
                        {
                            DropDownListsExt drpObj = new DropDownListsExt();
                            drpObj.ID = Convert.ToInt64(dr["ID"]);
                            drpObj.Name = dr["Name"].ToString();
                            list.Add(drpObj);
                        }
                    }
                }
            }
            return list;
        }

        public List<DropDownListsExt> GetHotelCreditCardList( string HotelID)
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var HotelIDParam = new SqlParameter("@HotelID", HotelID);
             
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetCreditCardList_Result>("B_Ex_GetCreditCard_TB_TypeCreditCard_SP @HotelID", HotelIDParam).ToList();
            foreach (GetCreditCardList_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropDownListsExt drpObj = new DropDownListsExt();
                        drpObj.ID = Convert.ToInt64(dr["ID"]);
                        drpObj.Name = dr["Name"].ToString();
                        list.Add(drpObj);
                    }
                }
            }
            return list;
        }

        public List<DropDownListsExt> GetChannelManager()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetChannelManagerDropdown_Result>("B_GetChannelManager_TB_ChannelManager_Sp").ToList();
            foreach (GetChannelManagerDropdown_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropDownListsExt drpObj = new DropDownListsExt();
                        drpObj.ID = Convert.ToInt64(dr["ID"]);
                        drpObj.Name = dr["Name"].ToString();
                        list.Add(drpObj);
                    }
                }
            }
            return list;
        }

        public List<DropDownListsExt> GetCulture()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetCultureDropdown_Result1>("B_Ex_Getculture_BizTbl_Culture").ToList();
            foreach (GetCultureDropdown_Result1 Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Description);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropDownListsExt drpObj = new DropDownListsExt();
                        drpObj.ID = Convert.ToInt64(dr["ID"]);
                        drpObj.Name = dr["Name"].ToString();
                        list.Add(drpObj);
                    }
                }
            }
            return list;
        }

        public List<DropDownListsExt> FillYearList()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();

            int startIndex = 1901;
            int endIndex = DateTime.Now.Year;
            for (int i = startIndex; i <= endIndex; i++)
            {
                DropDownListsExt drpObj = new DropDownListsExt();
                drpObj.ID = Convert.ToInt64(i);
                drpObj.Name =i.ToString();
                list.Add(drpObj);
            }
            return list;
        }

        public List<DropDownListsExt> FillUnitList(int startIndex,int endIndex)
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            for (int i = startIndex; i <= endIndex; i++)
            {
                DropDownListsExt drpObj = new DropDownListsExt();
                drpObj.ID = Convert.ToInt64(i);
                drpObj.Name = i.ToString();
                list.Add(drpObj);
            }
            return list;
        }


        public List<DropDownListsExt> FillTimeList(int starttime, int Endtime)
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();

            int startIndex = starttime;
            int endIndex = Endtime;
            for (int i = startIndex; i <= endIndex; i++)
            {
                string time = (i == 24 ? "00:00" : string.Format("{0:00}", i) + ":00");
                string halfTime = (i == 24 ? "00:30" : string.Format("{0:00}", i) + ":30");

                DropDownListsExt drpObj = new DropDownListsExt();
                drpObj.TimeID = time.ToString();
                drpObj.Name = time.ToString();
                list.Add(drpObj);
                if (i != endIndex)
                {
                    DropDownListsExt drpObj1 = new DropDownListsExt();
                    drpObj1.TimeID = halfTime.ToString();
                    drpObj1.Name = halfTime.ToString();
                    list.Add(drpObj1);
                }
             
            }
            return list;
        }

        public List<DropDownListsExt> ReadTemplate()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("MailTemplateID");
            var result = entity.Database.SqlQuery<GetTemplatedropdown_Result>("B_Ex_GetTemplate_BizTbl_MailQueue_SP @CultureCode", Culture).ToList();
            foreach (GetTemplatedropdown_Result Val in result)
            {
                dt.Rows.Add(Val.id, Val.MailTemplateID);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["id"]);
                    drpObj.Name = dr["MailTemplateID"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }

        public List<DropDownListsExt> ReadTables()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Tables");
            var result = entity.Database.SqlQuery<GetTablesdropdown_Result>("B_Ex_GetTables_BizTbl_Table_SP @CultureCode", Culture).ToList();
            foreach (GetTablesdropdown_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Tables);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["Tables"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }

        public List<DropDownListsExt> ReadSecurityGroup()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Description");
            var result = entity.Database.SqlQuery<GetSecurityGroupdropdown_Result>("B_Ex_GetSecurityGroup_BizTbl_SecurityGroup_SP @CultureCode", Culture).ToList();
            foreach (GetSecurityGroupdropdown_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Description);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["Description"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }

        
        public List<DropDownListsExt> GetPricePlicy()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("PricePolicy");
            var result = entity.Database.SqlQuery<GetPricePolicyDropdown_Result>("B_Ex_GetPricePlicy_TB_TypePricePolicy_SP @CultureCode", Culture).ToList();
            foreach (GetPricePolicyDropdown_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.PricePolicy);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["PricePolicy"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }
        public List<DropDownListsExt> GetHotelroom()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            // var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetRoomIDDropdown_Result>("B_Ex_GetRoomID_TB_HotelRoom_Sp").ToList();
            foreach (GetRoomIDDropdown_Result Val in result)
            {
                dt.Rows.Add(Val.ID);
            }
            if (dt.Rows.Count > 0)
            {

                DropDownListsExt drpObjdefault = new DropDownListsExt();


                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);

                    list.Add(drpObj);
                }
            }
            return list;
        }

        public List<DropDownListsExt> GetHotels()
        {
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetHotels_TB_Hotel_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureCode);
            cmd.Parameters.AddWithValue("@OrderBy", "Name ASC,ID ASC");
            cmd.Parameters.AddWithValue("@PagingSize", int.MaxValue);
            cmd.Parameters.AddWithValue("@PageIndex", 1);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();

            List<DropDownListsExt> ListOfModel = new List<DropDownListsExt>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt HotelObj = new DropDownListsExt();
                    HotelObj.ID = Convert.ToInt32(dr["ID"]);
                    HotelObj.Name = dr["Name"].ToString();

                    ListOfModel.Add(HotelObj);
                }
            }
            return ListOfModel;
        }
        public List<DropDownListsExt> GetRegionsByHotelID(int HotelID)
        {
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("TB_SP_GetHotelRegions", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureCode);
            cmd.Parameters.AddWithValue("@OrderBy", "ID");
            cmd.Parameters.AddWithValue("@HotelID", HotelID);
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();

            List<DropDownListsExt> ListOfModel = new List<DropDownListsExt>();

            if (dt.Rows.Count > 0)
            {
                DropDownListsExt drpObj1 = new DropDownListsExt();
                ListOfModel.Add(drpObj1);
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt HotelObj = new DropDownListsExt();
                    HotelObj.ID = Convert.ToInt64(dr["RegionID"]);
                   // HotelObj.Name = dr["Name"].ToString();

                    ListOfModel.Add(HotelObj);
                }
            }
            return ListOfModel;
        }
        public List<DropDownListsExt> ReadUser()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            //var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Users");
            var result = entity.Database.SqlQuery<GetUserDropdown_Result>("B_Ex_GetUser_BizTbl_User_SP").ToList();
            foreach (GetUserDropdown_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Users);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt64(dr["ID"]);
                    drpObj.Name = dr["Users"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }

        public List<DropDownListsExt> ReadCities()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetCityDropdown_Result1>("B_Ex_GetDropdownCity_TB_TB_Region_SP @CultureCode", Culture).ToList();
            foreach (GetCityDropdown_Result1 Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt64(dr["ID"]);
                    drpObj.Name = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }

        public List<DropDownListsExt> ReadRoles()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            //      var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Code");
            var result = entity.Database.SqlQuery<GetRoleDropdown_Result>("B_Ex_GetDropdownRole_BizTbl_SecurityGroup_SP").ToList();
            foreach (GetRoleDropdown_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Code);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt64(dr["ID"]);
                    drpObj.Code = dr["Code"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }

        public List<DropDownListsExt> ReadDescription()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("SecurityCode");
            var result = entity.Database.SqlQuery<GetDescriptionDropdown_Result>("B_Ex_GetDropdownDescription_BizTbl_Security_SP @CultureCode", Culture).ToList();
            foreach (GetDescriptionDropdown_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.SecurityCode);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt64(dr["ID"]);
                    drpObj.Security = dr["SecurityCode"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }


        public List<DropDownListsExt> ReadFirm()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            //     var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetFirmsDropDown_Result1>("B_Ex_GetFirmDropdown_TB_Firm_SP").ToList();
            foreach (GetFirmsDropDown_Result1 Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt64(dr["ID"]);
                    drpObj.Name = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }

        public List<DropDownListsExt> ReadRequest()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetRequestType_Result>("B_Ex_GetDropdownRequestType_TB_TypeFirmRequest_SP @CultureCode", Culture).ToList();
            foreach (GetRequestType_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt64(dr["ID"]);
                    drpObj.RequestType = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }

        public List<DropDownListsExt> ReadReservationID()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            //    var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");

            var result = entity.Database.SqlQuery<GetReservationID_Result1>("B_Ex_GetDropdownReservationID_TB_Reservation_SP").ToList();
            foreach (GetReservationID_Result1 Val in result)
            {
                dt.Rows.Add(Val.ID);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt64(dr["ID"]);

                    list.Add(drpObj);
                }
            }
            return list;
        }

        public List<DropDownListsExt> ReadFirmRequestStatus()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetRequestStatus_Result>("B_Ex_GetDropdownRequestStatus_TB_TypeFirmRequestStatus_SP @CultureCode", Culture).ToList();
            foreach (GetRequestStatus_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt64(dr["ID"]);
                    drpObj.RequestStatus = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }

         public List<DropDownListsExt> GetDates()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
        
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Date");
            var result = entity.Database.SqlQuery<GetDate_Result1>("B_Ex_GetDates_TB_Date_SP ").ToList();
            foreach (GetDate_Result1 Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Date);
            }
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["Date"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }


        public List<DropDownListsExt> GetHotelPromotionID()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");

            var result = entity.Database.SqlQuery<GetHotelPromotionID_Result>("B_Ex_GetHotelPromotionID_TB_HotelPromotion_SP").ToList();
            foreach (GetHotelPromotionID_Result Val in result)
            {
                dt.Rows.Add(Val.ID);
            }
            if (dt.Rows.Count > 0)
            {
              

                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                  
                    list.Add(drpObj);
                }
            }
            return list;
        }

        public List<DropDownListsExt> GetRegion()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Region");
            var result = entity.Database.SqlQuery<GetRegion_Result>("B_Ex_GetRegionsDropdown_TB_Region_SP @CultureCode", Culture).ToList();
            foreach (GetRegion_Result Val in result)
            {
                dt.Rows.Add(Val.ID,Val.Region);
            }
            if (dt.Rows.Count > 0)
            {


                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt64(dr["ID"]);
                    drpObj.Name = dr["Region"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }
        public List<DropDownListsExt> GetReservationStatus()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetReservationStatusdropdown_Result>("B_Ex_GetReservationStatus_TB_TypeReservationStatus_SP @CultureCode", Culture).ToList();
            foreach (GetReservationStatusdropdown_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }

        public List<DropDownListsExt> GetTravellerType()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetTravellerDropDown_Result>("B_Ex_GetTraveller_TB_TypeTraveller_SP @CultureCode", Culture).ToList();
            foreach (GetTravellerDropDown_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }

        public List<DropDownListsExt> GetReviewScale()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetReviewScaleDropDown_Result>("B_Ex_GetReviewScale_TB_TypeReviewScale_SP @CultureCode", Culture).ToList();
            foreach (GetReviewScaleDropDown_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }

        public List<DropDownListsExt> GetReviewType()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetReviewTypedropdown_Result>("B_Ex_GetReviewType_TB_TypeReview_SP @CultureCode", Culture).ToList();
            foreach (GetReviewTypedropdown_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }
        public List<DropDownListsExt> ReadUnit()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetUnitDropdown_Result>("B_Ex_GetUnit_TB_Unit_SP @CultureCode", Culture).ToList();
            foreach (GetUnitDropdown_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }
        public List<DropDownListsExt> ReadAttributeType()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetAttributeType_Result>("B_Ex_GetAttributeType_TB_TypeAttribute_SP @CultureCode", Culture).ToList();
            foreach (GetAttributeType_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }
        public List<DropDownListsExt> ReadAttributeCategory()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetAttributeCategory_Result>("B_Ex_GetAttributeCategory_TB_AttributeHeader_SP @CultureCode", Culture).ToList();
            foreach (GetAttributeCategory_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }
        public List<DropDownListsExt> ReadRelatedAttribute()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetRelatedAttribute_Result>("B_Ex_GetRelatedAttribute_TB_Attribute_SP @CultureCode", Culture).ToList();
            foreach (GetRelatedAttribute_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }
        public List<DropDownListsExt> GetPromotion()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetPromotion_Result>("B_Ex_GetPromotion_TB_Promotion_SP @CultureCode", Culture).ToList();
            foreach (GetPromotion_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }
        public List<DropDownListsExt> ReadDeal()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetDealDropdown_Result>("B_Ex_GetDealDropdown_TB_Deal_SP @CultureCode", Culture).ToList();

            foreach (GetDealDropdown_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt64(dr["ID"]);
                    drpObj.Name = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }
        public List<DropDownListsExt> GetRoomType()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetRoomType_Result>("B_Ex_GetRoomType_TB_TypeRoom_SP @CultureCode", Culture).ToList();
            foreach (GetRoomType_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }
        public List<DropDownListsExt> GetSmokingType()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetSmokingType_Result>("B_Ex_GetTypeSmoking_TB_TypeSmoking_SP @CultureCode", Culture).ToList();
            foreach (GetSmokingType_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }

        public List<DropDownListsExt> GetTypeView()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetTypeView_Result>(" B_Ex_GetTypeView_TB_TypeView_SP @CultureCode", Culture).ToList();
            foreach (GetTypeView_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }

        public List<DropDownListsExt> GetBedType()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetBedType_Result>("B_Ex_GetBedType_TB_TypeBed_SP @CultureCode", Culture).ToList();
            foreach (GetBedType_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }
        public List<DropDownListsExt> GetMessageSubject()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetTypeMessageSubject_Result>("B_Ex_GetMessageSubject_TB_TypeMessageSubject_SP @CultureCode", Culture).ToList();
            foreach (GetTypeMessageSubject_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }

        public List<DropDownListsExt> GetMessageStatus()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetMessageStatus_Result>("B_Ex_GetMessageStatus_TB_TypeMessageStatus_SP @CultureCode", Culture).ToList();
            foreach (GetMessageStatus_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }
        public List<DropDownListsExt> GetCancelType()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetCancelType_Result>("B_Ex_GetDropdownCancelType_TB_TypeCancel_SP @CultureCode", Culture).ToList();
            foreach (GetCancelType_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropDownListsExt drpObj = new DropDownListsExt();
                        drpObj.ID = Convert.ToInt64(dr["ID"]);
                        drpObj.Name = dr["Name"].ToString();
                        list.Add(drpObj);
                    }
                }
            }
            return list;
        }

        public List<DropDownListsExt> GetPenaltyRate()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("PartID");

            var result = entity.Database.SqlQuery<GetPenaltyRate_Result1>("B_Ex_GetDropdownPenaltyRate_TB_TypePenaltyRate_SP @CultureCode", Culture).ToList();
            foreach (GetPenaltyRate_Result1 Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name, Val.PartID);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropDownListsExt drpObj = new DropDownListsExt();
                        drpObj.ID = Convert.ToInt64(dr["ID"]);
                        drpObj.PartID = Convert.ToInt64(dr["PartID"]);
                        drpObj.Name = dr["Name"].ToString();
                        list.Add(drpObj);
                    }
                }
            }
            return list;
        }
        public List<DropDownListsExt> GetTransferPeriod()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetTransferPeriod_Result>("B_Ex_GetTransferPeriod_TB_TransferPeriod_SP").ToList();
            foreach (GetTransferPeriod_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropDownListsExt drpObj = new DropDownListsExt();
                        drpObj.ID = Convert.ToInt64(dr["ID"]);
                        drpObj.Name = dr["Name"].ToString();
                        list.Add(drpObj);
                    }
                }
            }
            return list;
        }

        public List<DropDownListsExt> GetBusinessPartnerPax()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetBusinessPartnerPax_Result>("B_Ex_GetBusinessPartnerPax_TB_BusinessPartnerPax_SP").ToList();
            foreach (GetBusinessPartnerPax_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropDownListsExt drpObj = new DropDownListsExt();
                        drpObj.ID = Convert.ToInt64(dr["ID"]);
                        drpObj.Name = dr["Name"].ToString();
                        list.Add(drpObj);
                    }
                }
            }
            return list;
        }
        public List<DropDownListsExt> GetPeriod()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetPeriod_Result>("B_Ex_GetPeriod_TB_Period_SP").ToList();
            foreach (GetPeriod_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt32(dr["ID"]);
                    drpObj.Name = dr["Name"].ToString();
                    list.Add(drpObj);
                }
            }
            return list;
        }

        public List<DropDownListsExt> GetInvoiceID()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            //    var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");

            var result = entity.Database.SqlQuery<GetInvoiceID_Result>("B_Ex_GetInvoiceID_TB_Invoice_SP").ToList();
            foreach (GetInvoiceID_Result Val in result)
            {
                dt.Rows.Add(Val.ID);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt64(dr["ID"]);

                    list.Add(drpObj);
                }
            }
            return list;
        }

        public List<DropDownListsExt> GetBusinessPartner()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();

            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            dt.Columns.Add("FirmID");
            var result = entity.Database.SqlQuery<GetBusinessPartner_Result>("B_GetBusinessPartner_TB_BusinessPartner_Sp").ToList();
            foreach (GetBusinessPartner_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name,Val.FirmID);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropDownListsExt drpObj = new DropDownListsExt();
                        drpObj.ID = Convert.ToInt64(dr["ID"]);
                        drpObj.Name = dr["Name"].ToString();
                        drpObj.FirmID = dr["FirmID"].ToString();
                        list.Add(drpObj);
                    }
                }
            }
            return list;
        }

        public List<DropDownListsExt> GetTourFrequency()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetTourFrequency_Result>("B_Ex_GetTourFrequencyDropdown_TB_TypeTourFrequency_SP @CultureCode", Culture).ToList();
            foreach (GetTourFrequency_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropDownListsExt drpObj = new DropDownListsExt();
                        drpObj.ID = Convert.ToInt64(dr["ID"]);
                        drpObj.Name = dr["Name"].ToString();
                        list.Add(drpObj);
                    }
                }
            }
            return list;
        }

        public List<DropDownListsExt> GetTypeDeposit()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetTypeDeposit_Result>("B_Ex_GetTypeDeposit_TB_TypeDeposit_SP @CultureCode", Culture).ToList();
            foreach (GetTypeDeposit_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropDownListsExt drpObj = new DropDownListsExt();
                        drpObj.ID = Convert.ToInt64(dr["ID"]);
                        drpObj.Name = dr["Name"].ToString();
                        list.Add(drpObj);
                    }
                }
            }
            return list;
        }


        public List<DropDownListsExt> GetBusinessPartnerType()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetBusinessPartnerTypeDropdown_Result>("B_Ex_GetBusinessPartnerType_TB_TypeBusinessPartner_SP @CultureCode", Culture).ToList();
            foreach (GetBusinessPartnerTypeDropdown_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropDownListsExt drpObj = new DropDownListsExt();
                        drpObj.ID = Convert.ToInt64(dr["ID"]);
                        drpObj.Name = dr["Name"].ToString();
                        list.Add(drpObj);
                    }
                }
            }
            return list;
        }


        
        public List<DropDownListsExt> GetBusinessPartnerCancelPolicyID()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            //    var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");

            var result = entity.Database.SqlQuery<GetBusinessPartnerCancelPolicy_Result>("B_Ex_GetBusinessPartnerCancelPolicy_TB_BusinessPartnerCancelPolicy_SP").ToList();
            foreach (GetBusinessPartnerCancelPolicy_Result Val in result)
            {
                dt.Rows.Add(Val.ID);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt drpObj = new DropDownListsExt();
                    drpObj.ID = Convert.ToInt64(dr["ID"]);

                    list.Add(drpObj);
                }
            }
            return list;
        }

        public List<DropDownListsExt> GetCreditCardType()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            //  var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetCreditCardType_Result>("B_Ex_GetDropdownCreditCardType_TB_TypeCreditCard_SP").ToList();
            foreach (GetCreditCardType_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropDownListsExt drpObj = new DropDownListsExt();
                        drpObj.ID = Convert.ToInt64(dr["ID"]);
                        drpObj.Name = dr["Name"].ToString();
                        list.Add(drpObj);
                    }
                }
            }
            return list;
        }

        public List<DropDownListsExt> GetFreebieID()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            //  var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("FreebieID");

            var result = entity.Database.SqlQuery<GetFreebieID_Result>("B_Ex_GetFreebieID_TB_TypeFreebie_SP").ToList();
            foreach (GetFreebieID_Result Val in result)
            {
                dt.Rows.Add(Val.FreebieID);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropDownListsExt drpObj = new DropDownListsExt();
                        drpObj.ID = Convert.ToInt64(dr["FreebieID"]);

                        list.Add(drpObj);
                    }
                }
            }
            return list;
        }
        public List<DropDownListsExt> GetCreateUser()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            //  var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("CreateUser");

            var result = entity.Database.SqlQuery<GetcreateUser_Result>("B_Ex_GetCreateUser_BizTbl_User_SP").ToList();
            foreach (GetcreateUser_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.CreateUser);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropDownListsExt drpObj = new DropDownListsExt();
                        drpObj.ID = Convert.ToInt64(dr["ID"]);
                        drpObj.Name = dr["CreateUser"].ToString();
                        list.Add(drpObj);
                    }
                }
            }
            return list;
        }
        public List<DropDownListsExt> GetTourddl()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetTourDropDown_Result>("B_Ex_GetTour_TB_Tour_SP @CultureCode", Culture).ToList();
            foreach (GetTourDropDown_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropDownListsExt drpObj = new DropDownListsExt();
                        drpObj.ID = Convert.ToInt64(dr["ID"]);
                        drpObj.Name = dr["Name"].ToString();
                        list.Add(drpObj);
                    }
                }
            }
            return list;
        }

        public List<DropDownListsExt> GetHotelAttribute()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetHotelAttribute_Result>("B_Ex_GetHotelAttribute_TB_Attribute_SP @CultureCode", Culture).ToList();
            foreach (GetHotelAttribute_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropDownListsExt drpObj = new DropDownListsExt();
                        drpObj.ID = Convert.ToInt64(dr["ID"]);
                        drpObj.Name = dr["Name"].ToString();
                        list.Add(drpObj);
                    }
                }
            }
            return list;
        }


        public List<DropDownListsExt> GetClosestAirportDropdown()
        {
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetRegions_TB_Region_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureCode);
            cmd.Parameters.AddWithValue("@OrderBy", "Name Asc");
            cmd.Parameters.AddWithValue("@RegionType", "AIRP");
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();

            List<DropDownListsExt> list = new List<DropDownListsExt>();

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt HotelObj = new DropDownListsExt();
                    HotelObj.ID = Convert.ToInt64(dr["ID"]);
                    HotelObj.Name = dr["Name"].ToString();
                    HotelObj.Longitude = dr["Longitude"].ToString();
                    HotelObj.Latitude = dr["Latitude"].ToString();

                    list.Add(HotelObj);
                }
            }
           
            return list;
        }

        public List<DropDownListsExt> GetClosestAirport(string RCountryID, string Latitude, string Longitude)
        {
            if (RCountryID == "")
            {
                RCountryID = null;
            }
            DataTable dt = new DataTable();
            SQLCon.Open();
            SqlCommand cmd = new SqlCommand("B_Ex_GetRegions_TB_Region_SP", SQLCon);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@Culture", CultureCode);
            cmd.Parameters.AddWithValue("@OrderBy", "Name");
            cmd.Parameters.AddWithValue("@CountryID", RCountryID);
            cmd.Parameters.AddWithValue("@RegionType", "AIRP");
            
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            sda.Fill(dt);
            SQLCon.Close();

            List<DropDownListsExt> ListOfModel = new List<DropDownListsExt>();
            List<DropDownListsExt> ListOfModelDistance = new List<DropDownListsExt>();
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    DropDownListsExt HotelObj = new DropDownListsExt();
                    HotelObj.ID = Convert.ToInt64(dr["ID"]);
                  //  string ClosestAirportDistance = dr["ClosestAirportDistance"].ToString();
                    HotelObj.Name = dr["Name"].ToString();
                    HotelObj.Latitude = dr["Latitude"].ToString();
                    HotelObj.Longitude = dr["Longitude"].ToString();
                    //HotelObj.ClosestAirportDistance = dr["ClosestAirportDistance"].ToString();

                    ListOfModel.Add(HotelObj);
                }
            }

            DataTable AirportDW = new DataTable();
            if (Latitude != "" && Longitude != "" && Latitude != null && Longitude != null)
            {
                SQLCon.Open();
                SqlCommand cmdAirport = new SqlCommand("[TB_SP_GetRegionsInVicinity]", SQLCon);
                cmdAirport.CommandType = CommandType.StoredProcedure;
                cmdAirport.Parameters.AddWithValue("@CenterLatitude", Latitude);
                cmdAirport.Parameters.AddWithValue("@CenterLongitude", Longitude);
                cmdAirport.Parameters.AddWithValue("@Radius", 150000);
                cmdAirport.Parameters.AddWithValue("@Culture", CultureCode);
                cmdAirport.Parameters.AddWithValue("@OrderBy", "DistanceToCenter");
                cmdAirport.Parameters.AddWithValue("@CountryID", RCountryID);
                cmdAirport.Parameters.AddWithValue("@RegionType", "AIRP");
                SqlDataAdapter sdaAirport = new SqlDataAdapter(cmdAirport);
                sdaAirport.Fill(AirportDW);

                SQLCon.Close();
            }

            if (Latitude != "" && Longitude != "" && Latitude != null && Longitude != null)
            {
                AirportDW.Columns.Add("Distance");
                foreach (DataRow dr in AirportDW.Rows)
                {
                    if (dr["Latitude"].ToString() != "" && dr["Longitude"].ToString() != "")
                    {
                        double distance = GetDistanceBetweenCoordinates(Double.Parse(Latitude), Double.Parse(Longitude), Double.Parse(dr["Latitude"].ToString()), Double.Parse(dr["Longitude"].ToString()));
                        // regionDW
                        DataRow[] rows = AirportDW.Select("ID = '" + dr["ID"].ToString() + "'");
                        distance = Math.Round(distance, 1);
                        rows[0]["Distance"] = distance;
                        rows[0]["Name"] = dr["Name"].ToString() + " - " + distance + " km";
                    }
                    else
                    {
                        DataRow[] rows = AirportDW.Select("ID = '" + dr["ID"].ToString() + "'");
                        rows[0]["Distance"] = 0;
                        rows[0]["Name"] = dr["Name"].ToString();

                    }
                }

            }
            if (Latitude != "" && Longitude != "" && Latitude != null && Longitude != null)
            {

                if (AirportDW.Rows.Count > 0)
                {
                    //DataView dvAirport = AirportDW.DefaultView;
                    //dvAirport.Sort = "Distance";
                    //AirportDW = dvAirport.ToTable();

                    foreach (DataRow dr1 in AirportDW.Rows)
                    {
                        {
                            DropDownListsExt drpObj = new DropDownListsExt();
                            drpObj.ID = Convert.ToInt64(dr1["ID"]);
                            drpObj.Name = dr1["Name"].ToString();
                            drpObj.Latitude = dr1["Latitude"].ToString();
                            drpObj.Longitude = dr1["Longitude"].ToString();
                            ListOfModelDistance.Add(drpObj);
                        }
                    }
                    ListOfModel = ListOfModelDistance;
                }

             
            }

            return ListOfModel;
        }
        public List<DropDownListsExt> GetAuthority()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetAuthority_Result>("B_Ex_GetAuthority_BizTbl_SecurityGroup_SP @CultureCode", Culture).ToList();
            foreach (GetAuthority_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {

                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropDownListsExt drpObj = new DropDownListsExt();
                        drpObj.ID = Convert.ToInt64(dr["ID"]);
                        drpObj.Name = dr["Name"].ToString();
                        list.Add(drpObj);
                    }
                }
            }
            return list;
        }
        public List<DropDownListsExt> GetTypeFirmRequest()
        {
            List<DropDownListsExt> list = new List<DropDownListsExt>();
            DBEntities entity = new DBEntities();
            var Culture = new SqlParameter("@CultureCode", CultureCode);
            DataTable dt = new DataTable();
            dt.Columns.Add("ID");
            dt.Columns.Add("Name");
            var result = entity.Database.SqlQuery<GetTypeFirmRequest_Result>("B_Ex_GetTypeFirmRequest_TB_TypeFirmRequest_SP @CultureCode", Culture).ToList();
            foreach (GetTypeFirmRequest_Result Val in result)
            {
                dt.Rows.Add(Val.ID, Val.Name);
            }
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    {
                        DropDownListsExt drpObj = new DropDownListsExt();
                        drpObj.ID = Convert.ToInt64(dr["ID"]);
                        drpObj.Name = dr["Name"].ToString();
                        list.Add(drpObj);
                    }
                }
            }
            return list;
        }
  

  public List<DropDownListsExt> GetReservationOperation()
  {
      List<DropDownListsExt> list = new List<DropDownListsExt>();
      DBEntities entity = new DBEntities();
      var Culture = new SqlParameter("@CultureCode", CultureCode);
      DataTable dt = new DataTable();
      dt.Columns.Add("ID");
      dt.Columns.Add("Name");
      var result = entity.Database.SqlQuery<GetReservationOperationdropdown_Result>("B_Ex_GetReservationOperation_TB_TypeReservationOperation_SP @CultureCode", Culture).ToList();
      foreach (GetReservationOperationdropdown_Result Val in result)
      {
          dt.Rows.Add(Val.ID, Val.Name);
      }
      if (dt.Rows.Count > 0)
      {
          foreach (DataRow dr in dt.Rows)
          {
              DropDownListsExt drpObj = new DropDownListsExt();
              drpObj.ID = Convert.ToInt32(dr["ID"]);
              drpObj.Name = dr["Name"].ToString();
              list.Add(drpObj);
          }
      }
      return list;
  }
  public List<DropDownListsExt> GetTypeInvoiceStatus()
  {
      List<DropDownListsExt> list = new List<DropDownListsExt>();
      DBEntities entity = new DBEntities();
      var Culture = new SqlParameter("@CultureCode", CultureCode);
      DataTable dt = new DataTable();
      dt.Columns.Add("ID");
      dt.Columns.Add("Name");
      var result = entity.Database.SqlQuery<GetTypeInvoiceStatus_Result>("B_Ex_GetTypeInvoiceStatus_TB_TypeInvoiceStatus_SP @CultureCode", Culture).ToList();
      foreach (GetTypeInvoiceStatus_Result Val in result)
      {
          dt.Rows.Add(Val.ID, Val.Name);
      }
      if (dt.Rows.Count > 0)
      {
          foreach (DataRow dr in dt.Rows)
          {
              {
                  DropDownListsExt drpObj = new DropDownListsExt();
                  drpObj.ID = Convert.ToInt64(dr["ID"]);
                  drpObj.Name = dr["Name"].ToString();
                  list.Add(drpObj);
              }
          }
      }
      return list;
  }
  public List<DropDownListsExt> GetRegiondrodown_Region()
  {

      List<DropDownListsExt> list = new List<DropDownListsExt>();
      DataTable dt = new DataTable();
      SQLCon.Open();
      SqlCommand cmd = new SqlCommand("TB_SP_GetRegions", SQLCon);
      cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.AddWithValue("@Culture", CultureCode);
      SqlDataAdapter sda = new SqlDataAdapter(cmd);
      sda.Fill(dt);
      SQLCon.Close();

      if (dt.Rows.Count > 0)
      {

          foreach (DataRow dr in dt.Rows)
          {
              DropDownListsExt drpObj = new DropDownListsExt();
              drpObj.ID = Convert.ToInt64(dr["ID"]);
              drpObj.CountryID = Convert.ToInt64(dr["CountryID"]);
              drpObj.Name = dr["Name"].ToString();
              list.Add(drpObj);
          }
      }
      return list;
  }

  public List<DropDownListsExt> GetRegionsByCountry(int Country)
  {
      DataTable dt = new DataTable();
      SQLCon.Open();
      SqlCommand cmd = new SqlCommand("B_Ex_GetRegions_TB_Region_SP", SQLCon);
      cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.AddWithValue("@Culture", CultureCode);
      cmd.Parameters.AddWithValue("@OrderBy", "Name Asc");
      cmd.Parameters.AddWithValue("@CountryID", Country);
      SqlDataAdapter sda = new SqlDataAdapter(cmd);
      sda.Fill(dt);
      SQLCon.Close();

      List<DropDownListsExt> list = new List<DropDownListsExt>();

      if (dt.Rows.Count > 0)
      {
          foreach (DataRow dr in dt.Rows)
          {
              DropDownListsExt HotelObj = new DropDownListsExt();
              HotelObj.ID = Convert.ToInt64(dr["ID"]);
              HotelObj.Name = dr["Name"].ToString();
              HotelObj.Longitude = dr["Longitude"].ToString();
              HotelObj.Latitude = dr["Latitude"].ToString();

              list.Add(HotelObj);
          }
      }
      return list;
  }

  public List<DropDownListsExt> GetCityByCountry(int Country)
  {
      DataTable dt = new DataTable();
      SQLCon.Open();
      SqlCommand cmd = new SqlCommand("B_Ex_GetRegions_TB_Region_SP", SQLCon);
      cmd.CommandType = CommandType.StoredProcedure;
      cmd.Parameters.AddWithValue("@Culture", CultureCode);
      cmd.Parameters.AddWithValue("@OrderBy", "Name Asc");
      cmd.Parameters.AddWithValue("@CountryID", Country);
      cmd.Parameters.AddWithValue("@IsCity", 1);
      SqlDataAdapter sda = new SqlDataAdapter(cmd);
      sda.Fill(dt);
      SQLCon.Close();

      List<DropDownListsExt> list = new List<DropDownListsExt>();

      if (dt.Rows.Count > 0)
      {
          foreach (DataRow dr in dt.Rows)
          {
              DropDownListsExt HotelObj = new DropDownListsExt();
              HotelObj.ID = Convert.ToInt64(dr["ID"]);
              HotelObj.Name = dr["Name"].ToString();
              HotelObj.Longitude = dr["Longitude"].ToString();
              HotelObj.Latitude = dr["Latitude"].ToString();

              list.Add(HotelObj);
          }
      }
      return list;
  }
  public List<DropDownListsExt> GetComission(int HotelMinumumComissionRate)
  {
      List<DropDownListsExt> list = new List<DropDownListsExt>();

      for (int i = HotelMinumumComissionRate; i <= 100; i++)
      {
          DropDownListsExt drpObj = new DropDownListsExt();
          drpObj.ComissionID = i;
          drpObj.Comission = i;
          list.Add(drpObj);
      }

      return list;
  }
  public List<DropDownListsExt> GetYear(int startyear)
  {
      List<DropDownListsExt> list = new List<DropDownListsExt>();
      DBEntities entity = new DBEntities();

      int startIndex = startyear;
      DateTime dt = DateTime.Now;
      string getendyear = dt.Year.ToString();
      //int endyear = d.Year();
      int endyear = Convert.ToInt32(getendyear);
      int endIndex = endyear;
      for (int year = startIndex; year <= endIndex; year++)
      {

          DropDownListsExt drpObj = new DropDownListsExt();
          drpObj.year = year.ToString();
          list.Add(drpObj);


      }
      return list;
  }

    }

  

    public class DropDownListsExt 
    {
        public long ID { get; set; }
        public long PartID { get; set; }
        public string TimeID { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public string ClosestAirportDistance { get; set; }
        public string HotelType { get; set; }
        public string City { get; set; }
        public string Security { get; set; }
        public string Code { get; set; }
        public string RequestType { get; set; }
        public string RequestStatus { get; set; }
        public string year { get; set; }
        public int ComissionID { get; set; }
        public int Comission { get; set; }
        public long CountryID { get; set; }

        public string FirmID { get; set; }
    }

}