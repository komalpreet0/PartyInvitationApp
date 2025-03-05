using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartyInvitationApp.Models
{
    public class Invitation
    {
        public int Id { get; set; }

        [Required]
        public string GuestName { get; set; } = string.Empty; // FIX: Initialize with default value

        [Required]
        [EmailAddress]
        public string GuestEmail { get; set; } = string.Empty; // FIX: Initialize with default value

        public int PartyId { get; set; }
        public Party Party { get; set; } = null!; // FIX: Use "null!" to indicate non-null value
    }
}
