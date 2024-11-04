using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathZoneTrigger : MonoBehaviour
{
    //[SerializeField] private GameObject hero; //�����
    [SerializeField] private GameObject pauseMenu; //���� �����
    [SerializeField] private GameObject playBtn; //���� �����
    [SerializeField] private GameObject gameOverImg; //���� �����

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
