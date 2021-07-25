using DG.Tweening;
using UnityEngine;

public class MovingPlatformWithButton : AnimatedElement
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
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(inverseStartPos, 0.25f);
        Gizmos.DrawSphere(inverseEndPos, 0.25f);
        Gizmos.DrawLine(inverseStartPos, inverseEndPos);
    }

    public void MovePlatform()
    {
        PLayAnimation();
    }

    protected override void PLayAnimation()
    {
        Sequence sequence = DOTween.Sequence().SetUpdate(UpdateType.Fixed);
        sequence.AppendInterval(StartPositionDelay);
        sequence.Append(transform.DOLocalMove(EndPosition, ToEndPositionMoveTime));
        isEnd = true;
        sequence.AppendCallback(PlaySound);
        sequence.AppendInterval(EndPositionDelay);
        sequence.Append(transform.DOLocalMove(StartPosition, ToStartPositionMoveTime));
        isEnd = false;
        sequence.AppendCallback(PlaySound);
        sequence.SetLoops(Loops);
    }
}
