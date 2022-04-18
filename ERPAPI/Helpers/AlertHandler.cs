using ERP.Contexts;
using ERPAPI.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ERPAPI.Helpers
{
   
    public  class AlertHandler
    {
        public static ApplicationDbContext _context = null;
        public static ILogger _logger;
        private static string _usuario = string.Empty;

        public AlertHandler( ApplicationDbContext context, ILogger logger, string usuario)
        {

            _context = context; 
            _logger = logger;
           usuario = usuario;
        }


        /// <summary>
        /// Inserta una nueva Alert
        /// </summary>
        /// <param name="_Alert"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public  Alert AlertaProductos(long productId, string documento, int idDocumento)
        {
            Alert _Alertq = new Alert();
            try
            {


                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        SubProduct producto  = new SubProduct();
                        producto = _context.SubProduct.Where(q => q.SubproductId ==  productId).FirstOrDefault();
                        if (producto.ProductTypeId == 3)
                        {
                            return null;
                        }
                            Alert Alerta = new Alert();
                            Alerta.DocumentId = idDocumento;
                            Alerta.DocumentName = documento;
                            Alerta.AlertName = "Productos";
                            Alerta.Code = "PRODUCT01";
                            Alerta.DescriptionAlert = "Lista de producto Prohibida";
                            Alerta.Description = $"Producto Prohibido {producto.ProductName} en {documento}";
                            Alerta.DescriptionAlert = $"Producto Prohibido {producto.ProductName} en {documento}";
                            Alerta.FechaCreacion = DateTime.Now;
                            Alerta.FechaModificacion = DateTime.Now;
                            Alerta.UsuarioCreacion = _usuario;
                            Alerta.UsuarioModificacion =_usuario;
                           
                        _context.SaveChanges();
                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                        // return BadRequest($"Ocurrio un error:{ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return null;
            }

            return _Alertq;
        }
    }
}
