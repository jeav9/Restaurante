using Microsoft.EntityFrameworkCore;
using Restaurante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.Data
{
    public class RestauranteContext : DbContext
    {
        public RestauranteContext(DbContextOptions<RestauranteContext> options)
            :base(options) { }

        public DbSet<Orden> Ordenes { get; set; }
        public DbSet<DetalleOrden> DetalleOrdenes { get; set; }
        public DbSet<Producto> Productos { get; set; }
    }
}
