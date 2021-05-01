using FluentValidation;
using OnyxScoutApplication.Shared.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation.Validators;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;

namespace OnyxScoutApplication.Shared.Models.FluentValidations
{
    public class ScoutFormFormatValidator : AbstractValidator<ScoutFormFormatDto>
    {
        public ScoutFormFormatValidator()
        {
            RuleFor(x => x.Year).GreaterThanOrEqualTo(2000).LessThanOrEqualTo(2099);
            RuleForEach(x => x.FieldsInStages).SetValidator(new FieldsByStagesValidator());
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
