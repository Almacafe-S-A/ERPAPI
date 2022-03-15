using ERP.Contexts;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Contexts
{
    public class appAuditor
    {
        private static ApplicationDbContext _context = null;
        private static ILogger _logger;
        private static string Usuario = string.Empty;

        /// <summary>
        ///    Constructor de la clase de auditoria
        /// </summary>
        /// <param name="context"> Contexto del entty framework </param>
        /// <param name="logger"> Logger para los errores </param>
        /// <param name="entityState"> Estado de la entidad que se esta procesando </param>
        /// <param name="User"> Usuario que inicio la accion  </param>
        public appAuditor(ApplicationDbContext context, ILogger logger,  string User) {
            _context = context;
            _logger = logger;
            Usuario = User;
        }

        /// <summary>
        /// Se encarga de hacer el registro de la auditoria en la base de datos, debe ser llamado justo antes de la operacion AsyncSaveChanges del EF.
        /// </summary>
        public void SetAuditor() {
            try
            {
                foreach (EntityEntry pEntry in _context.ChangeTracker.Entries())
                {
                    SetAuditoria(pEntry,Usuario??"SYSTEM");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { GetFullException(ex) }");
            }
            
        }

        /// <summary>
        /// Obtiene los valores en formato de string para cada entrada nueva, modificada o eliminada de la base de datos.
        /// </summary>
        /// <param name="pEntry"> Entry que esta siendo modificada en el Context </param>
        /// <param name="psUsuario"> Usuario que inicio la accion</param>
        private void SetAuditoria(EntityEntry pEntry, string psUsuario)
        {
            try
            {
                var newValues = new StringBuilder();
                var oldValues = new StringBuilder();
                var primaryKeys = new StringBuilder();

                string Action = "";
                string Reference = pEntry.Entity.GetType().Name;
                bool Insert = false;

               
                switch (pEntry.State)
                {
                   
                    case EntityState.Deleted:
                        Action = "D";
                        GetPrimaryKey(pEntry, primaryKeys);
                        GetDeleteValues(pEntry, oldValues);
                        Insert = true;
                        break;
                    case EntityState.Modified:
                        Action = "U";
                        GetPrimaryKey(pEntry, primaryKeys);
                        GetUpdateValues(pEntry, newValues, oldValues);
                        Insert = true;
                        break;
                    case EntityState.Added:
                        Action = "I";
                        GetNewValues(pEntry, newValues);
                        Insert = true;
                        break;
                    default:
                        break;
                }
                if (Insert)
                    SaveAuditor(psUsuario, Reference, Convert.ToString(primaryKeys), Action, Convert.ToString(newValues), Convert.ToString(oldValues));

            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { GetFullException(ex) }");
            }
        }

        /// <summary>
        /// Obtiene las llaves primarias de una entidad
        /// </summary>
        /// <param name="pEntry"> Entdad que esta siendo alterada</param>
        /// <param name="primaryKey">StringBuilder donde se regresa la llave</param>
        private  void GetPrimaryKey(EntityEntry pEntry, StringBuilder primaryKey)
        {
            try
            {
                var ObjectEntry = pEntry.Metadata.FindPrimaryKey().Properties.Select(p => pEntry.Property(p.Name).CurrentValue).ToArray();
                string keys = string.Empty;
                foreach (var item in ObjectEntry) {
                    if (item != null) {
                        if (!(item.ToString().Equals(long.MinValue.ToString()) || item.ToString().Equals(Convert.ToString(long.MaxValue)))) 
                            keys += $"{Convert.ToString(item)} || ";                        
                    }
                   
                }
                   
                if (!string.IsNullOrEmpty(keys))
                    keys = keys.Substring(0, (keys.Length - 3));
                primaryKey.Append(keys);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error appAuditor.GetPrimaryKey: { ex.ToString() }");
            }
        }

        /// <summary>
        /// Obtiene los valores para una entida que esta siendo agregada
        /// </summary>
        /// <param name="pEntry"> Entdad que esta siendo alterada</param>
        /// <param name="newValues">StringBuilder donde se regresan los valores </param>
        private void GetNewValues(EntityEntry pEntry, StringBuilder newValues) {
            try
            {
                foreach (var item in pEntry.CurrentValues.Properties)
                {
                    var vValues = pEntry.CurrentValues[item.Name];
                    if (vValues != null) {
                        if((item.Name.ToUpper().Contains("ID")  && vValues.ToString().StartsWith('-')))
                            newValues.AppendFormat("{0} = {1} , ", item.Name, "");
                        else
                            newValues.AppendFormat("{0} = {1} , ", item.Name, vValues);
                    }
                        
                }

                if (newValues.Length > 0)
                    newValues = newValues.Remove(newValues.Length - 3, 3);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error appAuditor.GetNewValues: { GetFullException(ex) }");
            }
        }


        /// <summary>
        /// Obtiene los valores nuevos y los antiguos de los campos que son modificados en la entidad
        /// </summary>
        /// <param name="pEntry"> Entdad que esta siendo alterada</param>
        /// <param name="newValues">StringBuilder donde se regresan los valores nuevos </param>
        /// <param name="oldValues"> StringBuilder donde se regresan los valores antiguos </param>
        private void GetUpdateValues(EntityEntry pEntry, StringBuilder newValues, StringBuilder oldValues) {
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

        /// <summary>
        /// Obtiene los valores de la entidad que esta siendo eliminada
        /// </summary>
        /// <param name="pEntry"> Entdad que esta siendo alterada</param>
        /// <param name="oldValues">StringBuilder donde se regresan los valores antiguos </param>
        private void GetDeleteValues(EntityEntry pEntry, StringBuilder oldValues) {
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


        /// <summary>
        /// Recibe una exception y regresa el mensaje y todos los mensaje de las innerException, si las hay
        /// </summary>
        /// <param name="Exception"></param>
        /// <returns></returns>
        private string GetFullException(Exception Exception)
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

        /// <summary>
        /// Se encarga de ejecutar el procedimiento almacenado para registrar el cambio en las entidades
        /// </summary>
        /// <param name="User"> Usuario que inició la acción </param>
        /// <param name="Entity">Nombre de la entidad afectada </param>
        /// <param name="Keys"> Llaves primarias de la entidad </param>
        /// <param name="Action">Accion que se realiza: I insert, U update, D Delete </param>
        /// <param name="NewValues"> String de los valores nuevos que se van a alterar </param>
        /// <param name="OldValues"> String de los valores antiguos que se alteraron </param>
        private void SaveAuditor(string User, string Entity, string Keys, string Action, string NewValues, string OldValues) {
            try
            {
                _context.Database.ExecuteSqlCommand("SP_AUDITORIA @p0, @p1, @p2, @p3, @p4, @p5", parameters: new[] { User, Entity, Keys, Action, NewValues, OldValues });
            }
            catch (Exception ex)
            {
                _logger.LogError($"SaveAuditor Ocurrio un error: { GetFullException(ex) }");
            }
        }
    }
}
