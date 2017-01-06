using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
//using Business;
using System.Collections;

namespace gbsExtranetMVC.Models.Repositories
{
    public class CountriesRepository : BaseRepository
    {

        public List<Countries> ReadAll()
        {
            string Culture = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
          //  string Culture = "en";

            var countrylist = db.TB_Country.ToList();

           // BizUtil.LoadList(BizDB,Culture,)

            var countries = (from c in countrylist
                        select new TB_Country
                        {
                            ID = c.ID,
                            Name_en = c.Name_en,
                            Name_ar = c.Name_ar,
                            Name_de = c.Name_de,
                            Name_es = c.Name_es,
                            Name_fr = c.Name_fr,
                            Name_it = c.Name_it,
                            Name_ja = c.Name_ja
                        }).ToList();

            List<Countries> ListofCountries = new List<Countries>();

            switch (Culture)
            {
                case "en":
                    ListofCountries = (from country in countries
                                       select new Countries
                                       {
                                           CountryID = country.ID,
                                           CountryName = country.Name_en
                                       }).ToList();
                    break;

                case "fr":
                    ListofCountries = (from country in countries
                                       select new Countries
                                       {
                                           CountryID = country.ID,
                                           CountryName = country.Name_fr
                                       }).ToList();
                    break;

                case "de":
                    ListofCountries = (from country in countries
                                       select new Countries
                                       {
                                           CountryID = country.ID,
                                           CountryName = country.Name_de
                                       }).ToList();
                    break;

                case "ar":
                    ListofCountries = (from country in countries
                                       select new Countries
                                       {
                                           CountryID = country.ID,
                                           CountryName = country.Name_ar
                                       }).ToList();
                    break;

                case "ru":
                    ListofCountries = (from country in countries
                                       select new Countries
                                       {
                                           CountryID = country.ID,
                                           CountryName = country.Name_ru
                                       }).ToList();
                    break;
                case "tr":
                    ListofCountries = (from country in countries
                                       select new Countries
                                       {
                                           CountryID = country.ID,
                                           CountryName = country.Name_tr
                                       }).ToList();
                    break;

                default:
                    ListofCountries = (from country in countries
                                       select new Countries
                                       {
                                           CountryID = country.ID,
                                           CountryName = country.Name_en
                                       }).ToList();

                    break;
            }


            return ListofCountries;
        }

        public Business.TB_Country GetCountryInfoFromIPAddress(string UserIPAddress)
        {
          Business.TB_Country userCountryInfo = Business.BizApplication.GetCountryInfoFromIPAddress(BizDB, UserIPAddress);

          return userCountryInfo;
        }

        
    }

    public class Countries
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
    }

   
}