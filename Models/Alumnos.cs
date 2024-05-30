namespace GymAJT.Models
{
    public class Alumnos
    {
        public decimal IdAlumnoPk { get; set; }
        public int? Codigo { get; set; }
        public string? Nombres { get; set; }
        public string? Curso { get; set; }
        public string? Correo { get; set; }
        public string? CorreoPaternal { get; set; }
    }
}
