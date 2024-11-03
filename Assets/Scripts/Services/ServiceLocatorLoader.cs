using UnityEngine;

public class ServiceLocatorLoader : MonoBehaviour 
{
    EventBus _bus;
    [SerializeField]
    PlayerController _controller;
    [SerializeField]
    GameAssets gameAssets;
    private void Awake()
    {
        _bus = new EventBus();
        ServiceLocator.Current.Register( _bus );
        ServiceLocator.Current.Register(gameAssets);
        ServiceLocator.Current.Register(_controller);
    }
}