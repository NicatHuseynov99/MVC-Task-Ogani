using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ogani.Models;
namespace Ogani.Areas.Admin.ViewComponents
{
    public class HeaderViewComponent : ViewComponent
    {
        private readonly UserManager<AppUser> _userManager;
        public HeaderViewComponent(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var user = await _userManager.FindByNameAsync(User.Identity.Name);
            return await Task.FromResult(View(user));
        }
    }
}

