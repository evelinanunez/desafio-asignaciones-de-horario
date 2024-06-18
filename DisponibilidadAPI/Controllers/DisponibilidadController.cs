using DisponibilidadAPI.Enums;
using DisponibilidadAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace DisponibilidadAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DisponibilidadController : ControllerBase
    {
        private readonly List<Empleado> Empleados;
        private readonly List<Equipo> Equipos;


        public DisponibilidadController(List<Empleado> empleados ,List<Equipo> equipos)
        {
            Empleados = empleados;
            Equipos = equipos;
        }
        [HttpGet("disponibles/{ddmmyyyy}")]
        public ActionResult<List<Empleado>> GetEquiposDisponibles(string ddmmyyyy)
        {
            try
            {
                DateTime fecha = DateTime.ParseExact(ddmmyyyy, "ddMMyyyy", null);

                List<Empleado> empleadosDisponibles = new List<Empleado>();

                foreach (var equipo in Equipos)
                {
                    if (equipo.ElEquipoEstaDisponible(fecha))
                    {
                        foreach (var empleado in equipo.Empleados)
                        {
                            empleadosDisponibles.Add(empleado);
                        }
                    }
                }

                foreach (var empleado in Empleados)
                {
                    if (empleado.EstaDisponible(fecha) && !empleado.PerteneceAUnEquipo)
                    {
                        empleadosDisponibles.Add(empleado);
                    }
                }

                return Ok(empleadosDisponibles);
            }
            catch (FormatException)
            {
                return BadRequest("Formato de fecha inválido. Debe ser ddMMyyyy.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error inesperado: {ex.Message}");
            }
        }

        [HttpGet("disponibilidadEmpleado/{legajo:int}")]
        public ActionResult<Empleado> TraerDisponibilidadDeUnEmpleado(int legajo)
        {
            try
            {
                var empleado = Empleados.FirstOrDefault(e => e.Legajo == legajo);
                if (empleado == null)
                {
                    return NotFound($"No se encontró el empleado con el legajo {legajo}");
                }

                return Ok(empleado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error inesperado: {ex.Message}");
            }
        }

        [HttpGet("traerempleados")]
        public ActionResult<List<Empleado>> TraerEmpleados()
        {
            try
            {
                return Ok(Empleados);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error inesperado: {ex.Message}");
            }
        }


        [HttpGet("traerequipos")]
        public ActionResult<List<Equipo>> TraerEquipos()
        {
            try
            {
                return Ok(Equipos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error inesperado: {ex.Message}");
            }
        }
    }
}
