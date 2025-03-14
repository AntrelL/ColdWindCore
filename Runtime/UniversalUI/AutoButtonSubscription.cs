using ColdWind.Core.ModularCompositeRoot;
using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace ColdWind.Core.UniversalUI
{
    [RequireComponent(typeof(Button))]
    public class AutoButtonSubscription : MonoScript<Action>
    {
        private UnityAction _onClick;
        private Button _button;

        protected override void Constructor(Action onClick)
        {
            _button = GetComponent<Button>();
            _onClick = new UnityAction(onClick);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(_onClick);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(_onClick);
        }
    }
}
