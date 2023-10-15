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

public class YearlyReportController : Controller
{
    private readonly ApplicationDbContext _context;

    public YearlyReportController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var years = _context.Donations
            .Select(d => d.Date.Year)
            .Distinct()
            .OrderByDescending(y => y)
            .ToList();

        var amountByYear = new Dictionary<int, float>();

        foreach (var year in years)
        {
            amountByYear[year] = _context.Donations
                .Where(d => d.Date.Year == year)
                .Sum(d => d.Amount);
        }

        ViewData["Years"] = years;
        ViewData["AmountByYear"] = amountByYear;

        return View();
    }


}