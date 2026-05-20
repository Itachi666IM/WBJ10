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

    [HideInInspector] public const string PASSIVE_UPGRADE_1 = "PA1";
    [HideInInspector] public const string PASSIVE_UPGRADE_2 = "PA2";
    [HideInInspector] public const string PASSIVE_UPGRADE_3 = "PA3";
    private float timeBetweenTrashRemoval = 5f;
    private float nextRemovalTime;
    public static GameManager Instance { get; private set; }
    private bool once = false;

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
            SFX.Instance.PlayClick();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });

        shopButton.onClick.AddListener(() =>
        {
            SFX.Instance.PlayClick();
            SceneManager.LoadScene("Shop");
        });

        exitButton.onClick.AddListener(() =>
        {
            SFX.Instance.PlayClick();
            Application.Quit();
        });

        resumeButton.onClick.AddListener(() =>
        {
            SFX.Instance.PlayClick();
            isPaused = false;
            pauseMenu.SetActive(false);
        });
        restartGameButton.onClick.AddListener(() =>
        {
            SFX.Instance.PlayClick();
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        });
        quitGameButton.onClick.AddListener(() =>
        {
            SFX.Instance.PlayClick();
            Application.Quit();
        });
    }

    private void Start()
    {
        energyDepreciation = PlayerPrefs.GetFloat(ENERGY_DEPLETION);
    }

    private void Update()
    {
        if(TrashCan.Instance.cash >= 1000)
        {
            SceneManager.LoadScene("Win");
        }
        if(!isPaused)
        {
            energy -= Time.deltaTime * energyDepreciation;
            UpdateEnergyValue();
        }
        if(energy == 20)
        {
            SFX.Instance.PlayEnergyLow();
        }
        if(energy<=0 &&!once)
        {
            once = true;
            isPaused = true;
            timeUpScreen.SetActive(true);
            if(PlayerPrefs.GetInt(PASSIVE_UPGRADE_1)==1)
            {
                PA1();
            }
        }

        if(PlayerPrefs.GetInt(PASSIVE_UPGRADE_2)==1)
        {
            if(Time.time > nextRemovalTime)
            {
                PA2();
                nextRemovalTime = Time.time + timeBetweenTrashRemoval;
            }
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
        PlayerPrefs.SetInt(PASSIVE_UPGRADE_1, 0);
        PlayerPrefs.SetInt(PASSIVE_UPGRADE_2, 0);
        PlayerPrefs.SetInt(PASSIVE_UPGRADE_3, 0);
    }

    private void PA1()
    {
        Trash[] leftoverTrash = FindObjectsByType<Trash>(FindObjectsSortMode.None);
        if(leftoverTrash!=null)
        {
            SFX.Instance.PlayBroom();
            foreach(Trash trash in leftoverTrash)
            {
                trash.DestroyObjectAndGiveCash();
            }
        }
        else
        {
            return;
        }
    }

    private void PA2()
    {
        Trash randomTrash = FindAnyObjectByType<Trash>();
        if(randomTrash != null)
        {
            randomTrash.DestroyObjectAndGiveCash();
        }
        else
        {
            return;
        }
    }
}
