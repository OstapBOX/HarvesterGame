using Firebase.Analytics;
using UnityEngine;

public class LevelLoggingBehaviour : MonoBehaviour
{
    private void Start() {
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelStart, new Parameter(FirebaseAnalytics.ParameterLevel, PlayerPrefs.GetInt("GamesPlayed", 0)));

    }

    private void OnDestroy() {
        FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventLevelEnd);
    }
}
