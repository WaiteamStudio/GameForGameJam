using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    public Button[] levelButtons;

    private void Start()
    {
        // Получаем текущий максимальный уровень, который достиг игрок
        int levelReached = PlayerPrefs.GetInt("LevelReached", 1);

        // Активируем кнопки до уровня, который был разблокирован
        for (int i = 0; i < levelButtons.Length; i++)
        {
            levelButtons[i].interactable = i + 1 <= levelReached;
        }
    }
    //через инспектор передать? Почему бы и нет
    public void LoadLevel(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    public void CompleteLevel(int levelNumber)
    {
        int levelReached = PlayerPrefs.GetInt("LevelReached", 1);

        // Обновляем только если текущий уровень больше сохранённого
        if (levelNumber > levelReached)
        {
            PlayerPrefs.SetInt("LevelReached", levelNumber);
            PlayerPrefs.Save(); // Сохраняем изменения
        }
    }
}
