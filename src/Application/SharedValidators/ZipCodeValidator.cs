using EasyRh.Exceptions;
using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace EasyRh.Application.SharedValidators;

public class ZipCodeValidator<T> : PropertyValidator<T, string>
{
    private readonly Regex _zipCodeRegex = new Regex(@"^(\d{5}-\d{3}|\d{5}(-\d{4})?)$");

    public override string Name => "ZipCodeValidator";

    public override bool IsValid(ValidationContext<T> context, string zipCode)
    {
        if (IsNullOrBlnk(zipCode))
        {
            context.MessageFormatter.AppendArgument("ErrorMessage",ResourceErrorMessage.No_ZipCode);
            return false;
        }

        if (IsInValidZipCode(zipCode))
        {
            context.MessageFormatter.AppendArgument("ErrorMessage",ResourceErrorMessage.Invalid_ZipCode);
            return false;
        }

        return true;
    }

    private bool IsNullOrBlnk(string zipCode) => string.IsNullOrWhiteSpace(zipCode);

    private bool IsInValidZipCode(string zipCode) => _zipCodeRegex.IsMatch(zipCode);

    protected override string GetDefaultMessageTemplate(string errorCode) => "{ErrorMessage}";

}