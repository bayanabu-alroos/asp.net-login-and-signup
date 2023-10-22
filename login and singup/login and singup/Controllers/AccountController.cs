using login_and_singup.Models;
using login_and_singup.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace login_and_singup.Controllers
{
    [Authorize]

    public class AccountController : Controller
    {
        #region Setting
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public AccountController(UserManager <AppUser> userManager,
            SignInManager<AppUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }
        #endregion

        #region Users Info 
        #region Register
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult>  Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new AppUser
                {
                    Email = model.EmailAddress,
                    PhoneNumber = model.Phone,
                    UserName = model.EmailAddress,
                    City =model.City,
                    Gender = model.Gender,
                };
                var userWithEmail = await _userManager.FindByEmailAsync(model.EmailAddress);
                if (userWithEmail != null)
                {
                    ModelState.AddModelError("Email", "Email already exists.");
                    return View(model);
                }
                //UserManager
                var isVaidData = await _userManager.CreateAsync(user, model.Password);
                if (isVaidData != null)
                {
                    TempData["SuccessMessage"] = "Register created successfully!";
                    return RedirectToAction("Login", "Account");
                }
                //All error
                foreach (var item in isVaidData.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }

            }
            return View(model);
        }
        #endregion

        #region Login
        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login (LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var isLogedin = await _signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, false);
                if (isLogedin.Succeeded)
                {
                    TempData["SuccessMessage"] = "Login is successfully!";

                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError(string.Empty, "Please Check User /Password");
            }
            return View(model);
        }
        #endregion


        #region Logout
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Account");
        }
        #endregion

        #endregion

        #region Roles
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult CreateRole()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleViewModel model)
        {
            if (ModelState.IsValid)
            {
                var role = new IdentityRole
                {
                    Name = model.RoleName
                };
                var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded)
                {
                    TempData["SuccessMessage"] = "Register created successfully!";
                    return RedirectToAction("RolesList", "Account");
                }
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
            }
            return View(model);
        }
        [HttpGet]
        public IActionResult RolesList()
        {
            var roles = _roleManager.Roles;
            return View(roles);
        }

        [HttpGet]
        public IActionResult NotFoundRole()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> EditRole (string id)
        {
            var role =await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return View("NotFoundRole");
            }
            var model = new EditRoleViewModel
            {
                Id = role.Id,
                RoleName = role.Name,
            };
            //Retrrive all Users
            foreach(var user in _userManager.Users)
            {
                // if user in this role  => add username prop EditRoleViewModel
                if(await _userManager.IsInRoleAsync(user,role.Name))
                {
                    model.Users.Add(user.UserName);
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditRole(EditRoleViewModel model)
        {
            var role = await _roleManager.FindByIdAsync (model.Id);
            if (role == null)
            {
                ViewBag.ErrorMassg = $"No Role with this Id = {model.Id}";
                return View("NotFoundRole");
            }
            else
            {
                role.Name = model.RoleName;
                var updatedStatus = await _roleManager.UpdateAsync(role);
                if (updatedStatus.Succeeded)
                {
                    return RedirectToAction("RolesList", "Account");
                }
                foreach (var item in updatedStatus.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> EditUserInRole(string id)
        {
            var role = await  _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMsg = $"No Role With this ID ={id}";
                return View("NotFoundRole");
            }
            var model = new List<UsersInRoleViewModel>();
            foreach (var user in _userManager.Users)
            {
                var userRoleViewModel = new UsersInRoleViewModel
                {
                    UserId = user.Id,
                    UserName = user.UserName
                };
                if (await _userManager.IsInRoleAsync(user, role.Name))
                {
                    userRoleViewModel.IsSelected = true;
                }
                else
                {
                    userRoleViewModel.IsSelected = false;
                }
                model.Add(userRoleViewModel);
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserInRole(List<UsersInRoleViewModel> model, string id)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                ViewBag.ErrorMsg = $"No Role with this id ={id}";
                return View("NotFoundRoleNotFoundRole");
            }
            for (int i = 0; i < model.Count; i++)
            {
                var user = await _userManager.FindByIdAsync(model[i].UserId);
                IdentityResult result;
                if (model[i].IsSelected && !(await _userManager.IsInRoleAsync(user, role.Name)))
                {
                    result = await _userManager.AddToRoleAsync(user, role.Name);
                }
                else if (!model[i].IsSelected && await _userManager.IsInRoleAsync(user, role.Name))
                {
                    result = await _userManager.RemoveFromRoleAsync(user, role.Name);
                }
                else
                {
                    continue;
                }
                if (result.Succeeded)
                {
                    if (i < (model.Count - 1))
                    {
                        continue;
                    }
                    else
                    {
                        return RedirectToAction("RolesList");
                    }
                }
            }
            return RedirectToAction("RolesList");
        }

        #endregion
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
