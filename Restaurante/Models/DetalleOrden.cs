using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.Models
{
    public class DetalleOrden
    {   
        [Key]
        public int Id { get; set; }
        [ForeignKey("Orden")]
        public int OrderId { get; set; }
        [ForeignKey("Producto")]
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public virtual Orden Orden { get; set; }
        public virtual Producto Producto { get; set; }
    }
}
