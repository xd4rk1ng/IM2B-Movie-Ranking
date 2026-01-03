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

        public DbSet<User> Users { get; set; }
        public DbSet<FilmeEntity> Filmes { get; set; }
        public DbSet<AtorEntity> Atores { get; set; }
        public DbSet<PapelEntity> Papeis { get; set; }

       
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<FilmeEntity>()
                .HasMany(f => f.Atores)
                .WithMany(a => a.Filmes)
                .UsingEntity<PapelEntity>(
                    j => j.HasOne(p => p.Ator).WithMany().HasForeignKey(p => p.AtorId),
                    j => j.HasOne(p => p.Filme).WithMany().HasForeignKey(p => p.FilmeId),
                    j => j.Property(p => p.Personagem).HasDefaultValue("Desconhecido")
                );

        }
    }
}
        
