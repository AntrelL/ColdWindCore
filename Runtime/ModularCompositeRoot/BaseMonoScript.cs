using ColdWind.Core.AdvancedDebugModule;
using System;
using UnityEngine;

namespace ColdWind.Core.ModularCompositeRoot.Internal
{
    public abstract class BaseMonoScript : MonoBehaviour
    {
        public bool IsConstructed { get; private set; }

        protected void OnConstructed()
        {
            if (IsConstructed)
            {
                AdvancedDebug.LogError<MonoScript>(
                    "The constructor can only be called once", isTypeName: false);

                return;
            }

            IsConstructed = true;
        }

        protected void RunConstructorExecutor(Action constructorExecutor)
        {
            constructorExecutor.Invoke();
            OnConstructed();
        }
    }
}
