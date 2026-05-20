using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    [SerializeField] private Button replayButton;
    [SerializeField] private Button exitButton;

    private void Awake()
    {
        replayButton.onClick.AddListener(() =>
        {
            MenuManager.Instance.isNewGame = true;
            SceneManager.LoadScene("Menu");
        });
        exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }
}
