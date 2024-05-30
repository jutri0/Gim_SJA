namespace GymAJT.Models
{
    public class Equipos
    {
        public decimal IdEquipoPk { get; set; }
        public string? Nombre { get; set; }
        public int? Cantidad { get; set; }
        public int? CantidadMax { get; set; }
        public string? Observaciones { get; set; }
    }
}
