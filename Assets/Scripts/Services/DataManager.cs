using UnityEngine;

public class DataManager : MonoBehaviour
{
    public int score;
    public string playerName;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
}
