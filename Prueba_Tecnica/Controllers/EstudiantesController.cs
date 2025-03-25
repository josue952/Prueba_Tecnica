using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Prueba_Tecnica.Models;

namespace Prueba_Tecnica.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController : ControllerBase
    {
        private readonly PruebaTecnicaContext _context;

        public EstudiantesController(PruebaTecnicaContext context)
        {
            _context = context;
        }

        // GET: api/estudiantes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estudiante>>> GetEstudiantes()
        {
            return await _context.Estudiantes.ToListAsync();
        }

        // GET: api/estudiantes/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Estudiante>> GetEstudiante(int id)
        {
            var estudiante = await _context.Estudiantes
                                           .FirstOrDefaultAsync(e => e.IdEstudiante == id);
            if (estudiante == null)
            {
                return NotFound(new { message = "Estudiante no encontrado" });
            }
            return Ok(estudiante);
        }

        // POST: api/estudiantes
        [HttpPost]
        public async Task<IActionResult> PostEstudiante(Estudiante estudiante)
        {
            // Verifica si el código ya está en uso
            if (_context.Estudiantes.Any(e => e.CodigoEstudiante == estudiante.CodigoEstudiante))
            {
                return BadRequest(new { message = "El codigo de este estudiante ya fue utilizado, intente con uno nuevo" });
            }

            // Verifica si el correo ya está en uso
            if (_context.Estudiantes.Any(e => e.Correo == estudiante.Correo))
            {
                return BadRequest(new { message = "El correo de este estudiante ya fue utilizado, intente con uno nuevo" });
            }

            _context.Estudiantes.Add(estudiante);

            try
            {
                await _context.SaveChangesAsync();
                var message = $"Estudiante {estudiante.Nombre} {estudiante.Apellido} Ingresado exitosamente";
                return Ok(new { message });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Error creando nuevo usuario" });
            }
        }


        // PUT: api/estudiantes/{codigoEstudiante}
        [HttpPut("{codigoEstudiante}")]
        public async Task<IActionResult> PutEstudiante(int codigoEstudiante, Estudiante estudiante)
        {
            // Buscar el estudiante existente por su código
            var estudianteExistente = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.CodigoEstudiante == codigoEstudiante);

            if (estudianteExistente == null)
            {
                return NotFound(new { message = "Estudiante no encontrado." });
            }

            // Verifica si el código ya está en uso
            if (_context.Estudiantes.Any(e => e.CodigoEstudiante == estudiante.CodigoEstudiante))
            {
                return BadRequest(new { message = "El codigo de este estudiante ya fue utilizado, intente con uno nuevo" });
            }

            // Verifica si el correo ya está en uso
            if (_context.Estudiantes.Any(e => e.Correo == estudiante.Correo))   
            {
                return BadRequest(new { message = "El correo de este estudiante ya fue utilizado, intente con uno nuevo" });
            }

            // Actualizar las propiedades del estudiante (excluyendo Id y CodigoEstudiante)
            estudianteExistente.Nombre = estudiante.Nombre;
            estudianteExistente.Apellido = estudiante.Apellido;
            estudianteExistente.FechaNacimiento = estudiante.FechaNacimiento;
            estudianteExistente.Edad = estudiante.Edad;
            estudianteExistente.Correo = estudiante.Correo;
            estudianteExistente.DetallesBitacora = estudiante.DetallesBitacora;

            try
            {
                await _context.SaveChangesAsync();
                var message = $"Estudiante {estudiante.Nombre} {estudiante.Apellido} Actualizado exitosamente";
                return Ok(new { message });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Error actualizando usuario" });
            }
        }

        // DELETE: api/estudiantes/{codigoEstudiante}
        [HttpDelete("{codigoEstudiante}")]
        public async Task<IActionResult> DeleteEstudiante(int codigoEstudiante)
        {
            // Buscar el estudiante por su codigo
            var estudiante = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.CodigoEstudiante == codigoEstudiante);

            if (estudiante == null)
            {
                return NotFound(new { message = "Estudiante no encontrado." });
            }

            _context.Estudiantes.Remove(estudiante);

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { message = "Estudiante eliminado exitosamente" });
            }
            catch (Exception)
            {
                return BadRequest(new { message = "Error eliminando usuario" });
            }
        }

    }
}

