using FluentValidation;
using OnyxScoutApplication.Shared.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Validators;

namespace OnyxScoutApplication.Shared.Models.FluentValidations
{
    public class ScoutFormFormatValidator : AbstractValidator<ScoutFormFormatDto>
    {
        public ScoutFormFormatValidator()
        {
            RuleFor(x => x.Year).GreaterThanOrEqualTo(2000).LessThanOrEqualTo(2099);
            RuleForEach(x => x.FieldsInStages).SetValidator(new FieldsByStagesValidator());
            //RuleFor(x => x.FieldsByStages).Must(UniqueName).WithMessage("Fields name must be unique");
        }

        private static bool UniqueName(List<FieldDto> fields)
        {
            return !fields.ConcatAllCascadeFields().GroupBy(f => f.Name).Any(x => x.Skip(1).Any());
        }
    }

    public class FieldsByStagesValidator :  AbstractValidator<FieldsInStageDto>
    {
        public FieldsByStagesValidator()
        {
            RuleForEach(x => x.Fields).SetValidator(new FieldValidator());
        }
    }
}
