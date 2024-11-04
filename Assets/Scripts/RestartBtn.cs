using UnityEngine.SceneManagement;
using UnityEngine;

public class RestartBtn : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
    }
}
