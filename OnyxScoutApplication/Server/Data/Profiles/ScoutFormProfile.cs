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
            CreateMap<ScoutFormFormatDto, ScoutFormFormat>().ForMember(des => des.Fields, opt => opt.MapFrom<ScoutFormFormatValueConverter>());

            CreateMap<ScoutFormDataDto, ScoutFormData>().ForMember((des) => des.Value, opt => opt.MapFrom<ScoutFormDataValueConverter>());
            CreateMap<ScoutFormData, ScoutFormDataDto>().ConvertUsing<ScoutFormDataDtoConverter>();

            CreateMap<ScoutForm, ScoutForm>();
            CreateMap<ScoutFormDto, ScoutForm>().ForMember(des => des.Data, opt => opt.MapFrom<ScoutFormValueConverter>());
            CreateMap<ScoutForm, ScoutFormDto>()
                .ForMember(des => des.AutonomousData, opt => opt.MapFrom(src => src.Data.Where(i => i.Field.FieldStageType == FieldStageType.Autonomous)))
                .ForMember(src => src.TeleoperatedData, opt => opt.MapFrom(des => des.Data.Where(i => i.Field.FieldStageType == FieldStageType.Teleoperated)))
                .ForMember(src => src.EndGameData, opt => opt.MapFrom(des => des.Data.Where(i => i.Field.FieldStageType == FieldStageType.EndGame)));

            CreateMap<ScoutFormFormatDto, ScoutFormDto>().ConvertUsing<ScoutFormFromatToScoutFormConverter>();
        }
    }
}
