using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using shared.Models;
using IM2B.ViewModels;
using shared.Interfaces;

namespace IM2B.Controllers
{
    public class FilmeController : Controller
    {
        // TODO: Adicionar DbContext quando configurar o banco de dados
        private readonly IGenericRepository<Filme> _filmeRepo;

        public FilmeController(IGenericRepository<Filme> filmeRepo)
        {
            _filmeRepo = filmeRepo;
        }

        // Index - Listar todos os filmes
        public async Task<IActionResult> Index()
        {
            var filmes = await _filmeRepo.GetAllAsync();
            //var filmes = new List<Filme>(); // Placeholder
            return View(filmes);
        }

        // Details - Ver detalhes de um filme específico
        public IActionResult Details(int id)
        {
            // TODO: var filme = _context.Filmes.Include(f => f.Atores).FirstOrDefault(f => f.Id == id);
            // if (filme == null) return NotFound();

            //var filme = new Filme(); // Placeholder
            return View(/*filme*/);
        }

        // Create GET - Formulário para criar novo filme
        [Authorize(Roles = "Curador")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create POST - Processar criação do filme
        [Authorize(Roles = "Curador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Filme filme)
        {
            if (ModelState.IsValid)
            {
                // TODO: _context.Filmes.Add(filme);
                // TODO: _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(filme);
        }

        // Edit GET - Formulário para editar filme
        [Authorize(Roles = "Curador")]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            // TODO: var filme = _context.Filmes.Find(id);
            // if (filme == null) return NotFound();

            //var filme = new Filme(); // Placeholder
            return View(/*filme*/);
        }

        // Edit POST - Processar edição do filme
        [Authorize(Roles = "Curador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Filme filme)
        {
            if (id != filme.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                // TODO: _context.Update(filme);
                // TODO: _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(filme);
        }

        // Delete GET - Confirmar exclusão
        [Authorize(Roles = "Curador")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            // TODO: var filme = _context.Filmes.Find(id);
            // if (filme == null) return NotFound();

            //var filme = new Filme(); // Placeholder
            return View(/*filme*/);
        }

        // Delete POST - Processar exclusão
        [Authorize(Roles = "Curador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: var filme = _context.Filmes.Find(id);
            // TODO: _context.Filmes.Remove(filme);
            // TODO: _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // AtribuirPapel GET - Formulário para atribuir ator a filme
        [Authorize(Roles = "Curador")]
        [HttpGet]
        public IActionResult AtribuirPapel(int filmeId)
        {
            // TODO: var filme = _context.Filmes.Find(filmeId);
            // if (filme == null) return NotFound();

            // TODO: ViewBag.Atores = new SelectList(_context.Atores, "Id", "Nome");

            var viewModel = new AtribuirPapelViewModel
            {
                FilmeId = filmeId
            };

            return View(viewModel);
        }

        // AtribuirPapel POST - Processar atribuição de papel
        [Authorize(Roles = "Curador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AtribuirPapel(AtribuirPapelViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                // TODO: ViewBag.Atores = new SelectList(_context.Atores, "Id", "Nome");
                return View(viewModel);
            }

            // Validação: Impedir atribuição duplicada
            // TODO: var filme = _context.Filmes.Include(f => f.Atores)
            //           .FirstOrDefault(f => f.Id == viewModel.FilmeId);

            // TODO: var atorJaAssociado = filme.Atores.Any(a => a.Id == viewModel.AtorId);

            // if (atorJaAssociado)
            // {
            //     ModelState.AddModelError("", "Este ator já está associado a este filme.");
            //     ViewBag.Atores = new SelectList(_context.Atores, "Id", "Nome");
            //     return View(viewModel);
            // }

            // TODO: var ator = _context.Atores.Find(viewModel.AtorId);
            // TODO: filme.Atores.Add(ator);
            // TODO: _context.SaveChanges();

            return RedirectToAction(nameof(Details), new { id = viewModel.FilmeId });
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
