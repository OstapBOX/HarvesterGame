using UnityEngine;
using DG.Tweening;

public class CameraShake : MonoBehaviour {
    public static CameraShake instance { get; private set; }

    private void Start() {
        instance = this;
    }

    public void ShakeCamera(float _shakeTime, float _shakeStrength) {
        transform.DOShakePosition(_shakeTime, _shakeStrength, 30, 90, false, true);
    }
}
