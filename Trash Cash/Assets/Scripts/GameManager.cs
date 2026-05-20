using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
public class GameManager : MonoBehaviour
{
    [SerializeField] private Slider energySlider;
    [SerializeField] private float energy = 100f;
    [SerializeField] private GameObject timeUpScreen;
    [SerializeField] private float energyDepreciation = 10;

    [SerializeField] private Button restartButton;
    [SerializeField] private Button shopButton;
    [SerializeField] private Button exitButton;

    [SerializeField] private GameObject pauseMenu;
    [HideInInspector] public bool isPaused = false;

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartGameButton;
    [SerializeField] private Button quitGameButton;

    [HideInInspector] public const string TRASH_DAMAGE = "Trash_Damage";
    [HideInInspector] public const string TRASH_SPAWNRATE = "Trash_Spawnrate";
    [HideInInspector] public const string CASH_FOR_TRASH = "Cash_For_Trash";
    [HideInInspector] public const string TRASH_HEALTH = "Trash_Health";
    [HideInInspector] public const string ENERGY_DEPLETION = "Energy_Depletion";
    [HideInInspector] public const string TRASH_FADE_DELAY = "Trash_Fade_Delay";

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;

        if(MenuManager.Instance.isNewGame)
        {
            ResetAllValues();
            TrashCan.Instance.UpdateCash();
            MenuManager.Instance.isNewGame = false;
        }

        restartButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });

        shopButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene("Shop");
        });

        exitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        resumeButton.onClick.AddListener(() =>
        {
            isPaused = false;
            pauseMenu.SetActive(false);
        });
        restartGameButton.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
        quitGameButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });
    }

    private void Start()
    {
        energyDepreciation = PlayerPrefs.GetFloat(ENERGY_DEPLETION);
    }

    private void Update()
    {
        if(!isPaused)
        {
            energy -= Time.deltaTime * energyDepreciation;
            UpdateEnergyValue();
        }
        if(energy<=0)
        {
            isPaused = true;
            timeUpScreen.SetActive(true);
        }
        if(Keyboard.current.escapeKey.wasPressedThisFrame && !isPaused)
        {
            isPaused= true;
            pauseMenu.SetActive(true);
        }
        else if(Keyboard.current.escapeKey.wasPressedThisFrame && isPaused)
        {
            isPaused = false;
            pauseMenu.SetActive(false);
        }
    }

    public void UpdateEnergyValue()
    {  
        energySlider.value = energy;
    }

    public void ResetAllValues()
    {
        TrashCan.Instance.cash = 0;
        PlayerPrefs.SetFloat(TRASH_DAMAGE, 10);
        PlayerPrefs.SetFloat(TRASH_SPAWNRATE, 1.5f);
        PlayerPrefs.SetFloat(CASH_FOR_TRASH, 0.5f);
        PlayerPrefs.SetFloat(TRASH_HEALTH, 100f);
        PlayerPrefs.SetFloat(ENERGY_DEPLETION, 10f);
        PlayerPrefs.SetFloat(TRASH_FADE_DELAY, 4f);
    }
}
