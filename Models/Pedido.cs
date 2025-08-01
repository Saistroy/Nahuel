using System.ComponentModel.DataAnnotations;

namespace DeliveryLocal.Models
{
    public class Pedido
    {
        public int Id { get; set; }

        [Required]
        public string Producto { get; set; } = string.Empty;

        [Required]
        public string Cliente { get; set; } = string.Empty;

        [Required]
        public string Direccion { get; set; } = string.Empty;

        [Required]
        public string Telefono { get; set; } = string.Empty;

        public double Latitud { get; set; }
        public double Longitud { get; set; }

        public string Estado { get; set; } = "Pendiente";
        public DateTime Fecha { get; set; } = DateTime.Now;
    }
}
