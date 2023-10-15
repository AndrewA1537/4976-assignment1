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

public class YtdController : Controller
{
    private readonly ApplicationDbContext _context;

    public YtdController(ApplicationDbContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        var names = _context.ContactList.ToList();
        var donationSums = new Dictionary<string, float>();

        foreach (var name in names)
        {
            int accountNo = name.AccountNo;
            float totalDonation = _context.Donations
                .Where(d => d.AccountNo == accountNo)
                .Sum(d => d.Amount);

            donationSums[name.FirstName] = totalDonation;
        }

        ViewData["Names"] = names;
        ViewData["DonationSums"] = donationSums;
        ViewData["TotalDonation"] = donationSums.Values.Sum();

        return View();
    }


}