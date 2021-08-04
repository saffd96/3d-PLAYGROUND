using DG.Tweening;
using UnityEngine;

public class MovingPlatformWithButton : AnimatedElement
{
    private Vector3 inverseStartPos;
    private Vector3 inverseEndPos;

    private void OnValidate()
    {
        inverseStartPos = transform.TransformPoint(startPosition);
        inverseEndPos = transform.TransformPoint(endPosition);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(inverseStartPos, 0.25f);
        Gizmos.DrawSphere(inverseEndPos, 0.25f);
        Gizmos.DrawLine(inverseStartPos, inverseEndPos);
    }

    public void MovePlatform()
    {
        PlayAnimation();
    }

    public override void PlayAnimation()
    {
        Sequence sequence = DOTween.Sequence().SetUpdate(UpdateType.Fixed);
        sequence.AppendInterval(startPositionDelay);
        sequence.Append(transform.DOLocalMove(endPosition, toEndPositionMoveTime));
        sequence.AppendCallback(PlaySound);
        sequence.SetLoops(loops);
    }
}
