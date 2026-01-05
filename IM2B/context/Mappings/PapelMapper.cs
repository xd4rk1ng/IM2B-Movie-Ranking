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
                entity.Filme?.ToModel() ?? new Filme(),
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
                AtorId = model.AtorId,
                Personagem = model.Personagem,
                Principal = model.Principal
            };
        }
    }
}
