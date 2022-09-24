using System.Collections.Generic;

namespace Moonee.MoonSDK.Internal.Editor
{
    public static class BuildErrorConfig
    {
        public enum ErrorID
        {
            SettingsNoFacebookAppID,
            SettingsNoFacebookClientID,
            GANoIOSKey,
            GANoAndroidAndKey,
            NoAdjustToken,
            NoIOSIronSourceKey,
            NoIOSAdMobKey,
            NoAndroidIronSourceKey,
            NoAndroidAdMobKey,
            INVALID_PLATFORM
        }

        public static readonly Dictionary<ErrorID, string> ErrorMessageDict = new Dictionary<ErrorID, string>
         {
             {ErrorID.INVALID_PLATFORM, "Invalid Platform please switch to IOS or Android on your Build Settings"},
             {ErrorID.SettingsNoFacebookAppID, "Moon SDK Settings is missing Facebook App ID"},
             {ErrorID.SettingsNoFacebookClientID, "Moon SDK Settings is missing Facebook Client ID"},
             {ErrorID.GANoIOSKey, "Moon SDK Settings is missing iOS GameAnalytics keys"},
             {ErrorID.GANoAndroidAndKey, "Moon SDK Settings is missing Android GameAnalytics keys! add 'ignore' in both fields to disable Android analytics"},
             {ErrorID.NoAdjustToken, "Moon SDK Settings is missing Adjust App Token"},
             {ErrorID.NoIOSIronSourceKey, "Moon SDK Settings is missing iOS Iron Source App Key"},
             {ErrorID.NoIOSAdMobKey, "Moon SDK Settings is missing iOS Ad Mob Adapter Key"},
             {ErrorID.NoAndroidIronSourceKey, "Moon SDK Settings is missing Android Iron Source App Key"},
             {ErrorID.NoAndroidAdMobKey, "Moon SDK Settings is missing Android Ad Mob Adapter Key"}
         };
    }
}
