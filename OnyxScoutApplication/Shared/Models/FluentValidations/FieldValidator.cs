using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;

namespace OnyxScoutApplication.Shared.Models.FluentValidations
{
    public class FieldValidator : AbstractValidator<FieldDto>
    {
        public FieldValidator()
        {
            RuleFor(x => x.Name).Must(s => !string.IsNullOrWhiteSpace(s))
                .WithMessage("Please write a valid field name");
            RuleFor(x => x.NumericDefaultValue).Must(BeNumberBetween)
                .WithMessage((model, s) =>
                    "Default value must be a number between " + model.MinValue + " and " + model.MaxValue)
                .When(c => c.FieldType == FieldType.Numeric);
            RuleFor(x => x.Options).Must(v => v.Count >= 2).WithMessage("At least two options are required")
                .When(c => c.FieldType == FieldType.OptionSelect);
            RuleFor(x => x.Options).Must(v => !v.Any(string.IsNullOrEmpty))
                .WithMessage("Make sure all options are not empty").When(c => c.FieldType == FieldType.OptionSelect);
        }

        private static bool BeNumberBetween(FieldDto model, int? value)
        {
            if (value == null)
            {
                return true;
            }

            int v = (int) value;
            return v >= model.MinValue && v <= model.MaxValue;
        }
    }
}
