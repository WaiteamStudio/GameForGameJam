using UnityEngine.SceneManagement;
using UnityEngine;

public class ZoneTrigger : MonoBehaviour
{
    [SerializeField] private int lvlId;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            SceneManager.LoadScene(lvlId);
        }
    }
}
