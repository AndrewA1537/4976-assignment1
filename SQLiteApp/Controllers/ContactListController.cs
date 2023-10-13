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
public class ContactListController : Controller
{
    private readonly ApplicationDbContext _context;

    public ContactListController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: ContactList
    public async Task<IActionResult> Index()
    {
        return _context.ContactLists != null ?
                    View(await _context.ContactLists.ToListAsync()) :
                    Problem("Entity set 'ApplicationDbContext.ContactLists'  is null.");
    }

    // GET: ContactList/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null || _context.ContactLists == null)
        {
            return NotFound();
        }

        var contactList = await _context.ContactLists
            .FirstOrDefaultAsync(m => m.AccountNo == id);
        if (contactList == null)
        {
            return NotFound();
        }

        return View(contactList);
    }

    // GET: ContactList/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: ContactList/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("AccountNo,FirstName,LastName,Email,Street,City,PostalCode,Country")] ContactList contactList)
    {
        if (ModelState.IsValid)
        {
            // Modified By
            contactList.Created = DateTime.Now;
            contactList.Modified = DateTime.Now;
            contactList.CreatedBy = User.Identity.Name;
            contactList.ModifiedBy = User.Identity.Name;
            
            _context.Add(contactList);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(contactList);
    }

    // GET: ContactList/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null || _context.ContactLists == null)
        {
            return NotFound();
        }

        var contactList = await _context.ContactLists.FindAsync(id);
        if (contactList == null)
        {
            return NotFound();
        }
        return View(contactList);
    }

    // POST: ContactList/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("AccountNo,FirstName,LastName,Email,Street,City,PostalCode,Country")] ContactList contactList)
    {
        if (id != contactList.AccountNo)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                // Modified By
                contactList.Modified = DateTime.Now;
                contactList.ModifiedBy = User.Identity.Name;
                
                _context.Update(contactList);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContactListExists(contactList.AccountNo))
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
        return View(contactList);
    }

    // GET: ContactList/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null || _context.ContactLists == null)
        {
            return NotFound();
        }

        var contactList = await _context.ContactLists
            .FirstOrDefaultAsync(m => m.AccountNo == id);
        if (contactList == null)
        {
            return NotFound();
        }

        return View(contactList);
    }

    // POST: ContactList/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        if (_context.ContactLists == null)
        {
            return Problem("Entity set 'ApplicationDbContext.ContactLists'  is null.");
        }
        var contactList = await _context.ContactLists.FindAsync(id);
        if (contactList != null)
        {
            _context.ContactLists.Remove(contactList);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ContactListExists(int id)
    {
        return (_context.ContactLists?.Any(e => e.AccountNo == id)).GetValueOrDefault();
    }
}
