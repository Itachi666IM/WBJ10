using UnityEngine;
using UnityEngine.UI;

public class PassiveUpgrade : MonoBehaviour
{
    private Button myButton;
    private Image myImage;
    [SerializeField] private string keyForPlayerPrefs;
    [SerializeField] private ShopManager shopManager;
    private bool canBuy = false;
    private void Awake()
    {
        myButton = GetComponent<Button>();
        myImage = GetComponent<Image>();
        int passiveUpgradeInfo = PlayerPrefs.GetInt(keyForPlayerPrefs);
        CheckForCash();
        if(passiveUpgradeInfo == 1 )
        {
            Destroy(gameObject);
        }
        else
        {
            if(canBuy)
            {
                myButton.onClick.AddListener(() =>
                {
                    SFX.Instance.PlayCash();
                    TrashCan.Instance.cash -= 20;
                    TrashCan.Instance.UpdateCash();
                    shopManager.UpdateCashText();
                    PlayerPrefs.SetInt(keyForPlayerPrefs, 1);
                    gameObject.SetActive(false);
                });
            }
        }

    }

    private void CheckForCash()
    {
        if (TrashCan.Instance.cash >= 20f)
        {
            canBuy = true;
        }
        else
        {
            canBuy = false;
            myImage.color = Color.gray;
            myButton.interactable = false;
        }
    }

    private void Update()
    {
        if(PlayerPrefs.GetInt(keyForPlayerPrefs)==1)
        {
            Destroy(gameObject);
        }
        CheckForCash();
    }
}
