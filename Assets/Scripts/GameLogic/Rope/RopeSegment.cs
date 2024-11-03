using UnityEngine;

public class RopeSegment : MonoBehaviour
{
    private HingeJoint2D hingeJoint;
    public float collisionForceThreshold = 5f; // Порог силы для разрезания

    void Start()
    {
        hingeJoint = GetComponent<HingeJoint2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Проверяем, если сила столкновения превышает порог, разрезаем сегмент
        if (collision.relativeVelocity.magnitude > collisionForceThreshold)
        {
            CutSegment();
        }
    }

    public void CutSegment()
    {
        Destroy(hingeJoint); // Удаляем соединение с предыдущим сегментом
        Destroy(this);       // Удаляем этот скрипт, чтобы отключить дальнейшую обработку коллизий
    }
}
