using DG.Tweening;
using TMPro;
using UnityEngine;

public class BigBlock : AnimatedElement
{
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

    protected override void PLayAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(StartPositionDelay);
        sequence.Append(transform.DOLocalMove(EndPosition, ToEndPositionMoveTime).SetEase(ToEndPointEase));
        sequence.AppendInterval(EndPositionDelay);
        sequence.Append(transform.DOLocalMove(StartPosition, ToStartPositionMoveTime).SetEase(ToStartPointEase));
        sequence.SetLoops(Loops);
    }
}
