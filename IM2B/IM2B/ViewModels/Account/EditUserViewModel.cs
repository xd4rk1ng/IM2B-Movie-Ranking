using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace IM2B.ViewModels.Account
{
    public class EditUserViewModel : IValidatableObject
    {
        public string Id { get; set; }

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

        [Display(Name = "Alterar Password")]
        public bool ChangePassword { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Confirmar senha")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Registrar como Curador")]
        public bool IsCurador { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            // Only validate password fields when user requested a change.
            if (!ChangePassword)
            {
                yield break;
            }

            // Require a password when ChangePassword is checked.
            if (string.IsNullOrWhiteSpace(Password))
            {
                yield return new ValidationResult(
                    "Insira a nova senha ou desmarque 'Alterar Password'.",
                    new[] { nameof(Password) });
                yield break;
            }

            // Validate password length when provided
            if (Password.Length < 6 || Password.Length > 100)
            {
                yield return new ValidationResult(
                    "A senha deve ter entre 6 e 100 caracteres.",
                    new[] { nameof(Password) });
            }

            // ConfirmPassword must match when a new password is provided
            if (Password != ConfirmPassword)
            {
                yield return new ValidationResult(
                    "A senha e a confirmação não coincidem.",
                    new[] { nameof(ConfirmPassword) });
            }
        }
    }
}
