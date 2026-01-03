using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using shared.Models;
using shared.Interfaces;
using System.Threading.Tasks;

namespace IM2B.Controllers
{
    public class AtorController : Controller
    {
        // TODO: Adicionar DbContext quando configurar o banco de dados
        private readonly IGenericRepository<Ator> _atorRepo;

        public AtorController(IGenericRepository<Ator> atorRepo)
        {
            _atorRepo = atorRepo;
        }

        // Index - Listar todos os atores
        public async Task<IActionResult> Index()
        {
            var atores = await _atorRepo.GetAllAsync();
            return View(atores);
        }

        // Details - Ver detalhes de um ator específico
        public async Task<IActionResult> Details(int id)
        {
            var ator = await _atorRepo.GetByIdAsync(id);
            if (ator == null) return NotFound();

            return View(ator);
        }

        // Create GET - Formulário para criar novo ator
        [Authorize(Roles = "Curador")]
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Create POST - Processar criação do ator
        [Authorize(Roles = "Curador")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Ator ator)
        {
            if (ModelState.IsValid)
            {
                // TODO: _context.Atores.Add(ator);
                // TODO: _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(ator);
        }

        // Edit GET - Formulário para editar ator
        [Authorize(Roles = "Curador")]
        [HttpGet("Ator/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var ator = await _atorRepo.GetByIdAsync(id);
            if (ator == null) return NotFound();

            return View(ator);
        }

        // Edit POST - Processar edição do ator
        [Authorize(Roles = "Curador")]
        [HttpPost("Ator/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Ator ator)
        {
            
            if (id != ator.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                _atorRepo.UpdateAsync(ator);
                return RedirectToAction(nameof(Index));
            }
            return View(ator);
        }

        // Delete GET - Confirmar exclusão
        [Authorize(Roles = "Curador")]
        [HttpGet]
        public IActionResult Delete(int id)
        {
            // TODO: var ator = _context.Atores.Find(id);
            // if (ator == null) return NotFound();

            //var ator = new Ator(); // Placeholder
            return View(/*ator*/);
        }

        // Delete POST - Processar exclusão
        [Authorize(Roles = "Curador")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            // TODO: var ator = _context.Atores.Include(a => a.Filmes).FirstOrDefault(a => a.Id == id);
            // if (ator == null) return NotFound();

            // TODO: _context.Atores.Remove(ator);
            // TODO: _context.SaveChanges();
            return RedirectToAction(nameof(Index));
        }

        // Buscar - Pesquisar atores por nome
        public IActionResult Buscar(string termo)
        {
            if (string.IsNullOrWhiteSpace(termo))
            {
                return RedirectToAction(nameof(Index));
            }

            // TODO: var atores = _context.Atores
            //           .Where(a => a.Nome.Contains(termo))
            //           .ToList();

            var atores = new List<Ator>(); // Placeholder
            ViewBag.TermoBusca = termo;
            return View("Index", atores);
        }
    }
}
