using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartyInvitationApp.Models
{
    public class Invitation
    {
        [Key]
        public int Id { get; set; } // ✅ Changed from InvitationId to Id

        [Required]
        public string GuestName { get; set; }

        [Required]
        [EmailAddress]
        public string GuestEmail { get; set; }

        [Required]
        public InvitationStatus Status { get; set; } = InvitationStatus.InviteNotSent;

        // Foreign Key linking to Party
        [ForeignKey("Party")]
        public int PartyId { get; set; } // ✅ Fixed PartyId definition

        public Party Party { get; set; } // ✅ Navigation Property
    }
}
