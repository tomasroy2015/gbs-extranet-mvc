using System;
using gbsExtranetMVC.Models.Repositories;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Web.Mvc;

namespace gbsExtranetMVC.Helpers
{
    /// <summary>
    /// Used to Generate Select List to pass from Controller to View to bind it to the Dropdown or ComboBox Controls
    /// </summary>
    public static class DropDownLists
    {
        public static List<SelectListItem> CreateBlankListItem()
        {
            SelectListItem it = new SelectListItem();
            it.Text = "-- Select --";
            it.Value = "";
            it.Selected = true;

            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(it);

            return list;
        }

        public static SelectList Getcurrencydropdown(string CurrencyID)
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            var currency = modelRepo.ReadCurrencies();
            if (CurrencyID == null || CurrencyID == "0")
            {
                CurrencyID = "1";
            }
            List<SelectListItem> _Listpenaltrate = new List<SelectListItem>();

            foreach (var item in currency)
            {
                SelectListItem itr = new SelectListItem();
                itr.Text = item.Name;
                itr.Value = item.ID.ToString();
                itr.Selected = false;

                _Listpenaltrate.Add(itr);
            }

            return new SelectList(_Listpenaltrate, "Value", "Text", CurrencyID);
        }

        public static SelectList Getunitdropdown(string UnitID)
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            var unit = modelRepo.ReadUnit();
            if (UnitID == null || UnitID == "0")
            {
                UnitID = "1";
            }
            List<SelectListItem> _Listpenaltrate = new List<SelectListItem>();

            foreach (var item in unit)
            {
                SelectListItem itr = new SelectListItem();
                itr.Text = item.Name;
                itr.Value = item.ID.ToString();
                itr.Selected = false;

                _Listpenaltrate.Add(itr);
            }

