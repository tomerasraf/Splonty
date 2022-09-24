using System;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Moonee.MoonSDK.Internal.GDPR
{
    public class GDPR : MonoBehaviour
    {
        public event Action OnCompleted;

        [SerializeField] private TextMeshProUGUI appName;
        [SerializeField] private Image appIcon;
        [SerializeField] private Button agreeButton;
        public static bool IsAlreadyAsked => PlayerPrefs.GetInt(SaveName, 0) > 0;

        private const string SaveName = "GDPR";

        private void Start()
        {
            MoonSDKSettings settings = MoonSDKSettings.Load();

            agreeButton.onClick.AddListener(OnAgreeButtonClicked);

            var text = String.Format(appName.text, Application.productName);
            appName.text = text;
            appIcon.sprite = settings.appIcon;
        }
        public void Ask()
        {
            MoonSDKSettings settings = MoonSDKSettings.Load();

            if (IsAlreadyAsked || !settings.GDPR || !CheckGDPRCountry.CheckCountryForGDPR())
            {
                Destroy(this);
                return;
            }
            gameObject.SetActive(true);
        }
        public void OnAgreeButtonClicked()
        {
            OnCompleted?.Invoke();
            PlayerPrefs.SetInt(SaveName, 1);
            Close();
        }
        private void Close()
        {
            gameObject.SetActive(false);
        }
    }
}