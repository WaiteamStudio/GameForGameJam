using UnityEngine;

public class SettingsBtn : Sounds
{
    [SerializeField] private GameObject settingsMenu;

    public void OpenSettingsMenu()
    {
        PlaySound(sounds[0]);
        settingsMenu.SetActive(true);
    }

    public void CloseSettingsMenu()
    {
        PlaySound(sounds[0]);
        settingsMenu.SetActive(false);
    }
}
