using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Resources.Abstract;
using Resources.Entities;
using gbsExtranetMVC.Models;

namespace Resources.Concrete
{
    public class DbResourceProvider : BaseResourceProvider
    {
        // Database connection string        
        private static string connectionString = null;

        public DbResourceProvider()
        {

            // connectionString = ConfigurationManager.ConnectionStrings["MvcInternationalization"].ConnectionString;
        }

        public DbResourceProvider(string connection)
        {
            // connectionString = connection;
        }

        protected override IList<ResourceEntry> ReadResources()
        {
            var resources = new List<ResourceEntry>();

            using (DBEntities db = new DBEntities())
            {
                var culture = db.BizTbl_Message.ToList();


                foreach (var item in culture)
                {
                    {
                        //Türkçe
                        ResourceEntry rs = new ResourceEntry();
                        rs.Culture = "tr-TR";
                        rs.Name = item.Code;
                        rs.Value = item.Description_tr;
                        resources.Add(rs);
                    }

                    {
                        //English us
                        ResourceEntry rs = new ResourceEntry();
                        rs.Culture = "en-us";
                        rs.Name = item.Code;
                        rs.Value = item.Description_en;
                        resources.Add(rs);
                    }

                    {
                        //English GB
                        ResourceEntry rs = new ResourceEntry();
                        rs.Culture = "en-gb";
                        rs.Name = item.Code;
                        rs.Value = item.Description_en;
                        resources.Add(rs);
                    }

                    {
                        //Deutsch
                        ResourceEntry rs = new ResourceEntry();
                        rs.Culture = "de-DE";
                        rs.Name = item.Code;
                        rs.Value = item.Description_de;
                        resources.Add(rs);
                    }


                    {
                        //Español
                        ResourceEntry rs = new ResourceEntry();
                        rs.Culture = "es-ES";
                        rs.Name = item.Code;
                        rs.Value = item.Description_es;
                        resources.Add(rs);
                    }

                    {
                        //Français
                        ResourceEntry rs = new ResourceEntry();
                        rs.Culture = "fr-FR";
                        rs.Name = item.Code;
                        rs.Value = item.Description_fr;
                        resources.Add(rs);
                    }

                    {
                        //Русский
                        ResourceEntry rs = new ResourceEntry();
                        rs.Culture = "ru-RU";
                        rs.Name = item.Code;
                        rs.Value = item.Description_ru;
                        resources.Add(rs);
                    }
                    {
                        //Italiano
                        ResourceEntry rs = new ResourceEntry();
                        rs.Culture = "it-IT";
                        rs.Name = item.Code;
                        rs.Value = item.Description_it;
                        resources.Add(rs);
                    }

                    {
                        //العربية Arabic 
                        ResourceEntry rs = new ResourceEntry();
                        rs.Culture = "ar-SA";
                        rs.Name = item.Code;
                        rs.Value = item.Description_ar;
                        resources.Add(rs);
                    }

                    {
                        //Japanes 日本人
                        ResourceEntry rs = new ResourceEntry();
                        rs.Culture = "ja-JP";
                        rs.Name = item.Code;
                        rs.Value = item.Description_ja;
                        resources.Add(rs);
                    }

                    {
                        //Português Portugal
                        ResourceEntry rs = new ResourceEntry();
                        rs.Culture = "pt-PT";
                        rs.Name = item.Code;
                        rs.Value = item.Description_pt;
                        resources.Add(rs);
                    }

                    {
                        //Chinese
                        ResourceEntry rs = new ResourceEntry();
                        rs.Culture = "zh-CN";
                        rs.Name = item.Code;
                        rs.Value = item.Description_zh;
                        resources.Add(rs);
                    }
                }
            }


            //const string sql = "select Culture, Name, Value from dbo.Resources;";

            //using (var con = new SqlConnection(connectionString)) {
            //    var cmd = new SqlCommand(sql, con);

            //    con.Open();

            //    using (var reader = cmd.ExecuteReader()) {
            //        while (reader.Read()) {
            //            resources.Add(new ResourceEntry { 
            //                Name = reader["Name"].ToString(),
            //                Value = reader["Value"].ToString(),
            //                Culture = reader["Culture"].ToString()
            //            });
            //        }

            //        if (!reader.HasRows) throw new Exception("No resources were found");
            //    }
            //}

            return resources;

        }

