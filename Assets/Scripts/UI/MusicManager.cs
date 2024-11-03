using UnityEngine.UI;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] private Slider sliderVolumeMusic;
    [SerializeField] private float volume;
    [SerializeField] private AudioSource audio;

    private void Start()
    {
        Load();
        ValueMusic();
    }

    public void SliderMusic()
    {
        volume = sliderVolumeMusic.value;
        Save();
        ValueMusic();
    }

    private void ValueMusic()
    {
        audio.volume = volume;
        sliderVolumeMusic.value = volume;
    }

    private void Save()
    {
        PlayerPrefs.SetFloat("volume", volume);
    }

    private void Load()
    {
        volume = PlayerPrefs.GetFloat("volume", volume);
    }
}
