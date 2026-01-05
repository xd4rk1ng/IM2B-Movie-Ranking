using context.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace context.Seeders
{
    public class Seeder
    {
        private readonly ApplicationContext _context;

        public Seeder(ApplicationContext context)
        {
            _context = context;
        }

        public async Task SeedContentsAsync()
        {
            if (_context.Filmes.Any()) return; // evita duplicar dados se já existirem

            var atores = new List<AtorEntity>
            {
                new AtorEntity
                {
                    Nome = "Leonardo DiCaprio",
                    DataNasc = new DateOnly(1974, 11, 11),
                    Biografia = "Ator e produtor americano, conhecido por seus papéis em Titanic, Inception e The Revenant, pelo qual ganhou o Oscar."
                },
                new AtorEntity
                {
                    Nome = "Meryl Streep",
                    DataNasc = new DateOnly(1949, 6, 22),
                    Biografia = "Considerada uma das maiores atrizes de todos os tempos, vencedora de três Oscars e 21 indicações."
                },
                new AtorEntity
                {
                    Nome = "Morgan Freeman",
                    DataNasc = new DateOnly(1937, 6, 1),
                    Biografia = "Ator, diretor e narrador americano, vencedor do Oscar por Million Dollar Baby."
                },
                new AtorEntity
                {
                    Nome = "Cate Blanchett",
                    DataNasc = new DateOnly(1969, 5, 14),
                    Biografia = "Atriz australiana vencedora de dois Oscars, conhecida por sua versatilidade e talento excepcional."
                },
                new AtorEntity
                {
                    Nome = "Tom Hanks",
                    DataNasc = new DateOnly(1956, 7, 9),
                    Biografia = "Ator e produtor americano, vencedor de dois Oscars consecutivos por Philadelphia e Forrest Gump."
                },
                new AtorEntity
                {
                    Nome = "Viola Davis",
                    DataNasc = new DateOnly(1965, 8, 11),
                    Biografia = "Atriz e produtora americana, primeira atriz negra a conquistar o Triple Crown of Acting (Oscar, Emmy e Tony)."
                }
            };

            _context.Atores.AddRange(atores);
            await _context.SaveChangesAsync();

            var filmes = new List<FilmeEntity>
            {
                new FilmeEntity
                {
                    Titulo = "Inception",
                    DataLancamento = new DateOnly(2010, 7, 16),
                    Sinopse = "Um ladrão que invade os sonhos das pessoas para roubar segredos é oferecido a chance de ter seu passado criminal apagado se conseguir implantar uma ideia na mente de alguém.",
                    Avaliacao = 3,
                    Duracao = new TimeSpan(2, 28, 0), // 2h 28min
                    //Poster = "/images/inception.jpg"
                },
                new FilmeEntity
                {
                    Titulo = "The Iron Lady",
                    DataLancamento = new DateOnly(2011, 12, 26),
                    Sinopse = "A biografia de Margaret Thatcher, a primeira-ministra britânica, desde sua ascensão ao poder até seus anos finais.",
                    Avaliacao = 5,
                    Duracao = new TimeSpan(1, 45, 0), // 1h 45min
                    //Poster = "/images/ironlady.jpg"
                },
                new FilmeEntity
                {
                    Titulo = "The Shawshank Redemption",
                    DataLancamento = new DateOnly(1994, 9, 23),
                    Sinopse = "Dois homens presos formam uma amizade ao longo dos anos, encontrando consolo e eventual redenção através de atos de decência comum.",
                    Avaliacao = 4,
                    Duracao = new TimeSpan(2, 22, 0), // 2h 22min
                    //Poster = "/images/shawshank.jpg"
                }
            };

            _context.Filmes.AddRange(filmes);
            await _context.SaveChangesAsync();

            var papeis = new List<PapelEntity>
            {
                // Inception - Leonardo DiCaprio e Cate Blanchett
                new PapelEntity
                {
                    FilmeId = filmes[0].Id,
                    AtorId = atores[0].Id, // Leonardo DiCaprio
                    Personagem = "Dom Cobb",
                    Principal = true
                },
                new PapelEntity
                {
                    FilmeId = filmes[0].Id,
                    AtorId = atores[3].Id, // Cate Blanchett (papel adicional)
                    Personagem = "Mal Cobb",
                    Principal = false
                },

                // The Iron Lady - Meryl Streep e Morgan Freeman
                new PapelEntity
                {
                    FilmeId = filmes[1].Id,
                    AtorId = atores[1].Id, // Meryl Streep
                    Personagem = "Margaret Thatcher",
                    Principal = true
                },
                new PapelEntity
                {
                    FilmeId = filmes[1].Id,
                    AtorId = atores[2].Id, // Morgan Freeman
                    Personagem = "Denis Thatcher",
                    Principal = false
                },

                // The Shawshank Redemption - Morgan Freeman, Tom Hanks e Viola Davis
                new PapelEntity
                {
                    FilmeId = filmes[2].Id,
                    AtorId = atores[2].Id, // Morgan Freeman (repetindo em outro filme)
                    Personagem = "Ellis Boyd 'Red' Redding",
                    Principal = true
                },
                new PapelEntity
                {
                    FilmeId = filmes[2].Id,
                    AtorId = atores[4].Id, // Tom Hanks
                    Personagem = "Andy Dufresne",
                    Principal = true
                },
                new PapelEntity
                {
                    FilmeId = filmes[2].Id,
                    AtorId = atores[5].Id, // Viola Davis
                    Personagem = "Warden",
                    Principal = false
                }
            };

            _context.Papeis.AddRange(papeis);
            await _context.SaveChangesAsync();
        }
    }
}