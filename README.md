# 4976-assn1

Brief description of your project.

## Features

-   **Feature 1**: Brief description of feature 1.
-   **Feature 2**: Brief description of feature 2.
-   ...

## Getting Started

### Prerequisites

-   .NET SDK (Specify the version, e.g., .NET 5, .NET 6).
-   Any other dependencies or software needed.

### Installation

1. **Clone the repository**:

    ```bash
     git clone https://github.com/AndrewA1537/4976-assn1.git
    ```

2. **Change directory** to the project directory:

    ```bash
    cd your-project-name
    ```

3. **Run the application**:
    ```bash
    dotnet watch
    ```

### App Building Steps

1.  **Create the .NET MVC app**:

    ```bash
     dotnet new mvc --auth individual -o SQLiteApp
    ```

2.  **globally install the following tools for Entity Framework and Code Generation respectively**:

    ```bash
    dotnet tool install -g dotnet-ef
    dotnet tool install -g dotnet-aspnet-codegenerator
    ```

3.  **Install the following packages**:

    ```bash
    dotnet add package Microsoft.EntityFrameworkCore
    dotnet add package Microsoft.EntityFrameworkCore.Tools
    dotnet add package Microsoft.EntityFrameworkCore.Design
    dotnet add package Microsoft.VisualStudio.Web.CodeGeneration.Design
    ```

4.  **Inside ~/Models create the following models**:

    ```bash
     ContactList.cs
     TransactionType.cs
     PaymentMethod.cs
     Donations.cs
    ```

5.  **Populate the models with the appropriate Database schema ex.**:

    ```bash
    public class ContactList
    {
        [Key]
        public int AccountNo { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Street { get; set; }
        public string? City { get; set; }
        public string? PostalCode { get; set; }
        public string? Country { get; set; }
    }
    ```

6.  **Inside ~/Data create a database context class to work with relational databases using Entity Framework**:

    ```bash
    public class ApplicationDbContext : IdentityDbContext
    {
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : base(options) { }

        public DbSet<ContactList>? ContactLists { get; set; }
        public DbSet<TransactionType>? TransactionTypes { get; set; }
        public DbSet<PaymentMethod>? PaymentMethods { get; set; }
        public DbSet<Donations>? Donations { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<ContactList>().ToTable("ContactList");
            builder.Entity<TransactionType>().ToTable("TransactionType");
            builder.Entity<PaymentMethod>().ToTable("PaymentMethod");
            builder.Entity<Donations>().ToTable("Donations");

            builder.Seed();
        }
    }
    ```

7.  **Inside ~/Data create a class dedicated to seeding data**:

    ```bash
    public static class SeedData
    {
        // this is an extension method to the ModelBuilder class
        public static void Seed(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ContactList>().HasData(
            GetContactList()
            );
            modelBuilder.Entity<TransactionType>().HasData(
            GetTransactionType()
            );
            modelBuilder.Entity<PaymentMethod>().HasData(
            GetPaymentMethod()
            );
            modelBuilder.Entity<Donations>().HasData(
            GetDonations()
            );
        }

        public static List<ContactList> GetContactList()
        {
            List<ContactList> contacts = new List<ContactList>()
                {
                    new ContactList()
                    {
                        AccountNo  = 1,
                        FirstName  = "John",
                        LastName   = "Doe",
                        Email      = "john@email.com",
                        Street     = "123 Main St",
                        City       = "Anytown",
                        PostalCode = "12345",
                        Country    = "USA",
                        Created    = DateTime.Now,
                        Modified   = DateTime.Now,
                        CreatedBy  = "System",
                        ModifiedBy = "System"
                    },
                }
            return contacts;
        }
    }
    ```

8.  **Since the default database for .NET applications is sqlite, we don't need to alter the connection string. We can use the following commands to run our migrations and then update the database so we can see our data**:

    ```bash
    dotnet ef migrations add M1 -o Data/Migrations


    dotnet ef database update
    ```

