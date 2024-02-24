using Microsoft.EntityFrameworkCore;

namespace Fernando_Musto_LaboratorioPracticodeWebApi.Models
{
    public class blogContext :DbContext
    {
        public blogContext( DbContextOptions<blogContext> options):base(options) 
        { 
        
        
        }

        public DbSet<calificaciones> calificaciones { get; set; }
        public DbSet<comentarios> comentarios { get; set; }
        public DbSet<publicaciones> publicaciones { get; set;}
        public DbSet<roles> roles { get; set; }
        public DbSet<usuarios> usuarios { get; set; }

    }
}
