using Unity.VisualScripting;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject ropeSegmentpf; // Префаб сегмента веревки
    public int segmentCount = 10; // Количество сегментов
    public GameObject weightpf; // Груз, который будет висеть на веревке

    private void Start()
    {
        GameObject previousSegment = null;

        for (int i = 0; i < segmentCount; i++)
        {
            // Создаем новый сегмент веревки
            GameObject newSegment = Instantiate(ropeSegmentpf, transform);
            newSegment.transform.position = transform.position + Vector3.down * i * 0.2f; // Задаем позицию сегмента

            // Подключаем HingeJoint2D к предыдущему сегменту
            if (previousSegment != null)
            {
                HingeJoint2D joint = newSegment.GetComponent<HingeJoint2D>();
                joint.connectedBody = previousSegment.GetComponent<Rigidbody2D>();
            }
            else
            {
                // Если это первый сегмент, соединяем его с начальной точкой (например, потолок)
                HingeJoint2D joint = newSegment.GetComponent<HingeJoint2D>();
                joint.connectedBody = GetComponent<Rigidbody2D>();
            }

            previousSegment = newSegment;
        }

        weightpf = Instantiate(weightpf, transform);
        // Соединяем последний сегмент с грузом
        HingeJoint2D weightJoint = weightpf.GetOrAddComponent<HingeJoint2D>();
        weightJoint.connectedBody = previousSegment.GetComponent<Rigidbody2D>();
    }
}
