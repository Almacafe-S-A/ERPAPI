using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace ERPAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VendorInvoiceController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public VendorInvoiceController(ILogger<VendorInvoiceController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de VendorInvoice paginado
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetVendorInvoicePag(int numeroDePagina = 1, int cantidadDeRegistros = 20)
        {
            List<VendorInvoice> Items = new List<VendorInvoice>();
            try
            {
                var query = _context.VendorInvoice.AsQueryable();
                var totalRegistro = query.Count();

                Items = await query
                   .Skip(cantidadDeRegistros * (numeroDePagina - 1))
                   .Take(cantidadDeRegistros)
                    .ToListAsync();

                Response.Headers["X-Total-Registros"] = totalRegistro.ToString();
                Response.Headers["X-Cantidad-Paginas"] = ((Int64)Math.Ceiling((double)totalRegistro / cantidadDeRegistros)).ToString();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Obtiene el Listado de VendorInvoicees 
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetVendorInvoice()
        {
            List<VendorInvoice> Items = new List<VendorInvoice>();
            try
            {
                Items = await _context.VendorInvoice.ToListAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            //  int Count = Items.Count();
            return await Task.Run(() => Ok(Items));
        }

        /// <summary>
        /// Obtiene los Datos de la VendorInvoice por medio del Id enviado.
        /// </summary>
        /// <param name="VendorInvoiceId"></param>
        /// <returns></returns>
        [HttpGet("[action]/{VendorInvoiceId}")]
        public async Task<IActionResult> GetVendorInvoiceById(Int64 VendorInvoiceId)
        {
            VendorInvoice Items = new VendorInvoice();
            try
            {
                Items = await _context.VendorInvoice.Where(q => q.VendorInvoiceId == VendorInvoiceId).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva VendorInvoice
        /// </summary>
        /// <param name="_pVendorInvoice"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<VendorInvoice>> Insert([FromBody]VendorInvoice pVendorInvoice)
        {
            VendorInvoice _VendorInvoiceq = new VendorInvoice();
            _VendorInvoiceq = pVendorInvoice;
            try
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {

                        

                        VendorInvoice _VendorInvoice = await _context.VendorInvoice.Where(q => q.BranchId == _VendorInvoiceq.BranchId)
                                             //.Where(q => q.IdPuntoEmision == _VendorInvoice.IdPuntoEmision)
                                             .FirstOrDefaultAsync();
                        //if (_VendorInvoice != null)
                        //{
                        //    _VendorInvoiceq.NumeroDEI = _context.VendorInvoice.Where(q => q.BranchId == _VendorInvoice.BranchId)
                        //                          .Where(q => q.IdPuntoEmision == _VendorInvoice.IdPuntoEmision).Max(q => q.NumeroDEI);
                        //}

                        _VendorInvoiceq.NumeroDEI += 1;


                        //  Int64 puntoemision = _context.Users.Where(q=>q.Email==_VendorInvoiceq.UsuarioCreacion).Select(q=>q.)

                        Int64 IdCai = await _context.NumeracionSAR
                                                 .Where(q => q.BranchId == _VendorInvoiceq.BranchId)
                                                 //.Where(q => q.IdPuntoEmision == _VendorInvoiceq.IdPuntoEmision)
                                                 .Where(q => q.Estado == "Activo").Select(q => q.IdCAI).FirstOrDefaultAsync();


                        if (IdCai == 0)
                        {
                            return BadRequest("No existe un CAI activo para el punto de emisión");
                        }

                        _VendorInvoiceq.Sucursal = await _context.Branch.Where(q => q.BranchId == _VendorInvoice.BranchId).Select(q => q.BranchCode).FirstOrDefaultAsync();
                        //  _VendorInvoiceq.Caja = await _context.PuntoEmision.Where(q=>q.IdPuntoEmision== _VendorInvoice.IdPuntoEmision).Select(q => q.PuntoEmisionCod).FirstOrDefaultAsync();
                        _VendorInvoiceq.CAI = await _context.CAI.Where(q => q.IdCAI == IdCai).Select(q => q._cai).FirstOrDefaultAsync();

                        Numalet let;
                        let = new Numalet();
                        let.SeparadorDecimalSalida = "Lempiras";
                        let.MascaraSalidaDecimal = "00/100 ";
                        let.ApocoparUnoParteEntera = true;
                        _VendorInvoiceq.TotalLetras = let.ToCustomCardinal((_VendorInvoiceq.Total)).ToUpper();

                        _context.VendorInvoice.Add(_VendorInvoiceq);
                        //await _context.SaveChangesAsync();
                        foreach (var item in _VendorInvoice.VendorInvoiceLine)
                        {
                            item.VendorInvoiceId = _VendorInvoiceq.VendorInvoiceId;
                            _context.VendorInvoiceLine.Add(item);
                        }

                        await _context.SaveChangesAsync();

                        JournalEntryConfiguration _journalentryconfiguration = await (_context.JournalEntryConfiguration
                                                                       .Where(q => q.TransactionId == 1)
                                                                       .Where(q => q.BranchId == _VendorInvoiceq.BranchId)
                                                                       .Where(q => q.EstadoName == "Activo")
                                                                       .Include(q => q.JournalEntryConfigurationLine)
                                                                       ).FirstOrDefaultAsync();

                        BitacoraWrite _writejec = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _VendorInvoice.VendorId,
                            DocType = "JournalEntryConfiguration",
                            ClaseInicial =
                             Newtonsoft.Json.JsonConvert.SerializeObject(_journalentryconfiguration, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_journalentryconfiguration, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "InsertVendorInvoice",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _VendorInvoice.UsuarioCreacion,
                            UsuarioModificacion = _VendorInvoice.UsuarioModificacion,
                            UsuarioEjecucion = _VendorInvoice.UsuarioModificacion,

                        });

                        // await _context.SaveChangesAsync();

                        double sumacreditos = 0, sumadebitos = 0;
                        if (_journalentryconfiguration != null)
                        {
                            //Crear el asiento contable configurado
                            //.............................///////
                            JournalEntry _je = new JournalEntry
                            {
                                Date = _VendorInvoiceq.VendorInvoiceDate,
                                Memo = "Factura de ventas",
                                DatePosted = _VendorInvoiceq.VendorInvoiceDate,
                                ModifiedDate = DateTime.Now,
                                CreatedDate = DateTime.Now,
                                ModifiedUser = _VendorInvoiceq.UsuarioModificacion,
                                CreatedUser = _VendorInvoiceq.UsuarioCreacion,
                                DocumentId = _VendorInvoiceq.VendorInvoiceId,
                            };



                            foreach (var item in _journalentryconfiguration.JournalEntryConfigurationLine)
                            {

                                VendorInvoiceLine _iline = new VendorInvoiceLine();
                                _iline = _VendorInvoiceq.VendorInvoiceLine.Where(q => q.ItemName == item.SubProductName).FirstOrDefault();
                                if (_iline != null || item.SubProductName.ToUpper().Contains(("Impuesto").ToUpper()))
                                {
                                    if (!item.AccountName.ToUpper().Contains(("Impuestos sobre ventas").ToUpper())
                                           && !item.AccountName.ToUpper().Contains(("Sobre Servicios Diversos").ToUpper()))
                                    {
                                        _je.JournalEntryLines.Add(new JournalEntryLine
                                        {
                                            AccountId = Convert.ToInt32(item.AccountId),
                                            Description = item.AccountName,
                                            Credit = item.DebitCredit == "Credito" ? _iline.SubTotal : 0,
                                            Debit = item.DebitCredit == "Debito" ? _iline.SubTotal : 0,
                                            CreatedDate = DateTime.Now,
                                            ModifiedDate = DateTime.Now,
                                            CreatedUser = _VendorInvoiceq.UsuarioCreacion,
                                            ModifiedUser = _VendorInvoiceq.UsuarioModificacion,
                                            Memo = "",
                                        });

                                        sumacreditos += item.DebitCredit == "Credito" ? _iline.SubTotal : 0;
                                        sumadebitos += item.DebitCredit == "Debito" ? _iline.SubTotal : 0;
                                    }
                                    else
                                    {
                                        _je.JournalEntryLines.Add(new JournalEntryLine
                                        {
                                            AccountId = Convert.ToInt32(item.AccountId),
                                            Description = item.AccountName,
                                            Credit = item.DebitCredit == "Credito" ? _VendorInvoiceq.Tax + _VendorInvoiceq.Tax18 : 0,
                                            Debit = item.DebitCredit == "Debito" ? _VendorInvoiceq.Tax + _VendorInvoiceq.Tax18 : 0,
                                            CreatedDate = DateTime.Now,
                                            ModifiedDate = DateTime.Now,
                                            CreatedUser = _VendorInvoiceq.UsuarioCreacion,
                                            ModifiedUser = _VendorInvoiceq.UsuarioModificacion,
                                            Memo = "",
                                        });

                                        sumacreditos += item.DebitCredit == "Credito" ? _VendorInvoiceq.Tax + _VendorInvoiceq.Tax18 : 0;
                                        sumadebitos += item.DebitCredit == "Debito" ? _VendorInvoiceq.Tax + _VendorInvoiceq.Tax18 : 0;
                                    }
                                }

                                // _context.JournalEntryLine.Add(_je);

                            }


                            if (sumacreditos != sumadebitos)
                            {
                                transaction.Rollback();
                                _logger.LogError($"Ocurrio un error: No coinciden debitos :{sumadebitos} y creditos{sumacreditos}");
                                return BadRequest($"Ocurrio un error: No coinciden debitos :{sumadebitos} y creditos{sumacreditos}");
                            }

                            _je.TotalCredit = sumacreditos;
                            _je.TotalDebit = sumadebitos;
                            _context.JournalEntry.Add(_je);

                            await _context.SaveChangesAsync();
                        }

                        BitacoraWrite _write = new BitacoraWrite(_context, new Bitacora
                        {
                            IdOperacion = _VendorInvoice.VendorId,
                            DocType = "VendorInvoice",
                            ClaseInicial =
                            Newtonsoft.Json.JsonConvert.SerializeObject(_VendorInvoice, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            ResultadoSerializado = Newtonsoft.Json.JsonConvert.SerializeObject(_VendorInvoice, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore }),
                            Accion = "Insert",
                            FechaCreacion = DateTime.Now,
                            FechaModificacion = DateTime.Now,
                            UsuarioCreacion = _VendorInvoice.UsuarioCreacion,
                            UsuarioModificacion = _VendorInvoice.UsuarioModificacion,
                            UsuarioEjecucion = _VendorInvoice.UsuarioModificacion,

                        });



                        await _context.SaveChangesAsync();

                        transaction.Commit();
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                        throw ex;
                    }
                }
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_VendorInvoiceq));
        }

        /// <summary>
        /// Actualiza la VendorInvoice
        /// </summary>
        /// <param name="_VendorInvoice"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<VendorInvoice>> Update([FromBody]VendorInvoice _VendorInvoice)
        {
            VendorInvoice _VendorInvoiceq = _VendorInvoice;
            try
            {
                _VendorInvoiceq = await (from c in _context.VendorInvoice
                                 .Where(q => q.VendorInvoiceId == _VendorInvoice.VendorInvoiceId)
                                   select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_VendorInvoiceq).CurrentValues.SetValues((_VendorInvoice));

                //_context.VendorInvoice.Update(_VendorInvoiceq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_VendorInvoiceq));
        }

        /// <summary>
        /// Elimina una VendorInvoice       
        /// </summary>
        /// <param name="_VendorInvoice"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]VendorInvoice _VendorInvoice)
        {
            VendorInvoice _VendorInvoiceq = new VendorInvoice();
            try
            {
                _VendorInvoiceq = _context.VendorInvoice
                .Where(x => x.VendorInvoiceId == (Int64)_VendorInvoice.VendorInvoiceId)
                .FirstOrDefault();

                _context.VendorInvoice.Remove(_VendorInvoiceq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_VendorInvoiceq));

        }







    }
}
