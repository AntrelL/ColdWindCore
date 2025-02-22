using System.Linq;
using System.Text;
using UnityEngine;

namespace ColdWind.Core.AdvancedDebugModule
{
    public static class AdvancedDebug
    {
        private const char NamespaceSeparationSymbol = '.';

        private static readonly string EditorPartOfNamespace = NamespaceSeparationSymbol + "Editor"; 

        public static void Log<T>(string message, bool isModuleName = true, bool isTypeName = true) => 
            Debug.Log(CreateCompositeMessage<T>(message, isModuleName, isTypeName));

        public static void LogWarning<T>(string message, bool isModuleName = true, bool isTypeName = true) => 
            Debug.LogWarning(CreateCompositeMessage<T>(message, isModuleName, isTypeName));

        public static void LogError<T>(string message, bool isModuleName = true, bool isTypeName = true) => 
            Debug.LogError(CreateCompositeMessage<T>(message, isModuleName, isTypeName));

        private static string CreateCompositeMessage<T>(string message, bool isModuleName = true, bool isTypeName = true)
        {
            if ((isTypeName || isModuleName) == false)
                return message;

            string typeName = typeof(T).Name;
            string moduleName = typeof(T).Namespace
                .Remove(EditorPartOfNamespace)
                .Split(NamespaceSeparationSymbol)
                .Last();

            StringBuilder completedMessage = new();

            completedMessage.Append(isModuleName ? $"[{moduleName}]" : string.Empty);
            completedMessage.Append(isTypeName ? " " + typeName : string.Empty);

            completedMessage.Append(completedMessage.Length > 0 ? ": " : string.Empty);
            completedMessage.Append(message);

            return completedMessage.ToString().Trim();
        }
    }
}
