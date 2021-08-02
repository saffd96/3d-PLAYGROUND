using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MovingPlatformButton : AnimatedElement
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
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(inverseStartPos, 0.25f);
        Gizmos.DrawSphere(inverseEndPos, 0.25f);
        Gizmos.DrawLine(inverseStartPos, inverseEndPos);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!IsPlayer(other)) return;

        StartCoroutine(PlaySoundWithDelay(SoundDelay));
        PLayAnimation();
        onCollision?.Invoke();
    }
}