            return new SelectList(_Listpenaltrate, "Value", "Text", UnitID);
        }
        public static SelectList GetCountries(long? CountryID)
        {
            CountriesRepository modelRepo = new CountriesRepository();
            var countries = modelRepo.ReadAll();

            List<SelectListItem> _ListCounties = new List<SelectListItem>();
       
                foreach (var item in countries)
                {
                    SelectListItem itr = new SelectListItem();
                    itr.Text = item.CountryName;
                    itr.Value = item.CountryID.ToString();
                    itr.Selected = false;

                    _ListCounties.Add(itr);
                }
          
            return new SelectList(_ListCounties, "Value", "Text", CountryID);
        }

        public static SelectList GetRegions(string CountryID, string RegionID, string Latitude, string Longitude, string ArgRegionID)
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            var Regions = modelRepo.GetRegionsDropdown(CountryID, RegionID, Latitude, Longitude);

            List<SelectListItem> _ListRegions = new List<SelectListItem>();
        
                foreach (var item in Regions)
                {
                    SelectListItem itr = new SelectListItem();
                    itr.Text = item.Name;
                    itr.Value = item.ID.ToString();
                    itr.Selected = false;

                    _ListRegions.Add(itr);
                }
            
            return new SelectList(_ListRegions, "Value", "Text", ArgRegionID);
        }
        public static SelectList GetClosestAirport(string CountryID, string Latitude, string Longitude, string ClosestAirportID)
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            var Regions = modelRepo.GetClosestAirport(CountryID, Latitude, Longitude);

            List<SelectListItem> _ListRegions = new List<SelectListItem>();

            foreach (var item in Regions)
            {
                SelectListItem itr = new SelectListItem();
                itr.Text = item.Name;
                itr.Value = item.ID.ToString();
                itr.Selected = false;

                _ListRegions.Add(itr);
            }

            return new SelectList(_ListRegions, "Value", "Text", ClosestAirportID);
        }
        public static SelectList GetRegionsMultpleDropdown(string CountryID, string RegionID, string Latitude, string Longitude)
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            var Regions = modelRepo.GetRegionsDropdown(CountryID, RegionID, Latitude, Longitude);

            List<SelectListItem> _ListRegions = new List<SelectListItem>();

            foreach (var item in Regions)
            {
                SelectListItem itr = new SelectListItem();
                itr.Text = item.Name;
                itr.Value = item.ID.ToString();
                itr.Selected = false;

                _ListRegions.Add(itr);
            }

            return new SelectList(_ListRegions, "Value", "Text");
        }
        public static ArrayList GetRegionsByHotel(int HotelID)
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            var regions = modelRepo.GetRegionsByHotelID(HotelID);

            ArrayList list = new ArrayList();

            foreach (var item in regions)
            {
                SelectListItem itr = new SelectListItem();
                // itr.Text = item.Name;
                //itr.Value = item.ID.ToString();
                //itr.Selected = false;

                list.Add(item.ID.ToString());
            }


            return new ArrayList(list);
        }
        public static SelectList GetFirms(string FirmID)
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            var Firms = modelRepo.GetFirmDropdown();

            List<SelectListItem> _Listfirms = new List<SelectListItem>();

            foreach (var item in Firms)
            {
                SelectListItem itr = new SelectListItem();
                itr.Text = item.Name;
                itr.Value = item.ID.ToString();
                itr.Selected = false;

                _Listfirms.Add(itr);
            }

            return new SelectList(_Listfirms, "Value", "Text", FirmID);
        }
        //public static JsonResult GetFirms(string FirmID)
        //{
        //    DropDownListsRepository modelRepo = new DropDownListsRepository();
        //    var Firms = modelRepo.GetFirmDropdown();

        //    List<SelectListItem> _Listfirms = new List<SelectListItem>();

        //    foreach (var item in Firms)
        //    {
        //        SelectListItem itr = new SelectListItem();
        //        itr.Text = item.Name;
        //        itr.Value = item.ID.ToString();
        //        itr.Selected = false;

        //        _Listfirms.Add(itr);
        //    }
        //    var slist = new SelectList(_Listfirms, "Value", "Text", FirmID);
        //    return new JsonResult() { Data = slist, JsonRequestBehavior = JsonRequestBehavior.AllowGet, MaxJsonLength = Int32.MaxValue };
      
        //}

        public static SelectList PropertyType(string PropertyTypeID)
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            var Firms = modelRepo.GetPropertyType();

            List<SelectListItem> _Listfirms = new List<SelectListItem>();
         
                foreach (var item in Firms)
                {
                    SelectListItem itr = new SelectListItem();
                    itr.Text = item.Name;
                    itr.Value = item.ID.ToString();
                    itr.Selected = false;

                    _Listfirms.Add(itr);
                }
          
            return new SelectList(_Listfirms, "Value", "Text", PropertyTypeID);
        }
        public static SelectList TypeHotelClass(string TypeHotelClassID)
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            var Firms = modelRepo.GetPropertyClass();

            List<SelectListItem> _Listfirms = new List<SelectListItem>();
           
                foreach (var item in Firms)
                {
                    SelectListItem itr = new SelectListItem();
                    itr.Text = item.Name;
                    itr.Value = item.ID.ToString();
                    itr.Selected = false;

                    _Listfirms.Add(itr);
                }
         
            return new SelectList(_Listfirms, "Value", "Text", TypeHotelClassID);
        }

        public static SelectList TypeHotelChain(string TypeHotelChainID)
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            var Firms = modelRepo.GetTypeHotelChain();

            List<SelectListItem> _Listfirms = new List<SelectListItem>();
       
                foreach (var item in Firms)
                {
                    SelectListItem itr = new SelectListItem();
                    itr.Text = item.Name;
                    itr.Value = item.ID.ToString();
                    itr.Selected = false;

                    _Listfirms.Add(itr);
                }
           
            return new SelectList(_Listfirms, "Value", "Text", TypeHotelChainID);
        }

        public static SelectList TypeHotelAccommodation(string TypeHotelAccommodationID)
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            var Firms = modelRepo.GetTypeHotelAccommodation();

            List<SelectListItem> _Listfirms = new List<SelectListItem>();
           
                foreach (var item in Firms)
                {
                    SelectListItem itr = new SelectListItem();
                    itr.Text = item.Name;
                    itr.Value = item.ID.ToString();
                    itr.Selected = false;

                    _Listfirms.Add(itr);
                }
      
            return new SelectList(_Listfirms, "Value", "Text", TypeHotelAccommodationID);
        }

        //public static SelectList GetHotelCreditCardList(string HotelCreditCardListID)
        //{
        //    DropDownListsRepository modelRepo = new DropDownListsRepository();
        //    var Firms = modelRepo.GetHotelCreditCardList(HotelCreditCardListID);

        //    List<string> _Listfirms = new List<string>();

        //        foreach (var item in Firms)
        //        {
        //            SelectListItem itr = new SelectListItem();
        //           // itr.Text = item.Name;
        //            //itr.Value = item.ID.ToString();
        //            //itr.Selected = false;

        //            _Listfirms.Add(item.ID.ToString());
        //        }

   
        //    return new SelectList(_Listfirms);
        //}
        public static ArrayList GetHotelCreditCardList(string HotelCreditCardListID)
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            var Firms = modelRepo.GetHotelCreditCardList(HotelCreditCardListID);

            ArrayList list = new ArrayList();

            foreach (var item in Firms)
            {
                SelectListItem itr = new SelectListItem();
                // itr.Text = item.Name;
                //itr.Value = item.ID.ToString();
                //itr.Selected = false;

                list.Add(item.ID.ToString());
            }


            return new ArrayList(list);
        }
        public static SelectList GetAllCreditCardList()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            var Firms = modelRepo.GetCreditCardType();

            List<SelectListItem> _Listfirms = new List<SelectListItem>();
   
                foreach (var item in Firms)
                {
                    SelectListItem itr = new SelectListItem();
                    itr.Text = item.Name;
                    itr.Value = item.ID.ToString();
                    itr.Selected = false;

                    _Listfirms.Add(itr);
                }

            return new SelectList(_Listfirms, "Value", "Text");
        }
        public static SelectList GetChannelManager(string ChannelManagerID)
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            var Firms = modelRepo.GetChannelManager();

            List<SelectListItem> _Listfirms = new List<SelectListItem>();

                foreach (var item in Firms)
                {
                    SelectListItem itr = new SelectListItem();
                    itr.Text = item.Name;
                    itr.Value = item.ID.ToString();
                    itr.Selected = false;

                    _Listfirms.Add(itr);
                }

            return new SelectList(_Listfirms, "Value", "Text", ChannelManagerID);
        }

        public static SelectList GetCulture(string CultureID)
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            var Firms = modelRepo.GetCulture();

            List<SelectListItem> _Listfirms = new List<SelectListItem>();
    
                foreach (var item in Firms)
                {
                    SelectListItem itr = new SelectListItem();
                    itr.Text = item.Name;
                    itr.Value = item.ID.ToString();
                    itr.Selected = false;

                    _Listfirms.Add(itr);
                }
         
            return new SelectList(_Listfirms, "Value", "Text", CultureID);
        }

        public static SelectList FillYearList(string Year)
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            var Firms = modelRepo.FillYearList();

            List<SelectListItem> _Listfirms = new List<SelectListItem>();
           
                foreach (var item in Firms)
                {
                    SelectListItem itr = new SelectListItem();
                    itr.Text = item.Name;
                    itr.Value = item.ID.ToString();
                    itr.Selected = false;

                    _Listfirms.Add(itr);
                }
      
            return new SelectList(_Listfirms, "Value", "Text", Year);
        }

        public static SelectList FillTimeList(int starttime,int endtime, string Time)
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            var Firms = modelRepo.FillTimeList(starttime, endtime);

            List<SelectListItem> _Listfirms = new List<SelectListItem>();
           
                foreach (var item in Firms)
                {
                    SelectListItem itr = new SelectListItem();
                    itr.Text = item.TimeID;
                    itr.Value = item.TimeID.ToString();
                    itr.Selected = false;

                    _Listfirms.Add(itr);
                }
         
            return new SelectList(_Listfirms, "Value", "Text", Time);
        }

        public static SelectList GetCurrency(int CurrencyID)
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            var Currency = modelRepo.ReadCurrencies();

            List<SelectListItem> _ListCurrency = new List<SelectListItem>();
          
                foreach (var item in Currency)
                {
                    SelectListItem itr = new SelectListItem();
                    itr.Text = item.Name;
                    itr.Value = item.ID.ToString();
                    itr.Selected = false;

                    _ListCurrency.Add(itr);
                }
         
            return new SelectList(_ListCurrency, "Value", "Text", CurrencyID);
        }
        public static SelectList GetStatus(long? StatusID)
        {
            CommonRepository modelRepo = new CommonRepository();
            var status = modelRepo.ReadAllStatus();

            List<SelectListItem> _ListStatus = new List<SelectListItem>();
          
                foreach (var item in status)
                {
                    SelectListItem itr = new SelectListItem();
                    itr.Text = item.StatusName;
                    itr.Value = item.StatusID.ToString();
                    itr.Selected = false;

                    _ListStatus.Add(itr);
                }
         
            return new SelectList(_ListStatus, "Value", "Text", StatusID);
        }

        public static SelectList GetTitle(long? TitleID)
        {
            CommonRepository modelRepo = new CommonRepository();
            var status = modelRepo.ReadAllTitles();

            List<SelectListItem> _ListTitle = new List<SelectListItem>();
          
                foreach (var item in status)
                {
                    SelectListItem itr = new SelectListItem();
                    itr.Text = item.TitleName;
                    itr.Value = item.TitleID.ToString();
                    itr.Selected = false;

                    _ListTitle.Add(itr);
                }
          
            return new SelectList(_ListTitle, "Value", "Text", TitleID);
        }

        public static SelectList GetListOfTables()
        {
            List<SelectListItem> _ListItems = new List<SelectListItem>();

            TablesRepositary tbleobject = new TablesRepositary();
            List<TablesExt> list = new List<TablesExt>();
            DataTable dt = new DataTable();
            string CultureCode = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            dt = tbleobject.GetTables(CultureCode);
            if (dt != null)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    SelectListItem itr = new SelectListItem();
                    itr.Text = dr["Name"].ToString();
                    itr.Value = dr["ID"].ToString();
                    itr.Selected = false;

                    _ListItems.Add(itr);
                }
            }

            return new SelectList(_ListItems, "Value", "Text");
        }

        public static SelectList GetListEmailTemplates(int? SelectedItem)
        {

            DropDownListsRepository modelRepo = new DropDownListsRepository();

            var emailTemplates = modelRepo.ReadTemplate();

            List<SelectListItem> _ListItems = new List<SelectListItem>();

            TablesRepositary tbleobject = new TablesRepositary();
            List<TablesExt> list = new List<TablesExt>();
            foreach (var dr in emailTemplates)
                {
                    SelectListItem itr = new SelectListItem();
                    itr.Text = dr.Name;
                    itr.Value = dr.ID.ToString();
                    itr.Selected = false;

                    _ListItems.Add(itr);
                }

            return new SelectList(_ListItems, "Value", "Text", SelectedItem);
        }


        public static SelectList GetRoomType()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            var RoomType = modelRepo.GetRoomType();

            List<SelectListItem> _ListRoomType = new List<SelectListItem>();

            foreach (var item in RoomType)
            {
                SelectListItem itr = new SelectListItem();
                itr.Text = item.Name;
                itr.Value = item.ID.ToString();
                itr.Selected = false;

                _ListRoomType.Add(itr);
            }

            return new SelectList(_ListRoomType, "Value", "Text");
        }

        public static SelectList GetSmokingType()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            var SmokingStatus = modelRepo.GetSmokingType();

            List<SelectListItem> _ListSmokingStatus = new List<SelectListItem>();

            foreach (var item in SmokingStatus)
            {
                SelectListItem itr = new SelectListItem();
                itr.Text = item.Name;
                itr.Value = item.ID.ToString();
                itr.Selected = false;

                _ListSmokingStatus.Add(itr);
            }

            return new SelectList(_ListSmokingStatus, "Value", "Text");
        }


        public static SelectList GetTypeView()
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            var TypeView = modelRepo.GetTypeView();

            List<SelectListItem> _ListTypeView = new List<SelectListItem>();

            foreach (var item in TypeView)
            {
                SelectListItem itr = new SelectListItem();
                itr.Text = item.Name;
                itr.Value = item.ID.ToString();
                itr.Selected = false;

                _ListTypeView.Add(itr);
            }

            return new SelectList(_ListTypeView, "Value", "Text");
        }

        public static SelectList GetPenaltyRatedropdown(int PenaltyRateTypeID)
        {
            if(PenaltyRateTypeID==0)
            {
                PenaltyRateTypeID = 1;
            }
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            var penaltyrate = modelRepo.GetPenaltyRate();

            List<SelectListItem> _Listpenaltrate = new List<SelectListItem>();

            foreach (var item in penaltyrate)
            {
                SelectListItem itr = new SelectListItem();
                itr.Text = item.Name;
                itr.Value = item.ID.ToString();
                itr.Selected = false;

                _Listpenaltrate.Add(itr);
            }

            return new SelectList(_Listpenaltrate, "Value", "Text", PenaltyRateTypeID);
        }

        public static SelectList Getunitdropdown(int UnitID)
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            var penaltyrate = modelRepo.ReadUnit();

            List<SelectListItem> _Listpenaltrate = new List<SelectListItem>();

            foreach (var item in penaltyrate)
            {
                SelectListItem itr = new SelectListItem();
                itr.Text = item.Name;
                itr.Value = item.ID.ToString();
                itr.Selected = false;

                _Listpenaltrate.Add(itr);
            }

            return new SelectList(_Listpenaltrate, "Value", "Text", UnitID);
        }

        public static SelectList Getunitcurrencydropdown(int CurrencyID)
        {
            DropDownListsRepository modelRepo = new DropDownListsRepository();
            var penaltyrate = modelRepo.ReadCurrencies();

            List<SelectListItem> _Listpenaltrate = new List<SelectListItem>();

            foreach (var item in penaltyrate)
            {
                SelectListItem itr = new SelectListItem();
                itr.Text = item.Name;
                itr.Value = item.ID.ToString();
                itr.Selected = false;

                _Listpenaltrate.Add(itr);
            }

            return new SelectList(_Listpenaltrate, "Value", "Text", CurrencyID);
        }

        //TODO: Uncomment the below to get Select List of specific Entities

        //public static SelectList GetEntityName(List<EntityType> ListEntity, long? EntityID)
        //{
        //    List<SelectListItem> _ListEntity = new List<SelectListItem>();
        //    {
        //        foreach (var item in ListEntity)
        //        {
        //            SelectListItem itr = new SelectListItem();
        //            itr.Text = item.Value;
        //            itr.Value = item.ID.ToString();
        //            itr.Selected = false;

        //            _ListEntity.Add(itr);
        //        }
        //    }
        //    return new SelectList(_ListEntity, "Value", "Text", EntityID);
        //}
    }

    public class YesNo
    {
        public bool Value { get; set; }
        public string Text { get; set; }
        public string ValueStr { get; set; }

        public List<YesNo> buildYesNoNA()
        {
            List<YesNo> listofYesNoNA = new List<YesNo>();

            YesNo yes = new YesNo();
            yes.Text = "Yes";
            yes.ValueStr = "Yes";
            listofYesNoNA.Add(yes);

            YesNo no = new YesNo();
            no.Text = "No";
            no.ValueStr = "No";
            listofYesNoNA.Add(no);

            YesNo na = new YesNo();
            na.Text = "N/A";
            na.ValueStr = "N/A";
            listofYesNoNA.Add(na);

            return listofYesNoNA;
        }
    }
}