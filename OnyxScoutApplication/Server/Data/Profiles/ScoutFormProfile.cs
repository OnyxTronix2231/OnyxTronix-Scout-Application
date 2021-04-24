using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OnyxScoutApplication.Server.Data.Profiles.Resolvers;
using OnyxScoutApplication.Server.Models;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos;

namespace OnyxScoutApplication.Server.Data.Profiles
{
    public class ScoutFormProfile : Profile
    {
        public ScoutFormProfile()
        {
            CreateMap<FieldDto, Field>().ForMember(des => des.Options, opt =>
            {
                opt.PreCondition(src =>
                    src.FieldType == FieldType.OptionSelect || src.FieldType == FieldType.MultipleChoice);
                opt.MapFrom(src => src.Options.Aggregate((i, j) => i + ";" + j));
            }).ForMember(des => des.TextDefaultValue,
                opt => opt.MapFrom(src =>
                    src.FieldType == FieldType.MultipleChoice
                        ? src.DefaultSelectedOptions.Aggregate(string.Empty, (i, j) => i + ";" + j)
                        : src.TextDefaultValue));

            CreateMap<Field, FieldDto>().ForMember(des => des.Options,
                    opt => opt.MapFrom(des => des.Options.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()))
                .ForMember(des => des.DefaultSelectedOptions,
                    opt => opt.MapFrom(src =>
                        src.TextDefaultValue.Split(';', StringSplitOptions.RemoveEmptyEntries).ToList()));

            CreateMap<ScoutFormFormat, ScoutFormFormat>();
            CreateMap<ScoutFormFormatDto, ScoutFormFormatDto>();
            CreateMap<ScoutFormFormat, ScoutFormFormatDto>();

            CreateMap<ScoutFormFormatDto, ScoutFormFormat>();

            CreateMap<ScoutFormDataDto, ScoutFormData>()
                .ForMember(des => des.Value, opt => opt.MapFrom<ScoutFormDataValueConverter>());
            CreateMap<ScoutFormData, ScoutFormDataDto>().ConvertUsing<ScoutFormDataDtoConverter>();

            CreateMap<ScoutForm, ScoutForm>();
            CreateMap<ScoutFormDto, ScoutForm>();
            CreateMap<ScoutForm, ScoutFormDto>();

            CreateMap<ScoutFormFormatDto, ScoutFormDto>().ConvertUsing<ScoutFormFormatToScoutFormConverter>();

            CreateMap<FieldsInStage, FieldsInStageDto>();
            CreateMap<FieldsInStageDto, FieldsInStage>();
            
            CreateMap<FormDataInStage, FormDataInStageDto>();
            CreateMap<FormDataInStageDto, FormDataInStage>();
            
            CreateMap<IdentityRole, IdentityRoleDto>();
            CreateMap<IdentityRoleDto, IdentityRole>();

            CreateMap<ApplicationUserRole, ApplicationUserRoleDto>();
            CreateMap<ApplicationUserRoleDto, ApplicationUserRole>();

            CreateMap<ApplicationRole, ApplicationRoleDto>();
            CreateMap<ApplicationRoleDto, ApplicationRole>();

            CreateMap<ApplicationUser, ApplicationUser>();
            CreateMap<ApplicationUser, ApplicationUserDto>();
            CreateMap<ApplicationUserDto, ApplicationUser>();

            CreateMap<CustomEvent, CustomEvent>();
            CreateMap<CustomEventDto, CustomEventDto>();
            CreateMap<CustomEvent, CustomEventDto>();
            CreateMap<CustomEventDto, CustomEvent>();
            CreateMap<CustomEventDto, Event>();

            CreateMap<CustomMatch, CustomMatch>();
            CreateMap<CustomMatchDto, CustomMatchDto>();
            CreateMap<CustomMatch, CustomMatchDto>();
            CreateMap<CustomMatchDto, CustomMatch>();
            CreateMap<CustomMatchDto, Match>();

            CreateMap<CustomAlliances, CustomAlliances>();
            CreateMap<CustomAlliancesDto, CustomAlliancesDto>();
            CreateMap<CustomAlliances, CustomAlliancesDto>();
            CreateMap<CustomAlliancesDto, CustomAlliances>();
            CreateMap<CustomAlliancesDto, Alliances>();

            CreateMap<CustomAlliance, CustomAlliance>();
            CreateMap<CustomAllianceDto, CustomAllianceDto>();
            CreateMap<CustomAlliance, CustomAllianceDto>();
            CreateMap<CustomAllianceDto, CustomAlliance>();
            CreateMap<CustomAllianceDto, Alliance>().ForMember(des => des.TeamKeys, opt =>
                opt.MapFrom(src => src.Teams.Select(i => "frc" + i.TeamNumber)));

            CreateMap<CustomTeam, CustomTeam>();
            CreateMap<CustomTeamDto, CustomTeamDto>();
            CreateMap<CustomTeam, CustomTeamDto>();
            CreateMap<CustomTeamDto, CustomTeam>();
            CreateMap<CustomTeamDto, Team>();
        }
    }
}
