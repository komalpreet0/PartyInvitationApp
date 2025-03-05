using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PartyInvitationApp.Models
{
    public class Party
    {
        [Key]
        public int Id { get; set; } // ✅ Ensure primary key exists

        [Required]
        public string Description { get; set; } = string.Empty;

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public string Location { get; set; } = string.Empty;

        // ✅ Ensure it has an Invitation list
        public List<Invitation> Invitations { get; set; } = new List<Invitation>();
    }
}
