using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NextLevelInput : MonoBehaviour
{
    [SerializeField] Button nextLevelButton;

    private void Start()
    {
        nextLevelButton.onClick.AddListener(() =>
        {
            LevelManager.Instance.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });
    }

}
