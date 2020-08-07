using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.Models
{
    public class Orden
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date => DateTime.Now;
        public string ClientName { get; set; }
        public string CashierName { get; set; }
        public virtual ICollection<DetalleOrden> DetalleOrdenes { get; set; }
    }
}
