using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PartyInvitationApp.Models
{
    public class Party
    {
        [Key]  // Primary Key
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int PartyId { get; set; }  // ✅ Ensure PartyId exists

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public string Location { get; set; } = string.Empty;

        public ICollection<Invitation> Invitations { get; set; } = new List<Invitation>();
    }
}
