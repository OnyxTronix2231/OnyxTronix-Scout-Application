using AutoMapper;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Server.Data.Profiles.Resolvers
{
    public class ScoutFormFromatToScoutFormConverter : ITypeConverter<ScoutFormFormatDto, ScoutFormDto>
    {
        private readonly IMapper mapper;
        public ScoutFormFromatToScoutFormConverter(IMapper mapper)
        {
            this.mapper = mapper;
        }

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

        private ScoutFormDataDto GetScoutFormDataFromField(FieldDto field)
        {
            ScoutFormDataDto scoutFormData = new ScoutFormDataDto() { Field = field, FieldID = field.Id };
            foreach (var f in field.CascadeFields)
            {
                scoutFormData.CascadeData.Add(GetScoutFormDataFromField(f));
            }
            return scoutFormData;
        }
    }
}
