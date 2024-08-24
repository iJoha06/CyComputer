using System;
using System.Collections.Generic;

namespace CyComputer.Models;

public partial class DetalleEntradum
{
    public int IdDetalleEntrada { get; set; }

    public int? IdGuiaEntrada { get; set; }

    public int? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Precio { get; set; }

    public virtual GuiaEntradum? IdGuiaEntradaNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