9.  **Next lets build some Controllers and Views using .NET's awesome codegenerator tools**:

    ```bash
    dotnet aspnet-codegenerator controller -name ContactListController -outDir Controllers -m ContactList -dc ApplicationDbContext --useDefaultLayout


    dotnet aspnet-codegenerator controller -name DonationsController -outDir Controllers -m Donations -dc ApplicationDbContext --useDefaultLayout


    dotnet aspnet-codegenerator controller -name PaymentMethodController -outDir Controllers -m PaymentMethod -dc ApplicationDbContext --useDefaultLayout


    dotnet aspnet-codegenerator controller -name TransactionTypeController -outDir Controllers -m TransactionType -dc ApplicationDbContext --useDefaultLayout
    ```

10. **Now kill the server if it's running and add these nav links in ~/Views/Shared/\_Layout.cshtml**:

    ```bash
    <li class="nav-item">
        <a class="nav-link text-dark" asp-area="" asp-controller="ContactList"
            asp-action="Index">ContactList</a>
    </li>
    <li class="nav-item">
        <a class="nav-link text-dark" asp-area="" asp-controller="Donations"
            asp-action="Index">Donations</a>
    </li>
    <li class="nav-item">
        <a class="nav-link text-dark" asp-area="" asp-controller="PaymentMethod"
            asp-action="Index">PaymentMethod</a>
    </li>
    <li class="nav-item">
        <a class="nav-link text-dark" asp-area="" asp-controller="TransactionType"
            asp-action="Index">TransactionType</a>
    </li>
    ```

11. **Now we can add user roles inside ~/Data make a class called ModelBuilderExtensions**:

    ```bash
    public static class ModelBuilderExtensions
    {
        public static void Seed(this ModelBuilder builder)
        {
            var pwd = "P@$$w0rd";
            var passwordHasher = new PasswordHasher<IdentityUser>();

            // Seed Roles
            var adminRole            = new IdentityRole("Admin");
            adminRole.NormalizedName = adminRole.Name!.ToUpper();

            var financeRole = new IdentityRole("Finance");
            financeRole.NormalizedName = financeRole.Name!.ToUpper();

            List<IdentityRole> roles = new List<IdentityRole>()
            {
                adminRole,
                financeRole
            };

            builder.Entity<IdentityRole>().HasData(roles);


            // Seed Users
            var adminUser = new IdentityUser
            {
                UserName       = "a@a.a",
                Email          = "a@a.a",
                EmailConfirmed = true,
            };
            adminUser.NormalizedUserName = adminUser.UserName.ToUpper();
            adminUser.NormalizedEmail    = adminUser.Email.ToUpper();
            adminUser.PasswordHash       = passwordHasher.HashPassword(adminUser, pwd);


            var financeUser = new IdentityUser
            {
                UserName       = "f@f.f",
                Email          = "f@f.f",
                EmailConfirmed = true,
            };
            financeUser.NormalizedUserName = financeUser.UserName.ToUpper();
            financeUser.NormalizedEmail    = financeUser.Email.ToUpper();
            financeUser.PasswordHash       = passwordHasher.HashPassword(financeUser, pwd);

            List<IdentityUser> users = new List<IdentityUser>()
            {
                adminUser,
                financeUser,
            };

            builder.Entity<IdentityUser>().HasData(users);

            // Seed UserRoles
            List<IdentityUserRole<string>> userRoles = new List<IdentityUserRole<string>>();

            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[0].Id,
                RoleId = roles.First(q => q.Name == "Admin").Id
            });

            userRoles.Add(new IdentityUserRole<string>
            {
                UserId = users[1].Id,
                RoleId = roles.First(q => q.Name == "Finance").Id
            });

            builder.Entity<IdentityUserRole<string>>().HasData(userRoles);
        }
    }
    ```

12. **Update the ApplicationDbContext OnModelCreating()**:

    ```bash
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<ContactList>().ToTable("ContactList");
        builder.Entity<TransactionType>().ToTable("TransactionType");
        builder.Entity<PaymentMethod>().ToTable("PaymentMethod");
        builder.Entity<Donations>().ToTable("Donations");

        // seed the database
        SeedData.Seed(builder);

        // seed the users and roles
        ModelBuilderExtensions.Seed(builder);
    }
    ```

