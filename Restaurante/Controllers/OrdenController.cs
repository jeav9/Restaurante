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
    public class OrdenController : ControllerBase
    {
        private readonly RestauranteContext _context;
        public OrdenController(RestauranteContext context)
        {
            _context = context;

            if(_context.Productos.Count() == 0)
            {
                List<Producto> productos = new List<Producto>();
                productos.Add(new Producto { Name = "Plato1", Description = "plato 1", Price = 20d });
                productos.Add(new Producto { Name = "Plato2", Description = "plato 2", Price = 20d });
                productos.Add(new Producto { Name = "Plato3", Description = "plato 3", Price = 20d });
                _context.Productos.AddRange(productos);
                _context.SaveChanges();
            }
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var ordenes = await _context.Ordenes.Select(s => new Orden
            {
                Id = s.Id,
                CashierName = s.CashierName,
                ClientName = s.ClientName,
                DetalleOrdenes = s.DetalleOrdenes
            }).ToListAsync();

            return Ok(ordenes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var orden = await _context.Ordenes.FirstOrDefaultAsync(s => s.Id == id);
            if(orden == null)
            {
                return NotFound();
            }

            return Ok(orden);
        }

        // GetByNombre
        [HttpGet("GetByClientName/{name}")]
        public async Task<ActionResult> GetByClienteName(string name)
        {
            var orden = await _context.Ordenes.FirstOrDefaultAsync(s => s.ClientName.Contains(name));
            if (orden == null)
            {
                return NotFound();
            }

            return Ok(orden);
        }

        //GetByFecha
        [HttpGet("GetByDate/{date}")]
        public async Task<ActionResult> GetByClienteName(DateTime date)
        {
            var orden = await _context.Ordenes.FirstOrDefaultAsync(s => s.Date.Date == date.Date);
            if (orden == null)
            {
                return NotFound();
            }

            return Ok(orden);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Orden orden)
        {
            _context.Ordenes.Add(orden);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = orden.Id }, orden);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Orden orden)
        {
            if (id == orden.Id)
            {
                _context.Entry(orden).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var orden = await _context.Ordenes.FindAsync(id);
            if (orden == null)
            {
                return NotFound();
            }
            _context.Ordenes.Remove(orden);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
