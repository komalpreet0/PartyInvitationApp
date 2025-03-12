using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyInvitationApp.Data;
using PartyInvitationApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PartyInvitationApp.Controllers
{
    public class PartyController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PartyController(ApplicationDbContext context)
        {
            _context = context;
        }

        // For listing all parties
        public async Task<IActionResult> Index()
        {
            var parties = await _context.Parties.Include(p => p.Invitations).ToListAsync();
            return View(parties ?? new List<Party>()); //For ensuring model is not null
        }

        // For displaying form to create a new party
        public IActionResult Create()
        {
            return View();
        }

        // For Handling party creation
        [HttpPost]
        public async Task<IActionResult> Create(Party party)
        {
            if (!ModelState.IsValid)
                return View(party);

            _context.Parties.Add(party);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // FOr displaying form to edit an existing party
        public async Task<IActionResult> Edit(int id)
        {
            var party = await _context.Parties.FindAsync(id);
            if (party == null)
                return NotFound();

            return View(party);
        }

        // For handling party editing
        [HttpPost]
        public async Task<IActionResult> Edit(int id, Party party)
        {
            if (id != party.PartyId)
                return NotFound();

            if (!ModelState.IsValid)
                return View(party);

            _context.Update(party);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // For deleting a party
        public async Task<IActionResult> Delete(int id)
        {
            var party = await _context.Parties.FindAsync(id);
            if (party == null)
                return NotFound();

            _context.Parties.Remove(party);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // For viewing details and manage a party
        public async Task<IActionResult> Manage(int id)
        {
            var party = await _context.Parties.Include(p => p.Invitations).FirstOrDefaultAsync(p => p.PartyId == id);
            if (party == null)
                return NotFound();

            return View(party);
        }
    }
}
