namespace FacturacionWeb.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("t_detalleFactura")]
    public class DetalleFactura
    {
        [Key]
        public int IdDetalle { get; set; }
        [Required]
        public int IdFactura { get; set; }
        [Required]
        public int IdProducto { get; set; }
        [Required]
        public int Cantidad { get; set; }
        [ForeignKey("IdFactura")]
        public virtual Factura Factura { get; set; } = null!;
        [ForeignKey("IdProducto")]
        public virtual Producto Producto { get; set; } = null!;
    }
}