using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurante.Data;
using Restaurante.Models;

namespace Restaurante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetalleOrdenController : Controller
    {
        private readonly RestauranteContext _context;
        public DetalleOrdenController(RestauranteContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var ordenes = await _context.DetalleOrdenes.ToListAsync();
            return Ok(ordenes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var orden = await _context.DetalleOrdenes.FirstOrDefaultAsync(s => s.Id == id);
            if (orden == null)
            {
                return NotFound();
            }

            return Ok(orden);
        }

        //GetByMasterId
        [HttpGet("GetByOrderId/{orderId}")]
        public async Task<ActionResult> GetByOrderId(int orderId)
        {
            var orden = await _context.DetalleOrdenes.FirstOrDefaultAsync(s => s.OrderId == orderId);
            if (orden == null)
            {
                return NotFound();
            }

            return Ok(orden);
        }

        //GetByFecha
        [HttpGet("GetByDate/{date}")]
        public async Task<ActionResult> GetByDate(DateTime date)
        {
            var orden = await _context.DetalleOrdenes.FirstOrDefaultAsync(s => s.Date == date);
            if (orden == null)
            {
                return NotFound();
            }

            return Ok(orden);
        }

        [HttpPost]
        public async Task<ActionResult> Post(DetalleOrden detalle)
        {
            _context.DetalleOrdenes.Add(detalle);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = detalle.Id }, detalle);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, DetalleOrden detalle)
        {
            if(id == detalle.Id)
            {
                _context.Entry(detalle).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var detalle = await _context.DetalleOrdenes.FindAsync(id);
            if(detalle == null)
            {
                return NotFound();
            }
            _context.DetalleOrdenes.Remove(detalle);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
