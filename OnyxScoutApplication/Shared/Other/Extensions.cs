using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OnyxScoutApplication.Shared.Other
{
    public static class Extensions
    {
        public static List<FieldDto> ConcatAllCascadeFields(this List<FieldDto> fields)
        {
            return fields.Count == 0
                ? fields
                : fields.Concat(ConcatAllCascadeFields(fields.SelectMany(i => i.CascadeFields)
                    .ToList())).ToList();
        }

        public static SortedList<T> ToSortedList<T>(this IEnumerable<T> list) where T : IComparable<T>
        {
            return (SortedList<T>) list;
        }
    }
}
