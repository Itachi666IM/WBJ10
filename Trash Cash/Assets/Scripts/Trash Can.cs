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
        cash = PlayerPrefs.GetFloat(CASH,0f);
        cashText.text = "Cash = " + cash.ToString();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        damageAmount = PlayerPrefs.GetFloat(GameManager.TRASH_DAMAGE, 10f);
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
    }

    public void UpdateCash()
    {
        PlayerPrefs.SetFloat(CASH, cash);
        cashText.text = "Cash = " + cash.ToString();
    }
}
