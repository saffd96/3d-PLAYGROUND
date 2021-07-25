using DG.Tweening;
using UnityEngine;

public abstract class AnimatedElement : Platform
{
    [Header("Moving Settings")]
    [SerializeField] protected Vector3 StartPosition;
    [SerializeField] protected Vector3 EndPosition;
    [SerializeField] protected Ease ToStartPointEase = Ease.Linear;
    [SerializeField] protected Ease ToEndPointEase = Ease.Linear;

    [Tooltip("Set -1 to make it infinite")]
    [SerializeField] protected int Loops = 1;

    [Tooltip("Set active if need to wait until animation played")]
    [SerializeField] private bool isNeedToWaitEnd;

    [Header("Timing")]
    [SerializeField] protected float StartPositionDelay;
    [SerializeField] protected float EndPositionDelay;
    [Tooltip("Set -1 to cancel Animation")]
    [SerializeField] protected float ToStartPositionMoveTime;
    [SerializeField] protected float ToEndPositionMoveTime;

    [Header("Sounds")]
    [SerializeField] private AudioClip startSound;
    [SerializeField] private AudioClip endSound;

    protected bool isEnd;
    private bool isAnimationPlayed;

    protected virtual void PLayAnimation()
    {
        Sequence sequence = DOTween.Sequence().SetUpdate(UpdateType.Fixed);
        sequence.AppendInterval(StartPositionDelay);
        sequence.Append(transform.DOLocalMove(EndPosition, ToEndPositionMoveTime));
        isEnd = true;
        sequence.AppendCallback(PlaySound);

        if (ToStartPositionMoveTime == -1)
        {
            return;
        }

        sequence.AppendInterval(EndPositionDelay);
        sequence.Append(transform.DOLocalMove(StartPosition, ToStartPositionMoveTime));
        isEnd = false;
        sequence.AppendCallback(PlaySound);
        sequence.SetLoops(Loops);
    }

    protected bool IsPlayer(Collider collider)
    {
        return collider.gameObject.CompareTag("Player");
    }

    protected void PlaySound()
    {
        if (isEnd)
        {
            //play end sound
        }
        else
        {
            //play start sound
        }
    }

    private void ChangeToggle()
    {
        isAnimationPlayed = !isAnimationPlayed;
    }

    public void StartAnimation()
    {
        if (isNeedToWaitEnd)
        {
            if (isAnimationPlayed) return;

            ChangeToggle();
            PLayAnimation();
            ChangeToggle();
        }
        else
        {
            PLayAnimation();
        }
    }
}
