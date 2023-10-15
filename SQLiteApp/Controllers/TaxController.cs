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

[Authorize(Roles = "Admin, Finance")]

public class TaxController : Controller{
    private readonly ApplicationDbContext _context;

    public TaxController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        ViewData["AccountNo"] = new SelectList(_context.ContactList, "AccountNo", "Email");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Display(int AccountNo)
    {
        ViewData["Name"] = _context.ContactList.Where(x => x.AccountNo == AccountNo).Select(x => x.FirstName).FirstOrDefault();
        ViewData["Donations"] = _context.Donations.Where(x => x.AccountNo == AccountNo).ToList();
        ViewData["Total"] = _context.Donations.Where(x => x.AccountNo == AccountNo).Sum(x => x.Amount);
    return View();
    }

    // [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    // public IActionResult Error()
    // {
    //     return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    // }

}