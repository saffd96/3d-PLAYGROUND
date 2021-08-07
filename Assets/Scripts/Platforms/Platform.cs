using DG.Tweening;
using UnityEngine;

public class Platform : AnimatedElement
{
    private Vector3 inverseStartPos;
    private Vector3 inverseEndPos;
    
    private Tween tweener;
    
    private void OnValidate()
    {
        inverseStartPos = transform.TransformPoint(startPosition);
        inverseEndPos = transform.TransformPoint(endPosition);
        
        startPosition = transform.position;
        endPosition = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other))
        {
            PlayAnimation();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (IsPlayer(other))
        {
            PlayBackWardAnimation();
        }
    }
    
    public override void PlayAnimation()
    {
        tweener?.Kill();
        
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMove(endPosition, toEndPositionMoveTime).SetEase(toEndPointEase));
        sequence.SetLoops(loops);
        tweener = sequence;
    }

    private void PlayBackWardAnimation()
    {
        tweener?.Kill();
        Sequence sequence = DOTween.Sequence();
        sequence.Append(transform.DOLocalMove(startPosition, toStartPositionMoveTime).SetEase(toStartPointEase));
        sequence.SetLoops(loops);
        
        tweener = sequence;
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.white;
        Gizmos.DrawSphere(inverseStartPos, 0.25f);
        Gizmos.DrawSphere(inverseEndPos, 0.25f);
        Gizmos.DrawLine(inverseStartPos, inverseEndPos);
    }
}
