using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class TutorialManager : MonoBehaviour
{
    [SerializeField] private GameObject[] gameObjectsToEnable;
    [SerializeField] private TMP_Text tutorialText;
    [SerializeField] private string[] tutorials;
    [SerializeField] private Button skipButton;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button playButton;
    private string currentTutorial;
    private GameObject currentGameObjectEnabled;
    private int index = 0;

    private void Awake()
    {
        tutorialText.text = "";
        currentTutorial = tutorials[index];
        currentGameObjectEnabled = gameObjectsToEnable[index];
        currentGameObjectEnabled.SetActive(true);
        tutorialText.text = currentTutorial;

        skipButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Game");
        });
        nextButton.onClick.AddListener(() =>
        {
            if(index+1<gameObjectsToEnable.Length)
            {
                index++;
                tutorialText.text = "";
                currentTutorial = tutorials[index];
                currentGameObjectEnabled = gameObjectsToEnable[index];
                currentGameObjectEnabled.SetActive(true);
                tutorialText.text = currentTutorial;
            }
            else
            {
                nextButton.gameObject.SetActive(false);
                skipButton.gameObject.SetActive(false);
                playButton.gameObject.SetActive(true);
            }
        });
        playButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Game");
        });
    }
}
