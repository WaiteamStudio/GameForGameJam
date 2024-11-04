using UnityEngine.SceneManagement;
using UnityEngine;

public class StartBtn : Sounds
{
    private Scene crntScene;
    private void Start()
    {
        crntScene = SceneManager.GetActiveScene();
    }

    public void Lvl1()
    {
        PlaySound(sounds[0]);
        SceneManager.LoadScene(1); Time.timeScale = 1f;
    }
}
