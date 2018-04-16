using System.Linq;
using System.Web.Mvc;
using System.Web.Security;
using PetShop.BLL.DTO;
using PetShop.BLL.Interfaces;
using PetShop.WebUI.Models;
using AutoMapper;


namespace PetShop.WebUI.Controllers
{
    public class AccountController : Controller
    {
        private IUserService _userService;

        public AccountController(IUserService service)
        {
            _userService = service;
        }
        // GET: Account
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Seller");
            }
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = null;
                user = Mapper.Map<UserDTO, AppUser>(_userService.GetUsers().FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password));
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.Email, true);
                    return RedirectToAction("Index", "Seller");
                }
                ModelState.AddModelError("", "Doesn't correct e-mail or password");
            }
            return View();
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Products", "Home");
        }
    }
}