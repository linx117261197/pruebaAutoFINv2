namespace FacturacionWeb.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("t_productos")]
    public class Producto
    {
        [Key]
        public int IdProducto { get; set; }
        [Required(ErrorMessage = "El nombre del producto es obligatorio.")]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;
        [StringLength(50)]
        public string? Marca { get; set; }
        [Required(ErrorMessage = "El costo es obligatorio.")]
        [Column(TypeName = "numeric(5, 2)")]
        public decimal Costo { get; set; }
        [Required(ErrorMessage = "El precio de venta es obligatorio.")]
        [Column(TypeName = "numeric(5, 2)")]
        public decimal PrecioVenta { get; set; }
    }
}