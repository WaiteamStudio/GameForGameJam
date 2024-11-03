using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioMixerManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider SoundsSlider;
    public Slider MusicSlider;

    private const string SoundsPrefKey = "Sounds";
    private const string MusicPrefKey = "Music";
    private void Awake()
    {
        SoundsSlider.onValueChanged.AddListener((float value) => OnVolumeSliderChanged(value, SoundsPrefKey));
        MusicSlider.onValueChanged.AddListener((float value) => OnVolumeSliderChanged(value, MusicPrefKey));
    }
    private void Start()
    {
        Load();
    }

    private void Load()
    {
        float musicVolume = PlayerPrefs.GetFloat(MusicPrefKey, 0.75f);
        float soundsVolume = PlayerPrefs.GetFloat(SoundsPrefKey, 0.75f); // �������� �� ��������� � 0.75
        SoundsSlider.value = soundsVolume; // ������������� ������� � ����������� ���������
        MusicSlider.value = musicVolume; // ������������� ������� � ����������� ���������
    }

    public void OnVolumeSliderChanged(float value,string group)
    {
        SetVolume(value, group); // ������������� ���������
        PlayerPrefs.SetFloat(group, value); // ��������� �������� � PlayerPrefs
        PlayerPrefs.Save(); // ����������� ��������� ���������
    }

    private void SetVolume(float volume,string group)
    {
        audioMixer.SetFloat(group, Mathf.Log10(volume) * 20); // ����������� �������� �������� � ��������
    }
}
