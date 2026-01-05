using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shared.Models;
using shared.Interfaces;
using IM2B.ViewModels.Filme;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.BearerToken;

namespace IM2B.Controllers
{
    public class FilmeController : Controller
    {
        // TODO: Adicionar DbContext quando configurar o banco de dados
        private readonly IGenericRepository<Filme> _filmeRepo;
        private readonly IGenericRepository<Ator> _atorRepo;
        private readonly IPapelRepository<Papel> _papelRepo;

        public FilmeController(IGenericRepository<Filme> filmeRepo, IGenericRepository<Ator> atorRepo, IPapelRepository<Papel> papelRepo)
        {
            _filmeRepo = filmeRepo;
            _papelRepo = papelRepo;
            _atorRepo = atorRepo;
        }

        // Index - Listar todos os filmes
        public async Task<IActionResult> Index(string search)
        {
            var filmes = await _filmeRepo.GetAllAsync();
            if (!string.IsNullOrWhiteSpace(search))
                filmes = filmes.Where(a => a.Titulo.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();
            ViewData["CurrentSearch"] = search ?? "";
            return View(filmes);
        }

        // Details - Ver detalhes de um filme específico
        public async Task<IActionResult> Details(int id)
        {
            // Get the filme entity
            var filme = await _filmeRepo.GetByIdAsync(id);
            if (filme == null) return NotFound();

            // Get all roles (papeis) for this filme, including actor info
            var papeis = await _papelRepo.GetAllForFilmeIdAsync(id);

            // Map papeis into AtorViewModel for the filme view model
            var atoresVm = papeis.Select(p => new IM2B.ViewModels.Filme.AtorViewModel
            {
                Id = p.AtorId,
                Nome = p.Ator.Nome,
                Personagem = p.Personagem,
                Principal = p.Principal,
            }).ToList();

            // Build the DetailsFilmeViewModel
            var vm = new DetailsFilmeViewModel
            {
                Id = filme.Id,
                Titulo = filme.Titulo,
                Sinopse = filme.Sinopse,
                DataLancamento = filme.DataLancamento,
                Duracao = filme.Duracao,
                Avaliacao = filme.Avaliacao,
                Atores = atoresVm
            };

            return View(vm);
        }

        // Create GET - Formulário para criar novo filme
        [Authorize(Roles = "Curador")]
        [HttpGet]
        public IActionResult Create()
        {
            var vm = new FormFilmeViewModel();
            return View(vm);
        }

        // Create POST - Processar criação do filme
        [Authorize(Roles = "Curador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FormFilmeViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var filme = new Filme
                {
                    Titulo = vm.Titulo,
                    Sinopse = vm.Sinopse,
                    DataLancamento = vm.DataLancamento,
                    Duracao = TimeSpan.FromMinutes(vm.Duracao),
                    Avaliacao = vm.Avaliacao,
                };
                int filmeId = await _filmeRepo.AddAsync(filme);
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // Edit GET - Formulário para editar filme
        [Authorize(Roles = "Curador")]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var filme = await _filmeRepo.GetByIdAsync(id);
            if (filme == null) return NotFound();

            var vm = new FormFilmeViewModel()
            {
                Id = filme.Id,
                Titulo = filme.Titulo,
                Avaliacao = filme.Avaliacao,
                DataLancamento = filme.DataLancamento,
                Duracao = (int)filme.Duracao.TotalMinutes,
                Sinopse = filme.Sinopse,
            };

            return View(vm);
        }

        // Edit POST - Processar edição do filme
        [Authorize(Roles = "Curador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FormFilmeViewModel vm)
        {
            if (id != vm.Id)
                return NotFound();

            if (ModelState.IsValid)
            {
                var filme = await _filmeRepo.GetByIdAsync(id);
                if (filme == null) 
                    return NotFound();

                filme.Titulo = vm.Titulo;
                filme.Sinopse = vm.Sinopse;
                filme.DataLancamento = vm.DataLancamento;
                filme.Duracao = TimeSpan.FromMinutes(vm.Duracao);
                filme.Avaliacao = vm.Avaliacao;

                await _filmeRepo.UpdateAsync(filme);
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // Delete POST - Processar exclusão
        [Authorize(Roles = "Curador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            return RedirectToAction(nameof(Index));
        }

        // AtribuirPapel GET - Formulário para atribuir ator a filme
        [Authorize(Roles = "Curador")]
        [HttpGet]
        public async Task<IActionResult> AtribuirPapel(int filmeId)
        {
            // Actors available to assign
            var atores = await _atorRepo.GetAllAsync();
            ViewBag.Atores = atores;

            // Already assigned actors
            var papeis = await _papelRepo.GetAllForFilmeIdAsync(filmeId);
            var atoresAtuais = papeis.Select(p => new AtorViewModel
            {
                Id = p.AtorId,
                Nome = p.Ator.Nome,
                Personagem = p.Personagem,
                Principal = p.Principal,
            }).ToList();

            ViewBag.AtoresAtuais = atoresAtuais;

            var vm = new AtribuirPapelViewModel
            {
                FilmeId = filmeId
            };

            return View(vm);
        }

        // AtribuirPapel POST - Processar atribuição de papel
        [Authorize(Roles = "Curador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AtribuirPapel(AtribuirPapelViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                // Reload actors and assigned actors for redisplay if validation fails
                var atores = await _atorRepo.GetAllAsync();
                ViewBag.Atores = atores;

                var papeis = await _papelRepo.GetAllForFilmeIdAsync(vm.FilmeId);
                var atoresAtuais = papeis.Select(p => new AtorViewModel
                {
                    Id = p.AtorId,
                    Nome = p.Ator.Nome,
                    Personagem = p.Personagem,
                    Principal = p.Principal,
                }).ToList();
                ViewBag.AtoresAtuais = atoresAtuais;

                return View(vm);
            }

            // Check if the actor is already assigned to this film
            var papeisExistentes = await _papelRepo.GetAllForFilmeIdAsync(vm.FilmeId);
            bool atorJaAssociado = papeisExistentes.Any(p => p.AtorId == vm.AtorId);

            if (atorJaAssociado)
            {
                ModelState.AddModelError("", "Este ator já está associado a este filme.");

                // Reload actors and assigned actors for redisplay
                var atores = await _atorRepo.GetAllAsync();
                ViewBag.Atores = atores;

                var atoresAtuais = papeisExistentes.Select(p => new AtorViewModel
                {
                    Id = p.AtorId,
                    Nome = p.Ator.Nome,
                    Personagem = p.Personagem,
                    Principal = p.Principal,
                }).ToList();
                ViewBag.AtoresAtuais = atoresAtuais;

                return View(vm);
            }

            var papel = new Papel
            {
                FilmeId = vm.FilmeId,
                AtorId = vm.AtorId,
                Personagem = vm.Personagem,
                Principal = vm.Principal
            };

            await _papelRepo.AddAsync(papel);

            return RedirectToAction("Details", "Filme", new { id = vm.FilmeId });
        }

        // RemoverAtor - Remover ator de um filme
        [Authorize(Roles = "Curador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoverAtor(int filmeId, int atorId)
        {
            // TODO: var filme = _context.Filmes.Include(f => f.Atores)
            //           .FirstOrDefault(f => f.Id == filmeId);

            // if (filme == null) return NotFound();

            // TODO: var ator = filme.Atores.FirstOrDefault(a => a.Id == atorId);
            // if (ator != null)
            // {
            //     filme.Atores.Remove(ator);
            //     _context.SaveChanges();
            // }

            return RedirectToAction(nameof(Details), new { id = filmeId });
        }
    }
}
