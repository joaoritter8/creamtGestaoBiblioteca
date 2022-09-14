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
    public class PessoasController : Controller
    {
        private readonly GestaoBibliotecaContext _context;

        public PessoasController(GestaoBibliotecaContext context)
        {
            _context = context;
        }

        // GET: Pessoas
        public async Task<IActionResult> Index()
        {
              return _context.Pessoa != null ? 
                          View(await _context.Pessoa.ToListAsync()) :
                          Problem("Entity set 'GestaoBibliotecaContext.Pessoa'  is null.");
        }

        // GET: Pessoas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Pessoa == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoa
                .FirstOrDefaultAsync(m => m.PessoaID == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // GET: Pessoas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Pessoas/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("PessoaID,PessoaNome,PessoaCPF,PessoaRG,PessoaEndereco,PessoaTelefone,PessoaEmail,PessoaTipo,PessoaStatus")] Pessoa pessoa)
        {
            if (ModelState.IsValid)
            {
                _context.Add(pessoa);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(pessoa);
        }

        // GET: Pessoas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Pessoa == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoa.FindAsync(id);
            if (pessoa == null)
            {
                return NotFound();
            }
            return View(pessoa);
        }

        // POST: Pessoas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("PessoaID,PessoaNome,PessoaCPF,PessoaRG,PessoaEndereco,PessoaTelefone,PessoaEmail,PessoaTipo,PessoaStatus")] Pessoa pessoa)
        {
            if (id != pessoa.PessoaID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(pessoa);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!PessoaExists(pessoa.PessoaID))
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
            return View(pessoa);
        }

        // GET: Pessoas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Pessoa == null)
            {
                return NotFound();
            }

            var pessoa = await _context.Pessoa
                .FirstOrDefaultAsync(m => m.PessoaID == id);
            if (pessoa == null)
            {
                return NotFound();
            }

            return View(pessoa);
        }

        // POST: Pessoas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Pessoa == null)
            {
                return Problem("Entity set 'GestaoBibliotecaContext.Pessoa'  is null.");
            }
            var pessoa = await _context.Pessoa.FindAsync(id);
            if (pessoa != null)
            {
                _context.Pessoa.Remove(pessoa);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool PessoaExists(int id)
        {
          return (_context.Pessoa?.Any(e => e.PessoaID == id)).GetValueOrDefault();
        }
    }
}
