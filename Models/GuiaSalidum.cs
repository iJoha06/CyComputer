using System;
using System.Collections.Generic;

namespace CyComputer.Models;

public partial class GuiaSalidum
{
    public int IdGuiaSalida { get; set; }

    public int? IdAlmacen { get; set; }

    public int? IdCliente { get; set; }

    public DateTime? Fecha { get; set; }

    public string? Descripcion { get; set; }

    public virtual ICollection<DetalleSalidum> DetalleSalida { get; set; } = new List<DetalleSalidum>();

    public virtual Almacen? IdAlmacenNavigation { get; set; }

    public virtual Cliente? IdClienteNavigation { get; set; }
}
