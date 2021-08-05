using DG.Tweening;
using UnityEngine;

public class BigBlock : AnimatedElement
{
    private Vector3 inverseStartPos;
    private Vector3 inverseEndPos;

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
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(startPositionDelay);
        sequence.Append(transform.DOLocalMove(endPosition, toEndPositionMoveTime).SetEase(toEndPointEase));
        sequence.AppendCallback(IsForwardAnimationComplete);
        // sequence.AppendCallback(IsPlayerOnButton);
    }

    public override void PlayReverseAnimation()
    {
        Sequence sequence = DOTween.Sequence();
        sequence.AppendInterval(endPositionDelay);
        sequence.Append(transform.DOLocalMove(startPosition, toStartPositionMoveTime).SetEase(toStartPointEase));
        StartCoroutine(PlaySoundWithDelay(endPositionDelay + soundDelay));
        sequence.SetLoops(loops);
    }

    // private void IsPlayerOnButton()
    // {
    //     if (!button.isPlayerOnButton)
    //     {
    //         PlayReverseAnimation();
    //     }
    //     else
    //     {
    //         button.isPlayerOnButton = false;
    //     }
    // }

    private void IsForwardAnimationComplete()
    {
        isForwardAnimationCompleted = true;

        if (isBackwardAnimationRequested)
        {
            PlayReverseAnimation();
        }
    }
}
