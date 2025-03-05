﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyInvitationApp.Data;
using PartyInvitationApp.Models;
using PartyInvitationApp.Services;
using System.Linq;
using System.Threading.Tasks;

namespace PartyInvitationApp.Controllers
{
    public class InvitationController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly EmailService _emailService;

        public InvitationController(ApplicationDbContext context, EmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // 📌 List all invitations for a specific party
        public async Task<IActionResult> Index(int partyId)
        {
            var invitations = await _context.Invitations
                .Where(i => i.PartyId == partyId)
                .ToListAsync();

            ViewBag.PartyId = partyId; // Pass PartyId to the view for navigation
            return View(invitations);
        }

        // 📌 Send an email invitation
        [HttpPost]
        public async Task<IActionResult> SendInvitation(int partyId, string guestName, string guestEmail)
        {
            if (string.IsNullOrEmpty(guestName) || string.IsNullOrEmpty(guestEmail))
            {
                ModelState.AddModelError("", "Guest Name and Email are required.");
                return RedirectToAction("Manage", "Party", new { id = partyId });
            }

            var invitation = new Invitation
            {
                GuestName = guestName,
                GuestEmail = guestEmail,
                PartyId = partyId,
                Status = InvitationStatus.InvitationSent
            };

            _context.Invitations.Add(invitation);
            await _context.SaveChangesAsync();

            // ✅ Send the invitation email
            string subject = $"You're Invited to the Party!";
            string body = $"Hello {guestName},<br/>" +
                          $"You've been invited to an event! Please RSVP here: " +
                          $"<a href='https://localhost:7008/Invitation/Respond/{invitation.Id}'>Click here to respond</a>";

            await _emailService.SendInvitationEmail(guestEmail, subject, body);

            return RedirectToAction("Manage", "Party", new { id = partyId });
        }

        // 📌 Display RSVP response page
        public async Task<IActionResult> Respond(int id)
        {
            var invitation = await _context.Invitations.FindAsync(id);
            if (invitation == null)
                return NotFound();

            return View(invitation);
        }

        // 📌 Handle RSVP submission
        [HttpPost]
        public async Task<IActionResult> Respond(int id, string response)
        {
            var invitation = await _context.Invitations.FindAsync(id);
            if (invitation == null)
                return NotFound();

            if (response == "Yes")
                invitation.Status = InvitationStatus.RespondedYes;
            else if (response == "No")
                invitation.Status = InvitationStatus.RespondedNo;

            _context.Update(invitation);
            await _context.SaveChangesAsync();

            return RedirectToAction("Manage", "Party", new { id = invitation.PartyId });
        }

        // 📌 Delete an invitation
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            var invitation = await _context.Invitations.FindAsync(id);
            if (invitation == null)
                return NotFound();

            _context.Invitations.Remove(invitation);
            await _context.SaveChangesAsync();

            return RedirectToAction("Manage", "Party", new { id = invitation.PartyId });
        }
    }
}
