using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private Image _loaderCanvas;
    [SerializeField] private Image _progressBar;

    private void Awake()
    {
        if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public async void LoadScene(string sceneName) {
        var scene = SceneManager.LoadSceneAsync(sceneName);
        scene.allowSceneActivation = false;

        //_loaderCanvas.SetActive(true);

        do
        {
           // _progressBar.fillAmount = scene.progress;
        } while (scene.progress < 0.9);

        scene.allowSceneActivation = true;
       // _loaderCanvas.SetActive(false);
    }

    private void StartScreenAnimation()
    {
        _loaderCanvas.DOColor(Color.black, 0f).OnComplete(() =>
        {
            _loaderCanvas.DOColor(Color.white, 3.5f).OnComplete(() =>
            {
                _loaderCanvas.DOColor(Color.black, 3.5f).OnComplete(() =>
                {
                    SceneManager.LoadScene(1);
                });
            });
        });
    }

}
