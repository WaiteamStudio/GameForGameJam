using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseBtns : Sounds
{
    [SerializeField] private GameObject pauseMenu;
    private void Awake()
    {
        ServiceLocator.Current.Get<EventBus>().Subscribe<PlayerDiedEvent>(OnPLayerDied);
    }
    public void OnPLayerDied(PlayerDiedEvent e)
    {
        Pause();
    }
    public void Pause()
    {
        PlaySound(sounds[0]);
        Time.timeScale = 0; 
        pauseMenu.SetActive(true);
    }

    public void UnPauseButton()
    {
        PlaySound(sounds[0]);
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
    }

    public void ExitToMainMenuBtn()
    {
        PlaySound(sounds[0]);
        SceneManager.LoadScene(0);
    }
}
