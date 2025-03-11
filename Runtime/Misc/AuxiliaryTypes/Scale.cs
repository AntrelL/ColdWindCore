using ColdWind.Core.AdvancedDebugModule;
using System;

namespace ColdWind.Core
{
    public class Scale : IRangeConfigurableOnlyScale
    {
        public const int MaxPercentage = 100;

        private float _value;
        private float _min;
        private float _max;

        public Scale(float value, float min, float max, bool autoLimitNewValueToRange = false)
        {
            _value = value;
            _min = min;
            _max = max;

            AutoLimitNewValueToRange = autoLimitNewValueToRange;
        }

        public bool AutoLimitNewValueToRange { get; set; }

        public float Ratio => (Value - Min) / (Max - Min);

        public float Percentage => Ratio * MaxPercentage;

        public float Value 
        {
            get => _value;
            set
            {
                if (value.IsInRange(Min, Max))
                {
                    _value = value;
                    return;
                }

                if (AutoLimitNewValueToRange)
                {
                    _value = Math.Clamp(value, Min, Max);
                    return;
                }

                AdvancedDebug.LogError<Scale>(
                    $"The value must be in the range [{Min}, {Max}]", isModuleName: false);
            }
        }

        public float Min
        {
            get => _min;
            set
            {
                if (value > Max)
                {
                    AdvancedDebug.LogError<Scale>(
                        $"The new min value cannot be greater than the max value", isModuleName: false);

                    return;
                }

                if (value > Value)
                    Value = value;

                _min = value;
            }
        }

        public float Max
        {
            get => _max;
            set
            {
                if (value < Min)
                {
                    AdvancedDebug.LogError<Scale>(
                        $"The new max value cannot be less than the min value", isModuleName: false);

                    return;
                }

                if (value < Value)
                    Value = value;

                _max = value;
            }
        }
    }
}
