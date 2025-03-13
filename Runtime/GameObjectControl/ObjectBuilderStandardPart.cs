using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace ColdWind.Core.GameObjectControl
{
    public static partial class ObjectBuilder
    {
        public static GameObject CreateNew(
            GameObject prefab, 
            Func<GameObject, GameObject> creator = null,
            Action<GameObject> constructor = null,
            bool isActivateObject = true)
        {
            bool initialPrefabState = prefab.activeSelf;
            prefab.SetActive(false);
            GameObject instance = creator?.Invoke(prefab) ?? GameObject.Instantiate(prefab);
            prefab.SetActive(initialPrefabState);

            constructor?.Invoke(instance);
            instance.SetActive(isActivateObject);

            return instance;
        }

        public static T CreateNew<T>(
            T prefab,
            Func<GameObject, GameObject> creator = null,
            Action<T> constructor = null,
            bool isActivateObject = true)
            where T : MonoBehaviour
        {
            T instance = CreateNew(prefab.gameObject, creator, null, false).GetComponent<T>();

            constructor?.Invoke(instance);
            instance.gameObject.SetActive(isActivateObject);

            return instance;
        }

        public static GameObject CreateNew(
            string prefabName,
            Func<GameObject, GameObject> creator = null,
            Action<GameObject> constructor = null,
            bool isActivateObject = true)
        {
            return CreateNew(LoadPrefab(prefabName), creator, constructor, isActivateObject);
        }

        public static T CreateNew<T>(
            string prefabName,
            Func<GameObject, GameObject> creator = null,
            Action<T> constructor = null,
            bool isActivateObject = true)
            where T : MonoBehaviour
        {
            return CreateNew(LoadPrefab<T>(prefabName), creator, constructor, isActivateObject);
        }

        public static GameObject CreateNew(
            string prefabName,
            InstantiationParameters instantiationParameters,
            Action<GameObject> constructor = null,
            bool isActivateObject = true)
        {
            return CreateNew(
                LoadPrefab(prefabName),
                (GameObject prefab) => Instantiate(prefab, instantiationParameters), 
                constructor, 
                isActivateObject);
        }

        public static T CreateNew<T>(
            string prefabName,
            InstantiationParameters instantiationParameters,
            Action<T> constructor = null,
            bool isActivateObject = true)
            where T : MonoBehaviour
        {
            return CreateNew(
                LoadPrefab<T>(prefabName),
                (GameObject prefab) => Instantiate(prefab, instantiationParameters),
                constructor,
                isActivateObject);
        }

        public static List<GameObject> CreateNewOnes(params string[] prefabNames)
        {
            return CreateNewOnes(true, prefabNames);
        }

        public static List<GameObject> CreateNewOnes(bool isActivateObjects, params string[] prefabNames)
        {
            return CreateNewOnes(null, isActivateObjects, prefabNames);
        }

        public static List<GameObject> CreateNewOnes(
            Func<GameObject, GameObject> creator, bool isActivateObjects, params string[] prefabNames)
        {
            return CreateNewOnes(creator, null, isActivateObjects, prefabNames);
        }

        public static List<GameObject> CreateNewOnes(
            Func<GameObject, GameObject> creator,
            Action<GameObject> constructor,
            bool isActivateObjects,
            params string[] prefabNames)
        {
            List<GameObject> instances = new();

            foreach (var prefabName in prefabNames)
            {
                instances.Add(CreateNew(prefabName, creator, constructor, isActivateObjects));
            }

            return instances;
        }
    }
}
