using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;

public class MovingPlatformButton : AnimatedElement
{
    [SerializeField] private UnityEvent onCollision;

    private Vector3 inverseStartPos;
    private Vector3 inverseEndPos;

    private void OnValidate()
    {
        inverseStartPos = transform.TransformPoint(startPosition);
        inverseEndPos = transform.TransformPoint(endPosition);
    }

    public override void PlayAnimation()
    {
        var sequence = DOTween.Sequence().SetUpdate(UpdateType.Fixed);

        sequence.AppendInterval(startPositionDelay);
        sequence.Append(transform.DOLocalMove(endPosition, toEndPositionMoveTime));
        sequence.SetLoops(loops);
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

        StartCoroutine(PlaySoundWithDelay(soundDelay));
        PlayAnimation();
        onCollision?.Invoke();
    }
}
