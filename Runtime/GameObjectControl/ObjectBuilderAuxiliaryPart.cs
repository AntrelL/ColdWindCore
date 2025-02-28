using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace ColdWind.Core.GameObjectControl
{
    public static partial class ObjectBuilder
    {
        private static GameObject Instantiate(
            GameObject prefab, InstantiationParameters parameters)
        {
            return Addressables.InstantiateAsync(prefab, parameters).WaitForCompletion();
        }

        private static Task<GameObject> InstantiateAsync(GameObject prefab)
        {
            return Addressables.InstantiateAsync(prefab).Task;
        }

        private static Task<GameObject> InstantiateAsync(
            string prefabName, InstantiationParameters instantiationParameters)
        {
            return Addressables.InstantiateAsync(prefabName, instantiationParameters).Task;
        }

        private static Task<GameObject> InstantiateAsync(string prefabName)
        {
            return Addressables.InstantiateAsync(prefabName).Task;
        }

        private static T LoadPrefab<T>(string prefabName) where T : MonoBehaviour
        {
            return LoadPrefab(prefabName).GetComponent<T>();
        }

        private static GameObject LoadPrefab(string prefabName)
        {
            return Addressables.LoadAssetAsync<GameObject>(prefabName).WaitForCompletion();
        }
    }
}
