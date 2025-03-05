using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PartyInvitationApp.Data;
using PartyInvitationApp.Models;
using System.Threading.Tasks;
using System.Linq;

namespace PartyInvitationApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public HomeController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var parties = await _context.Parties.ToListAsync();
            return View(parties ?? new List<Party>()); // ✅ Ensure model is not null
        }
    }
}
