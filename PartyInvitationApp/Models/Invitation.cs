using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartyInvitationApp.Models
{
    public class Invitation
    {
        [Key]
        public int Id { get; set; } // ✅ Ensure primary key exists

        [Required]
        public string GuestName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string GuestEmail { get; set; } = string.Empty;

        // ✅ Ensure PartyId and Status exist
        [ForeignKey("Party")]
        public int PartyId { get; set; }
        public Party Party { get; set; } = null!;

        public InvitationStatus Status { get; set; } = InvitationStatus.InviteNotSent;
    }

    public enum InvitationStatus
    {
        InviteNotSent,
        InviteSent,
        RespondedYes,
        RespondedNo
    }
}
