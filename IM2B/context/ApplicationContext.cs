using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using context.Entities;

namespace context
{
    public class ApplicationContext : IdentityDbContext<UserEntity>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
        }

        public DbSet<FilmeEntity> Filmes { get; set; }
        public DbSet<AtorEntity> Atores { get; set; }
        public DbSet<PapelEntity> Papeis { get; set; }

        // Comentado isto, porque desta forma, o EF por algum motivo criava o Papel e o AtorFilme, que no fundo sao a mesma coisa, ou seja "duplicava" a tabela
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
            builder.Entity<FilmeEntity>()
                .HasMany(e => e.Atores)
                .WithMany(e => e.Filmes)
                .UsingEntity<PapelEntity>();
        }
    }
}
        
