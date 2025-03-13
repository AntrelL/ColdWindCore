using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace ColdWind.Core.GameObjectControl
{
    public static partial class ObjectBuilder
    {
        public static async Task<GameObject> CreateNewAsync(
            GameObject prefab,
            Func<GameObject, Task<GameObject>> creator = null,
            Action<GameObject> constructor = null,
            bool isActivateObject = true)
        {
            GameObject instance = await (creator?.Invoke(prefab) ?? InstantiateAsync(prefab));
            instance.SetActive(isActivateObject);
            constructor?.Invoke(instance);

            return instance;
        }

        public static async Task<T> CreateNewAsync<T>(
            T prefab,
            Func<GameObject, Task<GameObject>> creator = null,
            Action<T> constructor = null,
            bool isActivateObject = true)
            where T : Component
        {
            T instance = (await CreateNewAsync(prefab.gameObject, creator, null, isActivateObject)).GetComponent<T>();
            constructor?.Invoke(instance);

            return instance;
        }

        public static async Task<GameObject> CreateNewAsync(
            string prefabName,
            Func<string, Task<GameObject>> creator = null,
            Action<GameObject> constructor = null,
            bool isActivateObject = true)
        {
            GameObject instance = await (creator?.Invoke(prefabName) ?? InstantiateAsync(prefabName));
            instance.SetActive(isActivateObject);
            constructor?.Invoke(instance);

            return instance;
        }

        public static async Task<T> CreateNewAsync<T>(
            string prefabName,
            Func<string, Task<GameObject>> creator = null,
            Action<T> constructor = null,
            bool isActivateObject = true)
            where T : Component
        {
            T instance = (await CreateNewAsync(prefabName, creator, null, isActivateObject)).GetComponent<T>();
            constructor?.Invoke(instance);

            return instance;
        }

        public static async Task<GameObject> CreateNewAsync(
            string prefabName,
            InstantiationParameters instantiationParameters,
            Action<GameObject> constructor = null,
            bool isActivateObject = true)
        {
            return await CreateNewAsync(
                prefabName,
                (string prefabName) => InstantiateAsync(prefabName, instantiationParameters),
                constructor,
                isActivateObject);
        }

        public static async Task<T> CreateNewAsync<T>(
            string prefabName,
            InstantiationParameters instantiationParameters,
            Action<T> constructor = null,
            bool isActivateObject = true)
            where T : Component
        {
            return await CreateNewAsync(
                prefabName,
                (string prefabName) => InstantiateAsync(prefabName, instantiationParameters),
                constructor,
                isActivateObject);
        }

        public static async Task<List<GameObject>> CreateNewOnesAsync(params string[] prefabNames)
        {
            return await CreateNewOnesAsync(true, prefabNames);
        }

        public static async Task<List<GameObject>> CreateNewOnesAsync(bool isActivateObjects, params string[] prefabNames)
        {
            return await CreateNewOnesAsync(null, isActivateObjects, prefabNames);
        }

        public static async Task<List<GameObject>> CreateNewOnesAsync(
            Func<string, Task<GameObject>> creator, bool isActivateObjects, params string[] prefabNames)
        {
            return await CreateNewOnesAsync(creator, null, isActivateObjects, prefabNames);
        }

        public static async Task<List<GameObject>> CreateNewOnesAsync(
            Func<string, Task<GameObject>> creator,
            Action<GameObject> constructor,
            bool isActivateObjects,
            params string[] prefabNames)
        {
            List<GameObject> instances = new();

            foreach (var prefabName in prefabNames)
            {
                instances.Add(await CreateNewAsync(prefabName, creator, constructor, isActivateObjects));
            }

            return instances;
        }
    }
}
