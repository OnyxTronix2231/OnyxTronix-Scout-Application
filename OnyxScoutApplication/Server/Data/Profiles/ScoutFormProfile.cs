using AutoMapper;
using OnyxScoutApplication.Server.Data.Profiles.Resolvers;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Server.Data.Profiles
{
    public class ScoutFormProfile : Profile
    {
        public ScoutFormProfile()
        {
            CreateMap<FieldDto, Field>();
            CreateMap<Field, FieldDto>();
            CreateMap<ScoutFormFormat, ScoutFormFormat>();
            CreateMap<ScoutFormFormatDto, ScoutFormFormatDto>();
            CreateMap<ScoutFormFormat, ScoutFormFormatDto>().ForMember(des => des.AutonomousFields, opt => opt.MapFrom(src => src.Fields.Where(i => i.FieldStageType == FieldStageType.Autonomous))).
                ForMember(src => src.TeleoperatedFields, opt => opt.MapFrom(des => des.Fields.Where(i => i.FieldStageType == FieldStageType.Teleoperated)))
                .ForMember(src => src.EndGameFields, opt => opt.MapFrom(des => des.Fields.Where(i => i.FieldStageType == FieldStageType.EndGame)));

            CreateMap<ScoutFormFormatDto, ScoutFormFormat>().ForMember(des => des.Fields, opt => opt.MapFrom<ScoutFormFormatResolver>());

            CreateMap<ScoutFormDataDto, ScoutFormData>().ForMember((des) => des.Value, opt => opt.MapFrom<ScoutFormDataValueResolver>());
            CreateMap<ScoutFormDto, ScoutForm>().ForMember(des => des.Data, opt => opt.MapFrom<ScoutFormResolver>());
        }
    }
}
