using shared.Models;
using context.Entities;

namespace context.Mappings
{
    public static class FilmeMapper
    {
        public static Filme ToModel(this FilmeEntity entity)
        {
            // Usa-se o construtor da classe para permitir validacoes e logica de negocio
            return new
            (
                entity.Id,
                entity.Titulo,
                entity.Sinopse,
                entity.DataLancamento,
                entity.Duracao,
                entity.Atores.Select(e => e.ToModel()).ToList(),
                entity.Avaliacao
            );
        }

        public static FilmeEntity ToEntity(this Filme model)
        {
            // Usa-se este inicializador basico porque a entidade nao deve possuir metodos, ou seja, nenhum construtor a usar
            return new()
            {
                Id = model.Id,
                Titulo = model.Titulo,
                Sinopse = model.Sinopse,
                DataLancamento = model.DataLancamento,
                Duracao = model.Duracao,
                Atores = model.Atores.Select(e => e.ToEntity()).ToList(),
                Avaliacao = model.Avaliacao
            };
        }
    }
}
