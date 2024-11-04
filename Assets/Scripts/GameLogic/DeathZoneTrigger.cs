using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneTrigger : MonoBehaviour
{
    //[SerializeField] private GameObject hero; //герой
    [SerializeField] private GameObject pauseMenu; //меню паузы
    [SerializeField] private GameObject playBtn; //меню паузы
    [SerializeField] private GameObject gameOverImg; //меню паузы

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Time.timeScale = 0;
            pauseMenu.SetActive(true);
            playBtn.SetActive(false);
            gameOverImg.SetActive(true);
        }
    }
}
