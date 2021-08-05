using DG.Tweening;
using UnityEngine;

public class AnimatedButton : AnimatedElement
{
    [SerializeField] private BigBlock[] bigBlocks;

    private Vector3 inverseStartPos;
    private Vector3 inverseEndPos;
    internal bool isPlayerOnButton;

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

    private void OnTriggerEnter(Collider other)
    {
        if (!IsPlayer(other)) return;
        
        isPlayerOnButton = true;

        StartCoroutine(PlaySoundWithDelay(soundDelay));
        PlayAnimation();
        
        foreach (var bigBlock in bigBlocks)
        {
            bigBlock.PlayAnimation();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!IsPlayer(other)) return;

        isPlayerOnButton = false;
        
        IsBackwardAnimationRequested();

        PlayReverseAnimation();

        // foreach (var bigBlock in bigBlocks)
        // {
        //     if (bigBlock.isForwardAnimationCompleted)
        //     {
        //         bigBlock.PlayReverseAnimation();
        //     }
        // }
    }

    public override void PlayAnimation()
    {
        baseSequence?.Kill();
        baseSequence = transform.DOLocalMove(endPosition, toEndPositionMoveTime).SetUpdate(true);
        baseSequence.SetLoops(loops);

        if (!isPlayerOnButton)
        {
            baseSequence.OnComplete(PlayReverseAnimation);
            baseSequence.OnComplete(IsBackwardAnimationRequested);
        }
    }

    public override void PlayReverseAnimation()
    {
        baseSequence?.Kill();

        var sequence = DOTween.Sequence().SetUpdate(UpdateType.Fixed);

        sequence.AppendInterval(endPositionDelay);
        sequence.Append(transform.DOLocalMove(startPosition, toStartPositionMoveTime));
        sequence.SetLoops(loops);
        baseSequence = sequence;
    }

    private void IsBackwardAnimationRequested()
    {
        foreach (var bigBlock in bigBlocks)
        {
            if (bigBlock.isForwardAnimationCompleted)
            {
                bigBlock.isBackwardAnimationRequested = true;
            }
        }
    }
}
