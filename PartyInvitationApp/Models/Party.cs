using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PartyInvitationApp.Models
{
    public class Party
    {
        public int Id { get; set; }

        [Required]
        public string Description { get; set; } = string.Empty; // FIX: Provide default value

        [Required]
        public DateTime EventDate { get; set; }

        [Required]
        public string Location { get; set; } = string.Empty; // FIX: Provide default value

        public List<Invitation> Invitations { get; set; } = new List<Invitation>(); // FIX: Initialize List
    }
}
