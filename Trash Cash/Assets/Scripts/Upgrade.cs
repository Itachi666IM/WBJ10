using UnityEngine;
using UnityEngine.UI;

public class Upgrade : MonoBehaviour
{
    private Button myButton;
    private SpriteRenderer myRenderer;
    private bool canBuy = false;
    [SerializeField] private float thresholdValue;
    [SerializeField] private float amountToBeChanged;
    [SerializeField] private string keyForPlayerPrefs;
    [SerializeField] private ShopManager shopManager;

    private void Awake()
    {
        myButton = GetComponent<Button>();
        if (TrashCan.Instance.cash >= 3)
        {
            canBuy = true;
            myButton.interactable = true;
        }
        else
        {
            canBuy= false;
            myRenderer.color = Color.red;
            myButton.interactable = false;
        }

        myButton.onClick.AddListener(() =>
        {
            if(canBuy)
            {
                TrashCan.Instance.cash -= 3;
                DefineVariableToBeChanged();
                shopManager.UpdateCashText();
            }
        });
    }

    private void DefineVariableToBeChanged()
    {
        float amountRetrieved = PlayerPrefs.GetFloat(keyForPlayerPrefs);
        amountRetrieved += amountToBeChanged;
        if(amountToBeChanged<0)
        {
            if(amountRetrieved <= thresholdValue)
            {
                amountToBeChanged = thresholdValue;
            }
        }
        else
        {
            if(amountRetrieved >= thresholdValue)
            {
                amountRetrieved = thresholdValue;
            }
        }
        PlayerPrefs.SetFloat(keyForPlayerPrefs, amountRetrieved);
    }
}
