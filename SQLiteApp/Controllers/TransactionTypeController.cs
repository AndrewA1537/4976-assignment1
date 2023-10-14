using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using NonProfitLibrary;
using SQLiteApp.Data;
using System.Linq;
using System;

namespace SQLiteApp.Controllers;

[Authorize(Roles = "Admin")]
public class TransactionTypeController : Controller
{
    private readonly ApplicationDbContext _context;

    public TransactionTypeController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: TransactionType
    public async Task<IActionResult> Index()
    {
        return _context.TransactionType != null ?
                    View(await _context.TransactionType.ToListAsync()) :
                    Problem("Entity set 'ApplicationDbContext.TransactionType'  is null.");
    }

    // GET: TransactionType/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.TransactionType == null)
        {
            return NotFound();
        }

        var transactionType = await _context.TransactionType
            .FirstOrDefaultAsync(m => m.TransactionTypeId == id);
        if (transactionType == null)
        {
            return NotFound();
        }

        return View(transactionType);
    }

    // GET: TransactionType/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: TransactionType/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("TransactionTypeId,Name,Description")] TransactionType transactionType)
    {
        if (ModelState.IsValid)
        {
            _context.Add(transactionType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(transactionType);
    }

    // GET: TransactionType/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.TransactionType == null)
        {
            return NotFound();
        }

        var transactionType = await _context.TransactionType.FindAsync(id);
        if (transactionType == null)
        {
            return NotFound();
        }
        return View(transactionType);
    }

    // POST: TransactionType/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("TransactionTypeId,Name,Description")] TransactionType transactionType)
    {
        if (id != transactionType.TransactionTypeId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(transactionType);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionTypeExists(transactionType.TransactionTypeId))
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
        return View(transactionType);
    }

    // GET: TransactionType/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.TransactionType == null)
        {
            return NotFound();
        }

        var transactionType = await _context.TransactionType
            .FirstOrDefaultAsync(m => m.TransactionTypeId == id);
        if (transactionType == null)
        {
            return NotFound();
        }

        return View(transactionType);
    }

    // POST: TransactionType/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.TransactionType == null)
        {
            return Problem("Entity set 'ApplicationDbContext.TransactionType'  is null.");
        }
        var transactionType = await _context.TransactionType.FindAsync(id);
        if (transactionType != null)
        {
            _context.TransactionType.Remove(transactionType);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TransactionTypeExists(int id)
    {
        return (_context.TransactionType?.Any(e => e.TransactionTypeId == id)).GetValueOrDefault();
    }
}
