using shared.Models;
using context.Entities;

namespace context.Mappings
{
    public static class AtorMapper
    {
        public static Ator ToModel(this AtorEntity entity)
        {
            // Usa-se o construtor da classe para permitir validacoes e logica de negocio
            return new Ator(
                entity.Id,
                entity.Nome,
                entity.DataNasc,
                entity.DataObito,
                entity.Biografia,
                entity.Filmes?.Select(e => e.ToModel()).ToList() ?? new List<Filme>()
            );
        }

        public static AtorEntity ToEntity(this Ator model)
        {
            // Usa-se este inicializador basico porque a entidade nao deve possuir metodos, ou seja, nenhum construtor a usar
            return new()
            {
                Id = model.Id,
                Nome = model.Nome,
                DataNasc = model.DataNasc,
                DataObito = model.DataObito,
                Biografia = model.Biografia,
                Filmes = model.Filmes.Select(e => e.ToEntity()).ToList(),
            };
        }
    }
}
