using Microsoft.AspNetCore.Mvc;
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

        // Send an email invitation
        [HttpPost]
        public async Task<IActionResult> SendInvitation(int partyId, string guestName, string guestEmail)
        {
            var invitation = new Invitation
            {
                GuestName = guestName,
                GuestEmail = guestEmail,
                PartyId = partyId,
                Status = InvitationStatus.InviteNotSent  // Initially marked as Not Sent
            };

            _context.Invitations.Add(invitation);
            await _context.SaveChangesAsync();

            // Update status to "InviteSent" after saving
            invitation.Status = InvitationStatus.InviteSent;
            _context.Update(invitation);
            await _context.SaveChangesAsync();

            // Send the invitation email with RSVP link
            string subject = $"You're Invited to {partyId}!";
            string body = $"Hello {guestName},<br/>" +
                          $"You've been invited to an event! Please RSVP below:<br/><br/>" +
                          $"<a href='https://localhost:7008/Invitation/Respond/{invitation.InvitationId}'>Click here to RSVP</a>";

            await _emailService.SendInvitationEmail(guestEmail, subject, body);

            return RedirectToAction("Manage", "Party", new { id = partyId });
        }

        // RSVP response page
        [HttpGet]
        public async Task<IActionResult> Respond(int id)
        {
            var invitation = await _context.Invitations.FindAsync(id);
            if (invitation == null)
                return NotFound();

            return View("Response", invitation);
            
        }

        // Handle RSVP submission
        [HttpPost]
        public async Task<IActionResult> Respond(int id, string response)
        {
            var invitation = await _context.Invitations.FindAsync(id);
            if (invitation == null)
                return NotFound();

            // Update status based on response
            if (response == "Yes")
                invitation.Status = InvitationStatus.RespondedYes;
            else if (response == "No")
                invitation.Status = InvitationStatus.RespondedNo;

            _context.Update(invitation);
            await _context.SaveChangesAsync();

            return RedirectToAction("Manage", "Party", new { id = invitation.PartyId });
        }
    }
}
