using Bogus;
using context.Entities;

namespace IM2B.Factories
{
    public class FilmeFactory
    {
        private int _nextId = 1;
        private readonly Faker _faker = new Faker("pt_PT");

        public FilmeEntity CreateRandom()
        {
            // Step 1: create the film without actors yet
            var filme = new FilmeEntity
            {
                Id = _nextId++,
                Titulo = _faker.Lorem.Sentence(3),
                Sinopse = _faker.Lorem.Paragraph(),
                DataLancamento = DateOnly.FromDateTime(_faker.Date.Past(50)),
                Duracao = TimeSpan.FromMinutes(_faker.Random.Int(80, 180)),
                Atores = new List<AtorEntity>(), // fill later
                Avaliacao = _faker.Random.Int(1, 10)
            };

            // Step 2: create actors that reference this film
            int atorId = 1;
            var atores = Enumerable.Range(0, _faker.Random.Int(5, 10))
                                   .Select(_ => new AtorEntity
                                   {
                                       Id = atorId++,
                                       Nome = _faker.Name.FullName(),
                                       DataNasc = DateOnly.FromDateTime(_faker.Date.Past(80, DateTime.Now.AddYears(-20))),
                                       DataObito = null,
                                       Biografia = _faker.Lorem.Paragraph(),
                                       Filmes = new List<FilmeEntity> { filme } // reference back
                                   })
                                   .ToList();

            // Step 3: assign actors to film
            filme.Atores = atores;

            return filme;
        }
    }
}

