using gbsExtranetMVC.Helpers;
using System.Collections.Generic;
using System.Transactions;
using System.Web;
using System.Web.Mvc;


namespace gbsExtranetMVC.Models.Repositories
{
    public class UsersRepository : BaseRepository
    {
        public List<UsersExt> ReadAll()
        {
            List<UsersExt> ListOfUser = new List<UsersExt>();

            //use Linq query to get data

            return ListOfUser;
        }

       
        public bool Create(UsersExt model, ModelStateDictionary modelState, Controller ctrl)
        {
            bool status = true;
            //Wrap it all in a transaction

            TransactionOptions transOptions = SetTransactionTimeoutForDebugging(HttpContext.Current);

            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, transOptions))
            {
                //if (db.tblUsers.Any(u => u.Username.ToLower().Equals(model.Username)))
                //{
                //    status = false;
                //    modelState.AddModelError("Username", "Username already Exists.");

                //}
                //else
                //{

                //    tblUsers tbluser = Map(model);

                //    tbluser.Password = SecurityUtils.EncryptText(tbluser.Password);

                //    db.tblUsers.Add(tbluser);
                //    db.SaveChanges();

                //    // UserID = tbluser.UserID;
                //    //Add to Audit Log
                //    SecurityUtils.AddAuditLog("User has been Added. User FullName = " + model.Fullname, ctrl);
                //    transaction.Complete();
                //}
            }
            return status;
        }

        public bool Update(UsersExt model, ModelStateDictionary modelState, Controller ctrl)
        {
            bool status = true;
            //Wrap it all in a transaction
            TransactionOptions transOptions = SetTransactionTimeoutForDebugging(HttpContext.Current);

            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, transOptions))
            {
                //if (db.tblUsers.Any(u => u.UserID != model.UserID && u.Username.ToLower().Equals(model.Username)))
                //{
                //    status = false;
                //    modelState.AddModelError("Username", "Username already Exists.");
                //}
                //else
                //{
                //    //TODO: Map to DB Object
                //    tblUsers tbluser = Map(model);

                //    tbluser.Password = SecurityUtils.EncryptText(tbluser.Password);

                //    db.tblUsers.Attach(tbluser);
                //    db.Entry(tbluser).State = System.Data.Entity.EntityState.Modified;
                //    db.SaveChanges();

                //    //TOD: Add to Audit Log
                //    SecurityUtils.AddAuditLog("User Details has been Updated. User FullName = " + model.Fullname, ctrl);

                //    //To get here, everything must be OK, so commit the transaction
                //    transaction.Complete();
                //}
            }

            return status;
        }

        public bool Delete(UsersExt model, Controller ctrl)
        {
            bool status = true;
            //Wrap it all in a transaction
            TransactionOptions transOptions = SetTransactionTimeoutForDebugging(HttpContext.Current);

            using (TransactionScope transaction = new TransactionScope(TransactionScopeOption.Required, transOptions))
            {
                //TODO: Get Current Object from DB
                //tblUsers tbluser = Map(model);

                ////TODO: Check if it is not null, then Remove from DB
                //db.tblUsers.Remove(tbluser);
                //db.SaveChanges();

                ////Add To Log
                //SecurityUtils.AddAuditLog("User has been Deleted FullName = " + model.Fullname, ctrl);


                ////To get here, everything must be OK, so commit the transaction
                //transaction.Complete();
            }

            return status;
        }

        //public tblUsers Map(UsersExt model)
        //{
        //    tblUsers tbluser = new tblUsers()
        //    {
        //        UserID = model.UserID,
        //        Fullname = model.Fullname,
        //        Username = model.Username,
        //        Password = model.Password,
        //        EmailAddress = model.EmailAddress,
        //        RoleID = model.RoleID,
        //        Locked = model.Locked
        //    };

        //    if (model.RowVersion_Str != null)
        //    {
        //        tbluser.RowVersion = Convert.FromBase64String(model.RowVersion_Str);
        //    }

        //    return tbluser;
        //}

        //public UsersExt Map(tblUsers model)
        //{
        //    UsersExt tbluser = new UsersExt()
        //    {
        //        UserID = model.UserID,
        //        Fullname = model.Fullname,
        //        Username = model.Username,
        //        Password = model.Password,
        //        EmailAddress = model.EmailAddress,
        //        RoleID = model.RoleID,
        //        Locked = model.Locked,
        //        RowVersion_Byte = model.RowVersion

        //    };

        //    tbluser.RowVersion_Str = Convert.ToBase64String(tbluser.RowVersion_Byte);

        //    return tbluser;
        //}
        /// <summary>
        /// Add the Action to Audit Log
        /// </summary>
        /// <param name="model">The Object for which this Auditlog took place</param>
        /// <param name="Action">"Added New User OR Updated User Details OR Deleted User"</param>
        private void AuditLog(UsersExt model, string Action, Controller ctrl)
        {
            SecurityUtils.AddAuditLog(Action +
                                " Username = \"" + model.Username + "\"", null, db, ctrl);
        }
    }
}