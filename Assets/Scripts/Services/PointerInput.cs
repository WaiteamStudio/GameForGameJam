using UnityEngine;

public class PointerInput : MonoBehaviour
{
    static Vector2 mousePosition;
    private void Update()
    {
        mousePosition = Input.mousePosition;
    }
    public static Vector3 GetPointerInput()
    {
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
    public static Vector2 GetPointerInputVector2()
    {
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }
}
