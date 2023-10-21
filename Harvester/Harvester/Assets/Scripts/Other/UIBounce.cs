using UnityEngine;
using DG.Tweening;

public class UIBounce : MonoBehaviour
{
    [SerializeField] private float scaleValue;
    [SerializeField] private float cycleTime;

    private Tween bounceTween;

    void Start()
    {
        StartTweening();
    }

    public void StartTweening() {
        bounceTween = transform.DOScale(scaleValue, cycleTime).SetLoops(999999, LoopType.Yoyo).SetEase(Ease.InSine);
    }

    public void StopTweening() {
        bounceTween.Kill();
        transform.DOScale(transform.localScale, cycleTime);
    }
}
