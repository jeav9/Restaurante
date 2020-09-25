using Restaurante.Helpers;
using Restaurante.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Restaurante.DomainServices
{
    public class OrdenDomainService
    {
        public ResultHelper<Orden> ValidateIfCreateOrder(Orden orden)
        {
            ResultHelper<Orden> result = new ResultHelper<Orden>();

            if(orden.DetalleOrdenes == null)
            {
                result.AddError("El detalle de la orden no pueder nulo");
                return result;
            }
            if (orden.ClientName.Equals(""))
            {
                result.AddError("El nombre del cliente es requerido");
                return result;
            }

            Orden nuevaOrden = new Orden
            {
                CashierName = orden.CashierName,
                ClientName = orden.ClientName
            };
            result.Value = nuevaOrden;

            return result;
        }
    }
}
