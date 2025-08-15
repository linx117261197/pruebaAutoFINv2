namespace FacturacionWeb.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("t_clientes")]
    public class Cliente
    {
        [Key]
        public int IdCliente { get; set; }
        [Required(ErrorMessage = "El nombre del cliente es obligatorio.")]
        [StringLength(50)]
        public string Nombre { get; set; } = string.Empty;
        [Required(ErrorMessage = "El domicilio es obligatorio.")]
        [StringLength(100)]
        public string Domicilio { get; set; } = string.Empty;
        [EmailAddress(ErrorMessage = "El formato del email no es válido.")]
        [StringLength(50)]
        public string? Email { get; set; }
    }
}