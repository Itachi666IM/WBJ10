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

    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
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
}
