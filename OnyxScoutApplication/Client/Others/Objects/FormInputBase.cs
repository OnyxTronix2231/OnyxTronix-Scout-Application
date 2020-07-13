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

    public class FormInputBase<T> : InputBase<string>
    {
        private T valueBind;

        [Parameter]
        public Field Field { get; set; }

        [Parameter]
        public bool IsEditMode { get; set; }

        protected T ValueBind
        {
            get => valueBind; set
            {
                valueBind = value;
                CurrentValue = valueBind.ToString();
            }
        }

        protected override void OnInitialized()
        {
            CurrentValue = ValueBind?.ToString();
        }

        protected override bool TryParseValueFromString(string value, out string result, [NotNullWhen(false)] out string validationErrorMessage)
        {
            result = value;
            validationErrorMessage = null;
            return true;
        }
    }
}
