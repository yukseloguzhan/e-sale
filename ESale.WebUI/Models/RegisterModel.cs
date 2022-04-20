using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ESale.WebUI.Models
{
    public class RegisterModel
    {

        [Required]
        public string FullName { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string RePassword { get; set; }



    }


    public class RegisterModelValidator : AbstractValidator<RegisterModel>
    {
        public RegisterModelValidator()
        {
            RuleFor(x => x.Password).MinimumLength(4).WithMessage("Şifreniz en az 6 karakter olmalıdır\n");
            RuleFor(x => x.Password).Matches("^\\d{6}$").WithMessage("Sayısal değer içermelidir\n");
        }

    }


}
