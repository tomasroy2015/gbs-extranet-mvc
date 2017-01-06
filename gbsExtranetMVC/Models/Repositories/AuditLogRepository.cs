using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace gbsExtranetMVC.Models.Repositories
{
    public class AuditLogRepository : BaseRepository
    {

        public AuditLogRepository()
            : base()
        {

        }
        //TODO: Uncomment the Following Code To Enable AuditLog Repository

        public AuditLogRepository(DBEntities dbContext)
            : base(dbContext)
        {

        }

        public void AddAuditLog(string _Action, int? userID)
        {
            //Add to Log

            tblAuditLogs log = new tblAuditLogs()
            {
                LogDateTime = DateTime.Now,
                Action = _Action,
                UserID = userID
            };

            //TODO: Uncomment the Following Lines to Enable AuditLog Function

            // db.AuditLog.Add(log);
            // db.SaveChanges();


        }

        /// <summary>
        /// Audit logging for use in transactions where the data context is passed in
        /// </summary>
        public void AddAuditLog(string _Action, int? userID, DBEntities db)
        {
            //Add to Log           

            tblAuditLogs log = new tblAuditLogs()
            {
                LogDateTime = DateTime.Now,
                Action = _Action,
                UserID = userID
            };

            //TODO: Uncomment the Following Line to Enable AuditLog Function

            // db.AuditLog.Add(log);

        }




    }
}