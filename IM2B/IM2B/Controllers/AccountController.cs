using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using context.Entities;
using IM2B.ViewModels.Account;

namespace IM2B.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly ILogger<AccountController> _logger;

        public AccountController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            ILogger<AccountController> logger)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
        }

        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    NomeCompleto = model.NomeCompleto,
                    IsCurador = model.IsCurador
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Utilizador criado com sucesso.");

                    await _userManager.AddToRoleAsync(user, "Utilizador");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        // GET: /Account/Login
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Login(string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.UserName,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    _logger.LogInformation("Utilizador autenticado com sucesso.");
                    return RedirectToLocal(returnUrl);
                }

                if (result.IsLockedOut)
                {
                    _logger.LogWarning("Conta de utilizador bloqueada.");
                    return RedirectToAction(nameof(Lockout));
                }
                if (result.IsNotAllowed)
                {
                    _logger.LogWarning("Não permitido (email não confirmado?)");
                }
                if (result.RequiresTwoFactor)
                {
                    _logger.LogWarning("Precisa de 2FA");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Tentativa de login inválida.");
                    Console.WriteLine($"{result}");
                    return View(model);
                }
            }

            return View(model);
        }

        // POST: /Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            _logger.LogInformation("Utilizador saiu do sistema.");
            return RedirectToAction("Index", "Home");
        }

        // GET: /Account/AccessDenied
        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        // GET: /Account/Lockout
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Lockout() // What is this used for?
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Curador, Utilizador")]
        public IActionResult Index() // Does not need to receive any parameters as the user gotten from the user manager
        {
            return View();
        }

        [HttpGet]
        [Authorize(Roles = "Curador")]
        public IActionResult UserList()
        {
            return View(_userManager.Users.ToList());
        }

        [HttpPost]
        [Authorize(Roles = "Curador")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded)
            {
                _logger.LogInformation("Utilizador eliminado com sucesso. UserId: {UserId}", id);
                TempData["StatusMessage"] = "Utilizador eliminado com sucesso.";
                return RedirectToAction(nameof(UserList));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            // Return the UserList view so the calling page can display errors.
            // Ensure the view expects a model of IEnumerable<User>.
            return View("UserList", _userManager.Users.ToList());
        }

        [HttpGet]
        [Authorize(Roles = "Curador")]
        public async Task<IActionResult> Edit(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            var model = new EditUserViewModel
            {
                Id = user.Id,
                NomeCompleto = user.NomeCompleto,
                UserName = user.UserName,
                Email = user.Email,
                IsCurador = user.IsCurador,
                ChangePassword = false
            };
            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Curador")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, EditUserViewModel model)
        {
            // Ensure the Id round-trips for redisplay on error
            model.Id = id;

            // If the user did NOT request a password change, remove any existing
            // ModelState entries for password fields so they don't block validation.
            if (!model.ChangePassword)
            {
                ModelState.Remove(nameof(model.Password));
                ModelState.Remove(nameof(model.ConfirmPassword));
            }

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // If user requested password change, validate/apply it.
            if (model.ChangePassword)
            {
                if (string.IsNullOrWhiteSpace(model.Password))
                {
                    ModelState.AddModelError(nameof(model.Password), "Insira a nova senha ou desmarque 'Alterar Password'.");
                    return View(model);
                }

                var token = await _userManager.GeneratePasswordResetTokenAsync(user);
                var pwdResult = await _userManager.ResetPasswordAsync(user, token, model.Password);
                if (!pwdResult.Succeeded)
                {
                    foreach (var error in pwdResult.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    return View(model);
                }
            }

            // Update non-password fields
            user.NomeCompleto = model.NomeCompleto;
            user.UserName = model.UserName;
            user.Email = model.Email;
            user.IsCurador = model.IsCurador;

            var result = await _userManager.UpdateAsync(user);
            if (result.Succeeded)
            {
                _logger.LogInformation("Utilizador atualizado com sucesso. UserId: {UserId}", id);
                TempData["StatusMessage"] = "Utilizador atualizado com sucesso.";
                return RedirectToAction(nameof(UserList));
            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }

            return View(model);
        }

        #region Helpers
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
        }

        #endregion
    }
}
