using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GestaoBiblioteca.Data;
using TesteBiblioteca.Models;

namespace GestaoBiblioteca.Controllers
{
    public class LivrosController : Controller
    {
        private readonly GestaoBibliotecaContext _context;

        public LivrosController(GestaoBibliotecaContext context)
        {
            _context = context;
        }

        // GET: Livros
        public async Task<IActionResult> Index()
        {
            var gestaoBibliotecaContext = _context.Livro.Include(l => l.Autor).Include(l => l.Categoria);
            return View(await gestaoBibliotecaContext.ToListAsync());
        }

        // GET: Livros/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Livro == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(m => m.LivroID == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // GET: Livros/Create
        public IActionResult Create()
        {
            ViewData["Autores"] = new SelectList(_context.Pessoa?.Where(p => p.PessoaTipo == "AU" && p.PessoaStatus), "PessoaID", "PessoaNome");
            ViewData["Categorias"] = new SelectList(_context.Categoria?.Where(c => c.CatStatus), "CatID", "CatNome");
            return View();
        }

        // POST: Livros/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LivroID,LivroISBN,LivroTitulo,PessoaID,LivroEditora,LivroEdicao,LivroAno,CatID,LivroStatus")] Livro livro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(livro);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["Autores"] = new SelectList(_context.Pessoa?.Where(p => p.PessoaTipo == "AU" && p.PessoaStatus), "PessoaID", "PessoaNome", livro.PessoaID);
            ViewData["Categorias"] = new SelectList(_context.Categoria?.Where(c => c.CatStatus == true), "CatID", "CatNome", livro.CatID);
            return View(livro);
        }

        // GET: Livros/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Livro == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro.FindAsync(id);
            if (livro == null)
            {
                return NotFound();
            }
            ViewData["Autores"] = new SelectList(_context.Pessoa?.Where(p => p.PessoaTipo == "AU" && p.PessoaStatus), "PessoaID", "PessoaNome", livro.PessoaID);
            ViewData["Categorias"] = new SelectList(_context.Categoria?.Where(c => c.CatStatus == true), "CatID", "CatNome", livro.CatID);
            return View(livro);
        }

        // POST: Livros/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LivroID,LivroISBN,LivroTitulo,PessoaID,LivroEditora,LivroEdicao,LivroAno,CatID,LivroStatus")] Livro livro)
        {
            if (id != livro.LivroID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(livro);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LivroExists(livro.LivroID))
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
            ViewData["Autores"] = new SelectList(_context.Pessoa?.Where(p => p.PessoaTipo == "AU" && p.PessoaStatus), "PessoaID", "PessoaNome", livro.PessoaID);
            ViewData["Categorias"] = new SelectList(_context.Categoria?.Where(c => c.CatStatus == true), "CatID", "CatNome", livro.CatID);
            return View(livro);
        }

        // GET: Livros/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Livro == null)
            {
                return NotFound();
            }

            var livro = await _context.Livro
                .Include(l => l.Autor)
                .Include(l => l.Categoria)
                .FirstOrDefaultAsync(m => m.LivroID == id);
            if (livro == null)
            {
                return NotFound();
            }

            return View(livro);
        }

        // POST: Livros/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Livro == null)
            {
                return Problem("Entity set 'GestaoBibliotecaContext.Livro'  is null.");
            }
            var livro = await _context.Livro.FindAsync(id);
            if (livro != null)
            {
                _context.Livro.Remove(livro);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LivroExists(int id)
        {
          return (_context.Livro?.Any(e => e.LivroID == id)).GetValueOrDefault();
        }
    }
}
