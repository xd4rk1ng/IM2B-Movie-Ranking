using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using shared.Models;
using shared.Interfaces;
using System.Threading.Tasks;
using IM2B.ViewModels.Ator;

namespace IM2B.Controllers
{
    public class AtorController : Controller
    {
        // TODO: Adicionar DbContext quando configurar o banco de dados
        private readonly IGenericRepository<Ator> _atorRepo;
        private readonly IPapelRepository<Papel> _papelRepo;

        public AtorController(IGenericRepository<Ator> atorRepo, IPapelRepository<Papel> papelRepo)
        {
            _atorRepo = atorRepo;
            _papelRepo = papelRepo;
        }

        // Index - Listar todos os atores
        public async Task<IActionResult> Index(string search)
        {
            var atores = await _atorRepo.GetAllAsync();
            if (!string.IsNullOrWhiteSpace(search))
                atores = atores.Where(a => a.Nome.Contains(search, StringComparison.OrdinalIgnoreCase)).ToList();

            ViewData["CurrentSearch"] = search ?? "";
            return View(atores);
        }

        // Details - Ver detalhes de um ator específico
        public async Task<IActionResult> Details(int id)
        {
            var ator = await _atorRepo.GetByIdAsync(id);
            if (ator == null) return NotFound();

            var papeis = await _papelRepo.GetAllForAtorIdAsync(id);
            var papeisVm = papeis.Select(p => new PapelViewModel
            {
                Id = p.Id,
                FilmeId = p.FilmeId,
                FilmeTitulo = p.Filme.Titulo,  // assuming p.Filme is included
                Personagem = p.Personagem,
                Principal = p.Principal,
                DataLancamento = p.Filme.DataLancamento
            }).ToList();

            var vm = new DetailsAtorViewModel()
            {
                Id = ator.Id,
                Nome = ator.Nome,
                Biografia = ator.Biografia,
                DataNasc = ator.DataNasc,
                DataObito = ator.DataObito,
                Papeis = papeisVm,
            };

            return View(vm);
        }

        // Create GET - Formulário para criar novo ator
        [Authorize(Roles = "Curador")]
        [HttpGet("Ator/Create/")]
        public IActionResult Create()
        {
            var vm = new FormAtorViewModel();
            return View(vm);
        }

        // Create POST - Processar criação do ator
        [Authorize(Roles = "Curador")]
        [HttpPost("Ator/Create/")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(FormAtorViewModel vm)
        {
            if (ModelState.IsValid)
            {
                var ator = new Ator
                {
                    Nome = vm.Nome,
                    DataNasc = vm.DataNasc,
                    DataObito = vm.DataObito,
                    Biografia = vm.Biografia
                };
                int atorId = await _atorRepo.AddAsync(ator);
                return RedirectToAction(nameof(Details), new { id = atorId });
            }
            return View(vm);
        }

        // Edit GET - Formulário para editar ator
        [Authorize(Roles = "Curador")]
        [HttpGet("Ator/Edit/{id}")]
        public async Task<IActionResult> Edit(int id)
        {
            var ator = await _atorRepo.GetByIdAsync(id);
            if (ator == null) return NotFound();

            var vm = new FormAtorViewModel()
            {
                Id = ator.Id,
                Nome = ator.Nome,
                DataNasc = ator.DataNasc,
                DataObito = ator.DataObito,
                Biografia = ator.Biografia
            };

            return View(vm);
        }

        // Edit POST - Processar edição do ator
        [Authorize(Roles = "Curador")]
        [HttpPost("Ator/Edit/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, FormAtorViewModel vm)
        {
            
            if (id != vm.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var ator = await _atorRepo.GetByIdAsync(id);
                if (ator == null)
                    return NotFound();
                ator.Nome = vm.Nome;
                ator.DataNasc = vm.DataNasc;
                ator.DataObito = vm.DataObito;
                ator.Biografia = vm.Biografia;


                await _atorRepo.UpdateAsync(ator);
                return RedirectToAction(nameof(Index));
            }
            return View(vm);
        }

        // Delete GET - Exibir confirmação de exclusão
        [Authorize(Roles = "Curador")]
        [HttpGet("Ator/Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var ator = await _atorRepo.GetByIdAsync(id);
            if (ator == null)
                return NotFound();

            return View(ator);
        }

        // Delete POST - Processar exclusão
        [Authorize(Roles = "Curador")]
        [HttpPost("Ator/Delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _atorRepo.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }


        // Delete POST - Processar exclusão
        //[Authorize(Roles = "Curador")]
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Delete(int id)
        //{
        // So para ter meeeesmo a certeza
        //    var result = await _atorRepo.GetByIdAsync(id);
        //    if (result == null)
        //        return NotFound();
        //
        //    await _atorRepo.DeleteAsync(id);
        //    return RedirectToAction(nameof(Index));
        //}

        // Buscar - Pesquisar atores por nome
        public IActionResult Search(string termo)
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
