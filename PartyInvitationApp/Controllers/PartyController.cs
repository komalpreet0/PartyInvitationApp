﻿using Microsoft.AspNetCore.Mvc;
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

        // List all parties
        public async Task<IActionResult> Index()
        {
            var parties = await _context.Parties.Include(p => p.Invitations).ToListAsync();
            return View(parties ?? new List<Party>()); // ✅ Ensure model is not null
        }

        // Display form to create a new party
        public IActionResult Create()
        {
            return View();
        }

        // Handle party creation
        [HttpPost]
        public async Task<IActionResult> Create(Party party)
        {
            if (!ModelState.IsValid)
                return View(party);

            _context.Parties.Add(party);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // Display form to edit an existing party
        public async Task<IActionResult> Edit(int id)
        {
            var party = await _context.Parties.FindAsync(id);
            if (party == null)
                return NotFound();

            return View(party);
        }

        // Handle party editing
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

        // Delete a party
        public async Task<IActionResult> Delete(int id)
        {
            var party = await _context.Parties.FindAsync(id);
            if (party == null)
                return NotFound();

            _context.Parties.Remove(party);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // View details and manage a party
        public async Task<IActionResult> Manage(int id)
        {
            var party = await _context.Parties.Include(p => p.Invitations).FirstOrDefaultAsync(p => p.PartyId == id);
            if (party == null)
                return NotFound();

            return View(party);
        }
    }
}
