using AutoMapper;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Server.Data.Profiles.Resolvers
{
    public class ScoutFormFormatToScoutFormConverter : ITypeConverter<ScoutFormFormatDto, ScoutFormDto>
    {
        public ScoutFormDto Convert(ScoutFormFormatDto source, ScoutFormDto destination, ResolutionContext context)
        {
            destination = new ScoutFormDto();
            foreach (var field in source.AutonomousFields)
            {
                destination.AutonomousData.Add(GetScoutFormDataFromField(field));
            }

            foreach (var field in source.TeleoperatedFields)
            {
                destination.TeleoperatedData.Add(GetScoutFormDataFromField(field));
            }

            foreach (var field in source.EndGameFields)
            {
                destination.EndGameData.Add(GetScoutFormDataFromField(field));
            }

            return destination;
        }

        private static ScoutFormDataDto GetScoutFormDataFromField(FieldDto field)
        {
            ScoutFormDataDto scoutFormData = new ScoutFormDataDto {Field = field, FieldId = field.Id};
            switch (field.FieldType)
            {
                case FieldType.None:
                    break;
                case FieldType.CascadeField:
                case FieldType.Boolean:
                    scoutFormData.BooleanValue = field.BoolDefaultValue;
                    break;
                case FieldType.OptionSelect:
                case FieldType.TextField:
                    scoutFormData.StringValue = field.TextDefaultValue;
                    break;
                case FieldType.Numeric:
                    scoutFormData.NumericValue = field.NumericDefaultValue;
                    break;
                case FieldType.MultipleChoice:
                    scoutFormData.SelectedOptions = field.DefaultSelectedOptions;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            foreach (var f in field.CascadeFields)
            {
                scoutFormData.CascadeData.Add(GetScoutFormDataFromField(f));
            }

            return scoutFormData;
        }
    }
}
