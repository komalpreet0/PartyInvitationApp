using System.ComponentModel.DataAnnotations;

namespace PartyInvitationApp.Models
{
    public class Invitation
    {
        [Key]
        public int InvitationId { get; set; }

        [Required]
        public string GuestName { get; set; }

        [Required, EmailAddress]
        public string GuestEmail { get; set; }

        public InvitationStatus Status { get; set; } = InvitationStatus.InvitationNotSent;

        public int PartyId { get; set; }
        public Party Party { get; set; }
    }
}
