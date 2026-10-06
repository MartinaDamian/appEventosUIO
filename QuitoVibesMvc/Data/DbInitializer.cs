using QuitoVibesMvc.Models;
using QuitoVibesMvc.Services;

namespace QuitoVibesMvc.Data
{
    public static class DbInitializer
    {
        public static void Initialize(ApplicationDbContext context, ISecurityService securityService)
        {
            // Garantizar que la base de datos esté creada
            context.Database.EnsureCreated();

            // Seed Usuarios si la tabla está vacía
            if (!context.Users.Any())
            {
                var adminUser = new User
                {
                    Username = "admin",
                    Email = "admin@quito.gob.ec",
                    NombreCompleto = "Administrador Municipio de Quito",
                    PasswordHash = securityService.HashPasswordMd5("admin123"), // MD5 Hashed!
                    Rol = "Admin",
                    FechaRegistro = DateTime.Now
                };

                var normalUser = new User
                {
                    Username = "usuario",
                    Email = "ciudadano@quito.gob.ec",
                    NombreCompleto = "Juan Pérez (Ciudadano)",
                    PasswordHash = securityService.HashPasswordMd5("quito123"), // MD5 Hashed!
                    Rol = "Usuario",
                    FechaRegistro = DateTime.Now
                };

                context.Users.AddRange(adminUser, normalUser);
            }

            // Seed Eventos si la tabla está vacía
            if (!context.Eventos.Any())
            {
                var eventos = new List<EventoCultural>
                {
                    new EventoCultural
                    {
                        Titulo = "Pregón Nocturno y Serenata a Quito - Fiestas de Quito",
                        Descripcion = "Apertura oficial de las fiestas capitalinas con bandas de pueblo, chiva tradicional y presentación de grupos folclóricos en la Plaza Grande.",
                        Lugar = "Centro Histórico",
                        Direccion = "Plaza de la Independencia (Plaza Grande), Chile y Venezuela",
                        Fecha = DateTime.Now.AddDays(5),
                        Hora = "19:00",
                        Precio = 0.00m,
                        Categoria = "Fiestas de Quito",
                        Vibra = "Tradicional",
                        ImagenUrl = "https://images.unsplash.com/photo-1514525253161-7a46d19cd819?w=600",
                        Organizador = "Secretaría de Cultura - Municipio de Quito",
                        Estado = "Activo"
                    },
                    new EventoCultural
                    {
                        Titulo = "Noche de Museos y Plazas Iluminadas",
                        Descripcion = "Recorrido guiado por el Museo de la Ciudad, Casa del Alabado y Centro Cultural Metropolitano con ingreso gratuito y activaciones teatrales.",
                        Lugar = "Centro Histórico",
                        Direccion = "Calle García Moreno y Sucre",
                        Fecha = DateTime.Now.AddDays(10),
                        Hora = "18:00",
                        Precio = 0.00m,
                        Categoria = "Exposición",
                        Vibra = "Relajada",
                        ImagenUrl = "https://images.unsplash.com/photo-1565008447742-97f6f38c985c?w=600",
                        Organizador = "Fundación Museos de la Ciudad",
                        Estado = "Activo"
                    },
                    new EventoCultural
                    {
                        Titulo = "Festival Gastronómico 'Sabores Quiteños' y Colada Morada",
                        Descripcion = "Feria gastronómica con las mejores huecas tradicionales de Quito, guaguas de pan artesanales y degustación al aire libre.",
                        Lugar = "Parque El Ejido",
                        Direccion = "Av. Patria y Av. 6 de Diciembre",
                        Fecha = DateTime.Now.AddDays(12),
                        Hora = "10:00",
                        Precio = 5.00m,
                        Categoria = "Gastronomía",
                        Vibra = "Familiar",
                        ImagenUrl = "https://images.unsplash.com/photo-1555396273-367ea4eb4db5?w=600",
                        Organizador = "Quito Turismo / Municipio de Quito",
                        Estado = "Activo"
                    },
                    new EventoCultural
                    {
                        Titulo = "Gala Sinfónica 'Sinfonía bajo las Estrellas'",
                        Descripcion = "Concierto magno de la Orquesta Sinfónica Nacional del Ecuador en el escenario histórico del Teatro Nacional Sucre.",
                        Lugar = "Centro Histórico",
                        Direccion = "Plaza del Teatro, Manabí y Flores",
                        Fecha = DateTime.Now.AddDays(15),
                        Hora = "20:00",
                        Precio = 12.50m,
                        Categoria = "Concierto",
                        Vibra = "Cultural",
                        ImagenUrl = "https://images.unsplash.com/photo-1465847899084-d164df4dedc6?w=600",
                        Organizador = "Fundación Teatro Nacional Sucre",
                        Estado = "Activo"
                    },
                    new EventoCultural
                    {
                        Titulo = "Urban Beats Quito - Festival de Música Independiente",
                        Descripcion = "Festival al aire libre reuniendo a bandas de indie rock, pop y beats urbanos emergentes de la capital.",
                        Lugar = "La Mariscal",
                        Direccion = "Plaza Quinde (Plaza Foch)",
                        Fecha = DateTime.Now.AddDays(20),
                        Hora = "16:00",
                        Precio = 0.00m,
                        Categoria = "Concierto",
                        Vibra = "Juvenil",
                        ImagenUrl = "https://images.unsplash.com/photo-1470225620780-dba8ba36b745?w=600",
                        Organizador = "Administración Zonal La Mariscal",
                        Estado = "Activo"
                    }
                };

                context.Eventos.AddRange(eventos);
            }

            context.SaveChanges();
        }
    }
}
