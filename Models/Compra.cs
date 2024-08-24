using System;
using System.Collections.Generic;

namespace CyComputer.Models;

public partial class Compra
{
    public int IdCompra { get; set; }

    public int? IdProveedor { get; set; }

    public DateTime? Fecha { get; set; }

    public decimal? Total { get; set; }

    public string? Estado { get; set; }

    public virtual ICollection<DetalleCompra> DetalleCompras { get; set; } = new List<DetalleCompra>();

    public virtual Proveedore? IdProveedorNavigation { get; set; }
}
