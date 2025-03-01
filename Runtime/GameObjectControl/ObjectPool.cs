using ColdWind.Core.AdvancedDebugModule;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace ColdWind.Core.GameObjectControl
{
    public class ObjectPool<T> where T : MonoBehaviour
    {
        private const float EmergencyExpansionRatio = 0.2f;

        private List<T> _activeObjects = new();
        private List<T> _inactiveObjects = new();

        private int _targetSize;

        public ObjectPool(
            int targetSize, 
            Func<T> creator, 
            Action<T> onObjectSpawned = null, 
            Action<T> onObjectDespawned = null)
        {
            OnObjectSpawned = onObjectSpawned;
            OnObjectDespawned = onObjectDespawned;

            Creator = creator;
            SetTargetSize(targetSize);
        }

        public Func<T> Creator { get; set; }

        public Action<T> OnObjectSpawned { get; set; }

        public Action<T> OnObjectDespawned { get; set; }

        public int Size => _activeObjects.Count + _inactiveObjects.Count;

        public int TargetSize => _targetSize;

        public void SetTargetSize(int value)
        {
            if (value < 0)
            {
                AdvancedDebug.LogError<ObjectPool<T>>(
                    "Target size cannot be less than zero", isModuleName: false);

                return;
            }

            _targetSize = value;
            RefreshSize();
        }

        public T Spawn(bool isActivate = true)
        {
            if (_inactiveObjects.Count == 0)
                UrgentlyExpand();

            T instance = _inactiveObjects[0];

            instance.gameObject.SetActive(isActivate);
            OnObjectSpawned?.Invoke(instance);

            _inactiveObjects.Remove(instance);
            _activeObjects.Add(instance);

            return instance;
        }

        public void Despawn(T instance)
        {
            if (_activeObjects.Contains(instance) == false)
            {
                AdvancedDebug.LogError<ObjectPool<T>>(
                    "Object passed to despawn does not belong to the pool", isModuleName: false);

                return;
            }

            instance.gameObject.SetActive(false);
            OnObjectDespawned?.Invoke(instance);

            _activeObjects.Remove(instance);
            _inactiveObjects.Add(instance);
        }

        public void Clear(bool force = false)
        {
            if (force)
            {
                for (int i = _activeObjects.Count - 1; i >= 0; i--)
                    Despawn(_activeObjects[i]);
            }

            SetTargetSize(0);
        }

        private void UrgentlyExpand()
        {
            SetTargetSize(TargetSize + (int)Math.Ceiling(TargetSize * EmergencyExpansionRatio));
        }

        private void RefreshSize()
        {
            int sizeDifference = Math.Abs(Size - TargetSize);

            if (Size < TargetSize)
                Expand(sizeDifference);
            else
                ShrinkAsPossible(sizeDifference);
        }

        private void Expand(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                T instance = Creator.Invoke();
                instance.gameObject.SetActive(false);
                _inactiveObjects.Add(instance);
            }
        }

        private void ShrinkAsPossible(int quantity)
        {
            int numberOfReductions = Math.Min(quantity, _inactiveObjects.Count);

            for (int i = 0; i < numberOfReductions; i++)
            {
                T instance = _inactiveObjects[0];
                _inactiveObjects.Remove(instance);
                GameObject.Destroy(instance.gameObject);
            }
        }
    }
}
