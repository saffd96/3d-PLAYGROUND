using UnityEngine;

public class MovingPlatform : AnimatedElement
{
    private void Start()
    {
        PLayAnimation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other))
        {
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other))
        {
            other.transform.SetParent(null);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(StartPosition, 0.25f);
        Gizmos.DrawSphere(EndPosition, 0.25f);
        Gizmos.DrawLine(StartPosition, EndPosition);
    }
}
