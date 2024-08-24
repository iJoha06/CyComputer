using System;
using System.Collections.Generic;

namespace CyComputer.Models;

public partial class GuiaEntradum
{
    public int IdGuiaEntrada { get; set; }

    public int? IdProveedor { get; set; }

    public int? IdAlmacen { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<DetalleEntradum> DetalleEntrada { get; set; } = new List<DetalleEntradum>();

    public virtual Almacen? IdAlmacenNavigation { get; set; }

    public virtual Proveedore? IdProveedorNavigation { get; set; }
}
