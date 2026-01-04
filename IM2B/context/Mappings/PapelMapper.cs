using shared.Models;
using context.Entities;

namespace context.Mappings
{
    public static class PapelMapper
    {
        public static Papel ToModel(this PapelEntity entity)
        {
            // Usa-se o construtor da classe para permitir validacoes e logica de negocio
            return new
            (
                entity.Id,
                entity.FilmeId,
                entity.Filme.ToModel(),
                entity.AtorId,
                entity.Ator?.ToModel() ?? new Ator(),
                entity.Personagem,
                entity.Principal
            );
        }

        public static PapelEntity ToEntity(this Papel model)
        {
            // Usa-se este inicializador basico porque a entidade nao deve possuir metodos, ou seja, nenhum construtor a usar
            return new()
            {
                Id = model.Id,
                FilmeId = model.FilmeId,
                Filme = model.Filme.ToEntity(),
                AtorId = model.AtorId,
                Ator = model.Ator.ToEntity(),
                Personagem = model.Personagem,
                Principal = model.Principal
            };
        }
    }
}
