
using Domain.ViewModels.Validations;
using FluentValidation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace Domain.ViewModels
{
    [Validation(typeof(CredentialsViewModelValidator))]
    public class CredentialsViewModel
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}
