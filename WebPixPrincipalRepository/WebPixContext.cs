using Microsoft.EntityFrameworkCore;
using WebPixPrincipalRepository.Entity;

namespace WebPixPrincipalRepository
{
    public class WebPixContext : DbContext
    {
        public DbSet<Page> Page { get; set; }
        public DbSet<Menu> Menu { get; set; }
        public DbSet<Perfil> Perfil { get; set; }
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Estilo> Estilo { get; set; }
        public DbSet<Configuracao> Configuracao { get; set; }
        public DbSet<Estrutura> Estrutura { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
           // optionsBuilder.UseSqlServer(@"Server=DESKTOP-9B04LJT\SQLEXPRESS;Database=WebPixPrincipal;Trusted_Connection=True;Integrated Security = True;");
            optionsBuilder.UseSqlServer(
                @"Server = 187.84.229.35; Database = WebPixPrincipal; User Id = dev;Password = Lucas-2007");
            
        }
    }
}
