using System.Threading.Tasks;
using UnityEditor.PackageManager;
using UnityEditor.PackageManager.Requests;

namespace ColdWind.Core.Editor
{
    public class Package
    {
        public const string DisplayName = "Cold Wind Core";
        public const string Name = "com.coldwind.core";

        private const int DelayRecheckingVersionRequest = 100;

        public static async Task<string> GetVersionAsync()
        {
            var taskCompletionSource = new TaskCompletionSource<string>();
            ListRequest listRequest = Client.List();

            while (listRequest.IsCompleted == false)
            {
                await Task.Delay(DelayRecheckingVersionRequest);
            }

            if (listRequest.Status == StatusCode.Failure)
                return null;   

            foreach (var package in listRequest.Result)
            {
                if (package.name == Name)
                    return package.version;
            }

            return null;
        }
    }
}
