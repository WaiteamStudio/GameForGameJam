using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Comics : MonoBehaviour
{
    [SerializeField] private Image[] comicImages; // Массив UI Image для отображения комиксов
    [SerializeField] private int nextSceneIndex;
    private int currentIndex = 0;

    private void Start()
    {
        UpdateImage();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            NextImage();
        }
    }

    private void NextImage()
    {
        currentIndex++;
        if (currentIndex >= comicImages.Length)
        {
            LoadNextScene();
        }
        else
        {
            UpdateImage();
        }
    }

    private void UpdateImage()
    {
        comicImages[currentIndex].gameObject.SetActive(true);
    }

    private void LoadNextScene()
    {
        SceneManager.LoadScene(nextSceneIndex);
    }
}