        protected override ResourceEntry ReadResource(string name, string culture)
        {
            ResourceEntry resource = null;

            var resources = new List<ResourceEntry>();

            using (DBEntities db = new DBEntities())
            {
                var dbculture = db.BizTbl_Message.ToList();


                foreach (var item in dbculture)
                {
                    {
                        //Türkçe
                        ResourceEntry rs = new ResourceEntry();
                        rs.Culture = "tr-TR";
                        rs.Name = item.Code;
                        rs.Value = item.Description_tr;
                        resources.Add(rs);
                    }

                    {
                        //English us
                        ResourceEntry rs = new ResourceEntry();
                        rs.Culture = "en-us";
                        rs.Name = item.Code;
                        rs.Value = item.Description_en;
                        resources.Add(rs);
                    }

                    {
                        //English GB
                        ResourceEntry rs = new ResourceEntry();
                        rs.Culture = "en-gb";
                        rs.Name = item.Code;
                        rs.Value = item.Description_en;
                        resources.Add(rs);
                    }

                    {
                        //Deutsch
                        ResourceEntry rs = new ResourceEntry();
                        rs.Culture = "de-DE";
                        rs.Name = item.Code;
                        rs.Value = item.Description_de;
                        resources.Add(rs);
                    }


                    {
                        //Español
                        ResourceEntry rs = new ResourceEntry();
                        rs.Culture = "es-ES";
                        rs.Name = item.Code;
                        rs.Value = item.Description_es;
                        resources.Add(rs);
                    }

                    {
                        //Français
                        ResourceEntry rs = new ResourceEntry();
                        rs.Culture = "fr-FR";
                        rs.Name = item.Code;
                        rs.Value = item.Description_fr;
                        resources.Add(rs);
                    }

                    {
                        //Русский
                        ResourceEntry rs = new ResourceEntry();
                        rs.Culture = "ru-RU";
                        rs.Name = item.Code;
                        rs.Value = item.Description_ru;
                        resources.Add(rs);
                    }
                    {
                        //Italiano
                        ResourceEntry rs = new ResourceEntry();
                        rs.Culture = "it-IT";
                        rs.Name = item.Code;
                        rs.Value = item.Description_it;
                        resources.Add(rs);
                    }

                    {
                        //العربية Arabic 
                        ResourceEntry rs = new ResourceEntry();
                        rs.Culture = "ar-SA";
                        rs.Name = item.Code;
                        rs.Value = item.Description_ar;
                        resources.Add(rs);
                    }

                    {
                        //Japanes 日本人
                        ResourceEntry rs = new ResourceEntry();
                        rs.Culture = "ja-JP";
                        rs.Name = item.Code;
                        rs.Value = item.Description_ja;
                        resources.Add(rs);
                    }

                    {
                        //Português Portugal
                        ResourceEntry rs = new ResourceEntry();
                        rs.Culture = "pt-PT";
                        rs.Name = item.Code;
                        rs.Value = item.Description_pt;
                        resources.Add(rs);
                    }

                    {
                        //Chinese
                        ResourceEntry rs = new ResourceEntry();
                        rs.Culture = "zh-CN";
                        rs.Name = item.Code;
                        rs.Value = item.Description_zh;
                        resources.Add(rs);
                    }
                }

                resource = resources.FirstOrDefault(f => f.Culture == culture);

            }

            //const string sql = "select Culture, Name, Value from dbo.Resources where culture = @culture and name = @name;";

            //using (var con = new SqlConnection(connectionString)) {
            //    var cmd = new SqlCommand(sql, con);

            //    cmd.Parameters.AddWithValue("@culture", culture);
            //    cmd.Parameters.AddWithValue("@name", name);

            //    con.Open();

            //    using (var reader = cmd.ExecuteReader()) {
            //        if (reader.Read()) {
            //            resource = new ResourceEntry {
            //                Name = reader["Name"].ToString(),
            //                Value = reader["Value"].ToString(),
            //                Culture = reader["Culture"].ToString()
            //            };
            //        }

            //        if (!reader.HasRows) throw new Exception(string.Format("Resource {0} for culture {1} was not found", name, culture));
            //    }
            //}

            return resource;

        }




    }
}
