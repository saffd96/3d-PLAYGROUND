using DG.Tweening;
using UnityEngine;

public class MovingPlatform : AnimatedElement
{
    private void Start()
    {
        PlayAnimation();
    }

    public override void PlayAnimation()
    {
        var sequence = DOTween.Sequence().SetUpdate(UpdateType.Fixed);

        sequence.AppendInterval(startPositionDelay);
        sequence.Append(transform.DOLocalMove(endPosition, toEndPositionMoveTime));
        sequence.AppendInterval(endPositionDelay);
        sequence.Append(transform.DOLocalMove(startPosition, toStartPositionMoveTime));
        sequence.SetLoops(loops);
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
        Gizmos.DrawSphere(startPosition, 0.25f);
        Gizmos.DrawSphere(endPosition, 0.25f);
        Gizmos.DrawLine(startPosition, endPosition);
    }
}
