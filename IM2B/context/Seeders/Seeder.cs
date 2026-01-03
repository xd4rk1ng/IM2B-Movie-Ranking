using Bogus;
using context.Entities;
using context.Factories;

namespace context.Seeders
{
    public class Seeder
    {
        private readonly ApplicationContext _context;
        private readonly Factory _factory;
        private readonly Faker _faker = new("pt_PT");

        public Seeder(ApplicationContext context)
        {
            _context = context;
            _factory = new Factory();
        }

        public void Seed(int count = 10)
        {
            if (_context.Filmes.Any()) return; // avoid duplicate seeding

            for (int i = 0; i < count; i++)
            {
                var filme = _factory.CreateRandom();

                // Track film and actors first
                _context.Filmes.Add(filme);
                _context.Atores.AddRange(filme.Atores);
                _context.SaveChanges();

                // Now create PapelEntity using IDs (safer than relying on navigation properties)
                var papeis = filme.Atores.Select(ator => new PapelEntity
                {
                    FilmeId = filme.Id,
                    AtorId = ator.Id,
                    Personagem = _faker.Name.FirstName(),
                    Principal = _faker.Random.Bool()
                }).ToList();

                _context.Papeis.AddRange(papeis);
                _context.SaveChanges();
            }
            _context.SaveChanges();
        }
    }
}
