using System.ComponentModel.DataAnnotations;

namespace IM2B.ViewModels.Account
{
    public class RegisterViewModel
    {
        [Required(ErrorMessage = "O nome completo é obrigatório")]
        [Display(Name = "Nome Completo")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O username é obrigatório")]
        [Display(Name = "Nome de Usuario")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        [EmailAddress(ErrorMessage = "Email inválido")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "A senha é obrigatória")]
        [StringLength(100, ErrorMessage = "A {0} deve ter pelo menos {2} caracteres.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        [Compare("Password", ErrorMessage = "A senha e a confirmação não coincidem.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Registrar como Curador")]
        public bool IsCurador { get; set; }
    }
}