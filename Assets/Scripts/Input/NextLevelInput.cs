using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevelInput : MonoBehaviour
{
    [SerializeField] GameData _gameData;
    [SerializeField] Button nextLevelButton;

    private void Start()
    {
        nextLevelButton.onClick.AddListener(() =>
        {
            _gameData.Level += 1;
            _gameData.score = 0;
            LevelManager.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });
    }

}
