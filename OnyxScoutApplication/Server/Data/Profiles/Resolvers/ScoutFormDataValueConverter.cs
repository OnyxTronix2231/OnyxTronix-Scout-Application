using AutoMapper;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;

namespace OnyxScoutApplication.Server.Data.Profiles.Resolvers
{
    public class ScoutFormDataValueConverter : IValueResolver<FormDataDto, FormData, string>
    {
        public string Resolve(FormDataDto source, FormData destination, string destMember,
            ResolutionContext context)
        {
            switch (source.Field.FieldType)
            {
                case FieldType.CascadeField:
                case FieldType.Boolean:
                    destination.Value = source.BooleanValue.ToString();
                    break;
                case FieldType.OptionSelect:
                case FieldType.TextField:
                    destination.Value = source.StringValue;
                    break;
                case FieldType.Numeric:
                    destination.Value = source.NumericValue.ToString();
                    break;
                case FieldType.MultipleChoice:
                    if (source.SelectedOptions != null)
                    {
                        destination.Value = string.Join(";", source.SelectedOptions);
                    }
                    break;
                case FieldType.None:
                    throw new ArgumentOutOfRangeException();
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return destination.Value;
        }
    }
}
