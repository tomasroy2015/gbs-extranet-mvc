using gbsExtranetMVC.Helpers;
using gbsExtranetMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using Business;
using System.Collections;
using gbsExtranetMVC.Models;
using Extension;

namespace gbsExtranetMVC.Models.Repositories
{
   
    public class FirmRepository : BaseRepository
    {
        public List<FirmExt> ReadAll()
        {
            List<FirmExt> ListOfModel = new List<FirmExt>();

            //Anshita: I have Successfully Referenced the Business DLL (which is VB.Net) in this Project and it is working perfectrly fine.
            //So, Now we don't have to right any new logic, just call the Functions and it will return the Datatable, and I have created a Simple function
            //which will convert the DataTable to List and Tadddaaaaaaa... We have required Information

            int TotalREcord = 0;
            //Business Logic, To get the Records from Database
            var dt = Business.BizFirm.GetFirms(BizDB,"ContactPersonName","en",10,1,ref TotalREcord,"","","","","","","","","","");


            //Use the Linq Query to get records from DataTable and Convert them to Required Object
            ListOfModel = (from DataRow dr in dt.Rows  
            select new FirmExt()  
            {  
                FirmID = Convert .ToInt32 (dr["ID"]),  
                Name = dr["Name"].ToString(),
                CountryID = Convert.ToInt32(dr["CountryID"].ToString()),
                Country = dr["CountryName"].ToString(),
                RegionName = dr["RegionName"].ToString(),
                Address = dr["Address"].ToString(),
                Active=  dr["Active"].ToString(),
                CityName = dr["CityName"].ToString(),
                CreateDate = (Convert.ToDateTime(dr["CreateDateTime"])).ToString("dd MMMM yyyy"),
                UpdateDate = (Convert.ToDateTime(dr["OpDateTime"])).ToString("dd MMMM yyyy"),
                ExecutiveEmail = dr["ContactPersonEmail"].ToString(),
                ExecutiveName = dr["ContactPersonName"].ToString(),
                ExecutivePhone = dr["ContactPersonPhone"].ToString(),
                ExecutivePosition = dr["ContactPersonPosition"].ToString(),
                ExecutiveSurname = dr["ContactPersonSurname"].ToString(),
                ExecutiveTitle = dr["ContactPersonSalutationTypeName"].ToString(),
                ExecutiveTitleID = Convert.ToInt32(dr["ContactPersonSalutationTypeID"].ToString()),
                Fax = dr["Fax"].ToString(),
                Phone = dr["Phone"].ToString(),
                Postcode = dr["PostCode"].ToString(),
                StatusID = Convert.ToInt32(dr["StatusID"].ToString()),
                Status = dr["StatusName"].ToString(),
                TaxID = dr["TaxNo"].ToString(),
                TaxOffice = dr["TaxDepartment"].ToString(),
                IPAddress =dr["IPAddress"].ToString(),
                IsActive = Convert.ToBoolean(dr["Active"].ToString())

            }).ToList();


          //ListOfModel = mytest;// dt.DataTableToList<Business.TB_Firm>();
            
            return ListOfModel;
        }

