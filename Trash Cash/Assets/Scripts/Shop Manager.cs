using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class ShopManager : MonoBehaviour
{
    [SerializeField] private Button playButton;
    [SerializeField] private Button exitButton;
    [SerializeField] private TMP_Text cashText;

    private void Awake()
    { 
        UpdateCashText();
        playButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Game");
        });

        exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    public void UpdateCashText()
    {
        cashText.text = TrashCan.Instance.cash.ToString();
    }
}
