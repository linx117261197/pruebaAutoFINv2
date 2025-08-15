namespace FacturacionWeb.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Linq;
    using System.Threading.Tasks;
    using FacturacionWeb.Models;
    using FacturacionWeb.ViewModels;

    public class ReportesController : Controller
    {
        private readonly FacturacionContext _context;

        public ReportesController(FacturacionContext context)
        {
            _context = context;
        }

        // GET: Reportes/ReporteFacturacion
        public async Task<IActionResult> ReporteFacturacion()
        {
            // Consulta para obtener los datos de cada factura
            var facturasData = await _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.DetalleFacturas)
                    .ThenInclude(d => d.Producto)
                .Select(f => new FacturaReporteItem
                {
                    NumeroFactura = f.NumeroFactura,
                    FechaFacturacion = f.FechaHora,
                    Cliente = f.Cliente.Nombre,
                    // Suma de la cantidad de productos en el detalle de la factura
                    Productos = f.DetalleFacturas.Sum(d => d.Cantidad),
                    // Suma del (precio de venta * cantidad) para cada producto en el detalle
                    MontoTotal = f.DetalleFacturas.Sum(d => d.Producto.PrecioVenta * d.Cantidad)
                })
                .ToListAsync();

            // Calcular los totales generales
            var facturacionTotal = facturasData.Sum(f => f.MontoTotal);
            var totalProductos = facturasData.Sum(f => f.Productos);

            // Crear el ViewModel y llenarlo con los datos
            var viewModel = new ReporteFacturacionViewModel
            {
                FacturacionTotal = facturacionTotal,
                TotalProductosVendidos = totalProductos,
                Facturas = facturasData
            };

            // NOTA: Aquí es donde llamarías a tu servicio de Crystal Reports.
            // Por ahora, retornamos una vista HTML para previsualizar los datos.
            return View(viewModel);
        }
    }
}