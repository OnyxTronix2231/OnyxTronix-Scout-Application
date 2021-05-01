using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;

namespace OnyxScoutApplication.Shared.Other
{
    public static class Extensions
    {
        public static List<FieldDto> WithCascadeFields(this List<FieldDto> field)
        {
            return field.Concat(field.SelectMany(i => WithCascadeFields(i.CascadeFields)).ToList()).ToList();
        }
    }
}
