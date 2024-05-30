namespace GymAJT.Models
{
    public class Devoluciones
    {
        public decimal IdDevolucionPk { get; set; }
        public string? AlumnoDevolucion { get; set; }
        public int? Prestamo { get; set; }
        public DateTime? FechaDevolucion { get; set; }
        public int? Equipo { get; set; }
        public int? Observaciones { get; set; }
    }
}
