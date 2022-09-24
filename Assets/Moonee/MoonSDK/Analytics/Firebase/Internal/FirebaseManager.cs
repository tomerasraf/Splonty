using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.RemoteConfig;
using Firebase.Extensions;

namespace Moonee.MoonSDK.Internal.Analytics
{
    public class FirebaseManager : MonoBehaviour
    {
        public static System.Action OnRemoteConfigValuesReceived;
        void Start()
        {
            var settings = MoonSDKSettings.Load();

            if (settings.Firebase)
            {
                Initialize();
            }
            else Destroy(this);
        }

        // Update is called once per frame
        void Initialize()
        {
            Firebase.FirebaseApp.CheckAndFixDependenciesAsync().ContinueWith(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == Firebase.DependencyStatus.Available)
                {
                    // Create and hold a reference to your FirebaseApp,
                    // where app is a Firebase.FirebaseApp property of your application class.
                    var app = Firebase.FirebaseApp.DefaultInstance;
                    var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
                    // Set a flag here to indicate whether Firebase is ready to use by your app.
                    SetDefaultRemoteConfigValues();
                }
                else
                {
                    UnityEngine.Debug.LogError(System.String.Format(
                      "Could not resolve all Firebase dependencies: {0}", dependencyStatus));
                    // Firebase Unity SDK is not safe to use here.
                }
            });
        }
        private void SetDefaultRemoteConfigValues()
        {
            var remoteConfig = FirebaseRemoteConfig.DefaultInstance;
            var settings = MoonSDKSettings.Load();

            remoteConfig.SetDefaultsAsync(new Dictionary<string, object>
            {
               {"int_grace_time", settings.int_grace_time},
               {"int_grace_level", settings.int_grace_level},
               {"cooldown_between_INTs", settings.cooldown_between_INTs},
               {"cooldown_after_RVs", settings.cooldown_after_RVs},
               {"Show_int_if_fail", false},
               {"INT_in_stage", false},

            }).ContinueWithOnMainThread(task =>
            {
                remoteConfig.FetchAndActivateAsync().ContinueWithOnMainThread(task =>
                {
                    RemoteConfigValues.int_grace_time = FirebaseRemoteConfig.DefaultInstance.GetValue("int_grace_time").DoubleValue;
                    RemoteConfigValues.int_grace_level = FirebaseRemoteConfig.DefaultInstance.GetValue("int_grace_level").LongValue;
                    RemoteConfigValues.cooldown_between_INTs = FirebaseRemoteConfig.DefaultInstance.GetValue("cooldown_between_INTs").DoubleValue;
                    RemoteConfigValues.cooldown_after_RVs = FirebaseRemoteConfig.DefaultInstance.GetValue("cooldown_after_RVs").DoubleValue;
                    RemoteConfigValues.Show_int_if_fail = FirebaseRemoteConfig.DefaultInstance.GetValue("Show_int_if_fail").BooleanValue;
                    RemoteConfigValues.INT_in_stage = FirebaseRemoteConfig.DefaultInstance.GetValue("INT_in_stage").BooleanValue;

                    OnRemoteConfigValuesReceived?.Invoke();
                    OnRemoteConfigValuesReceived = null;
                });
            });
        }
    }
}

