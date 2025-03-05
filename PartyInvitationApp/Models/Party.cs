using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PartyInvitationApp.Models
{
    public class Party
    {
        [Key]
        public int PartyId { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(200, ErrorMessage = "Description cannot exceed 200 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Event date is required")]
        public DateTime EventDate { get; set; }

        [Required(ErrorMessage = "Location is required")]
        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters")]
        public string Location { get; set; }

        // Navigation property for related invitations
        public List<Invitation> Invitations { get; set; } = new List<Invitation>();
    }
}
