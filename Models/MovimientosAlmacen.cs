using System;
using System.Collections.Generic;

namespace CyComputer.Models;

public partial class MovimientosAlmacen
{
    public int IdMovimiento { get; set; }

    public int? IdAlmacen { get; set; }

    public int? IdProducto { get; set; }

    public string? Tipo { get; set; }

    public DateTime? Fecha { get; set; }

    public int? Cantidad { get; set; }

    public string? Descripcion { get; set; }

    public virtual Almacen? IdAlmacenNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
