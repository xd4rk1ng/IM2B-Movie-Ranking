using Bogus;
using context.Entities;

namespace context.Factories
{
    public class Factory
    {
        private readonly Faker _faker = new("pt_PT");

        public FilmeEntity CreateRandom()
        {
            // Step 1: create the film without actors yet
            var filme = new FilmeEntity
            {
                //Id = _nextFilmeId++,
                Titulo = _faker.Lorem.Sentence(3),
                Sinopse = _faker.Lorem.Paragraph(),
                DataLancamento = DateOnly.FromDateTime(_faker.Date.Past(50)),
                Duracao = TimeSpan.FromMinutes(_faker.Random.Int(80, 180)),
                Atores = [], // fill later
                Avaliacao = _faker.Random.Int(1, 10)
            };

            // Step 2: create actors that reference this film
            
            var atores = Enumerable.Range(0, _faker.Random.Int(5, 10))
                                   .Select(_ => new AtorEntity
                                   {
                                       //Id = _nextAtorId++,
                                       Nome = _faker.Name.FullName(),
                                       DataNasc = DateOnly.FromDateTime(_faker.Date.Past(80, DateTime.Now.AddYears(-20))),
                                       DataObito = null,
                                       Biografia = _faker.Lorem.Paragraph(),
                                       Filmes = [filme] // reference back
                                   })
                                   .ToList();

            // Step 3: assign actors to film
            filme.Atores = atores;

            return filme;
        }
    }
}

