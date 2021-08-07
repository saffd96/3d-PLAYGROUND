using DG.Tweening;
using UnityEngine;

public class BigBlock : AnimatedElement
{
    private Vector3 inverseStartPos;
    private Vector3 inverseEndPos;
    private Tween tweener;
    
    internal bool isForwardAnimationCompleted;
    internal bool isBackwardAnimationRequested;

    private void OnValidate()
    {
        inverseStartPos = transform.TransformPoint(startPosition);
        inverseEndPos = transform.TransformPoint(endPosition);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(inverseStartPos, 0.25f);
        Gizmos.DrawSphere(inverseEndPos, 0.25f);
        Gizmos.DrawLine(inverseStartPos, inverseEndPos);
    }

    public override void PlayAnimation()
    {
        isForwardAnimationCompleted = false;
        isBackwardAnimationRequested = false;

        tweener?.Kill();
        
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(startPositionDelay);
        sequence.Append(transform.DOLocalMove(endPosition, toEndPositionMoveTime).SetEase(toEndPointEase));
        sequence.AppendCallback(IsForwardAnimationComplete);

        tweener = sequence;
    }

    public override void PlayReverseAnimation()
    {
        isBackwardAnimationRequested = true;

        if (isForwardAnimationCompleted)
        {
            PlayBackWardAnimationInternal();
        }
    }

    private void IsForwardAnimationComplete()
    {
        isForwardAnimationCompleted = true;

        if (isBackwardAnimationRequested)
        {
            PlayBackWardAnimationInternal();
        }
    }

    private void PlayBackWardAnimationInternal()
    {
        tweener?.Kill();
        
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(endPositionDelay);
        sequence.Append(transform.DOLocalMove(startPosition, toStartPositionMoveTime).SetEase(toStartPointEase));
        sequence.SetLoops(loops);
        tweener = sequence;
        
        StartCoroutine(PlaySoundWithDelay(endPositionDelay + soundDelay));

    }
}
