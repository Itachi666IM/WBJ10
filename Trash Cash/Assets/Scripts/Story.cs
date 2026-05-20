using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;
public class Story : MonoBehaviour
{
    [SerializeField] private TMP_Text displayText;
    [SerializeField] private Button nextButton;
    [SerializeField] private Button skipButton;
    [SerializeField] private Button playButton;
    [SerializeField] private string[] textStrings;
    [SerializeField] private float typingSpeed = 0.02f;
    private string currentString;
    private int index = 0;

    private void Awake()
    {
        displayText.text = "";
        currentString = textStrings[index];
        nextButton.onClick.AddListener(() =>
        {
            SFX.Instance.PlayClick();
            nextButton.gameObject.SetActive(false);
            NextSentence();
        });

        skipButton.onClick.AddListener(() =>
        {
            SFX.Instance.PlayClick();
            SceneManager.LoadScene("Tutorial");
        });

        playButton.onClick.AddListener(() =>
        {
            SFX.Instance.PlayClick();
            SceneManager.LoadScene("Tutorial");
        });
    }

    private void Start()
    {
        StartCoroutine(DisplayStringsWordByWord());
    }

    private void NextSentence()
    {
        SFX.Instance.PlayTyping();
        displayText.text = "";
        if (index < textStrings.Length - 1)
        {
            index++;
            currentString = textStrings[index];
            StartCoroutine(DisplayStringsWordByWord());
        }
        else
        {
            nextButton.gameObject.SetActive(false);
            skipButton.gameObject.SetActive(false);
            playButton.gameObject.SetActive(true);

        }
    }

    private IEnumerator DisplayStringsWordByWord()
    {
        foreach (char c in currentString.ToCharArray())
        {
            displayText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        nextButton.gameObject.SetActive(true);
        yield return new WaitForSeconds(typingSpeed);
    }
}
