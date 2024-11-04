using UnityEngine;
using UnityEngine.EventSystems;
public class ButtonSound : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler
{

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlaySound(SoundManager.Sound.ButtonOver);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlaySound(SoundManager.Sound.ButtonClick);
    }

    private void PlaySound(SoundManager.Sound clip)
    {
        SoundManager.PlaySound(clip);
    }
}
