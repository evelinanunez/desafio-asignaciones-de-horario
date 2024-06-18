using DisponibilidadAPI.Enums;
using System.Text.Json.Serialization;

namespace DisponibilidadAPI.Models
{
    public class Disponibilidad
    {
        public TipoDisponibilidad Tipo {  get; set; }

   
        public List<int> Dias { get; set; }

       
        public Disponibilidad(TipoDisponibilidad tipo, 
                              List <int> ?dias)
        {
            Tipo = tipo;
            Dias = dias ?? new List<int>();
        }


        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            Disponibilidad other = (Disponibilidad)obj;

            return Tipo == other.Tipo
                && CompararListas(Dias, other.Dias);
        }

        
        private bool CompararListas(List<int> lista1, List<int> lista2)
        {
            if (lista1 == null && lista2 == null)
                return true;

            if (lista1 == null || lista2 == null)
                return false;

            if (lista1.Count != lista2.Count)
                return false;

            for (int i = 0; i < lista1.Count; i++)
            {
                if (lista1[i] != lista2[i])
                    return false;
            }

            return true;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Tipo, Dias);
        }
    }
}
