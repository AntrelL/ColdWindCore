using ColdWind.Core.AdvancedDebugModule;
using System.Collections.Generic;
using UnityEngine;

namespace ColdWind.Core.ModularCompositeRoot
{
    public abstract class CompositeRoot : CompositeGroup
    {
        protected const int ExecutionOrderValue = -9000;

        [SerializeField] private List<CompositeGroup> _otherGroups = new();

        private void OnValidate() => CheckExecutionOrder();

        private void Awake()
        {
            LoadResources();
            _otherGroups.ForEach(group => group.LoadResources());

            Construct();
            _otherGroups.ForEach(group => group.Construct());
        }

        private void OnDisable()
        {
            Deconstruct();
            _otherGroups.ForEach(group => group.Deconstruct());
        }

        private void CheckExecutionOrder()
        {
            object[] attributes = GetType().GetCustomAttributes(typeof(DefaultExecutionOrder), false);

            if (attributes.Length == 0)
            {
                AdvancedDebug.LogWarning<CompositeRoot>(
                    $"{GetType().Name} has no established order of execution");

                return;
            }

            int executionOrder = ((DefaultExecutionOrder)attributes[0]).order;

            if (executionOrder != ExecutionOrderValue)
            {
                AdvancedDebug.LogWarning<CompositeRoot>(
                    $"{GetType().Name} must have an execution order of " +
                    $"{ExecutionOrderValue} (const {nameof(ExecutionOrderValue)})");
            }
        }
    }
}
