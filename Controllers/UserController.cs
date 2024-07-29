
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;


[Authorize]
    public class UserController : Controller
{
    private readonly ApplicationDbContext _context;

    public UserController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _context.userDetails.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Name,Email,Address,Number")] UserDetail userDetails)
    {
        if (ModelState.IsValid)
        {
            _context.Add(userDetails);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(userDetails);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var userDetails = await _context.userDetails.FindAsync(id);
        if (userDetails == null)
        {
            return NotFound();
        }
        return View(userDetails);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Email,Address,Number")] UserDetail userDetails)
    {
        if (id != userDetails.Id)
        {
            return NotFound();
        }

        if (ModelState.IsValid)
        {
            try
            {
                _context.Update(userDetails);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ItemExists(userDetails.Id))
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
        return View(userDetails);
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var userDetails = await _context.userDetails
            .FirstOrDefaultAsync(m => m.Id == id);
        if (userDetails == null)
        {
            return NotFound();
        }

        return View(userDetails);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var userDetails = await _context.userDetails.FindAsync(id);
        _context.userDetails.Remove(userDetails);
        await _context.SaveChangesAsync();
        return RedirectToAction(nameof(Index));
    }

    private bool ItemExists(int id)
    {
        return _context.userDetails.Any(e => e.Id == id);
    }
}