namespace FacturacionWeb.ViewModels
{
    using FacturacionWeb.Models;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using System.Collections.Generic;

    public class FacturaViewModel
    {
        public Factura Factura { get; set; } = new Factura();
        public List<DetalleFactura> Detalles { get; set; } = new List<DetalleFactura>();
        public IEnumerable<SelectListItem> Clientes { get; set; } = new List<SelectListItem>();
        public IEnumerable<SelectListItem> Productos { get; set; } = new List<SelectListItem>();
    }
}