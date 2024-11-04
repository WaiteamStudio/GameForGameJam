using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SceneChangeButton : MonoBehaviour
{
    [SerializeField]
    Loader.Scene Scene;
    Button button;
    protected void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(() => Loader.Load(Scene));
    }
}   
