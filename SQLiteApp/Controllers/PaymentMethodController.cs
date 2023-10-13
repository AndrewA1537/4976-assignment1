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
public class PaymentMethodController : Controller
{
    private readonly ApplicationDbContext _context;

    public PaymentMethodController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: PaymentMethod
    public async Task<IActionResult> Index()
    {
        return _context.PaymentMethods != null ?
                    View(await _context.PaymentMethods.ToListAsync()) :
                    Problem("Entity set 'ApplicationDbContext.PaymentMethods'  is null.");
    }

    // GET: PaymentMethod/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.PaymentMethods == null)
        {
            return NotFound();
        }

        var paymentMethod = await _context.PaymentMethods
            .FirstOrDefaultAsync(m => m.PaymentMethodId == id);
        if (paymentMethod == null)
        {
            return NotFound();
        }

        return View(paymentMethod);
    }

    // GET: PaymentMethod/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: PaymentMethod/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("PaymentMethodId,Name")] PaymentMethod paymentMethod)
    {
        if (ModelState.IsValid)
        {
            _context.Add(paymentMethod);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(paymentMethod);
    }

    // GET: PaymentMethod/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.PaymentMethods == null)
        {
            return NotFound();
        }

        var paymentMethod = await _context.PaymentMethods.FindAsync(id);
        if (paymentMethod == null)
        {
            return NotFound();
        }
        return View(paymentMethod);
    }

    // POST: PaymentMethod/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("PaymentMethodId,Name")] PaymentMethod paymentMethod)
    {
        if (id != paymentMethod.PaymentMethodId)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(paymentMethod);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentMethodExists(paymentMethod.PaymentMethodId))
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
        return View(paymentMethod);
    }

    // GET: PaymentMethod/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.PaymentMethods == null)
        {
            return NotFound();
        }

        var paymentMethod = await _context.PaymentMethods
            .FirstOrDefaultAsync(m => m.PaymentMethodId == id);
        if (paymentMethod == null)
        {
            return NotFound();
        }

        return View(paymentMethod);
    }

    // POST: PaymentMethod/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.PaymentMethods == null)
        {
            return Problem("Entity set 'ApplicationDbContext.PaymentMethods'  is null.");
        }
        var paymentMethod = await _context.PaymentMethods.FindAsync(id);
        if (paymentMethod != null)
        {
            _context.PaymentMethods.Remove(paymentMethod);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool PaymentMethodExists(int id)
    {
        return (_context.PaymentMethods?.Any(e => e.PaymentMethodId == id)).GetValueOrDefault();
    }
}
