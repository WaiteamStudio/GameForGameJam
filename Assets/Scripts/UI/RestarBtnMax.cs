using UnityEngine.SceneManagement;
using UnityEngine;

public class RestarBtnMax : MonoBehaviour
{
    public void RestartLvl()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;
    }
}
