using PartyInvitationApp.Models;
using System;
using System.Linq;

namespace PartyInvitationApp.Data
{
    public class DataSeeder
    {
        public static void Seed(ApplicationDbContext context)
        {
            if (!context.Parties.Any())
            {
                var party = new Party
                {
                    Description = "New Year Party",
                    EventDate = DateTime.Now.AddMonths(1),
                    Location = "Toronto",
                    Invitations = new()
                    {
                        new Invitation { GuestName = "John Doe", GuestEmail = "john@example.com", Status = InvitationStatus.InviteNotSent },
                        new Invitation { GuestName = "Jane Doe", GuestEmail = "jane@example.com", Status = InvitationStatus.InviteNotSent }
                    }
                };
                context.Parties.Add(party);
                context.SaveChanges();
            }
        }
    }
}
