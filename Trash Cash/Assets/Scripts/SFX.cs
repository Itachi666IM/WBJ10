using UnityEngine;
using UnityEngine.UI;

public class SFX : MonoBehaviour
{
    private AudioSource myAudio;
    [SerializeField] private Slider volumeSlider;
    [HideInInspector] public const string SFX_VOLUME = "SFX_Volume";
    public static SFX Instance {  get; private set; }

    [SerializeField] private AudioClip applause;
    [SerializeField] private AudioClip click;
    [SerializeField] private AudioClip cash;
    [SerializeField] private AudioClip treasure;
    [SerializeField] private AudioClip energyLow;
    [SerializeField] private AudioClip popup;
    [SerializeField] private AudioClip broom;
    [SerializeField] private AudioClip trashPickup;
    [SerializeField] private AudioClip typing;

    private void Awake()
    {
        Instance = this;
        myAudio = GetComponent<AudioSource>();
        myAudio.volume = PlayerPrefs.GetFloat(SFX_VOLUME,0.3f);
        volumeSlider.value = myAudio.volume;
        DontDestroyOnLoad(gameObject);
    }

    public void UpdateVolume()
    {
        myAudio.volume = volumeSlider.value;
        PlayerPrefs.SetFloat(SFX_VOLUME, volumeSlider.value);
    }

    public void PlayApplause()
    {
        myAudio.PlayOneShot(applause);
    }

    public void PlayClick()
    {
        myAudio.PlayOneShot(click);
    }

    public void PlayCash()
    {
        myAudio.PlayOneShot(cash);
    }

    public void PlayTreasure()
    {
        myAudio.PlayOneShot(treasure);
    }

    public void PlayEnergyLow()
    {
        myAudio.PlayOneShot(energyLow);
    }

    public void PlayPopup()
    {
        myAudio.PlayOneShot(popup);
    }

    public void PlayBroom()
    {
        myAudio.PlayOneShot(broom);
    }

    public void PlayTrashPickup()
    {
        myAudio.PlayOneShot(trashPickup);
    }

    public void PlayTyping()
    {
        myAudio.PlayOneShot(typing);
    }
}
