using Unity.VisualScripting;
using UnityEngine;

public class Rope : MonoBehaviour
{
    public GameObject ropeSegmentpf; // Префаб сегмента веревки
    public int segmentCount = 10; // Количество сегментов
    public float spaceBetween = 0.2f;
    public float weightRopeLength = 1f; // Количество сегментов
    public GameObject weightpf; // Груз, который будет висеть на веревке

    private void Start()
    {
        GameObject previousSegment = null;

        for (int i = 0; i < segmentCount; i++)
        {
            // Создаем новый сегмент веревки
            GameObject newSegment = Instantiate(ropeSegmentpf, transform);
            newSegment.transform.position = transform.position + Vector3.down * i * spaceBetween; // Задаем позицию сегмента

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
            if( i == segmentCount - 1 )
            {
                weightpf = Instantiate(weightpf, transform);
                HingeJoint2D weightJoint = weightpf.GetOrAddComponent<HingeJoint2D>();
                weightJoint.transform.position = transform.position + Vector3.down * (i+1) * (spaceBetween + weightRopeLength);
                weightJoint.connectedBody = previousSegment.GetComponent<Rigidbody2D>();
            }
        }

    }
}
