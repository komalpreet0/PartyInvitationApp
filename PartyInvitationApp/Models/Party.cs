using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PartyInvitationApp.Models
{
    public class Party
    {
        [Key]
        public int Id { get; set; } // ✅ Changed from PartyId to Id

        [Required]
        public string Description { get; set; }

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public string Location { get; set; }

        // Relationship: A Party can have multiple Invitations
        public List<Invitation> Invitations { get; set; } = new List<Invitation>();
    }
}
