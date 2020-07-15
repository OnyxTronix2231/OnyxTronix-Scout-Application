using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace OnyxScoutApplication.Client.Others.Objects
{
    public enum FormType
    {
        Update,
        Create
    }

    public abstract class FormInputBase<T> : InputBase<T>
    {
        [Parameter]
        public FieldDto Field { get; set; }

        [Parameter]
        public bool IsEditMode { get; set; }
    }
}
