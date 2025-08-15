namespace FacturacionWeb.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("t_facturas")]
    public class Factura
    {
        [Key]
        public int IdFactura { get; set; }
        [Required]
        [StringLength(10)]
        public string NumeroFactura { get; set; } = string.Empty;
        [Required]
        public DateTime FechaHora { get; set; }
        [Required]
        public int IdCliente { get; set; }
        [ForeignKey("IdCliente")]
        public virtual Cliente Cliente { get; set; } = null!;
        public virtual ICollection<DetalleFactura> DetalleFacturas { get; set; } = new List<DetalleFactura>();
    }
}