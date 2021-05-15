using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace OnyxScoutApplication.Shared.Models.ScoutFormFormatModels
{
    public class FieldDto : IComparable<FieldDto>
    {
        public int Id { get; set; }

       // public int ScoutFormFormatId { get; set; }

        public string Name { get; set; }

        public string TextDefaultValue { get; set; }

        public bool BoolDefaultValue { get; set; }

        public float? NumericDefaultValue { get; set; }
        
        public bool CascadeConditionDefaultValue { get; set; }
        
        public int MyProperty { get; set; }

        public FieldType FieldType { get; set; }

        public float? MaxValue { get; set; } = 9999;

        public float? MinValue { get; set; }

        public bool Required { get; set; }
        
        public bool AllowManualInput { get; set; }

        public List<OptionDto> Options { get; set; } = new();
        
        public List<OptionDto> DefaultSelectedOptions { get; set; } = new();

        public int MaximumSelectionLength { get; set; }

        public List<FieldDto> CascadeFields { get; set; } = new();

        public int Index { get; set; }

        public bool IsCollapsed { get; set; }
        
        public int CompareTo(FieldDto other)
        {
            if (ReferenceEquals(this, other)) return 0;
            if (ReferenceEquals(null, other)) return 1;
            return Index.CompareTo(other.Index);
        }
    }
    
    public class OptionDto : INotifyPropertyChanged
    {
        private string name;
        private int index;
        public int Id { get; set; }

        public string Name
        {
            get => name;
            set
            {
                name = value;
                OnPropertyChanged();
            }
        }

        public float PercentWeight { get; set; }

        public int Index
        {
            get => index;
            set
            {
                index = value;
                OnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
