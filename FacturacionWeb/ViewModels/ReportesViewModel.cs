namespace FacturacionWeb.ViewModels
{
    public class ReporteFacturacionViewModel
    {
        public decimal FacturacionTotal { get; set; }
        public int TotalProductosVendidos { get; set; }
        public List<FacturaReporteItem> Facturas { get; set; } = new List<FacturaReporteItem>();
    }

    public class FacturaReporteItem
    {
        public string NumeroFactura { get; set; } = string.Empty;
        public DateTime FechaFacturacion { get; set; }
        public string Cliente { get; set; } = string.Empty;
        public int Productos { get; set; }
        public decimal MontoTotal { get; set; }
    }
}