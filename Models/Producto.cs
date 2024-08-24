using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CyComputer.Models
{
    public partial class Producto
    {
        public int IdProducto { get; set; }

        public int? IdCategoria { get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio.")]
        public string Nombre { get; set; } = null!;

        [Required(ErrorMessage = "El campo Descripción es obligatorio.")]
        public string? Descripcion { get; set; }

        [Required(ErrorMessage = "El campo Precio es obligatorio.")]
        [Range(0, double.MaxValue, ErrorMessage = "El campo Precio debe ser un número válido.")]
        public decimal? Precio { get; set; }

        [Required(ErrorMessage = "El campo Stock es obligatorio.")]
        [Range(0, int.MaxValue, ErrorMessage = "El campo Stock debe ser un número válido.")]
        public int? Stock { get; set; }

        public virtual ICollection<DetalleCarrito> DetalleCarritos { get; set; } = new List<DetalleCarrito>();

        public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

        public virtual ICollection<DetalleEntradum> DetalleEntrada { get; set; } = new List<DetalleEntradum>();

        public virtual ICollection<DetallePedido> DetallePedidos { get; set; } = new List<DetallePedido>();

        public virtual ICollection<DetalleSalidum> DetalleSalida { get; set; } = new List<DetalleSalidum>();

        public virtual Categorium? IdCategoriaNavigation { get; set; }

        public virtual ICollection<MovimientosAlmacen> MovimientosAlmacens { get; set; } = new List<MovimientosAlmacen>();
    }
}