namespace ExpenseManager_v2._0.Areas.Identity.Pages.Account
{
    using System.ComponentModel.DataAnnotations;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authorization;
    using ExpenseManager_v2._0.Data.Models;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    using static ExpenseManager_v2._0.Data.DataConstants.ApplicationUser;

    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> signInManager;
        private readonly UserManager<ApplicationUser> userManager;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;            
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(PasswordMaxlength, ErrorMessage = ErrorMessagePassword, MinimumLength = PasswordMinlength)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
            
            [StringLength(FullNameMaxlength, MinimumLength = FullNameMinlength)]
            [Display(Name = "Full Name")]
            public string FullName { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm Password")]
            [Compare("Password", ErrorMessage = ErrorMessageConfirmPassword)]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { 
                    UserName = Input.Email, 
                    Email = Input.Email,
                    FullName = Input.FullName};
                
                var result = await this.userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {
                    await this.signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
    }
}
