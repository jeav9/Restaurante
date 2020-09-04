using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurante.Data;
using Restaurante.Models;

namespace Restaurante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly RestauranteContext _context;
        public ProductController(RestauranteContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var productos = await _context.Productos.ToListAsync();

            return Ok(productos);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var orden = await _context.Ordenes.FirstOrDefaultAsync(s => s.Id == id);
            if (orden == null)
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


        [HttpPost]
        public async Task<ActionResult> Post(Producto producto)
        {
            _context.Productos.Add(producto);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetById), new { id = producto.Id }, producto);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Producto producto)
        {
            if (id == producto.Id)
            {
                _context.Entry(producto).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                return NoContent();
            }
            return BadRequest();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var producto = await _context.Productos.FindAsync(id);
            if (producto == null)
            {
                return NotFound();
            }
            _context.Productos.Remove(producto);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}