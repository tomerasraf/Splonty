using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using Moonee.MoonSDK.Internal.Editor;

namespace Moonee.MoonSDK.Internal.Analytics.Editor
{
    public class AdjustBuildPrebuild : IPreprocessBuildWithReport
    {
        public int callbackOrder => 1;

        public void OnPreprocessBuild(BuildReport report)
        {
            CheckAndUpdateAdjustSettings(MoonSDKSettings.Load());
        }
        public static void CheckAndUpdateAdjustSettings(MoonSDKSettings settings)
        {
            if (!settings.Adjust) return;

            if (settings == null || string.IsNullOrEmpty(settings.adjustToken.Replace(" ", string.Empty)))
                BuildErrorWindow.LogBuildError(BuildErrorConfig.ErrorID.NoAdjustToken);
        }
    }
}