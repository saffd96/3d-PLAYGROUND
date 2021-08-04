using DG.Tweening;
using UnityEngine;

public class FallingPlatform : AnimatedElement
{
    private void OnValidate()
    {
        startPosition = transform.position;
        endPosition = new Vector3(transform.position.x, transform.position.y - 50, transform.position.z);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other))
        {
            StartCoroutine(PlaySoundWithDelay(startPositionDelay + soundDelay));
            PlayAnimation();
        }
    }

    public override void PlayAnimation()
    {
        transform.DOShakePosition(startPositionDelay, Vector3.one / 10, 10, 45f, false, false);
        var sequence = DOTween.Sequence().SetUpdate(UpdateType.Fixed);

        sequence.AppendInterval(startPositionDelay);
        sequence.Append(transform.DOLocalMove(endPosition, toEndPositionMoveTime));
        sequence.AppendInterval(endPositionDelay);
        sequence.Append(transform.DOLocalMove(startPosition, toStartPositionMoveTime));
        sequence.SetLoops(loops);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(startPosition, 0.25f);
        Gizmos.DrawSphere(endPosition, 0.25f);
        Gizmos.DrawLine(startPosition, endPosition);
    }
}
