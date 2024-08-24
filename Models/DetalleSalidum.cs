using System;
using System.Collections.Generic;

namespace CyComputer.Models;

public partial class DetalleSalidum
{
    public int IdDetalleSalida { get; set; }

    public int? IdGuiaSalida { get; set; }

    public int? IdProducto { get; set; }

    public int? Cantidad { get; set; }

    public decimal? Precio { get; set; }

    public virtual GuiaSalidum? IdGuiaSalidaNavigation { get; set; }

    public virtual Producto? IdProductoNavigation { get; set; }
}
