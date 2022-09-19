using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    [SerializeField] GameData gameData;

    public static LevelManager Instance;
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

    public void LoadScene(int sceneIndex) {
        gameData.comboHits = 0;
        SceneManager.LoadSceneAsync(sceneIndex);
    }
}