        public bool Create(FirmExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            //Wrap it all in a transaction
            
            
            TransactionOptions transOptions = SetTransactionTimeoutForDebugging(HttpContext.Current);

            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, transOptions))
            {
                //if (db.tblFirm.Any(u => u.Firm.ToLower().Equals(model.Firm)))
                //{
                //    status = false;
                //    Msg = "Firm \"" + model.Firm + "\" already Exists.";
                //}
                //else
                //{
                //    tblFirm tblModel = Map(model);

                //    db.tblFirm.Add(tblModel);
                //    db.SaveChanges();

                //    // UserID = tbluser.UserID;
                //    //Add to Audit Log
                //    AuditLog(model, "Firm has been Added. Firm = " + tblModel.Firm, ctrl);
                //    transaction.Complete();
                //}
            }
            return status;
        }

        public bool Update(FirmExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            //Wrap it all in a transaction
            TransactionOptions transOptions = SetTransactionTimeoutForDebugging(HttpContext.Current);

            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, transOptions))
            {
                //if (db.tblFirm.Any(u => u.Firm.ToLower().Equals(model.Firm)))
                //{
                //    status = false;
                //    Msg = "Firm \"" + model.Firm + "\" already Exists.";
                //}
                //else
                //{
                //    //TODO: Map to DB Object
                //    tblFirm tblModel = Map(model);

                //    db.tblFirm.Attach(tblModel);
                //    db.Entry(tblModel).State = System.Data.Entity.EntityState.Modified;
                //    db.SaveChanges();

                //    //TOD: Add to Audit Log
                //    AuditLog(model, "Firm has been Updated. Firm = " + tblModel.Firm, ctrl);

                //    //To get here, everything must be OK, so commit the transaction
                //    transaction.Complete();
                //}
            }

            return status;
        }

        public bool Delete(FirmExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            //Wrap it all in a transaction
            TransactionOptions transOptions = SetTransactionTimeoutForDebugging(HttpContext.Current);

            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, transOptions))
            {
                //TODO: Get Current Object from DB
                //var ItemToDelete = db.tblFirm.FirstOrDefault(m => m.FirmID == model.FirmID);
                //if (ItemToDelete != null)
                //{
                //    //TODO: Check if it is not null, then Remove from DB
                //    db.tblFirm.Remove(ItemToDelete);
                //    db.SaveChanges();

                //    //Add To Log
                //    AuditLog(model, "Firm has been Deleted. Firm = " + model.Firm, ctrl);

                //    //To get here, everything must be OK, so commit the transaction
                //    transaction.Complete();
                //}
                //else
                //{
                //    status = false;
                //    Msg = "Unable to Delete Selected Record.";
                //}
            }

            return status;
        }

        public TB_Firm Map(FirmExt model)
        {
            TB_Firm tblModel = new TB_Firm()
            {
                ID = model.FirmID,
                Name  = model.Name
            };

            if (model.RowVersion_Str != null)
            {
              //  tblModel.RowVersion = Convert.FromBase64String(model.RowVersion_Str);
            }

            return tblModel;
        }

        public FirmExt Map(TB_Firm model)
        {
            FirmExt tblModel = new FirmExt()
            {
                FirmID = model.ID,
                Name = model.Name,
               /// RowVersion_Byte = model.RowVersion
            };

            tblModel.RowVersion_Str = Convert.ToBase64String(tblModel.RowVersion_Byte);

            return tblModel;
        }

        /// <summary>
        /// Add the Action to Audit Log
        /// </summary>
        /// <param name="model">The Object for which this Audit log took place</param>
        /// <param name="Action">"Added New User OR Updated User Details OR Deleted User"</param>
        private void AuditLog(FirmExt model, string Action, Controller ctrl)
        {
            SecurityUtils.AddAuditLog(Action, null, db, ctrl);
        }

       
    }

    public class FirmExt
    {
        public int FirmID { get; set; }

        [Required(ErrorMessage = "Please Enter Firm")]
        public string Name { get; set; }

        public int CountryID { get; set; }

        public string Country { get; set; }

        public string RegionName { get; set; }

        public string CityName { get; set; }

        public string Address { get; set; }

        public string Postcode { get; set; }

        public string Phone { get; set; }

        public string Fax { get; set; }

        public string TaxOffice { get; set; }

        public string TaxID { get; set; }

        public int ExecutiveTitleID { get; set; }

        public string ExecutiveTitle { get; set; }

        public string ExecutiveName { get; set; }

        public string ExecutiveSurname { get; set; }

        public string ExecutivePosition { get; set; }

        public string ExecutivePhone { get; set; }

        public string ExecutiveEmail { get; set; }

        public int StatusID { get; set; }

        public string Status { get; set; }

        public string Active { get; set; }

        public bool IsActive { get; set; } /// used for add & edit checkbox

        public string CreateDate { get; set; }

        public string UpdateDate { get; set; }

        public string IPAddress { get; set; }

        public byte[] RowVersion_Byte { get; set; }
        public string RowVersion_Str { get; set; }
    }

    
}