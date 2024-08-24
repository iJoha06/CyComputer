using System;
using System.ComponentModel.DataAnnotations;

namespace CyComputer.Models
{
    public class Devolucion
    {
        public int Id { get; set; }

        [Required]
        public string NombreProducto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public string Motivo { get; set; }

        [Required]
        public string TipoDevolucion { get; set; }

        [Required]
        public DateTime Fecha { get; set; }
    }
}
