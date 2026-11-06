
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentalCRM.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using RentalCRM.Data;

public class RentalsController : Controller
{
    private readonly ApplicationDbContext _context;

    public RentalsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: RENTALS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.Rentals.ToListAsync());
    }

    // GET: RENTALS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rental = await _context.Rentals
            .FirstOrDefaultAsync(m => m.Id == id);
        if (rental == null)
        {
            return NotFound();
        }

        return View(rental);
    }

    // GET: RENTALS/Create
    public IActionResult Create()
    {
        
            ViewBag.CustomerId = new SelectList(
                _context.Customers,
                "Id",
                "FullName"
            );
            return View();
    }

    // POST: RENTALS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,CustomerId,RentalDate,ExpectedReturnDate,ActualReturnDate,TotalAmount,DepositAmount,Status")] Rental rental)
    {
        if (ModelState.IsValid)
        {
            _context.Add(rental);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(rental);
    }

    // GET: RENTALS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rental = await _context.Rentals.FindAsync(id);
        if (rental == null)
        {
            return NotFound();
        }
        return View(rental);
    }

    // POST: RENTALS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,CustomerId,Customer,RentalDate,ExpectedReturnDate,ActualReturnDate,TotalAmount,DepositAmount,Status")] Rental rental)
    {
        if (id != rental.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(rental);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(rental.Id))
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
        return View(rental);
    }

    // GET: RENTALS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rental = await _context.Rentals
            .FirstOrDefaultAsync(m => m.Id == id);
        if (rental == null)
        {
            return NotFound();
        }

        return View(rental);
    }

    // POST: RENTALS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var rental = await _context.Rentals.FindAsync(id);
        if (rental != null)
        {
            _context.Rentals.Remove(rental);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RentalExists(int? id)
    {
        return _context.Rentals.Any(e => e.Id == id);
    }
}
