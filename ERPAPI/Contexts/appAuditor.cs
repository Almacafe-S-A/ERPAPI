using ERP.Contexts;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text;

namespace ERPAPI.Contexts
{
    public class appAuditor
    {
        public static ApplicationDbContext _context = null;
        public static ILogger _logger;
        public static void SetAuditoria(EntityEntry pEntry, string psUsuario = "SYSTEM")
        {
            try
            {
                var newValues = new StringBuilder();
                var oldValues = new StringBuilder();
                var primaryKeys = new StringBuilder();

                string Action = "";
                string Reference = pEntry.Entity.GetType().Name;
                GetPrimaryKey(pEntry, primaryKeys);

                switch (pEntry.State)
                {
                    case Microsoft.EntityFrameworkCore.EntityState.Deleted:
                        Action = "D";
                        GetDeleteValues(pEntry, oldValues);
                        break;
                    case Microsoft.EntityFrameworkCore.EntityState.Modified:
                        Action = "U";
                        GetUpdateValues(pEntry, newValues, oldValues);
                        break;
                    case Microsoft.EntityFrameworkCore.EntityState.Added:
                        Action = "I";
                        GetNewValues(pEntry, newValues);
                        break;
                    default:
                        break;
                }

                string x = Convert.ToString(newValues );
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { GetFullException(ex) }");
            }
        }

        static void GetPrimaryKey(EntityEntry pEntry, StringBuilder primaryKey)
        {
            try
            {
                var ObjectEntry = pEntry.Metadata.FindPrimaryKey().Properties.Select(p => pEntry.Property(p.Name).CurrentValue).ToArray();
                string keys = string.Empty;
                foreach (var item in ObjectEntry)
                    keys += $"{Convert.ToString(item)} || ";

                if (!string.IsNullOrEmpty(keys))
                    keys = keys.Substring(0, (keys.Length - 3));
                primaryKey.Append(keys);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error appAuditor.GetPrimaryKey: { ex.ToString() }");
            }
        }


        static void GetNewValues(EntityEntry pEntry, StringBuilder newValues) {
            try
            {
                foreach (var item in pEntry.CurrentValues.Properties)
                {
                    var vValues = pEntry.CurrentValues[item.Name];
                    if (vValues != null)
                        newValues.AppendFormat("{0} = {1} , ", item.Name, vValues);
                }

                if (newValues.Length > 0)
                    newValues = newValues.Remove(newValues.Length - 3, 3);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error appAuditor.GetNewValues: { GetFullException(ex) }");
            }
        }


        static void GetUpdateValues(EntityEntry pEntry, StringBuilder newValues, StringBuilder oldValues) {
            try
            {
                PropertyValues dbValues = pEntry.GetDatabaseValues();
                foreach (var item in pEntry.OriginalValues.Properties)
                {
                    if (dbValues != null) {
                        var vOldValues = dbValues[item.Name];
                        var vNewValues = pEntry.CurrentValues[item.Name];

                        if (vOldValues != null && vNewValues != null && !Equals(vOldValues, vNewValues)) {
                            newValues.AppendFormat("{0} = {1} , ", item.Name, vNewValues);
                            oldValues.AppendFormat("{0} = {1} , ", item.Name, vOldValues);
                        }
                    }
                }

                if (newValues.Length > 0)
                    newValues = newValues.Remove(newValues.Length - 3, 3);

                if (oldValues.Length > 0)
                    oldValues = oldValues.Remove(oldValues.Length - 3, 3);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error appAuditor.GetNewValues: { GetFullException(ex) }");
            }
        }


        static void GetDeleteValues(EntityEntry pEntry, StringBuilder oldValues) {
            try
            {
                PropertyValues dbValues = pEntry.GetDatabaseValues();
                foreach (var item in dbValues.Properties)
                {
                    var vOldValues = dbValues[item.Name];
                    if(vOldValues!= null)
                        oldValues.AppendFormat("{0} = {1} , ", item.Name, vOldValues);
                }
                if (oldValues.Length > 0)
                    oldValues = oldValues.Remove(oldValues.Length - 3, 3);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error appAuditor.GetNewValues: { GetFullException(ex) }");
            }
        }



        private static string GetFullException(Exception Exception)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(Exception.Message);
            Exception ex = Exception.InnerException;
            while (ex != null)
            {
                sb.Append(ex.Message);
                ex = ex.InnerException;
            }
            return Convert.ToString(sb);
        }
    }
}
