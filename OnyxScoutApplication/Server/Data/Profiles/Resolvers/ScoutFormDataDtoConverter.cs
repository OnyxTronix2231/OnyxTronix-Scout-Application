using AutoMapper;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                FieldID = source.FieldID,
                Id = source.Id
            };

            switch (source.Field.FieldType)
            {
                case FieldType.Boolean:
                    destination.BooleanValue = bool.Parse(source.Value);
                    break;
                case FieldType.TextField:
                    destination.StringValue = source.Value;
                    break;
                case FieldType.Numeric:
                    destination.NumricValue = int.Parse(source.Value);
                    break;
                default:
                    break;
            }
            return destination;
        }
    }
}
