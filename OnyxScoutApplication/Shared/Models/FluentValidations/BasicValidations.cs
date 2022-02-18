using System;
using FluentValidation;

namespace OnyxScoutApplication.Shared.Models.FluentValidations
{
    public static class BasicValidations
    {

        public static bool IsValidTeamNumber(this int teamNumber)
        {
            return teamNumber is >= 1 and <= 9999;
        }
        
        public static bool IsValidGameYear(this int year)
        {
            return year is >= 2000 and <= 2099;
        }
        
        public static IRuleBuilderOptions<T, int> ValidTeamNumber<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder.Must(i => i.IsValidTeamNumber());
        }
        
        public static IRuleBuilderOptions<T, int> ValidGameYear<T>(this IRuleBuilder<T, int> ruleBuilder)
        {
            return ruleBuilder.Must(i => i.IsValidGameYear());
        }
    }
}
