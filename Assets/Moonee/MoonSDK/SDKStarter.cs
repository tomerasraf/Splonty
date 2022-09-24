using Moonee.MoonSDK.Internal;
using Moonee.MoonSDK.Internal.GDPR;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Moonee.MoonSDK
{
    public class SDKStarter : MonoBehaviour
    {
        [SerializeField] private GDPR gdpr;
        [SerializeField] private GameObject moonSDK;
        [SerializeField] private GameObject intro;

        private void Start()
        {
            gdpr.gameObject.SetActive(false);
            intro.gameObject.SetActive(false);

            StartCoroutine(Starter());
        }
        private void OnGDPRCompleted()
        {
            InitializeMoonSDK();
        }
        private void InitializeMoonSDK()
        {
            moonSDK.SetActive(true);
            DontDestroyOnLoad(moonSDK);
            SceneManager.LoadScene(1);
        }
        private IEnumerator Starter()
        {
            intro.SetActive(true);
            yield return new WaitForSeconds(4f);
            intro.SetActive(false);

            MoonSDKSettings settings = MoonSDKSettings.Load();

            if (CheckGDPRCountry.CheckCountryForGDPR() && !GDPR.IsAlreadyAsked && settings.GDPR )
            {
                DontDestroyOnLoad(this);
                gdpr.OnCompleted += OnGDPRCompleted;
                gdpr.gameObject.SetActive(true);
            }
            else InitializeMoonSDK();
        }
    }
}
