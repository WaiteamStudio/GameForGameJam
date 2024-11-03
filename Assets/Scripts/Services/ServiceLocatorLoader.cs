using UnityEngine;

public class ServiceLocatorLoader : MonoBehaviour 
{
    EventBus _bus;
    private void Awake()
    {
        _bus = new EventBus();
        ServiceLocator.Current.Register( _bus );
    }
}