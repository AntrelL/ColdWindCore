using ColdWind.Core.AdvancedDebugModule;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace ColdWind.Core.ResourceControlModule
{
    public class ResourceControl
    {
        private const bool StandardAutoSkip = false;

        private static Dictionary<string, AsyncOperationHandle<IList<GameObject>>> s_loadedResources = new();

        public static List<GameObject> LoadAssets(params string[] labels)
        {
            return LoadAssets(StandardAutoSkip, labels);
        }

        public static List<GameObject> LoadAssets(bool autoSkipIfLoaded, params string[] labels)
        {
            List<GameObject> loadedObjects = new();
            
            foreach (var label in labels)
                loadedObjects.AddRange(LoadAssets(label, true, autoSkipIfLoaded).Result);
            
            return loadedObjects;
        }

        public static List<AsyncOperationHandle<IList<GameObject>>> LoadAssetsAsync(params string[] labels)
        {
            return LoadAssetsAsync(StandardAutoSkip, labels);
        }

        public static List<AsyncOperationHandle<IList<GameObject>>> LoadAssetsAsync(bool autoSkipIfLoaded, params string[] labels)
        {
            List<AsyncOperationHandle<IList<GameObject>>> handles = new();

            foreach (var label in labels)
                handles.Add(LoadAssets(label, false, autoSkipIfLoaded));
            
            return handles;
        }

        public static void ReleaseAssets(params string[] labels)
        {
            ReleaseAssets(StandardAutoSkip, labels);
        }

        public static void ReleaseAssets(bool autoSkipIfNotLoaded, params string[] labels)
        {
            foreach (var label in labels)
            {
                if (IsAssetsLoaded(label) == false)
                {
                    if (autoSkipIfNotLoaded == false)
                    {
                        AdvancedDebug.LogError<ResourceControl>(
                            $"Resources with label: {label} cannot be freed because they were not loaded",
                            isModuleName: false);
                    }

                    return;
                }

                Addressables.Release(s_loadedResources[label]);
                s_loadedResources.Remove(label);
            }
        }

        public static bool IsAssetsLoaded(params string[] labels)
        {
            foreach (var label in labels)
            {
                if (s_loadedResources.ContainsKey(label) == false)
                    return false;
            }

            return true;
        }

        private static AsyncOperationHandle<IList<GameObject>> LoadAssets(
            string label, bool waitForCompletion, bool autoSkipIfLoaded)
        {
            if (IsAssetsLoaded(label))
            {
                if (autoSkipIfLoaded == false)
                {
                    AdvancedDebug.LogError<ResourceControl>(
                        $"Resources with label: {label} have already been loaded", isModuleName: false);
                }

                return s_loadedResources[label];
            }

            var handle = Addressables.LoadAssetsAsync<GameObject>(label, null);
            s_loadedResources.Add(label, handle);

            if (waitForCompletion)
                handle.WaitForCompletion();

            return handle;
        }
    }
}
