using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Comics : MonoBehaviour
{
    [SerializeField] private Image[] comicImages; // Массив UI Image для отображения комиксов
    [SerializeField] private int nextSceneIndex;
    private int currentIndex = 0;
    private Animator anim;
    private int pageIndex = 0;

    private void Start()
    {
        UpdateImage();
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //NextImage();
            pageIndex++;
            //anim.SetInteger("counter", pageIndex);
            anim.SetTrigger("triggerpage"); 
            AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if(stateInfo.IsName("Page3")) LoadNextScene();
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
