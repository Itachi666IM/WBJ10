using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    private Button myButton;
    private Image myImage;
    private bool canBuy = false;
    private bool hasReachedThreshold = false;
    [SerializeField] private float thresholdValue;
    [SerializeField] private float amountToBeChanged;
    [SerializeField] private string keyForPlayerPrefs;
    [SerializeField] private ShopManager shopManager;

    private void Awake()
    {
        myButton = GetComponent<Button>();
        myImage = GetComponent<Image>();
        CheckForCash();
        CheckForThresholdBeforeShowingUpgrade();
        myButton.onClick.AddListener(() =>
        {
            if(canBuy)
            {
                TrashCan.Instance.cash -= 3;
                TrashCan.Instance.UpdateCash();
                DefineVariableToBeChanged();
                shopManager.UpdateCashText();
            }
        });
    }

    private void Update()
    {
        CheckForCash();
        if(hasReachedThreshold)
        {
            gameObject.SetActive(false);
        }
    }

    private void CheckForCash()
    {
        if (TrashCan.Instance.cash >= 3)
        {
            canBuy = true;
            myButton.interactable = true;
        }
        else
        {
            canBuy = false;
            myImage.color = Color.gray;
            myButton.interactable = false;
        }
    }

    private void DefineVariableToBeChanged()
    {
        float amountRetrieved = PlayerPrefs.GetFloat(keyForPlayerPrefs);
        amountRetrieved += amountToBeChanged;
        if(amountToBeChanged<0)
        {
            if(amountRetrieved <= thresholdValue)
            {
                hasReachedThreshold = true;
                amountRetrieved = thresholdValue;
            }
        }
        else
        {
            if(amountRetrieved >= thresholdValue)
            {
                hasReachedThreshold = true;
                amountRetrieved = thresholdValue;
            }
        }
        PlayerPrefs.SetFloat(keyForPlayerPrefs, amountRetrieved);
    }

    private void CheckForThresholdBeforeShowingUpgrade()
    {
        float amountRetrieved = PlayerPrefs.GetFloat(keyForPlayerPrefs);
        if(amountRetrieved == thresholdValue)
        {
            gameObject.SetActive(false);  
        }
    }
}
