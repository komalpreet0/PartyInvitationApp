using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartyInvitationApp.Models
{
    public class Invitation
    {
        [Key]
        public int InvitationId { get; set; }

        [Required(ErrorMessage = "Guest name is required")]
        [StringLength(100, ErrorMessage = "Guest name cannot exceed 100 characters")]
        public string GuestName { get; set; }

        [Required(ErrorMessage = "Guest email is required")]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string GuestEmail { get; set; }

        [Required]
        public InvitationStatus Status { get; set; } = InvitationStatus.InvitationNotSent;

        // Foreign key linking invitation to a party
        [ForeignKey("Party")]
        public int PartyId { get; set; }
        public Party Party { get; set; }
    }
}
