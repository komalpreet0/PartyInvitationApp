using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using PartyInvitationApp.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace PartyInvitationApp.Services
{
    public static class DataSeeder
    {
        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            using var context = new PartyInvitationApp.Data.ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<PartyInvitationApp.Data.ApplicationDbContext>>());

            // Check if database already has data
            if (!context.Parties.Any())
            {
                var sampleParty = new Party
                {
                    Description = "New Year's Eve Celebration",
                    EventDate = new DateTime(2025, 12, 31),
                    Location = "Times Square, NY",
                    Invitations = new System.Collections.Generic.List<Invitation>
                    {
                        new Invitation
                        {
                            GuestName = "John Doe",
                            GuestEmail = "john.doe@example.com",
                            Status = InvitationStatus.InvitationSent
                        },
                        new Invitation
                        {
                            GuestName = "Jane Smith",
                            GuestEmail = "jane.smith@example.com",
                            Status = InvitationStatus.InvitationNotSent
                        }
                    }
                };

                context.Parties.Add(sampleParty);
                await context.SaveChangesAsync();
            }
        }
    }
}
