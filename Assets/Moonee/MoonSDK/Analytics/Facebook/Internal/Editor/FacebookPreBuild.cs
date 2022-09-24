using System.Collections.Generic;
using Facebook.Unity.Settings;
using Moonee.MoonSDK.Internal.Editor;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;


namespace Moonee.MoonSDK.Internal.Analytics.Editor
{
    public class FacebookPreBuild : IPreprocessBuildWithReport
    {
        public int callbackOrder => 1;

        public void OnPreprocessBuild(BuildReport report)
        {
            CheckAndUpdateFacebookSettings(MoonSDKSettings.Load());
        }

        public static void CheckAndUpdateFacebookSettings(MoonSDKSettings settings)
        {
#if UNITY_ANDROID || UNITY_IOS
            if (!settings.Facebook) return;

            if (settings == null || string.IsNullOrEmpty(settings.facebookAppId))
                BuildErrorWindow.LogBuildError(BuildErrorConfig.ErrorID.SettingsNoFacebookAppID);
            else if (settings == null || string.IsNullOrEmpty(settings.facebookAppId))
                BuildErrorWindow.LogBuildError(BuildErrorConfig.ErrorID.SettingsNoFacebookClientID);
            else
            {
                FacebookSettings.AppIds = new List<string> { settings.facebookAppId };
                FacebookSettings.ClientTokens = new List<string> { settings.facebookClientId };
                FacebookSettings.AppLabels = new List<string> { Application.productName };
                EditorUtility.SetDirty(FacebookSettings.Instance);
            }
#endif

        }
    }
}