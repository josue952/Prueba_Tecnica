using System;
using System.Collections.Generic;

namespace Prueba_Tecnica.Models;

public partial class Estudiante
    {
    public int IdEstudiante { get; set; }

    public int CodigoEstudiante { get; set; }

    public string Nombre { get; set; } = null!;

    public string Apellido { get; set; } = null!;

    public DateOnly FechaNacimiento { get; set; }

    public int Edad { get; set; }

    public string Correo { get; set; } = null!;

    public string? DetallesBitacora { get; set; }
}
