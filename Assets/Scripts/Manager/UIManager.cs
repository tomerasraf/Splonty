using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Game Data")]
    [SerializeField] GameData _gameData;

    [Header("Sprites")]
    [SerializeField] RectTransform pointer;

    [Header("Canvas")]
    [SerializeField] GameObject menuCanvas;
    [SerializeField] GameObject gameplayCanvas;
    [SerializeField] GameObject levelSummaryCanvas;

    [Header("Images")]
    [SerializeField] Image healthBarFiller;

    private void Start()
    {
        StartCoroutine(RotatePointer());
    }

    private void OnEnable()
    {
        EventManager.current.onStartGameTouch += UIDisplayOff;
        EventManager.current.onEndLevel += DisplayLevelSummaryUI;
        EventManager.current.onShapeHit += HealthBarUpdater;
    }

    private void OnDisable()
    {
        EventManager.current.onStartGameTouch -= UIDisplayOff;
        EventManager.current.onEndLevel -= DisplayLevelSummaryUI;
        EventManager.current.onShapeHit -= HealthBarUpdater;
        
    }

    private void HealthBarUpdater()
    {
        StartCoroutine(HealthBar());
    }

    private void UIDisplayOff()
    {
        menuCanvas.SetActive(false);
    }

    private void DisplayLevelSummaryUI()
    {
        levelSummaryCanvas.SetActive(true);
    }

    IEnumerator RotatePointer()
    {
        pointer.DOShapeCircle(pointer.anchoredPosition, 360, 3, true).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        yield return null;
    }
    IEnumerator HealthBar()
    {
        while (healthBarFiller.fillAmount != _gameData.healthPoints / 100)
        {
            healthBarFiller.fillAmount = Mathf.Lerp(healthBarFiller.fillAmount, _gameData.healthPoints / 100, 3 * Time.deltaTime);
            yield return null;
        }
        yield return null;
    }





}
