using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Moonee.MoonSDK.Internal
{
    [CreateAssetMenu(fileName = "Assets/Resources/Moonee/MoonSDKSettings", menuName = "MoonSDK/Settings file")]
    public class MoonSDKSettings : ScriptableObject
    {
        private const string SETTING_RESOURCES_PATH = "MoonSDK/Settings";

        private static bool  _facebookGroupEnabled;

        public static MoonSDKSettings Load() => Resources.Load<MoonSDKSettings>(SETTING_RESOURCES_PATH);

        [Header("Moon SDK version 1.1.2", order = 0)]

        [Space(10)]
        public bool Firebase;

        [Space(10)]
        [ConditionalHide("Firebase")]
        public double int_grace_time = 0;
        [ConditionalHide("Firebase")]
        public long int_grace_level = 0;
        [ConditionalHide("Firebase")]
        public double cooldown_between_INTs = 0;
        [ConditionalHide("Firebase")]
        public double cooldown_after_RVs = 0;
        [ConditionalHide("Firebase")]
        public bool Show_int_if_fail = false;
        [ConditionalHide("Firebase")]
        public bool INT_in_stage = false;

        [Space(10)]
        public bool GameAnalytics;

        [ConditionalHide("GameAnalytics")]
        [Tooltip("Your GameAnalytics Ios Game Key")]
        public string gameAnalyticsIosGameKey;

        [ConditionalHide("GameAnalytics")]
        [Tooltip("Your GameAnalytics Ios Secret Key")]
        public string gameAnalyticsIosSecretKey;

        [ConditionalHide("GameAnalytics")]
        [Tooltip("Your GameAnalytics Android Game Key")]
        public string gameAnalyticsAndroidGameKey;

        [ConditionalHide("GameAnalytics")]
        [Tooltip("Your GameAnalytics Android Secret Key")]
        public string gameAnalyticsAndroidSecretKey;

        [Space(10)]
        public bool Facebook;

        [ConditionalHide("Facebook")]
        [Tooltip("The Facebook App Id of your game")]
        public string facebookAppId;
        [ConditionalHide("Facebook")]
        [Tooltip("The Facebook Client Id of your game")]
        public string facebookClientId;


        [Space(10)]
        public bool Adjust;

        [ConditionalHide("Adjust")]
        [Tooltip("The Adjust App token of your game")]
        public string adjustToken;
        [ConditionalHide("Adjust")]
        [Tooltip("The Adjust iAP revenue tocekn")]
        public string adjustIAPRevenueToken;

        [Space(10)]
        public bool IronSource;

        [ConditionalHide("IronSource")]
        [Tooltip("The IOS Iron Source App key of your game")]
        public string ironSourceIOSAppKey;
        [ConditionalHide("IronSource")]
        [Tooltip("The Android Adjust App token of your game")]
        public string ironSourceAndroidAppKey;
        [Tooltip("The IOS Ad Mob Adapter key of your game")]
        [ConditionalHide("IronSource")]
        public string adMobIOSAppKey;
        [Tooltip("The Android Ad Mob Adapter key of your game")]
        [ConditionalHide("IronSource")]
        public string adMobAndroidAppKey;

        [Space(10)]
        [Tooltip("App Icon For GDPR")]
        public bool GDPR;
        [ConditionalHide("GDPR")]
        public Sprite appIcon;

        [Space(10)]
        [Tooltip("You studio logo for splash screen")]
        public bool StudioLogo;
        [ConditionalHide("StudioLogo")]
        public Sprite studioLogoSprite;
    }
}

