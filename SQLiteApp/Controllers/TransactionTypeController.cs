using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SQLiteApp.Data;
using NonProfitLibrary;

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
        return _context.TransactionTypes != null ?
                    View(await _context.TransactionTypes.ToListAsync()) :
                    Problem("Entity set 'ApplicationDbContext.TransactionTypes'  is null.");
    }

    // GET: TransactionType/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.TransactionTypes == null)
        {
            return NotFound();
        }

        var transactionType = await _context.TransactionTypes
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
            // Modified By
            transactionType.Created = DateTime.Now;
            transactionType.Modified = DateTime.Now;
            transactionType.CreatedBy = User.Identity.Name;
            transactionType.ModifiedBy = User.Identity.Name;

            _context.Add(transactionType);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(transactionType);
    }

    // GET: TransactionType/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.TransactionTypes == null)
        {
            return NotFound();
        }

        var transactionType = await _context.TransactionTypes.FindAsync(id);
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
                // Modified By
                transactionType.Modified = DateTime.Now;
                transactionType.ModifiedBy = User.Identity.Name;

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
        if (id == null || _context.TransactionTypes == null)
        {
            return NotFound();
        }

        var transactionType = await _context.TransactionTypes
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
        if (_context.TransactionTypes == null)
        {
            return Problem("Entity set 'ApplicationDbContext.TransactionTypes'  is null.");
        }
        var transactionType = await _context.TransactionTypes.FindAsync(id);
        if (transactionType != null)
        {
            _context.TransactionTypes.Remove(transactionType);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool TransactionTypeExists(int id)
    {
        return (_context.TransactionTypes?.Any(e => e.TransactionTypeId == id)).GetValueOrDefault();
    }
}
