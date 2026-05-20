using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;
    private AudioSource myAudio;
    private const string MUSIC_VOLUME = "Music_Volume";
    void ManageSingleton()
    {
        int instance = FindObjectsByType<AudioManager>(FindObjectsSortMode.None).Length;

        if(instance > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Awake()
    {
        ManageSingleton();
        myAudio = GetComponent<AudioSource>();
        myAudio.volume = PlayerPrefs.GetFloat(MUSIC_VOLUME,0.3f);
        volumeSlider.value = myAudio.volume;
    }

    public void SetMusicVolume()
    {
        myAudio.volume = volumeSlider.value;
        PlayerPrefs.SetFloat(MUSIC_VOLUME, volumeSlider.value);
    }
}
