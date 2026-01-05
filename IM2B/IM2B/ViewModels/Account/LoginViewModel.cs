using System.ComponentModel.DataAnnotations;

namespace IM2B.ViewModels.Account
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "O nome de utilizador é obrigatório")]
        [Display(Name = "Nome de utilizador")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [Display(Name = "Lembrar-me")]
        public bool RememberMe { get; set; }
    }
}
