using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CyComputer.Models;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    [Required(ErrorMessage = "El nombre es obligatorio")]
    public string Nombre { get; set; } = null!;

    [Required(ErrorMessage = "La Contraseña es obligatoria")]
    [DataType(DataType.Password)]
    public string Contrasena { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}