using ColdWind.Core.EventLinks;
using ColdWind.Core.ModularCompositeRoot;
using UnityEngine;

namespace ColdWind.Core.UniversalUI
{
    public class ProgressBar : MonoScript<Scale, IEventLink<float>>
    {
        [SerializeField] private Transform _filler;
        [SerializeField] private bool _verticalScaling;
        [SerializeField] private bool _smoothChange;
        [SerializeField][Min(0)] private float _changeRate;

        private Scale _scale;
        private IEventLink<float> _eventLink;

        private float _targetValue;

        protected override void Constructor(Scale scale, IEventLink<float> eventLink)
        {
            _eventLink = eventLink;
            _scale = scale;
            _scale.AutoLimitNewValueToRange = true;

            SetTargetValue(_scale.Value);
        }

        public IRangeConfigurableOnlyScale Scale => _scale;

        private void OnEnable() => _eventLink.Called += OnModelValueChanged;

        private void OnDisable() => _eventLink.Called -= OnModelValueChanged;

        private void Update()
        {
            if (_smoothChange == false)
                return;

            _scale.Value = Mathf.MoveTowards(_scale.Value, _targetValue, _changeRate * Time.deltaTime);
            UpdateFiller();
        }

        private void SetTargetValue(float value)
        {
            _targetValue = value;

            if (_smoothChange == false)
            {
                _scale.Value = value;
                UpdateFiller();
            }
        }

        private void UpdateFiller()
        {
            _filler.localScale = _verticalScaling
                ? _filler.localScale.SetValues(y: _scale.Value)
                : _filler.localScale.SetValues(x: _scale.Value);
        }

        private void OnModelValueChanged(float value) => SetTargetValue(value);
    }
}
