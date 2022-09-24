using System;
using UnityEditor;
using UnityEngine;
using Moonee.MoonSDK.Internal.Analytics.Editor;
using Moonee.MoonSDK.Internal.Advertisement.Editor;

namespace Moonee.MoonSDK.Internal.Editor
{
    [CustomEditor(typeof(MoonSDKSettings))]
    public class MoonSDKSettingsEditor : UnityEditor.Editor
    {
        private const string EditorPrefEditorIDFA = "EditorIDFA";
        private MoonSDKSettings SDKSettings => target as MoonSDKSettings;

        [MenuItem("Moonee/Moon SDK/Edit Settings", false, 100)]
        private static void EditSettings()
        {
            Selection.activeObject = CreateMooneeSDKSettings();
        }
        private static MoonSDKSettings CreateMooneeSDKSettings()
        {
            MoonSDKSettings settings = MoonSDKSettings.Load();
            if (settings == null) {
                settings = CreateInstance<MoonSDKSettings>();

                if (!AssetDatabase.IsValidFolder("Assets/Resources"))
                    AssetDatabase.CreateFolder("Assets", "Resources");

                if (!AssetDatabase.IsValidFolder("Assets/Resources/MoonSDK"))
                    AssetDatabase.CreateFolder("Assets/Resources", "MoonSDK");

                AssetDatabase.CreateAsset(settings, "Assets/Resources/MoonSDK/Settings.asset");
                settings = MoonSDKSettings.Load();
            }
            return settings;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            GUILayout.Space(15);

#if UNITY_IOS || UNITY_ANDROID      
            if (GUILayout.Button(Environment.NewLine + "Check and Sync Settings" + Environment.NewLine)) {
                CheckAndUpdateSdkSettings(SDKSettings);
            }
#else
            EditorGUILayout.HelpBox(BuildErrorConfig.ErrorMessageDict[BuildErrorConfig.ErrorID.INVALID_PLATFORM], MessageType.Error);   
#endif
            string editorIdfa = EditorPrefs.GetString(EditorPrefEditorIDFA);
            if (string.IsNullOrEmpty(editorIdfa))
            {
                editorIdfa = Guid.NewGuid().ToString();
                EditorPrefs.SetString(EditorPrefEditorIDFA, editorIdfa);
            }
        }
        private static void CheckAndUpdateSdkSettings(MoonSDKSettings settings)
        {
            Console.Clear();
            BuildErrorWindow.Clear();
            GameAnalyticsPreBuild.CheckAndUpdateGameAnalyticsSettings(settings);
            FacebookPreBuild.CheckAndUpdateFacebookSettings(settings);
            AdjustBuildPrebuild.CheckAndUpdateAdjustSettings(settings);
            IronSourcePreBuild.CheckAndUpdateFacebookSettings(settings);
            global::MoonSDK.UpdateAdjustToken(settings);
        }
    }
}