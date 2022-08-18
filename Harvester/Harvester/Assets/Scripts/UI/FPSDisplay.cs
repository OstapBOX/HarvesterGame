using UnityEngine;
using TMPro;

public class FPSDisplay : MonoBehaviour
{
    public TextMeshProUGUI fpsText;

    private float pollingTime = 0.5f;
    private float time;
    private int frameCount;

    private void Awake() {
        Application.targetFrameRate = 200;
        //QualitySettings.vSyncCount = 0;
    }

    private void Update() {
        time += Time.deltaTime;
        frameCount++;
        if(time >= pollingTime) {
            int frameRate = Mathf.RoundToInt(frameCount / time);
            fpsText.text = frameRate.ToString() + " FPS";
            time -= pollingTime;
            frameCount = 0;
        }
    }
}
