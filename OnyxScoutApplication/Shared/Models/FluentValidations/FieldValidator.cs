using FluentValidation;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;

namespace OnyxScoutApplication.Shared.Models.FluentValidations
{
    public class FieldValidator : AbstractValidator<FieldDto>
    {
        public FieldValidator()
        {
            RuleFor(x => x.Name)
                .Must(s => !string.IsNullOrWhiteSpace(s))
                .WithMessage(m => $"Please write a valid field name for {m.Name}");
            
            RuleFor(x => x.NumericDefaultValue)
                .Must(BeNumberBetween)
                .WithMessage(m => $"Default value must be a number between {m.MinValue} and {m.MaxValue} for {m.Name}")
                .When(c => c.FieldType == FieldType.Integer);
            
            RuleFor(x => x.Options)
                .Must(NotContainEmpty)
                .WithMessage(m => $"Make sure all options are not empty for {m.Name}")
                .Must(NotContainDuplictaes)
                .WithMessage(m => $"Duplicate options name for {m.Name}")
                .Must(ContainsOptions)
                .WithMessage(m => $"Provide at least two options for {m.Name}")
                .When(c => c.FieldType is FieldType.OptionSelect or FieldType.MultipleChoice);
        }

        private static bool NotContainEmpty(List<OptionDto> v)
        {
            return v.All(i => !string.IsNullOrWhiteSpace(i.Name));
        }

        private static bool ContainsOptions(List<OptionDto> options)
        {
            return options.Count >= 2;
        }

        private static bool NotContainDuplictaes(List<OptionDto> options)
        {
            return !options.GroupBy(i => i.Name).SelectMany(i => i.ToList().Skip(1)).Any();
        }

        private static bool BeNumberBetween(FieldDto model, float? value)
        {
            if (value == null)
            {
                return true;
            }

            float v = (float) value;
            return v >= model.MinValue && v <= model.MaxValue;
        }
    }
}
