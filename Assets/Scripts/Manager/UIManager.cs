using DG.Tweening;
using System.Collections;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [Header("Sprites")]
    [SerializeField] RectTransform pointer;

    [Header("Canvas")]
    [SerializeField] GameObject menuCanvas;
    [SerializeField] GameObject gameplayCanvas;
    [SerializeField] GameObject levelSummaryCanvas;


    private void Start()
    {
        StartCoroutine(RotatePointer());
    }

    private void OnEnable()
    {
        EventManager.current.onStartGameTouch += UIDisplayOff;
        EventManager.current.onEndLevel += DisplayLevelSummaryUI;
    }

    private void OnDisable()
    {
        EventManager.current.onStartGameTouch -= UIDisplayOff;
        EventManager.current.onEndLevel -= DisplayLevelSummaryUI;
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



}
