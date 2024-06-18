using DisponibilidadAPI.Enums;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DisponibilidadAPI.Models
{
    public class Empleado
    {
        public int Legajo { get; set; }
        public string Nombre { get; set; }

        public List<Disponibilidad> Disponibilidades { get; set; }

        public bool PerteneceAUnEquipo { get; set; }

        public Empleado(int legajo, string nombre, List<Disponibilidad> disponibilidades, bool perteneceAUnEquipo)
        {
            Legajo = legajo;
            Nombre = nombre;
            Disponibilidades = disponibilidades;
            PerteneceAUnEquipo = perteneceAUnEquipo;
        }

        public bool EstaDisponible(DateTime fecha)
        {
            foreach (var disponibilidad in Disponibilidades)
            {
                switch (disponibilidad.Tipo)
                {
                    case TipoDisponibilidad.FinDeSemana:
                        if (fecha.DayOfWeek == DayOfWeek.Saturday || fecha.DayOfWeek == DayOfWeek.Sunday)
                        {
                            return true;
                        }
                        break;

                    case TipoDisponibilidad.EntreSemana:
                        if (fecha.DayOfWeek >= DayOfWeek.Monday && fecha.DayOfWeek <= DayOfWeek.Friday)
                        {
                            return true;
                        }
                        break;

                    case TipoDisponibilidad.DiaDelMes:
                        if (disponibilidad.Dias.Contains(fecha.Day))
                        {
                            return true;
                        }
                        break;

                    case TipoDisponibilidad.Lunes:
                        if (fecha.DayOfWeek == DayOfWeek.Monday)
                        {
                            return true;
                        }
                        break;

                    case TipoDisponibilidad.Martes:
                        if (fecha.DayOfWeek == DayOfWeek.Tuesday)
                        {
                            return true;
                        }
                        break;

                    case TipoDisponibilidad.Miercoles:
                        if (fecha.DayOfWeek == DayOfWeek.Wednesday)
                        {
                            return true;
                        }
                        break;

                    case TipoDisponibilidad.Jueves:
                        if (fecha.DayOfWeek == DayOfWeek.Thursday)
                        {
                            return true;
                        }
                        break;

                    case TipoDisponibilidad.Viernes:
                        if (fecha.DayOfWeek == DayOfWeek.Friday)
                        {
                            return true;
                        }
                        break;

                    case TipoDisponibilidad.Sabado:
                        if (fecha.DayOfWeek == DayOfWeek.Saturday)
                        {
                            return true;
                        }
                        break;

                    case TipoDisponibilidad.Domingo:
                        if (fecha.DayOfWeek == DayOfWeek.Sunday)
                        {
                            return true;
                        }
                        break;
                }
            }
            return false;
        }


        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Empleado other = (Empleado)obj;

            return Legajo == other.Legajo && Nombre == other.Nombre
            && CompararListas(Disponibilidades, other.Disponibilidades)
            && PerteneceAUnEquipo == other.PerteneceAUnEquipo;
        }


        private bool CompararListas(List<Disponibilidad> lista1, List<Disponibilidad> lista2)
        {
            if (lista1 == null && lista2 == null)
                return true;

            if (lista1 == null || lista2 == null)
                return false;

            if (lista1.Count != lista2.Count)
                return false;

            for (int i = 0; i < lista1.Count; i++)
            {
                if (!lista1[i].Equals(lista2[i]))
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Legajo, Nombre, Disponibilidades, PerteneceAUnEquipo);
        }
    }
 

}
