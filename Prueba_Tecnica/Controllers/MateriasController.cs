using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica.Models;

namespace Prueba_Tecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MateriasController : ControllerBase
    {
        private readonly PruebaTecnicaContext _context;

        public MateriasController(PruebaTecnicaContext context)
        {
            _context = context;
        }

        // GET: api/materias/{codigoEstudiante}
        [HttpGet("{codigoEstudiante}")]
        public async Task<ActionResult<IEnumerable<Materia>>> GetMateria(int codigoEstudiante)
        {
            // Validar que exista un estudiante con ese código
            var estudiante = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.CodigoEstudiante == codigoEstudiante);
            if (estudiante == null)
            {
                return NotFound(new { message = "El código del estudiante no es válido o no existe." });
            }

            // Obtener todas las materias asignadas a ese estudiante
            var materias = await _context.Materias
                .Where(m => m.CodigoEstudiante == codigoEstudiante)
                .ToListAsync();
            if (materias == null || materias.Count == 0)
            {
                return NotFound(new { message = "No se encontraron materias asignadas a este estudiante." });
            }

            return Ok(materias);
        }

        // POST: api/materias/asignarmateria/{id}
        [HttpPost("asignarMateria/{id}")]
        public async Task<IActionResult> AsignarMateria(int id, [FromBody] AsignarMateriaRequest request)
        {
            //Validar que el id de la materia exista
            if (!await _context.Materias.AnyAsync(m => m.IdMateria == id))
            {
                return NotFound(new { message = "Materia no encontrada." });
            }

            // Validar que exista el estudiante con el código enviado
            var estudianteExistente = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.CodigoEstudiante == request.CodigoEstudiante);
            if (estudianteExistente == null)
            {
                return BadRequest(new { message = "El código del estudiante a asignar no existe." });
            }

            // Buscar la materia por su id
            var materiaExistente = await _context.Materias.FindAsync(id);
            if (materiaExistente == null)
            {
                return NotFound(new { message = "Materia no encontrada." });
            }

            // Actualizar únicamente el campo CodigoEstudiante
            materiaExistente.CodigoEstudiante = request.CodigoEstudiante;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = $"Materia ({materiaExistente.NombreMateria}) asignada exitosamente al estudiante con código {request.CodigoEstudiante}." });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Error asignando la materia." });
            }
        }

        // POST: api/materias
        [HttpPost]
        public async Task<IActionResult> PostMateria(Materia materia)
        {
            // Si se suministra un código de estudiante (valor distinto de 0),
            // validar que el estudiante exista
            if (materia.CodigoEstudiante != 0)
            {
                var estudianteExistente = await _context.Estudiantes
                    .FirstOrDefaultAsync(e => e.CodigoEstudiante == materia.CodigoEstudiante);

                if (estudianteExistente == null)
                {
                    return BadRequest(new { message = "El código del estudiante no existe." });
                }
            }
            // Agregar la materia (si no se proporcionó código, se guardará como 0, es decir, sin asignar)
            _context.Materias.Add(materia);
            try
            {
                await _context.SaveChangesAsync();
                var message = $"Materia ({materia.NombreMateria}) ingresada exitosamente";
                return Ok(new { message });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Error creando materia" });
            }
        }

        // PUT: api/materias/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMateria(int id, Materia materia)
        {
            // Buscar la materia existente usando el id de la URL
            var materiaExistente = await _context.Materias.FindAsync(id);
            if (materiaExistente == null)
            {
                return NotFound(new { message = "Materia no encontrada." });
            }

            // Si se está asignando un código de estudiante (valor distinto de 0),
            // se valida que exista dicho estudiante
            if (materia.CodigoEstudiante != 0)
            {
                var estudianteExistente = await _context.Estudiantes
                    .FirstOrDefaultAsync(e => e.CodigoEstudiante == materia.CodigoEstudiante);
                if (estudianteExistente == null)
                {
                    return BadRequest(new { message = "El código del estudiante a asignar no existe." });
                }
            }

            // Actualizar las propiedades de la materia con los valores enviados en el body
            materiaExistente.CodigoEstudiante = materia.CodigoEstudiante;
            materiaExistente.NombreMateria = materia.NombreMateria;
            materiaExistente.CodigoInstructor = materia.CodigoInstructor;
            materiaExistente.Horario = materia.Horario;
            materiaExistente.Ubicacion = materia.Ubicacion;
            materiaExistente.DetallesBitacora = materia.DetallesBitacora;

            try
            {
                await _context.SaveChangesAsync();
                var message = $"Materia ({materiaExistente.NombreMateria}) actualizada exitosamente";
                return Ok(new { message });
            }
            catch (DbUpdateConcurrencyException)
            {
                return BadRequest(new { message = "Error actualizando materia" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Error actualizando materia" });
            }
        }

        // DELETE: api/materias/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMateria(int id)
        {
            var materia = await _context.Materias.FindAsync(id);
            if (materia == null)
            {
                return NotFound(new { message = "Materia no encontrada." });
            }

            _context.Materias.Remove(materia);
            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Materia eliminada exitosamente" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Error eliminando materia" });
            }
        }   

    }
}
