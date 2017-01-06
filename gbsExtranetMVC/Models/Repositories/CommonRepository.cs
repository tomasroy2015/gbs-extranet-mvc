using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace gbsExtranetMVC.Models.Repositories
{
    public class CommonRepository : BaseRepository
    {

        string Culture = System.Threading.Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;
        public List<Status> ReadAllStatus()
        {
            //string Culture = "en";

            var statuslist = db.TB_TypeStatus.ToList();

            // BizUtil.LoadList(BizDB,Culture,)

            var result = (from c in statuslist
                          select new TB_TypeStatus
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

            List<Status> ListofStatus = new List<Status>();

            switch (Culture)
            {
                case "en":
                    ListofStatus = (from item in result
                                       select new Status
                                       {
                                           StatusID = item.ID,
                                           StatusName = item.Name_en
                                       }).ToList();
                    break;

                case "fr":
                    ListofStatus = (from item in result
                                       select new Status
                                       {
                                           StatusID = item.ID,
                                           StatusName = item.Name_fr
                                       }).ToList();
                    break;

                case "ar":
                    ListofStatus = (from item in result
                                    select new Status
                                    {
                                        StatusID = item.ID,
                                        StatusName = item.Name_ar
                                    }).ToList();
                    break;

                case "tr":
                    ListofStatus = (from item in result
                                    select new Status
                                    {
                                        StatusID = item.ID,
                                        StatusName = item.Name_tr
                                    }).ToList();
                    break;

                case "de":
                    ListofStatus = (from item in result
                                    select new Status
                                    {
                                        StatusID = item.ID,
                                        StatusName = item.Name_de
                                    }).ToList();
                    break;

                case "ru":
                    ListofStatus = (from item in result
                                    select new Status
                                    {
                                        StatusID = item.ID,
                                        StatusName = item.Name_ru
                                    }).ToList();
                    break;

                default:
                    ListofStatus = (from item in result
                                       select new Status
                                       {
                                           StatusID = item.ID,
                                           StatusName = item.Name_en
                                       }).ToList();

                    break;
            }


            return ListofStatus;
        }

        public List<Title> ReadAllTitles()
        {
            //string Culture = "en";

            var titlelist = db.TB_TypeSalutation.ToList();

            // BizUtil.LoadList(BizDB,Culture,)

            var result = (from c in titlelist
                          select new TB_TypeSalutation
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

            List<Title> ListofTitle = new List<Title>();

            switch (Culture)
            {
                case "en":
                    ListofTitle = (from item in result
                                    select new Title
                                    {
                                        TitleID = item.ID,
                                        TitleName = item.Name_en
                                    }).ToList();
                    break;

                case "fr":
                    ListofTitle = (from item in result
                                   select new Title
                                    {
                                        TitleID = item.ID,
                                        TitleName = item.Name_fr
                                    }).ToList();
                    break;

                case "ar":
                    ListofTitle = (from item in result
                                   select new Title
                                   {
                                       TitleID = item.ID,
                                       TitleName = item.Name_ar
                                   }).ToList();
                    break;

                case "tr":
                    ListofTitle = (from item in result
                                   select new Title
                                   {
                                       TitleID = item.ID,
                                       TitleName = item.Name_tr
                                   }).ToList();
                    break;

                case "ru":
                    ListofTitle = (from item in result
                                   select new Title
                                   {
                                       TitleID = item.ID,
                                       TitleName = item.Name_ru
                                   }).ToList();
                    break;

                case "de":
                    ListofTitle = (from item in result
                                   select new Title
                                   {
                                       TitleID = item.ID,
                                       TitleName = item.Name_de
                                   }).ToList();
                    break;

                default:
                    ListofTitle = (from item in result
                                   select new Title
                                    {
                                        TitleID = item.ID,
                                        TitleName = item.Name_en
                                    }).ToList();

                    break;
            }


            return ListofTitle;
        }
        public object CheckEmptyStringDBParameter(object Value, bool ReturnInteger = false, bool ReturnDate = false, bool ReturnDouble = false, bool ReturnDecimal = false, bool ReturnBoolean = false, bool ReturnLong = false)
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
            //    DateTime StrValue = Convert.ToDateTime(Value);
            //    return (DateTime)DateTime.Parse(StrValue).ToString("yyyy-MM-dd");
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
        public string GetIPAddress()
        {
            string UserIP = "";
            try
            {

                UserIP = System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];
                if (string.IsNullOrEmpty(UserIP))
                {
                    UserIP = System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
                }



                if (UserIP == "::1")
                {
                    UserIP = "127.0.0.1";
                }
            }
            catch
            {

            }
            return UserIP;
        }
    }



    public class Status
    {
        public int StatusID { get; set; }
        public string StatusName { get; set; }
    }

    public class Title
    {
        public int TitleID { get; set; }
        public string TitleName { get; set; }
    }


}