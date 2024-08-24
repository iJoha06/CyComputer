using System;
using System.Collections.Generic;

namespace CyComputer.Models;

public partial class Proveedore
{
    public int IdProveedor { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Direccion { get; set; }

    public string? Telefono { get; set; }

    public string? Correo { get; set; }

    public virtual ICollection<Compra> Compras { get; set; } = new List<Compra>();

    public virtual ICollection<GuiaEntradum> GuiaEntrada { get; set; } = new List<GuiaEntradum>();
}
