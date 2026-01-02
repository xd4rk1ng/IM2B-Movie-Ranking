using System;
using context;
using IM2B.Factories;

namespace IM2B.Seeders
{
    public class FilmeSeeder
    {
        private readonly ApplicationContext _context;
        private readonly FilmeFactory _factory;

        public FilmeSeeder(ApplicationContext context)
        {
            _context = context;
            _factory = new FilmeFactory();
        }

        public void Seed(int count = 50)
        {
            if (_context.Filmes.Any()) return; // avoid duplicate seeding

            var filmes = Enumerable.Range(0, count)
                                   .Select(_ => _factory.CreateRandom())
                                   .ToList();

            _context.Filmes.AddRange(filmes);
            _context.SaveChanges();
        }
    }
}
