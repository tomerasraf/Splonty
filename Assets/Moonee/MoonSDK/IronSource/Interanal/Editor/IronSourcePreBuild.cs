using Moonee.MoonSDK.Internal.Editor;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Moonee.MoonSDK.Internal.Advertisement.Editor
{
    public class IronSourcePreBuild : IPreprocessBuildWithReport
    {
        public int callbackOrder => 1;

        public void OnPreprocessBuild(BuildReport report)
        {
            CheckAndUpdateFacebookSettings(MoonSDKSettings.Load());
        }
        public static void CheckAndUpdateFacebookSettings(MoonSDKSettings settings)
        {
            if (!settings.IronSource) return;
#if UNITY_IOS

            if (settings == null || string.IsNullOrEmpty(settings.ironSourceIOSAppKey.Replace(" ", string.Empty)))
                BuildErrorWindow.LogBuildError(BuildErrorConfig.ErrorID.NoIOSIronSourceKey);

            if (settings == null || string.IsNullOrEmpty(settings.adMobIOSAppKey.Replace(" ", string.Empty)))
                BuildErrorWindow.LogBuildError(BuildErrorConfig.ErrorID.NoIOSAdMobKey);
            else
            {
                IronSourceMediatedNetworkSettingsInspector.IronSourceMediatedNetworkSettings.AdmobIOSAppId = settings.adMobIOSAppKey;
                IronSourceMediatedNetworkSettingsInspector.IronSourceMediatedNetworkSettings.EnableAdmob = true;
            }
#endif
#if UNITY_ANDROID
            if (settings == null || string.IsNullOrEmpty(settings.ironSourceAndroidAppKey.Replace(" ", string.Empty)))
                BuildErrorWindow.LogBuildError(BuildErrorConfig.ErrorID.NoAndroidIronSourceKey);

            if (settings == null || string.IsNullOrEmpty(settings.adMobAndroidAppKey.Replace(" ", string.Empty)))
            {
                BuildErrorWindow.LogBuildError(BuildErrorConfig.ErrorID.NoAndroidAdMobKey);
                IronSourceMediatedNetworkSettingsInspector.IronSourceMediatedNetworkSettings.EnableAdmob = false;
            }
            else
            {
                IronSourceMediatedNetworkSettingsInspector.IronSourceMediatedNetworkSettings.AdmobAndroidAppId = settings.adMobAndroidAppKey;
                IronSourceMediatedNetworkSettingsInspector.IronSourceMediatedNetworkSettings.EnableAdmob = true;
            }
#endif

        }
    }
}