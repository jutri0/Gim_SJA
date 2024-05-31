namespace GymAJT.Models
{
    public class Prestamos
    {
        public int IdPrestamoPk { get; set; }
        public string? AlumnoPrestamo { get; set; }
        public DateTime? FechaPrestamo { get; set; }
        public string? Equipo { get; set; }
        public int? CantidadEquipo { get; set; }
        public string? Observaciones { get; set; }
        public string? EstadoDevolucion { get; set; }
    }
}
