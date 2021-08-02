using System.Collections;
using DG.Tweening;
using UnityEngine;

public class FallingPlatform : AnimatedElement
{
    private void OnTriggerEnter(Collider other)
    {
        if (IsPlayer(other))
        {
            StartAnimation();
        }
    }

    protected override void PLayAnimation()
    {
        transform.DOShakePosition(StartPositionDelay, Vector3.one / 10, 10, 45f, false, false);
        StartCoroutine(PlaySoundWithDelay(StartPositionDelay + SoundDelay));
        base.PLayAnimation();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawSphere(StartPosition, 0.25f);
        Gizmos.DrawSphere(EndPosition, 0.25f);
        Gizmos.DrawLine(StartPosition, EndPosition);
    }
}
