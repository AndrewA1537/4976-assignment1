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

[Authorize(Roles = "Admin, Finance")]
public class DonationsController : Controller
{
    private readonly ApplicationDbContext _context;

    public DonationsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: Donations
    public async Task<IActionResult> Index()
    {
        var applicationDbContext = _context.Donations!.Include(d => d.Account).Include(d => d.PaymentMethod).Include(d => d.TransactionType);
        return View(await applicationDbContext.ToListAsync());
    }

    // GET: Donations/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.Donations == null)
        {
            return NotFound();
        }

        var donations = await _context.Donations
            .Include(d => d.Account)
            .Include(d => d.PaymentMethod)
            .Include(d => d.TransactionType)
            .FirstOrDefaultAsync(m => m.TransId == id);
        if (donations == null)
        {
            return NotFound();
        }

        return View(donations);
    }

    // GET: Donations/Create
    public IActionResult Create()
    {
        ViewData["AccountNo"] = new SelectList(_context.ContactLists, "AccountNo", "Email");
        ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "PaymentMethodId", "Name");
        ViewData["TransactionTypeId"] = new SelectList(_context.TransactionTypes, "TransactionTypeId", "Name");
        return View();
    }

    // POST: Donations/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("TransId,Date,AccountNo,TransactionTypeId,Amount,PaymentMethodId,Notes,Created,Modified,CreatedBy,ModifiedBy")] Donations donations)
    {
        if (ModelState.IsValid)
        {
            // Modified By
            donations.Created = DateTime.Now;
            donations.Modified = DateTime.Now;
            donations.CreatedBy = User.Identity.Name;
            donations.ModifiedBy = User.Identity.Name;

            _context.Add(donations);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["AccountNo"] = new SelectList(_context.ContactLists, "AccountNo", "Email", donations.AccountNo);
        ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "PaymentMethodId", "Name", donations.PaymentMethodId);
        ViewData["TransactionTypeId"] = new SelectList(_context.TransactionTypes, "TransactionTypeId", "Name", donations.TransactionTypeId);
        return View(donations);
    }

    // GET: Donations/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.Donations == null)
        {
            return NotFound();
        }

        var donations = await _context.Donations.FindAsync(id);
        if (donations == null)
        {
            return NotFound();
        }
        ViewData["AccountNo"] = new SelectList(_context.ContactLists, "AccountNo", "Email", donations.AccountNo);
        ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "PaymentMethodId", "Name", donations.PaymentMethodId);
        ViewData["TransactionTypeId"] = new SelectList(_context.TransactionTypes, "TransactionTypeId", "Name", donations.TransactionTypeId);
        return View(donations);
    }

    // POST: Donations/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("TransId,Date,AccountNo,TransactionTypeId,Amount,PaymentMethodId,Notes,Created,Modified,CreatedBy,ModifiedBy")] Donations donations)
    {
        if (id != donations.TransId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                // Modified By
                donations.Modified = DateTime.Now;
                donations.ModifiedBy = User.Identity.Name;

                _context.Update(donations);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DonationsExists(donations.TransId))
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
        ViewData["AccountNo"] = new SelectList(_context.ContactLists, "AccountNo", "Email", donations.AccountNo);
        ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "PaymentMethodId", "Name", donations.PaymentMethodId);
        ViewData["TransactionTypeId"] = new SelectList(_context.TransactionTypes, "TransactionTypeId", "Name", donations.TransactionTypeId);
        return View(donations);
    }

    // GET: Donations/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.Donations == null)
        {
            return NotFound();
        }

        var donations = await _context.Donations
            .Include(d => d.Account)
            .Include(d => d.PaymentMethod)
            .Include(d => d.TransactionType)
            .FirstOrDefaultAsync(m => m.TransId == id);
        if (donations == null)
        {
            return NotFound();
        }

        return View(donations);
    }

    // POST: Donations/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.Donations == null)
        {
            return Problem("Entity set 'ApplicationDbContext.Donations'  is null.");
        }
        var donations = await _context.Donations.FindAsync(id);
        if (donations != null)
        {
            _context.Donations.Remove(donations);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool DonationsExists(int id)
    {
        return (_context.Donations?.Any(e => e.TransId == id)).GetValueOrDefault();
    }
}

