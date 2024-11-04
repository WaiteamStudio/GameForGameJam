using UnityEngine.SceneManagement;
using UnityEngine;

public class AfterTitles : MonoBehaviour
{
    public int sceneIndex; // ������ �����, ������� ����� ���������

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            LoadScene();
        }
    }

    private void LoadScene()
    {
        SceneManager.LoadScene(sceneIndex);
    }
}
