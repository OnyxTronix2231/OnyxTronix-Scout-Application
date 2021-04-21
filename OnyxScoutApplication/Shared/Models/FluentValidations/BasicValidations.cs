using System;
using FluentValidation;

namespace OnyxScoutApplication.Shared.Models.FluentValidations
{
    public static class BasicValidations
    {

        public static bool IsValidTeamNumber(this int teamNumber)
        {
            return teamNumber >= 1 && teamNumber <= 9999;
        }
        
        public static bool IsValidGameYear(this int year)
        {
            return year >= 2000 && year <= 2099;
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
