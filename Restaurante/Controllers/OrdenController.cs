using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Restaurante.Core.OrdenManager;
using Restaurante.Data;
using Restaurante.Models;

namespace Restaurante.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrdenController : ControllerBase
    {
        private readonly IOrdenManager _ordenManager;
        public OrdenController(IOrdenManager ordenManager)
        {
            _ordenManager = ordenManager;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var ordenesResultado = await _ordenManager.GetOrdenesAsync();
            if (ordenesResultado.Success)
            {
                return Ok(ordenesResultado.Value);
            }

            return NotFound(ordenesResultado.Errors);
        }


        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var ordenResult = await _ordenManager.GetById(id);
            if(ordenResult.Success)
            {
                return Ok(ordenResult.Value); 
            }
            return NotFound(ordenResult.Errors);
        }


        [HttpGet("GetByClientName/{name}")]
        public async Task<ActionResult> GetByClienteName(string name)
        {
            var ordenResult = await _ordenManager.GetByClientName(name);
            if (ordenResult.Success)
            {
                return Ok(ordenResult.Value);
            }
            return NotFound(ordenResult.Errors);
        }

        [HttpGet("GetByDate/{date}")]
        public async Task<ActionResult> GetByDate(DateTime date)
        {
            var ordenResult = await _ordenManager.GetByDate(date);
            if (ordenResult.Success)
            {
                return Ok(ordenResult.Value);
            }
            return NotFound(ordenResult.Errors);
        }

        [HttpPost]
        public async Task<ActionResult> Post(Orden orden)
        {
            var result = await _ordenManager.Create(orden);
            if (result.Success)
            {
                return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, result.Value);
            }
            return BadRequest(result.Errors);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(int id, Orden orden)
        {
            var result = await _ordenManager.Update(orden, id);
            if (result.Success)
            {
                return Ok(result.Value);
            }
            return BadRequest(result.Errors);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _ordenManager.Delete(id);
            if (result.Success)
            {
                return NoContent();
            }
            return BadRequest(result.Errors);
        }
    }
}
