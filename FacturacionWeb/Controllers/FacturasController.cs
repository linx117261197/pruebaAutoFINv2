namespace FacturacionWeb.Controllers
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.EntityFrameworkCore;
    using FacturacionWeb.Models;
    using FacturacionWeb.ViewModels;

    public class FacturasController : Controller
    {
        private readonly FacturacionContext _context;

        public FacturasController(FacturacionContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var facturas = _context.Facturas.Include(f => f.Cliente);
            return View(await facturas.ToListAsync());
        }

        public async Task<IActionResult> Details(int? id)
        {
            if (id == null) return NotFound();

            var factura = await _context.Facturas
                .Include(f => f.Cliente)
                .Include(f => f.DetalleFacturas)
                    .ThenInclude(d => d.Producto) // ThenInclude para anidar relaciones
                .FirstOrDefaultAsync(m => m.IdFactura == id);

            if (factura == null) return NotFound();

            return View(factura);
        }

        public IActionResult Create()
        {
            var viewModel = new FacturaViewModel
            {
                Factura = new Factura { FechaHora = DateTime.Now },
                Clientes = new SelectList(_context.Clientes, "IdCliente", "Nombre"),
                Productos = new SelectList(_context.Productos, "IdProducto", "Nombre")
            };
            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FacturaViewModel viewModel, int[] productoId, int[] cantidad)
        {
            // Solo validamos el modelo del encabezado
            if (ModelState.IsValid)
            {
                var factura = viewModel.Factura;
                factura.FechaHora = DateTime.Now;
                _context.Facturas.Add(factura);
                await _context.SaveChangesAsync();

                if (productoId != null && cantidad != null)
                {
                    for (int i = 0; i < productoId.Length; i++)
                    {
                        var detalle = new DetalleFactura
                        {
                            IdFactura = factura.IdFactura,
                            IdProducto = productoId[i],
                            Cantidad = cantidad[i]
                        };
                        _context.DetalleFacturas.Add(detalle);
                    }
                    await _context.SaveChangesAsync();
                }
                return RedirectToAction(nameof(Index));
            }
            
            // Si hay un error, recargamos las listas
            viewModel.Clientes = new SelectList(_context.Clientes, "IdCliente", "Nombre", viewModel.Factura.IdCliente);
            viewModel.Productos = new SelectList(_context.Productos, "IdProducto", "Nombre");
            return View(viewModel);
        }
    }
}