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
        if(MenuManager.Instance.isNewGame)
        {
            MenuManager.Instance.isNewGame = false;
        }
        UpdateCashText();
        playButton.onClick.AddListener(() =>
        {
            SFX.Instance.PlayClick();
            SceneManager.LoadScene("Game");
        });

        exitButton.onClick.AddListener(() =>
        {
            SFX.Instance.PlayClick();
            Application.Quit();
        });
    }

    public void UpdateCashText()
    {
        cashText.text = TrashCan.Instance.cash.ToString();
    }
}
