using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestaoBiblioteca.Data;
using SolucaoCrea.Models;

namespace GestaoBiblioteca.Controllers
{
    public class MovimentacoesController : Controller
    {
        private readonly GestaoBibliotecaContext _context;

        public MovimentacoesController(GestaoBibliotecaContext context)
        {
            _context = context;
        }

        // GET: Movimentacoes
        public async Task<IActionResult> Index()
        {
            var gestaoBibliotecaContext = _context.Movimentacao.Include(m => m.Livro).Include(m => m.Pessoa);
            return View(await gestaoBibliotecaContext.ToListAsync());
        }

        // GET: Movimentacoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Movimentacao == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.Movimentacao
                .Include(m => m.Livro)
                .Include(m => m.Pessoa)
                .FirstOrDefaultAsync(m => m.MovimentacaoID == id);
            if (movimentacao == null)
            {
                return NotFound();
            }

            return View(movimentacao);
        }

        // GET: Movimentacoes/Create
        public IActionResult Create()
        {
            ViewData["Livros"] = new SelectList(_context.Livro?.Where(l=> l.LivroStatus == "DP"), "LivroID", "LivroTitulo");
            ViewData["Pessoas"] = new SelectList(_context.Pessoa?.Where(p => p.PessoaStatus), "PessoaID", "PessoaNome");
            ViewData["Profissionais"] = new SelectList(_context.Pessoa?.Where(p=> p.PessoaTipo == "PR" && p.PessoaStatus), "PessoaID", "PessoaNome");
            return View();
        }

        // POST: Movimentacoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MovimentacaoID,LivroID,PessoaID,ProfID,DataEmprestimo,DataMaxima,DataDevolucao,MovimentacaoStatus")] Movimentacao movimentacao)
        {
            if (ModelState.IsValid)
            {
                _context.Add(movimentacao);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Livros"] = new SelectList(_context.Livro?.Where(l => l.LivroStatus == "DP"), "LivroID", "LivroTitulo", movimentacao.LivroID);
            ViewData["Pessoas"] = new SelectList(_context.Pessoa?.Where(p => p.PessoaStatus), "PessoaID", "PessoaNome", movimentacao.PessoaID);
            ViewData["Profissionais"] = new SelectList(_context.Pessoa?.Where(p => p.PessoaTipo == "PR" && p.PessoaStatus), "PessoaID", "PessoaNome", movimentacao.ProfID);
            return View(movimentacao);
        }

        // GET: Movimentacoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Movimentacao == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.Movimentacao.FindAsync(id);
            if (movimentacao == null)
            {
                return NotFound();
            }
            ViewData["Livros"] = new SelectList(_context.Livro?.Where(l => l.LivroStatus == "DP"), "LivroID", "LivroTitulo", movimentacao.LivroID);
            ViewData["Pessoas"] = new SelectList(_context.Pessoa?.Where(p => p.PessoaStatus), "PessoaID", "PessoaNome", movimentacao.PessoaID);
            ViewData["Profissionais"] = new SelectList(_context.Pessoa?.Where(p => p.PessoaTipo == "PR" && p.PessoaStatus), "PessoaID", "PessoaNome", movimentacao.ProfID);
            return View(movimentacao);
        }

        // POST: Movimentacoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovimentacaoID,LivroID,PessoaID,ProfID,DataEmprestimo,DataMaxima,DataDevolucao,MovimentacaoStatus")] Movimentacao movimentacao)
        {
            if (id != movimentacao.MovimentacaoID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(movimentacao);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovimentacaoExists(movimentacao.MovimentacaoID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["Livros"] = new SelectList(_context.Livro?.Where(l => l.LivroStatus == "DP"), "LivroID", "LivroTitulo", movimentacao.LivroID);
            ViewData["Pessoas"] = new SelectList(_context.Pessoa?.Where(p => p.PessoaStatus), "PessoaID", "PessoaNome", movimentacao.PessoaID);
            ViewData["Profissionais"] = new SelectList(_context.Pessoa?.Where(p => p.PessoaTipo == "PR" && p.PessoaStatus), "PessoaID", "PessoaNome", movimentacao.ProfID);
            return View(movimentacao);
        }

        // GET: Movimentacoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Movimentacao == null)
            {
                return NotFound();
            }

            var movimentacao = await _context.Movimentacao
                .Include(m => m.Livro)
                .Include(m => m.Pessoa)
                .FirstOrDefaultAsync(m => m.MovimentacaoID == id);
            if (movimentacao == null)
            {
                return NotFound();
            }

            return View(movimentacao);
        }

        // POST: Movimentacoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Movimentacao == null)
            {
                return Problem("Entity set 'GestaoBibliotecaContext.Movimentacao'  is null.");
            }
            var movimentacao = await _context.Movimentacao.FindAsync(id);
            if (movimentacao != null)
            {
                _context.Movimentacao.Remove(movimentacao);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MovimentacaoExists(int id)
        {
          return (_context.Movimentacao?.Any(e => e.MovimentacaoID == id)).GetValueOrDefault();
        }
    }
}
