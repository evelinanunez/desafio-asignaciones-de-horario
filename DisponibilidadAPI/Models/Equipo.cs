namespace DisponibilidadAPI.Models
{
    public class Equipo
    {
        public string Nombre    { get; set; }
        public List<Empleado> Empleados {  get; set; }
        public Equipo(string nombre,List<Empleado> empleados) {
            Nombre= nombre;
            Empleados = empleados;
        }

        public bool ElEquipoEstaDisponible( DateTime fecha)
        {
            return Empleados.All(empleado => empleado.EstaDisponible(fecha));
        }
    }
}
