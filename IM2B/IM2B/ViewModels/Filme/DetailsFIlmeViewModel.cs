using System;
using System.Collections.Generic;

namespace IM2B.ViewModels.Filme
{
    public class DetailsFilmeViewModel
    {
        public int Id { get; set; }
        public string Titulo { get; set; } = "";
        public string Sinopse { get; set; } = "";
        public DateOnly DataLancamento { get; set; }
        public TimeSpan Duracao { get; set; }
        public int Avaliacao { get; set; }

        public List<AtorViewModel>? Atores { get; set; } = new();
    }
}
