//using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
//using Microsoft.EntityFrameworkCore;
//using IM2B.Models;

//namespace IM2B.Data
//{
//    public class ApplicationDbContext : IdentityDbContext<User>
//    {
//        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
//            : base(options)
//        {
//        }

//        public DbSet<Filme> Filmes { get; set; }
//        public DbSet<Ator> Atores { get; set; }
//        public DbSet<Papel> Papeis { get; set; }

//        protected override void OnModelCreating(ModelBuilder builder)
//        {
//            base.OnModelCreating(builder);

//            // Configurar relação M:N entre Filme e Ator através de Papel
//            builder.Entity<Papel>()
//                .HasOne(p => p.Filme)
//                .WithMany()
//                .HasForeignKey(p => p.FilmeId)
//                .OnDelete(DeleteBehavior.Cascade);

//            builder.Entity<Papel>()
//                .HasOne(p => p.Ator)
//                .WithMany()
//                .HasForeignKey(p => p.AtorId)
//                .OnDelete(DeleteBehavior.Cascade);

//            // Impedir duplicação: mesmo ator não pode ter o mesmo papel duas vezes no mesmo filme
//            builder.Entity<Papel>()
//                .HasIndex(p => new { p.FilmeId, p.AtorId, p.Personagem })
//                .IsUnique();
//        }
//    }
//}
