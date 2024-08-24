using System;
using System.Collections.Generic;

namespace CyComputer.Models;

public partial class Carrito
{
    public int IdCarrito { get; set; }

    public int? IdCliente { get; set; }

    public DateTime? FechaCreacion { get; set; }

    public virtual ICollection<DetalleCarrito> DetalleCarritos { get; set; } = new List<DetalleCarrito>();

    public virtual Cliente? IdClienteNavigation { get; set; }
}
