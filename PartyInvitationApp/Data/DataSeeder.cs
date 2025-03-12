using System;
using System.Linq;
using PartyInvitationApp.Models;
// For data seeding
namespace PartyInvitationApp.Data
{
    public static class DataSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Parties.Any())
            {
                var party = new Party
                {
                    Description = "Birthday Party",
                    EventDate = DateTime.Now.AddDays(10),//For date and time 
                    Location = "123 Party Street"
                };
                context.Parties.Add(party);
                context.SaveChanges();

                var invitation = new Invitation
                {
                    GuestName = "John Doe",
                    GuestEmail = "john@example.com",
                    Status = InvitationStatus.InviteNotSent,  
                    PartyId = party.PartyId
                };
                context.Invitations.Add(invitation);
                context.SaveChanges();
            }
        }
    }
}
