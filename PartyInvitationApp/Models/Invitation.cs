using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartyInvitationApp.Models
{
    public class Invitation
    {
        [Key]
        public int InvitationId { get; set; }

        [Required]
        public string GuestName { get; set; } = string.Empty;

        [Required, EmailAddress]
        public string GuestEmail { get; set; } = string.Empty;

        [Required]
        public InvitationStatus Status { get; set; } = InvitationStatus.InviteNotSent;

        [Required]
        public int PartyId { get; set; }

        [ForeignKey("PartyId")]
        public Party? Party { get; set; }
    }

    public enum InvitationStatus
    {
        InviteNotSent,
        InviteSent,
        RespondedYes,
        RespondedNo
    }
}