13. **Add this to program.cs and remove the method above to allow seed users and roles**:

    ```bash
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using SQLiteApp.Data;

    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlite(connectionString));

    // builder.Services.AddDatabaseDeveloperPageExceptionFilter();

    // builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    // .AddEntityFrameworkStores<ApplicationDbContext>();
    // builder.Services.AddControllersWithViews();

    builder.Services.AddIdentity<IdentityUser, IdentityRole>(
    options =>
    {
    options.Stores.MaxLengthForKeys = 128;
    })
    .AddEntityFrameworkStores<ApplicationDbContext>()
    .AddRoles<IdentityRole>()
    .AddDefaultUI()
    .AddDefaultTokenProviders();

    var app = builder.Build(); // ================================== Everything above is a service that we add, everything below is a service that we use
    ```

14. **Redo migrations and db update after deleting the db and migrations folder**:

    ```bash
    dotnet ef migrations add M1 -o Data/Migrations


    dotnet ef database update
    ```

15. **Now we can make sure only some pages are authorized, in the designated controllers add this code to the corresponding methods you want authorized**:

    ```bash
    [Authorize(Roles = "Admin")]

    [Authorize(Roles = "Admin, Finance")]
    ```

16. **Allow only certain roles to see certain nav links by altering the following code in ~Views/Shared/\_Layout.cshtml**:

    ```bash
    <ul class="navbar-nav flex-grow-1">
        <li class="nav-item">
            <a class="nav-link text-dark" asp-area="" asp-controller="Home" asp-action="Index">Home</a>
        </li>
        <li class="nav-item">
            <a class="nav-link text-dark" asp-area="" asp-controller="Home"
                asp-action="Privacy">Privacy</a>
        </li>
        @if (User.IsInRole("Admin"))
        {
            <li class="nav-item">
                <a class="nav-link text-dark" asp-area="" asp-controller="ContactList"
                    asp-action="Index">ContactList</a>
            </li>
        }
        @if (User.IsInRole("Admin") || User.IsInRole("Finance"))
        {
            <li class="nav-item">
                <a class="nav-link text-dark" asp-area="" asp-controller="Donations"
                    asp-action="Index">Donations</a>
            </li>
        }
        @if (User.IsInRole("Admin"))
        {
            <li class="nav-item">
                <a class="nav-link text-dark" asp-area="" asp-controller="PaymentMethod"
                    asp-action="Index">PaymentMethod</a>
            </li>
            <li class="nav-item">
                <a class="nav-link text-dark" asp-area="" asp-controller="TransactionType"
                    asp-action="Index">TransactionType</a>
            </li>
        }
    </ul>
    ```

17. **Note: To fix this error make sure the BIND methods in Donations are properly set**:

    ```bash
    public IActionResult Create()
    {
        ViewData["AccountNo"] = new SelectList(_context.ContactLists, "AccountNo", "Email");
        ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "PaymentMethodId", "Name");
        ViewData["TransactionTypeId"] = new SelectList(_context.TransactionTypes, "TransactionTypeId", "Name");
        return View();
    }


    public async Task<IActionResult> Create([Bind("TransId,Date,AccountNo,TransactionTypeId,Amount,PaymentMethodId,Notes,Created,Modified,CreatedBy,ModifiedBy")] Donations donations)
    {
        if (ModelState.IsValid)
        {
            _context.Add(donations);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        ViewData["AccountNo"] = new SelectList(_context.ContactLists, "AccountNo", "Email", donations.AccountNo);
        ViewData["PaymentMethodId"] = new SelectList(_context.PaymentMethods, "PaymentMethodId", "Name", donations.PaymentMethodId);
        ViewData["TransactionTypeId"] = new SelectList(_context.TransactionTypes, "TransactionTypeId", "Name", donations.TransactionTypeId);
        return View(donations);
    }
    ```

18. **Now we can make a class library by using these commands**:

    ```bash
    dotnet new classlib -o NonProfitLibrary

    dotnet new sln

    dotnet sln add NonProfitLibrary/NonProfitLibrary.csproj

    dotnet build

    "In vscode make sure to add the library to the app as a project reference in the SOLUTION EXPLORER"

    next is add packages

    update all namespaces

    run migrations

    update database
    ```

19. **Add this to the controllers to get them working properly**:

    ```bash
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
    ```

20. **DO NOT USE!! Now lets surface some hidden pages from .NET by using the following command so we can style them: NOTE for some reason this breaks the app so don't use it for now**:

    ```bash
    dotnet aspnet-codegenerator identity --files "Account.Register;Account.Login;Account.RegisterConfirmation" -dc ApplicationDbContext
    ```

21. **...**:

    ```bash
    
    ```
