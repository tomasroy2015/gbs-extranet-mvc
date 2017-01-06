using gbsExtranetMVC.Helpers;
using gbsExtranetMVC.Models.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;

namespace gbsExtranetMVC.Models.Repositories
{

    public class TempRepository : BaseRepository
    {
        public List<TempExt> ReadAll()
        {
            List<TempExt> ListOfModel = new List<TempExt>();

            //ListOfModel = (from m in db.tblTemp
            //               orderby m.Temp
            //               select new TempExt
            //               {
            //                   TempID = m.TempID,
            //                   Temp = m.Temp,
            //                   RowVersion_Byte = m.RowVersion
            //               }).ToList();

            //foreach (var item in ListOfModel)
            //{
            //    item.RowVersion_Str = Convert.ToBase64String(item.RowVersion_Byte);
            //}

            return ListOfModel;
        }

        public bool Create(TempExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            //Wrap it all in a transaction

            TransactionOptions transOptions = SetTransactionTimeoutForDebugging(HttpContext.Current);

            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, transOptions))
            {
                //if (db.tblTemp.Any(u => u.Temp.ToLower().Equals(model.Temp)))
                //{
                //    status = false;
                //    Msg = "Temp \"" + model.Temp + "\" already Exists.";
                //}
                //else
                //{
                //    tblTemp tblModel = Map(model);

                //    db.tblTemp.Add(tblModel);
                //    db.SaveChanges();

                //    // UserID = tbluser.UserID;
                //    //Add to Audit Log
                //    AuditLog(model, "Temp has been Added. Temp = " + tblModel.Temp, ctrl);
                //    transaction.Complete();
                //}
            }
            return status;
        }

        public bool Update(TempExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            //Wrap it all in a transaction
            TransactionOptions transOptions = SetTransactionTimeoutForDebugging(HttpContext.Current);

            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, transOptions))
            {
                //if (db.tblTemp.Any(u => u.Temp.ToLower().Equals(model.Temp)))
                //{
                //    status = false;
                //    Msg = "Temp \"" + model.Temp + "\" already Exists.";
                //}
                //else
                //{
                //    //TODO: Map to DB Object
                //    tblTemp tblModel = Map(model);

                //    db.tblTemp.Attach(tblModel);
                //    db.Entry(tblModel).State = System.Data.Entity.EntityState.Modified;
                //    db.SaveChanges();

                //    //TOD: Add to Audit Log
                //    AuditLog(model, "Temp has been Updated. Temp = " + tblModel.Temp, ctrl);

                //    //To get here, everything must be OK, so commit the transaction
                //    transaction.Complete();
                //}
            }

            return status;
        }

        public bool Delete(TempExt model, ref string Msg, Controller ctrl)
        {
            bool status = true;
            //Wrap it all in a transaction
            TransactionOptions transOptions = SetTransactionTimeoutForDebugging(HttpContext.Current);

            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, transOptions))
            {
                //TODO: Get Current Object from DB
                //var ItemToDelete = db.tblTemp.FirstOrDefault(m => m.TempID == model.TempID);
                //if (ItemToDelete != null)
                //{
                //    //TODO: Check if it is not null, then Remove from DB
                //    db.tblTemp.Remove(ItemToDelete);
                //    db.SaveChanges();

                //    //Add To Log
                //    AuditLog(model, "Temp has been Deleted. Temp = " + model.Temp, ctrl);

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

        public tblTemp Map(TempExt model)
        {
            tblTemp tblModel = new tblTemp()
            {
                TempID = model.TempID,
                Temp = model.Temp
            };

            if (model.RowVersion_Str != null)
            {
                tblModel.RowVersion = Convert.FromBase64String(model.RowVersion_Str);
            }

            return tblModel;
        }

        public TempExt Map(tblTemp model)
        {
            TempExt tblModel = new TempExt()
            {
                TempID = model.TempID,
                Temp = model.Temp,
                RowVersion_Byte = model.RowVersion
            };

            tblModel.RowVersion_Str = Convert.ToBase64String(tblModel.RowVersion_Byte);

            return tblModel;
        }

        /// <summary>
        /// Add the Action to Audit Log
        /// </summary>
        /// <param name="model">The Object for which this Audit log took place</param>
        /// <param name="Action">"Added New User OR Updated User Details OR Deleted User"</param>
        private void AuditLog(TempExt model, string Action, Controller ctrl)
        {
            SecurityUtils.AddAuditLog(Action, null, db, ctrl);
        }
    }

    public class TempExt
    {
        public long TempID { get; set; }

        [Required(ErrorMessage = "Please Enter Temp")]
        public string Temp { get; set; }

        public byte[] RowVersion_Byte { get; set; }
        public string RowVersion_Str { get; set; }
    }
}