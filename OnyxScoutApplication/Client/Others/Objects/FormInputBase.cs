using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using OnyxScoutApplication.Shared.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;
using OnyxScoutApplication.Shared.Models.ScoutFormFormatModels;

namespace OnyxScoutApplication.Client.Others.Objects
{
    public enum FormType
    {
        Update,
        Create,
        View
    }
    
    public abstract class FormInputBase<T> : InputBase<T>
    {
    }

    public abstract class SettingsInputBase : ComponentBase
    {
        [Parameter]
        public FieldDto Field { get; set; }
    }
}
