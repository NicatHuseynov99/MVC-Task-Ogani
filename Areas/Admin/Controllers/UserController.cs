using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ogani.Models;
using Ogani.ViewModels.Account;
using static Ogani.Utilities.Helpers.Helper;

namespace Ogani.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        public UserController(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IActionResult> Index()
        {
            List<UserVM> model = new List<UserVM>();
            var users = _userManager.Users.ToList();
            foreach (var item in users)
            {
                var Roles = (await _userManager.GetRolesAsync(item)).ToList();
                model.Add(new UserVM()
                {
                    Roles = Roles,
                    User = item
                });
            }

            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> MakeAdmin(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                await _userManager.RemoveFromRoleAsync(user, UserRoles.Member.ToString());
                await _userManager.AddToRoleAsync(user, UserRoles.Admin.ToString());
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> MakeMember(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                await _userManager.RemoveFromRoleAsync(user, UserRoles.Admin.ToString());
                await _userManager.AddToRoleAsync(user, UserRoles.Member.ToString());
            }
            return RedirectToAction("Index");
        }
    }
}
