using ColdWind.Core.AdvancedDebugModule;
using System;
using UnityEngine;

namespace ColdWind.Core.ModularCompositeRoot.Internal
{
    public abstract class BaseMonoScript : MonoBehaviour
    {
        public bool IsConstructed { get; private set; }

        private bool _isUnprocessedOnEnable = false;

        private void OnEnable()
        {
            if (IsConstructed)
                OnActivate();
            else
                _isUnprocessedOnEnable = true;
        }

        private void OnDisable() => OnDeactivate();

        protected void OnConstructed()
        {
            if (IsConstructed)
            {
                AdvancedDebug.LogError<MonoScript>(
                    "The constructor can only be called once", isTypeName: false);

                return;
            }

            IsConstructed = true;

            if (_isUnprocessedOnEnable)
            {
                OnActivate();
                _isUnprocessedOnEnable = false;
            }
        }

        protected void RunConstructorExecutor(Action constructorExecutor)
        {
            constructorExecutor.Invoke();
            OnConstructed();
        }

        protected virtual void OnActivate() { }

        protected virtual void OnDeactivate() { }
    }
}
