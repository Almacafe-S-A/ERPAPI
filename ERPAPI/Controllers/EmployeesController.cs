﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Employees")]
    [ApiController]
    public class EmployeesController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger _logger;

        public EmployeesController(ILogger<EmployeesController> logger, ApplicationDbContext context)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Obtiene el Listado de Employeeses 
        /// El estado define cuales son los cai activos
        /// </summary>
        /// <returns></returns>
        [HttpGet("[action]")]
        public async Task<IActionResult> GetEmployees()
        {
            List<Employees> Items = new List<Employees>();
            try
            {
                Items = await _context.Employees.ToListAsync();
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
        /// Obtiene los Datos de la Employees por medio del Id enviado.
        /// </summary>
        /// <param name="IdEmpleado"></param>
        /// <returns></returns>
        [HttpGet("[action]/{IdEmpleado}")]
        public async Task<IActionResult> GetEmployeesById(Int64 IdEmpleado)
        {
            Employees Items = new Employees();
            try
            {
                Items = await _context.Employees.Where(q => q.IdEmpleado == IdEmpleado).FirstOrDefaultAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }


            return await Task.Run(() => Ok(Items));
        }


        /// <summary>
        /// Inserta una nueva Employees
        /// </summary>
        /// <param name="_Employees"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<ActionResult<Employees>> Insert([FromBody]Employees _Employees)
        {
            Employees _Employeesq = new Employees();
            try
            {
                _Employeesq = _Employees;
                _context.Employees.Add(_Employeesq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return await Task.Run(() => BadRequest($"Ocurrio un error:{ex.Message}"));
            }

            return await Task.Run(() => Ok(_Employeesq));
        }

        /// <summary>
        /// Actualiza la Employees
        /// </summary>
        /// <param name="_Employees"></param>
        /// <returns></returns>
        [HttpPut("[action]")]
        public async Task<ActionResult<Employees>> Update([FromBody]Employees _Employees)
        {
            Employees _Employeesq = _Employees;
            try
            {
                _Employeesq = await (from c in _context.Employees
                                 .Where(q => q.IdEmpleado == _Employees.IdEmpleado)
                                     select c
                                ).FirstOrDefaultAsync();

                _context.Entry(_Employeesq).CurrentValues.SetValues((_Employees));

                //_context.Employees.Update(_Employeesq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Employeesq));
        }

        /// <summary>
        /// Elimina una Employees       
        /// </summary>
        /// <param name="_Employees"></param>
        /// <returns></returns>
        [HttpPost("[action]")]
        public async Task<IActionResult> Delete([FromBody]Employees _Employees)
        {
            Employees _Employeesq = new Employees();
            try
            {
                _Employeesq = _context.Employees
                .Where(x => x.IdEmpleado == (Int64)_Employees.IdEmpleado)
                .FirstOrDefault();

                _context.Employees.Remove(_Employeesq);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Ocurrio un error: { ex.ToString() }");
                return BadRequest($"Ocurrio un error:{ex.Message}");
            }

            return await Task.Run(() => Ok(_Employeesq));

        }







    }
}