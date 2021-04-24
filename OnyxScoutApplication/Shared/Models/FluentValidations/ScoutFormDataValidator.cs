using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;

namespace OnyxScoutApplication.Shared.Models.FluentValidations
{
    public class ScoutFormDataValidator : AbstractValidator<FormDataDto>
    {
        public ScoutFormDataValidator()
        {
            RuleFor(x => x.NumericValue).Must(BeNumberBetween)
                .WithMessage((model, s) =>
                    "Value must be a number between " + model.Field.MinValue + " and " + model.Field.MaxValue)
                .When(c => c.Field.FieldType == FieldType.Numeric);
            RuleFor(x => x.NumericValue).NotEmpty().WithMessage((model, s) => "Field required")
                .When(m => m.Field.Required && m.Field.FieldType == FieldType.Numeric);
            RuleFor(x => x.StringValue).NotEmpty().WithMessage((model, s) => "Field required").When(m =>
                m.Field.Required && (m.Field.FieldType == FieldType.TextField ||
                                     m.Field.FieldType == FieldType.OptionSelect));
            RuleForEach(x => x.CascadeData).SetValidator(this)
                .When(x => x.Field.FieldType == FieldType.CascadeField && x.BooleanValue);
        }

        private static bool BeNumberBetween(FormDataDto model, int? value)
        {
            if (value == null)
            {
                return true;
            }

            int v = (int) value;
            return v >= model.Field.MinValue && v <= model.Field.MaxValue;
        }
    }
}
