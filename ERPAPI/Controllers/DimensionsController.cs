﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ERP.Contexts;
using ERPAPI.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ERPAPI.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/Dimensions")]
    [ApiController]
    public class DimensionsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public DimensionsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Dimensions
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Dimensions>>> GetDimensions()
        {
            return await _context.Dimensions.ToListAsync();
        }

        // GET: api/Dimensions/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Dimensions>> GetDimensions(string id)
        {
            var dimensions = await _context.Dimensions.FindAsync(id);

            if (dimensions == null)
            {
                return NotFound();
            }

            return dimensions;
        }

        // PUT: api/Dimensions/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDimensions(string id, Dimensions dimensions)
        {
            if (id != dimensions.Num)
            {
                return BadRequest();
            }

            _context.Entry(dimensions).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DimensionsExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Dimensions
        [HttpPost]
        public async Task<ActionResult<Dimensions>> PostDimensions(Dimensions dimensions)
        {
            _context.Dimensions.Add(dimensions);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDimensions", new { id = dimensions.Num }, dimensions);
        }

        // DELETE: api/Dimensions/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Dimensions>> DeleteDimensions(string id)
        {
            var dimensions = await _context.Dimensions.FindAsync(id);
            if (dimensions == null)
            {
                return NotFound();
            }

            _context.Dimensions.Remove(dimensions);
            await _context.SaveChangesAsync();

            return dimensions;
        }

        private bool DimensionsExists(string id)
        {
            return _context.Dimensions.Any(e => e.Num == id);
        }
    }
}