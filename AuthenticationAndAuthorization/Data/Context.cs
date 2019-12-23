using AuthenticationAndAuthorization.Map;
using AuthenticationAndAuthorization.Models;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAndAuthorization.Data
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options) { }

        public DbSet<USUARIO> USUARIO { get; set; }
        public DbSet<PERFIL> PERFIL { get; set; }
        public DbSet<PERFIL_USUARIO> PERFIL_USUARIO { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PERFILMap());
            modelBuilder.ApplyConfiguration(new USUARIOMap());
            modelBuilder.ApplyConfiguration(new PERFIL_USUARIOMap());
        }
    }
}
