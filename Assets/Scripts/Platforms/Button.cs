using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Button : AnimatedElement
{
    [SerializeField] private UnityEvent onCollision;

    private Vector3 inverseStartPos;
    private Vector3 inverseEndPos;

    private void OnValidate()
    {
        inverseStartPos = transform.TransformPoint(StartPosition);
        inverseEndPos = transform.TransformPoint(EndPosition);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(inverseStartPos, 0.25f);
        Gizmos.DrawSphere(inverseEndPos, 0.25f);
        Gizmos.DrawLine(inverseStartPos, inverseEndPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsPlayer(other)) return;

        PLayAnimation();
        StartCoroutine(Delay());
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(ToEndPositionMoveTime);

        onCollision?.Invoke();
    }
}