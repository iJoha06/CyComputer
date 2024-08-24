using System;
using System.Collections.Generic;

namespace CyComputer.Models;

public partial class Almacen
{
    public int IdAlmacen { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Ubicacion { get; set; }

    public virtual ICollection<GuiaEntradum> GuiaEntrada { get; set; } = new List<GuiaEntradum>();

    public virtual ICollection<GuiaSalidum> GuiaSalida { get; set; } = new List<GuiaSalidum>();

    public virtual ICollection<MovimientosAlmacen> MovimientosAlmacens { get; set; } = new List<MovimientosAlmacen>();
}
