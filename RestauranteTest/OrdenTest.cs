using Microsoft.VisualStudio.TestTools.UnitTesting;
using Restaurante.DomainServices;
using Restaurante.Models;
using System.Collections.Generic;
using System.Linq;

namespace RestauranteTest
{
    [TestClass]
    public class OrdenTest
    {
        [TestMethod]
        public void ValidarSiDetalleEsNulo()
        {
            // Arrange / Preparar el ambiente de nuestra prueba

            OrdenDomainService ordenDomainService = new OrdenDomainService();

            Orden orden = new Orden();
            orden.CashierName = "Prueba cajero";
            orden.ClientName = "Cliente Prueba";

            // Act / Ejecucion de nuestro metodo en prueba

            var resultado = ordenDomainService.ValidateIfCreateOrder(orden);

            // Assert / La validacion de nuestro

            Assert.IsFalse(resultado.Success);
        }

        [TestMethod]
        public void ValidarSiClienteEsNulo()
        {
            // Arrange / Preparar el ambiente de nuestra prueba

            OrdenDomainService ordenDomainService = new OrdenDomainService();

            Orden orden = new Orden();
            orden.CashierName = "Prueba cajero";
            orden.ClientName = string.Empty;

            List<DetalleOrden> detallesOrden = new List<DetalleOrden>();
            detallesOrden.Add(new DetalleOrden { ProductId = 1, OrderId = 2, Quantity = 3});
            orden.DetalleOrdenes = detallesOrden;

            // Act / Ejecucion de nuestro metodo en prueba

            var resultado = ordenDomainService.ValidateIfCreateOrder(orden);

            // Assert / La validacion de nuestro
            var mensajeResultado = resultado.Errors.First();
            Assert.AreEqual("El nombre del cliente es requerido", mensajeResultado);
        }
    }
}
