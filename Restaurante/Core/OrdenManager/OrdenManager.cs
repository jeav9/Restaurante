using Microsoft.EntityFrameworkCore;
using Restaurante.Data;
using Restaurante.Helpers;
using Restaurante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.Core.OrdenManager
{
    public class OrdenManager : IOrdenManager
    {
        private readonly RestauranteContext _context;
        public OrdenManager(RestauranteContext context)
        {
            _context = context;
        }

        public async Task<ResultHelper<IEnumerable<Orden>>> GetOrdenesAsync()
        {
            var resultado = new ResultHelper<IEnumerable<Orden>>();
            var ordenes = await _context.Ordenes.Include(s => s.DetalleOrdenes).ToListAsync();

            if (ordenes.Count > 0)
            {
                resultado.Value = ordenes;
            }
            else
            {
                resultado.AddError("No existen ordenes en este momento");
            }
            return resultado;
        }

        public async Task<ResultHelper<Orden>> GetByClientName(string name)
        {
            var resultado = new ResultHelper<Orden>();
            var orden = await _context.Ordenes.FirstOrDefaultAsync(s => s.ClientName.Contains(name));
            if (orden != null)
            {
                resultado.Value = orden;
            }
            else
            {
                resultado.AddError("La orden no existe");
            }

            return resultado;
        }

        public async Task<ResultHelper<Orden>> GetByDate(DateTime date)
        {
            var resultado = new ResultHelper<Orden>();
            var orden = await _context.Ordenes.FirstOrDefaultAsync(s => s.Date.Date == date.Date);
            if (orden != null)
            {
                resultado.Value = orden;
            }
            else
            {
                resultado.AddError("La orden no existe");
            }

            return resultado;
        }

        public async Task<ResultHelper<Orden>> GetById(int id)
        {
            var resultado = new ResultHelper<Orden>();
            var orden = await _context.Ordenes.FirstOrDefaultAsync(s => s.Id == id);
            if (orden != null)
            {
                resultado.Value = orden;
            }
            else
            {
                resultado.AddError("La orden no existe");
            }

            return resultado;
        }

        public async Task<ResultHelper<Orden>> Create(Orden orden)
        {
            var resultado = new ResultHelper<Orden>();
            try
            {
                Orden nuevaOrden = new Orden
                {
                    CashierName = orden.CashierName,
                    ClientName = orden.ClientName
                };

                _context.Ordenes.Add(nuevaOrden);
                await _context.SaveChangesAsync();

                resultado.Value = nuevaOrden;
            }
            catch (Exception e)
            {
                resultado.AddError(e.Message);
            }
            return resultado;
        }

        public async Task<ResultHelper<Orden>> CreateWithDetails(Orden orden)
        {
            var resultado = new ResultHelper<Orden>();
            try
            {
                Orden nuevaOrden = new Orden
                {
                    CashierName = orden.CashierName,
                    ClientName = orden.ClientName
                };

                _context.Ordenes.Add(nuevaOrden);
                await _context.SaveChangesAsync();

                this.CrearDetalle(orden, nuevaOrden.Id);

                resultado.Value = nuevaOrden;
            }
            catch(Exception e)
            {
                resultado.AddError(e.Message);
            }
            return resultado;
        }

        public async Task<ResultHelper<Orden>> Update(Orden orden, int id)
        {
            var resultado = new ResultHelper<Orden>();
            try
            {
                if (id == orden.Id)
                {
                    _context.Entry(orden).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                    resultado.Value = orden;
                }
                else
                {
                    resultado.AddError("El id no coincide con el id de la orden");
                }
            }
            catch(Exception e)
            {
                resultado.AddError(e.Message);
            }
            return resultado;
        }

        public async Task<ResultHelper<string>> Delete(int id)
        {
            var resultado = new ResultHelper<string>();
            var orden = await _context.Ordenes.FindAsync(id);
            if (orden != null)
            {
                try
                {
                    _context.Ordenes.Remove(orden);
                    await _context.SaveChangesAsync();
                    resultado.Value = "La orden se elimino con exito";
                }
                catch (Exception e)
                {
                    resultado.AddError(e.Message);
                }
            }
            else
            {
                resultado.AddError("La orden no existe");
            }
            

            return resultado;
        }

        private void CrearDetalle(Orden orden, int id)
        {
            var detalleOrden = orden.DetalleOrdenes.Select(order => new DetalleOrden
            {
                OrderId = id,
                ProductId = order.ProductId,
                Quantity = order.Quantity
            }).ToList();

            _context.DetalleOrdenes.AddRange(detalleOrden);
             _context.SaveChanges();
        }
    }
}
