using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PartyInvitationApp.Data;
using PartyInvitationApp.Models;
using System.Linq;
using System.Threading.Tasks;

namespace PartyInvitationApp.Pages.InvitationResponses

{
    public class ResponseModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public ResponseModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PartyInvitationApp.Models.Invitation Invitation { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Invitation = await _context.Invitations.FindAsync(id);
            if (Invitation == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int id, string response)
        {
            var invitation = await _context.Invitations.FindAsync(id);
            if (invitation == null)
                return NotFound();

            invitation.Status = response == "Yes" ? InvitationStatus.RespondedYes : InvitationStatus.RespondedNo;

            _context.Update(invitation);
            await _context.SaveChangesAsync();

            return RedirectToPage("/Party/Manage", new { id = invitation.PartyId });
        }
    }
}
