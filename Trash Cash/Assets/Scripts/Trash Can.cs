using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class TrashCan : MonoBehaviour
{
    private Vector3 mousePosition;
    private float zPos = 0f;

    private const string CASH = "Cash";
    public float damageAmount = 50;
    [SerializeField] private TMP_Text cashText;
    public static TrashCan Instance { get; private set; }
    public float cash;
    private void Awake()
    {
        Instance = this;
        if(MenuManager.Instance.isNewGame)
        {
            cash = 0;
            UpdateCash();
        }
    }
    private void Start()
    {
        cash = PlayerPrefs.GetFloat(CASH,0f);
        cashText.text = "Cash = " + cash.ToString();
        damageAmount = PlayerPrefs.GetFloat(GameManager.TRASH_DAMAGE, 10f);
        Cursor.lockState = CursorLockMode.Confined;
    }

    private void Update()
    {
        if(!GameManager.Instance.isPaused)
        {
            mousePosition = Mouse.current.position.ReadValue();
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(mousePosition);
            worldPosition.z = zPos;
            transform.position = worldPosition;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<Trash>()!=null)
        {
            Trash trash = collision.GetComponent<Trash>();
            trash.TakeDamage(damageAmount);
        }

        if(collision.GetComponent<Treasure>()!=null)
        {
            Treasure treasure = collision.GetComponent<Treasure>();
            treasure.AddCash();
        }
    }

    public void UpdateCash()
    {
        PlayerPrefs.SetFloat(CASH, cash);
        if (cashText != null)
        {
            cashText.text = "Cash = " + cash.ToString();
        }
    }
}
