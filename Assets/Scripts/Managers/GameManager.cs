using DG.Tweening;
using System.Collections;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    [Header("Data")]
    [SerializeField] GameData _gameData;

    [Header("Objects")]
    GameObject level1ParentObject;
    GameObject winner3DText;

    [Header("Transforms")]
    [SerializeField] Transform lightSaber;
    Transform endLevelCollider;

    [Header("Vars")]
    [HideInInspector] public float levelSpeed;
    [SerializeField] float timeScale;

    [Header("Effects")]
    GameObject[] calebrationSFX;

    [Header("Sounds")]
    [SerializeField] AudioClip gameOverSound;
    [SerializeField] AudioClip endAmbience;


    private void Awake()
    {
        InitializeLevel();
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 120;
    }

    private void InitializeLevel()
    {
        level1ParentObject = GameObject.FindGameObjectWithTag("Level");
        endLevelCollider = GameObject.FindGameObjectWithTag("End Level").transform;
        calebrationSFX = GameObject.FindGameObjectsWithTag("Fireworks");
        winner3DText = GameObject.FindGameObjectWithTag("3D Text");

        level1ParentObject.transform.position = new Vector3(
        level1ParentObject.transform.position.x,
        -4.71f,
        level1ParentObject.transform.position.z);
    }

    private void StartGame()
    {
        /*float firstBeatToPlayer = level1ParentObject.transform.GetChild(0).position.z - lightSaber.transform.position.z;
        Debug.Log(firstBeatToPlayer);*/
        float firstBeat = GameObject.FindGameObjectWithTag("SyncLines").transform.position.z - GameObject.FindGameObjectWithTag("SaberLine").transform.position.z;
        Debug.Log(firstBeat);
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
        _gameData.currentLevelProgress = _gameData.fullLevelDistance;

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
