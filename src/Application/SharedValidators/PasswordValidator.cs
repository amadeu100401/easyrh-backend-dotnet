using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

using EasyRh.Exceptions;

namespace EasyRh.Application.SharedValidators;

public class PasswordValidator<T> : PropertyValidator<T, string>
{
    private readonly Regex _regexPasswordRule = new Regex("^(?=.*[A-Z])(?=.*[a-z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{10,}$");

    public override string Name => "passwordValidator";

    public override bool IsValid(ValidationContext<T> context, string password)
    {
        if(IsNullOrBlank(password))
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", ResourceErrorMessage.No_Password);
            return false;
        }

        if (IsInvalidPassword(password))
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", ResourceErrorMessage.Invalid_Password_Format);
            return false;
        }

        return true;
    }

    private bool IsNullOrBlank(string password) => string.IsNullOrWhiteSpace(password);

    private bool IsInvalidPassword(string password) => !_regexPasswordRule.IsMatch(password);

    protected override string GetDefaultMessageTemplate(string errorCode) => "{ErrorMessage}";
}
