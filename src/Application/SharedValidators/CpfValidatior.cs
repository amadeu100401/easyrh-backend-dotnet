using FluentValidation;
using FluentValidation.Validators;
using System.Text.RegularExpressions;

namespace EasyRh.Application.SharedValidators;

public class CpfValidatior<T> : PropertyValidator<T, string>
{
    private readonly Regex _cpfRegex = new Regex(@"^\d{3}\.?(\d{3}\.?){2}\d{3}-?\d{2}$");

    public override string Name => "CpfValidator";

    public override bool IsValid(ValidationContext<T> context, string cpf)
    {
        if (CpfIsEmptyOrBlank(cpf))
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", "O CPF deve ser informado.");
            return false;
        }

        if(IsInvalidValidCpf(cpf))
        {
            context.MessageFormatter.AppendArgument("ErrorMessage", "O CPF ínformado é inválido.");
            return false;
        }

        return true;

    }

    private bool CpfIsEmptyOrBlank(string cpf) => string.IsNullOrWhiteSpace(cpf);

    private bool IsInvalidValidCpf(string cpf) => _cpfRegex.IsMatch(cpf);
}
