using System;
using UnityEngine;

namespace ColdWind.Core.SerializableInterface
{
    [Serializable]
    public class SerializableInterface<T> where T : class
    {
        [HideInInspector, SerializeField] private UnityEngine.Object _object;

        public T Value => _object as T;
    }
}
