using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartyInvitationApp.Models
{
    public class Invitation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int InvitationId { get; set; }

        [Required]
        public string GuestName { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        public string GuestEmail { get; set; } = string.Empty;

        [Required]
        public InvitationStatus Status { get; set; } = InvitationStatus.InviteNotSent;

        // Foreign Key
        public int PartyId { get; set; }
        public Party Party { get; set; } = null!;
    }
}
