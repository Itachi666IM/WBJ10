using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button optionsButton;
    [SerializeField] private Button quitButton;
    [SerializeField] private Button backButton;

    [SerializeField] private GameObject optionsPanel;
    [SerializeField] private GameObject titleScreen;
    private void Awake()
    {
        playButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        });

        optionsButton.onClick.AddListener(() =>
        {
            titleScreen.SetActive(false);
            optionsPanel.SetActive(true);
        });

        backButton.onClick.AddListener(() =>
        {
            optionsPanel.SetActive(false);
            titleScreen.SetActive(true);
        });

        quitButton.onClick.AddListener(() => 
        {
            Application.Quit();
        });
    }
}
