using System.ComponentModel.DataAnnotations;

namespace QuitoVibesMvc.Models
{
    public class EventoCultural
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "El título del evento es obligatorio.")]
        [StringLength(100, ErrorMessage = "El título no puede exceder los 100 caracteres.")]
        [Display(Name = "Título del Evento")]
        public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "La descripción es obligatoria.")]
        [StringLength(1000, ErrorMessage = "La descripción no puede exceder los 1000 caracteres.")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; } = string.Empty;

        [Required(ErrorMessage = "El lugar o sector es obligatorio.")]
        [StringLength(100)]
        [Display(Name = "Lugar / Sector (ej. Centro Histórico)")]
        public string Lugar { get; set; } = string.Empty;

        [StringLength(200)]
        [Display(Name = "Dirección Exacta")]
        public string Direccion { get; set; } = string.Empty;

        [Required(ErrorMessage = "La fecha del evento es obligatoria.")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha del Evento")]
        public DateTime Fecha { get; set; } = DateTime.Now.AddDays(7);

        [StringLength(20)]
        [Display(Name = "Hora de Inicio")]
        public string Hora { get; set; } = "18:00";

        [Range(0, 1000, ErrorMessage = "El precio debe estar entre $0 y $1000.")]
        [Display(Name = "Precio ($ USD)")]
        public decimal Precio { get; set; } = 0;

        [Required(ErrorMessage = "La categoría es obligatoria.")]
        [StringLength(50)]
        [Display(Name = "Categoría")]
        public string Categoria { get; set; } = "Concierto"; // Concierto, Feria, Teatro, Fiestas de Quito, Exposición, Gastronomía

        [StringLength(50)]
        [Display(Name = "Vibra / Estilo")]
        public string Vibra { get; set; } = "Tradicional"; // Tradicional, Familiar, Juvenil, Nocturna, Relajada

        [Display(Name = "URL de Imagen del Evento")]
        public string ImagenUrl { get; set; } = "https://images.unsplash.com/photo-1514525253161-7a46d19cd819?w=600";

        [StringLength(100)]
        [Display(Name = "Entidad / Organizador")]
        public string Organizador { get; set; } = "Secretaría de Cultura - Municipio de Quito";

        [Display(Name = "Estado del Evento")]
        public string Estado { get; set; } = "Activo"; // Activo, Finalizado, Cancelado
    }
}
