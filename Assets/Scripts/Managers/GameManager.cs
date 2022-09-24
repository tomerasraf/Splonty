using DG.Tweening;
using System.Collections;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] GameData _gameData;

    [Header("Objects")]
    [SerializeField] GameObject level1ParentObject;
    [SerializeField] GameObject winner3DText;

    [Header("Transforms")]
    [SerializeField] Transform lightSaber;
    [SerializeField] Transform endLevelCollider;

    [Header("Vars")]
    [SerializeField] float levelSpeed;
    [SerializeField] float timeScale;

    [Header("Effects")]
    [SerializeField] GameObject[] calebrationSFX;

    [Header("Sounds")]
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] AudioClip endAmbience;


    private void Awake()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
    }

    private void StartGame()
    {
        StartCoroutine(MoveLevel());
        StartCoroutine(CountGameTime());
        StartCoroutine(LevelProgress());
        StartCoroutine(SetTimeScale());
    }

    private void GameOver()
    {
        SoundManager.Instance.StopPlayingSound();
        SoundManager.Instance.PlaySound(gameOverSound);
        StopAllCoroutines();
    }

    private void EndLevel()
    {
        SoundManager.Instance.playMusic(endAmbience);
        StopCoroutine(MoveLevel());
        StartCoroutine(FinishLine());
    }

    IEnumerator FinishLine()
    {

        for (int i = 0; i < calebrationSFX.Length; i++)
        {
            calebrationSFX[i].SetActive(true);
        }

        winner3DText.transform.DOScale(0, 0).OnComplete(() =>
        {
            winner3DText.SetActive(true);
            winner3DText.transform.DOScale(1, 0.5f);
        });

        yield return new WaitForSeconds(4f);
        EventManager.current.DisplaySummery();
        yield return null;
    }

    #region EventCallers
    private void OnEnable()
    {
        EventManager.current.onStartGameTouch += StartGame;
        EventManager.current.onEndLevel += EndLevel;
        EventManager.current.onGameOver += GameOver;
    }
    private void OnDisable()
    {
        EventManager.current.onGameOver -= GameOver;
        EventManager.current.onEndLevel -= EndLevel;
        EventManager.current.onStartGameTouch -= StartGame;
    }
    #endregion

    #region IEnumerators
    IEnumerator CountGameTime()
    {
        while (true)
        {
            _gameData.gameplayTime += Time.deltaTime;
            yield return null;
        }
    }
    IEnumerator MoveLevel()
    {
        yield return new WaitForSeconds(0.2f);

        while (_gameData.currentLevelProgress > 4)
        {
            level1ParentObject.transform.position = new Vector3(
                level1ParentObject.transform.position.x,
                level1ParentObject.transform.position.y,
                level1ParentObject.transform.position.z - levelSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator LevelProgress()
    {
        _gameData.fullLevelDistance = (endLevelCollider.position - lightSaber.position).sqrMagnitude;

        while (true)
        {
            if (endLevelCollider != null)
            {
                _gameData.currentLevelProgress = (endLevelCollider.position - lightSaber.position).sqrMagnitude;
            }

            yield return null;
        }
    }

    IEnumerator SetTimeScale()
    {
        while (true)
        {
            Time.timeScale = timeScale;
            yield return null;
        }
    }

    #endregion
}
