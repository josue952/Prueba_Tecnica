using System;
using System.Collections.Generic;

namespace Prueba_Tecnica.Models;

public partial class Materia
{
    public int IdMateria { get; set; }

    public int CodigoUsuario { get; set; }

    public string NombreMateria { get; set; } = null!;

    public string CodigoInstructor { get; set; } = null!;

    public string Horario { get; set; } = null!;

    public string Ubicacion { get; set; } = null!;

    public string? DetallesBitacora { get; set; }
}
