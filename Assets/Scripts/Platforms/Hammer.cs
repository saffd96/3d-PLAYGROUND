using DG.Tweening;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    [SerializeField] private Vector3 startAngle;
    [SerializeField] private Vector3 endAngle;
    [SerializeField] private AnimationCurve movementEase;

    [Header("Timing")]
    [SerializeField] private float startPositionDelay;
    [SerializeField] private float endPositionDelay;
    [SerializeField] private float toStartAngleTime;
    [SerializeField] private float toEndAngleTime;

    [Tooltip("Set -1 to make it infinite")]
    [SerializeField] protected int Loops = 1;

    private void Start()
    {
        PlayAnimation();
    }

    private void PlayAnimation()
    {
        Sequence sequence = DOTween.Sequence().SetUpdate(UpdateType.Fixed);
        sequence.AppendInterval(startPositionDelay);
        sequence.Append(transform.DORotate(endAngle, toEndAngleTime).SetEase(movementEase));
        sequence.AppendInterval(endPositionDelay);
        sequence.Append(transform.DORotate(startAngle, toStartAngleTime).SetEase(movementEase));
        sequence.SetLoops(Loops);
    }
}
