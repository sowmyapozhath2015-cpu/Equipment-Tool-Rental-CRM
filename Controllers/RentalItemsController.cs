
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RentalCRM.Data;
using RentalCRM.Models;

public class RentalItemsController : Controller
{
    private readonly ApplicationDbContext _context;

    public RentalItemsController(ApplicationDbContext context)
    {
        _context = context;
    }

    // GET: RENTALITEMS
    public async Task<IActionResult> Index()    
    {
        return View(await _context.RentalItems.ToListAsync());
    }

    // GET: RENTALITEMS/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rentalitem = await _context.RentalItems
            .FirstOrDefaultAsync(m => m.Id == id);
        if (rentalitem == null)
        {
            return NotFound();
        }

        return View(rentalitem);
    }

    // GET: RENTALITEMS/Create
    public IActionResult Create()
    {
        ViewBag.RentalId = new SelectList(
       _context.Rentals, "Id", "Id");

        ViewBag.ProductId = new SelectList(
            _context.Products, "Id", "ProductName");
        return View();

    }

    // POST: RENTALITEMS/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.


    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(
    [Bind("Id,RentalId,ProductId,Quantity,DailyRate,DepositPerUnit")]
    RentalItem rentalitem)
    {
        if (ModelState.IsValid)
        {
            var rental = await _context.Rentals.FindAsync(rentalitem.RentalId);
            var product = await _context.Products.FindAsync(rentalitem.ProductId);

            if (rental == null || product == null)
            {
                ModelState.AddModelError("", "Invalid Rental or Product.");
            }
            else if (rentalitem.Quantity <= 0 ||
                     rentalitem.DailyRate < 0 ||
                     rentalitem.DepositPerUnit < 0)
            {
                ModelState.AddModelError("", "Enter valid values.");
            }
            else if (product.Quantity < rentalitem.Quantity)
            {
                ModelState.AddModelError("", "Not enough stock available.");
            }
            else
            {
                product.Quantity -= rentalitem.Quantity;
                product.IsAvailable = product.Quantity > 0;

                var days = (rental.ExpectedReturnDate.Date -
                            rental.RentalDate.Date).Days;

                if (days < 1)
                    days = 1;

                rental.TotalAmount +=
                    rentalitem.DailyRate * rentalitem.Quantity * days;

                rental.DepositAmount +=
                    rentalitem.DepositPerUnit * rentalitem.Quantity;

                _context.RentalItems.Add(rentalitem);

                await _context.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
        }

        ViewBag.RentalId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
            _context.Rentals, "Id", "Id", rentalitem.RentalId);

        ViewBag.ProductId = new Microsoft.AspNetCore.Mvc.Rendering.SelectList(
            _context.Products, "Id", "ProductName", rentalitem.ProductId);

        return View(rentalitem);
    }

    // GET: RENTALITEMS/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rentalitem = await _context.RentalItems.FindAsync(id);
        if (rentalitem == null)
        {
            return NotFound();
        }
        return View(rentalitem);
    }

    // POST: RENTALITEMS/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int? id, [Bind("Id,RentalId,Rental,ProductId,Product,Quantity,DailyRate,DepositPerUnit")] RentalItem rentalitem)
    {
        if (id != rentalitem.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(rentalitem);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalItemExists(rentalitem.Id))
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
        return View(rentalitem);
    }

    // GET: RENTALITEMS/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var rentalitem = await _context.RentalItems
            .FirstOrDefaultAsync(m => m.Id == id);
        if (rentalitem == null)
        {
            return NotFound();
        }

        return View(rentalitem);
    }

    // POST: RENTALITEMS/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int? id)
    {
        var rentalitem = await _context.RentalItems.FindAsync(id);
        if (rentalitem != null)
        {
            _context.RentalItems.Remove(rentalitem);
        }

        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool RentalItemExists(int? id)
    {
        return _context.RentalItems.Any(e => e.Id == id);
    }
}
