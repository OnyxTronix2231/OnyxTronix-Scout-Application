using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;
using OnyxScoutApplication.Shared.Models.ScoutFormModels;

namespace OnyxScoutApplication.Shared.Other
{
    public static class Extensions
    {
        public static List<FieldDto> WithCascadeFields(this List<FieldDto> field)
        {
            return field.SelectMany(i => new[] {i}.Concat(WithCascadeFields(i.CascadeFields))).ToList();
        }
        
        public static List<FormDataDto> WithCascadeData(this List<FormDataDto> field)
        {
            return field.SelectMany(i => new[] {i}.Concat(WithCascadeData(i.CascadeData))).ToList();
        }
        
        public static List<Field> WithCascadeFields(this List<Field> field)
        {
            return field.Concat(field.SelectMany(i => WithCascadeFields(i.CascadeFields)).ToList()).ToList();
        }
        
        public static List<FormData> WithCascadeData(this List<FormData> field)
        {
            return field.SelectMany(i => new[] {i}.Concat(WithCascadeData(i.CascadeData))).ToList();
        }
    }
}
