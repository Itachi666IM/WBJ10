using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button newGameButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button backButton;

    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject titleScreen;

    public static MenuManager Instance { get; private set;  }

    public bool isNewGame = false;
    private void Awake()
    {
        Instance = this;
        playButton.onClick.AddListener(() =>
        {
            isNewGame = false;
            SFX.Instance.PlayClick();
            SceneManager.LoadScene("Game");
        });

        newGameButton.onClick.AddListener(() =>
        {
            isNewGame = true;
            SFX.Instance.PlayClick();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });

        optionsButton.onClick.AddListener(() =>
        {
            SFX.Instance.PlayClick();
            titleScreen.SetActive(false);
            optionsPanel.SetActive(true);
        });

        backButton.onClick.AddListener(() =>
        {
            SFX.Instance.PlayClick();
            optionsPanel.SetActive(false);
            titleScreen.SetActive(true);
        });

        quitButton.onClick.AddListener(() => 
        {
            SFX.Instance.PlayClick();
            Application.Quit();
        });
    }
}
