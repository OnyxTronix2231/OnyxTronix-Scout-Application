using AutoMapper;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnyxScoutApplication.Shared.Other;

namespace OnyxScoutApplication.Server.Data.Profiles.Resolvers
{
    public class ScoutFormDataDtoConverter : ITypeConverter<ScoutFormData, ScoutFormDataDto>
    {
        private readonly IMapper mapper;

        public ScoutFormDataDtoConverter(IMapper mapper)
        {
            this.mapper = mapper;
        }

        public ScoutFormDataDto Convert(ScoutFormData source, ScoutFormDataDto destination, ResolutionContext context)
        {
            destination = new ScoutFormDataDto
            {
                Field = mapper.Map<FieldDto>(source.Field),
                FieldId = source.FieldId,
                Id = source.Id
            };

            switch (source.Field.FieldType)
            {
                case FieldType.CascadeField:
                    destination.CascadeData = mapper.Map<List<ScoutFormDataDto>>(source.CascadeData);
                    goto case FieldType.Boolean;
                case FieldType.Boolean:
                    destination.BooleanValue = bool.Parse(source.Value);
                    break;
                case FieldType.OptionSelect:
                case FieldType.TextField:
                    destination.StringValue = source.Value;
                    break;
                case FieldType.Numeric:
                    if (!string.IsNullOrWhiteSpace(source.Value))
                    {
                        destination.NumericValue = int.Parse(source.Value);
                    }

                    break;
                case FieldType.MultipleChoice:
                    destination.SelectedOptions = source.Value?.Split(';').ToList();
                    break;
                case FieldType.None:
                    throw new ArgumentOutOfRangeException();
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return destination;
        }
    }
}
