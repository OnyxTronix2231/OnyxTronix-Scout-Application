﻿using AutoMapper;
using Microsoft.AspNetCore.Identity;
using OnyxScoutApplication.Server.Data.Profiles.Resolvers;
using OnyxScoutApplication.Server.Models;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnyxScoutApplication.Shared.Models.CustomeEventModels;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;
using OnyxScoutApplication.Shared.Models.TheBlueAllianceDtos;

namespace OnyxScoutApplication.Server.Data.Profiles
{
    public class ScoutFormProfile : Profile
    {
        public ScoutFormProfile()
        {
            CreateMap<FieldDto, Field>().
                ForMember(des => des.DefaultValue, opt => opt.MapFrom<FieldDtoToFieldValueConverter>())
                ;
                
            CreateMap<Field, FieldDto>()               
                .AfterMap<FieldToFieldDtoConvertor>();
            
            CreateMap<Field, Field>();
            
            CreateMap<Option, OptionDto>();
            CreateMap<OptionDto, Option>();

            CreateMap<ScoutFormFormat, ScoutFormFormat>();
            CreateMap<ScoutFormFormatDto, ScoutFormFormatDto>();
            CreateMap<ScoutFormFormat, ScoutFormFormatDto>();

            CreateMap<ScoutFormFormatDto, ScoutFormFormat>();

            CreateMap<FormDataDto, FormData>()
                .ForMember(des => des.Value, opt => opt.MapFrom<FormDataDtoToFormDataValueConverter>());
                //.ForMember(dst => dst.Field, op => op.Ignore());
            CreateMap<FormData, FormDataDto>().AfterMap<FormDataToFormDataDtoConvertor>();

            CreateMap<Form, Form>();
            CreateMap<FormDto, Form>();
            CreateMap<Form, FormDto>();
            CreateMap<Form, SimpleFormDto>();

            CreateMap<ScoutFormFormatDto, FormDto>().ConvertUsing<ScoutFormFormatToScoutFormConverter>();

            CreateMap<FieldsInStage, FieldsInStageDto>();
            CreateMap<FieldsInStageDto, FieldsInStage>();
            CreateMap<FieldsInStage, FieldsInStage>();

            CreateMap<FormDataInStage, FormDataInStageDto>();
            CreateMap<FormDataInStageDto, FormDataInStage>();
            
            CreateMap<FieldsInStageDto, FormDataInStageDto>().ForMember(dst => dst.FormData, 
                op => op.MapFrom(src => src.Fields))
                .ForMember(dst => dst.Id, opt => opt.Ignore());
            
            CreateMap<FieldDto, FormDataDto>().ConvertUsing<FieldToScoutFormDataConverter>();
            
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
