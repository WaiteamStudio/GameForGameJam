using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject gameOverTxt;
    [SerializeField] private GameObject contBtn;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            pauseMenu.SetActive(true);
            contBtn.SetActive(false);
            gameOverTxt.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
