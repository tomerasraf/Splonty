using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    [Header("Smooth UI Vars")]
    [SerializeField] float healthBarSmoothChange;
    [SerializeField] float scoreSmoothChange;

    [Header("Game Data")]
    [SerializeField] GameData _gameData;

    [Header("Sprites")]
    [SerializeField] RectTransform pointer;

    [Header("Canvas")]
    [SerializeField] GameObject menuCanvas;
    [SerializeField] GameObject gameplayCanvas;
    [SerializeField] GameObject levelSummaryCanvas;
    [SerializeField] GameObject reviveCanvas;

    [Header("Text")]
    [SerializeField] TextMeshProUGUI scoreText;
    [SerializeField] TextMeshProUGUI comboText;
    [SerializeField] TextMeshProUGUI summaryScoreText;
    [SerializeField] TextMeshProUGUI summaryComboText;
    [SerializeField] TextMeshProUGUI levelProgressPrecentege;

    [Header("Images")]
    [SerializeField] Image healthBarFiller;
    [SerializeField] Image progressBarFiller;

    [Header("Feedback Images")]
    [SerializeField] GameObject[] images;

    private void Start()
    {
        menuCanvas.SetActive(true);
        StartCoroutine(RotatePointer());
        ProgressBarUpdater();
    }

    #region Events
    private void OnEnable()
    {
        EventManager.current.onStartGameTouch += UIDisplayOff;
        EventManager.current.onStartGameTouch += DisplayGameplayCanvas;
        EventManager.current.onEndLevel += DisplayLevelSummaryCanvas;
        EventManager.current.onEndLevel += LevelSummaryUIUpdater;
        EventManager.current.onGameOver += DisplayReviveCanvas;
        EventManager.current.onUIHealthChange += HealthBarUpdater;
        EventManager.current.onScoreUpdate += ScoreUpdater;
        EventManager.current.onFeedback += PlayerFeedback;
        EventManager.current.onComboUIUpdater += ComboUIupdater;
    }

    private void OnDisable()
    {
        EventManager.current.onStartGameTouch -= UIDisplayOff;
        EventManager.current.onEndLevel -= DisplayLevelSummaryCanvas;
        EventManager.current.onEndLevel -= LevelSummaryUIUpdater;
        EventManager.current.onShapeHit -= HealthBarUpdater;
        EventManager.current.onGameOver -= DisplayReviveCanvas;
        EventManager.current.onUIHealthChange -= HealthBarUpdater;
        EventManager.current.onScoreUpdate -= ScoreUpdater;
        EventManager.current.onFeedback -= PlayerFeedback;
        EventManager.current.onComboUIUpdater -= ComboUIupdater;
    }
    #endregion

    #region Health Bar
    private void HealthBarUpdater()
    {
        StartCoroutine(HealthBar());
    }

    IEnumerator HealthBar()
    {
        while (healthBarFiller.fillAmount != _gameData.healthPoints / 100)
        {
            healthBarFiller.fillAmount = Mathf.Lerp(healthBarFiller.fillAmount, _gameData.healthPoints / 100, healthBarSmoothChange * Time.deltaTime);
            yield return null;
        }
        yield return null;
    }


    #endregion

    #region Progress Bar
    private void ProgressBarUpdater()
    {
        StartCoroutine(ProgressBar());
    }
    IEnumerator ProgressBar()
    {
        while (true)
        {
            progressBarFiller.fillAmount = _gameData.levelProgress;

            int progress = (int)_gameData.levelProgress;

            levelProgressPrecentege.text = progress.ToString();
            yield return null;
        }
    }

    #endregion

    #region Combo UI
    private void ComboUIupdater()
    {
        comboText.text = $"Combo X {_gameData.comboHits.ToString()}";

        comboText.DORewind();

        comboText.DOFade(0, 0).OnComplete(() =>
        {
            comboText.gameObject.SetActive(true);
            comboText.DOFade(1, 0.5f).OnComplete(() =>
            {
                comboText.DOFade(0, 1).OnComplete(() =>
                {
                    comboText.gameObject.SetActive(false);
                    comboText.DOFade(1, 0);
                });
            });
        });
    }

    #endregion

    #region DisplayUI
    private void UIDisplayOff()
    {
        menuCanvas.SetActive(false);
    }

    private void DisplayLevelSummaryCanvas()
    {
        levelSummaryCanvas.SetActive(true);
    }

    private void DisplayGameplayCanvas()
    {
        gameplayCanvas.SetActive(true);
    }

    private void DisplayReviveCanvas()
    {
        reviveCanvas.SetActive(true);
        gameplayCanvas.SetActive(false);
    }
    #endregion

    #region Level Summary
    private void LevelSummaryUIUpdater()
    {
        summaryScoreText.text = _gameData.score.ToString();
        summaryComboText.text = _gameData.highestCombo.ToString();
    }

    #endregion

    #region Score UI
    private void ScoreUpdater()
    {
        scoreText.text = _gameData.score.ToString();
    }
    private void PlayerFeedback(int feedbackIndex)
    {
        images[feedbackIndex].transform.DORewind();
        images[feedbackIndex].transform.DOScale(0, 0).OnComplete(() =>
        {
            images[feedbackIndex].SetActive(true);

            images[feedbackIndex].transform.DOScale(0.5f, 0.5f).OnComplete(() =>
            {
                images[feedbackIndex].transform.DOScale(0, 0.5f).OnComplete(() =>
                {
                    images[feedbackIndex].SetActive(false);
                });
            });
        });
    }
    #endregion

    #region MenuUI
    IEnumerator RotatePointer()
    {
        pointer.DOShapeCircle(pointer.anchoredPosition, 360, 3, true).SetEase(Ease.InOutSine).SetLoops(-1, LoopType.Yoyo);
        yield return null;
    }
    #endregion
}
