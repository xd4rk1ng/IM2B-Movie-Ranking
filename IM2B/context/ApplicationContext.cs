using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using context.Entities;

namespace context
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Ator> Atores { get; set; }
        public DbSet<Papel> Papeis { get; set; }

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    base.OnModelCreating(builder);

        //    // Configurar relação M:N entre Filme e Ator através de Papel
        //    builder.Entity<Papel>()
        //        .HasOne(p => p.Filme)
        //        .WithMany()
        //        .HasForeignKey(p => p.FilmeId)
        //        .OnDelete(DeleteBehavior.Cascade);

        //    builder.Entity<Papel>()
        //        .HasOne(p => p.Ator)
        //        .WithMany()
        //        .HasForeignKey(p => p.AtorId)
        //        .OnDelete(DeleteBehavior.Cascade);

        //    // Impedir duplicação: mesmo ator não pode ter o mesmo papel duas vezes no mesmo filme
        //    builder.Entity<Papel>()
        //        .HasIndex(p => new { p.FilmeId, p.AtorId, p.Personagem })
        //        .IsUnique();
        //}


        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Filme>()
                .HasMany(e => e.Atores)
                .WithMany(e => e.Filmes)
                .UsingEntity<Papel>();
        }
    }
}
        
