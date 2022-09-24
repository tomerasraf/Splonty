using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Moonee.MoonSDK.Internal.Intro
{
    public class IntroAnimationController : MonoBehaviour
    {
        [SerializeField] private RectTransform[] cards;
        [SerializeField] private Image logo;
        [SerializeField] private Image studioLogo;

        [SerializeField] private float cardMoveDuration = 0.5f;
        [SerializeField] private float cardMoveDelay = 0.25f;
        [SerializeField] private float logoAppearenceDuration = 1f;

        [SerializeField] private GameObject eyeBlink;

        void Start()
        {
            StartCoroutine(StartIntroAnimation());
        }
        IEnumerator StartIntroAnimation()
        {
            Application.targetFrameRate = 120;
            int step = 0;
            float delay = cardMoveDelay;
            logo.gameObject.SetActive(false);
            logo.rectTransform.anchoredPosition = new Vector2(0, -20);
            studioLogo.gameObject.SetActive(false);
            foreach (var card in cards)
            {
                card.anchoredPosition = new Vector2(0, -2000);
            }
            foreach (var card in cards)
            {
                StartCoroutine(MoveCard(card, new Vector3(0, 0 - step, 0), cardMoveDuration));
                step += 40;
                delay -= 0.04f;
                yield return new WaitForSeconds(delay);
            }
            yield return new WaitForSeconds(0.25f);
            logo.gameObject.SetActive(true);
            logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, 0f);
            StartCoroutine(MoveCard(logo.rectTransform, Vector3.zero, logoAppearenceDuration));
            
            StartCoroutine(UnFadeLogoColor(logo));

            var settings = MoonSDKSettings.Load();

            if (settings.StudioLogo)
            {
                studioLogo.sprite = settings.studioLogoSprite;
                studioLogo.SetNativeSize();
                StartCoroutine(UnFadeLogoColor(studioLogo));
            }
        }
        IEnumerator MoveCard(RectTransform rectTransform, Vector2 targetPosition, float duration)
        {
            float elapsedTime = 0;
            Vector3 startPos = rectTransform.anchoredPosition;
            Vector3 endPos = targetPosition;

            while (elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;

                rectTransform.anchoredPosition = Vector3.Lerp(startPos, endPos, Mathf.SmoothStep(0.0f, 1.0f, Mathf.SmoothStep(0.0f, 1.0f, elapsedTime / duration)));

                yield return null;
            }
        }
        IEnumerator UnFadeLogoColor(Image logo)
        {
            float elapsedTime = 0;
            float fadeValue = 0;
            logo.gameObject.SetActive(true);

            while (elapsedTime < logoAppearenceDuration)
            {
                elapsedTime += Time.deltaTime;

                fadeValue = Mathf.Lerp(0, 1, elapsedTime / logoAppearenceDuration);
                logo.color = new Color(logo.color.r, logo.color.g, logo.color.b, fadeValue);
                yield return null;
            }
            eyeBlink.SetActive(true);
            yield return new WaitForSeconds(0.5f);
        }
    }
}