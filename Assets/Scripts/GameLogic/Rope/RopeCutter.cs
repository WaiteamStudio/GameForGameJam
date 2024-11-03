using UnityEngine;

public class RopeCutter : MonoBehaviour
{
    [SerializeField]
    private int penetration;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        RopeSegment ropeSegment = collision.gameObject.GetComponent<RopeSegment>();
        if(ropeSegment!=null)
        {
            ropeSegment.CutSegment();
            penetration--;
        }
    }
}
