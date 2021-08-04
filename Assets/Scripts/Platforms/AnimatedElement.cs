using System.Collections;
using DG.Tweening;
using UnityEngine;

public abstract class AnimatedElement : MonoBehaviour
{
    [Header("Moving Settings")]
    [SerializeField] protected Vector3 startPosition;
    [SerializeField] protected Vector3 endPosition;
    [SerializeField] protected Ease toStartPointEase = Ease.Linear;
    [SerializeField] protected Ease toEndPointEase = Ease.Linear;

    [Tooltip("Set -1 to make it infinite")]
    [SerializeField] protected int loops = 1;

    [Header("Timing")]
    [SerializeField] protected float startPositionDelay;
    [SerializeField] protected float endPositionDelay;

    [Tooltip("Set -1 to cancel Animation")]
    [SerializeField] protected float toStartPositionMoveTime;
    [SerializeField] protected float toEndPositionMoveTime;

    [Header("Sounds")]
    [SerializeField] private SfxType soundEffect;
    [SerializeField] protected float soundDelay;

    protected Tween baseSequence;

    public virtual void PlayAnimation()
    {
        baseSequence?.Kill();
        baseSequence.OnComplete(PlayReverseAnimation);
    }

    public virtual void PlayReverseAnimation()
    {
        baseSequence?.Kill();

        var sequence = DOTween.Sequence().SetUpdate(UpdateType.Fixed);

        baseSequence = sequence;
    }

    protected bool IsPlayer(Collider collider)
    {
        return collider.gameObject.CompareTag("Player");
    }

    protected void PlaySound()
    {
        AudioManager.Instance.PLaySfx(soundEffect);
    }

    protected IEnumerator PlaySoundWithDelay(float delay)
    {
        yield return new WaitForSeconds(delay);

        PlaySound();
    }
}
